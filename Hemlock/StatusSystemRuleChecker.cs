using System;
using System.Collections.Generic;
using System.Linq;
using UtilityCollections;

namespace Hemlock {

	using TStatus = System.Int32;

	internal class RuleChecker<TObject> {
		private StatusSystem<TObject> rules;
		private MultiValueDictionary<TStatus, Relationship> relationships = new MultiValueDictionary<TStatus, Relationship>();
		private DefaultHashSet<List<TStatus>> visitedRps; // To avoid duplicating messages for rock-paper-scissors relationships.
		private DefaultHashSet<TStatus> started = new DefaultHashSet<TStatus>();
		private DefaultHashSet<TStatus> completed = new DefaultHashSet<TStatus>();
		internal RuleChecker(StatusSystem<TObject> rules) {
			visitedRps = new DefaultHashSet<List<TStatus>>(new IEnumValueEquality<TStatus>()); // Compare lists with value equality!
			this.rules = rules;
			CheckRules();
		}
		class IEnumValueEquality<T> : EqualityComparer<IEnumerable<T>> {
			public override bool Equals(IEnumerable<T> x, IEnumerable<T> y) => x.SequenceEqual(y);
			public override int GetHashCode(IEnumerable<T> obj) {
				unchecked {
					int result = -1;
					foreach(var t in obj) result ^= t.GetHashCode(); // XOR is even worse here than it is in the hashset version below.
					return result;
				}
			}
		}
		class HashSetValueEquality<T> : EqualityComparer<HashSet<T>> {
			//public static HashSetValueEquality<T> Instance => instance;
			//private static readonly HashSetValueEquality<T> instance = new HashSetValueEquality<T>();
			public override bool Equals(HashSet<T> x, HashSet<T> y) => x.SetEquals(y);
			public override int GetHashCode(HashSet<T> obj) {
				unchecked {
					int result = -1;
					foreach(var t in obj) result ^= t.GetHashCode(); // This doesn't need to be better than XOR.
					return result; // (I just wanted it to be better than "return 0".)
				}
			}
		}
		// There is room for improvement here:  errors/warnings could be proper objects, not just strings.
		internal List<string> GetErrors(bool includeWarnings) {
			List<string> result = new List<string>();
			CheckRpsErrors(result, includeWarnings);
			var negativeRelationships = new MultiValueDictionary<StatusPair, Relationship>();
			var positiveRelationships = new MultiValueDictionary<StatusPair, Relationship>();
			var mutualSuppressions = new DefaultHashSet<StatusPair>();
			foreach(Relationship r in relationships.GetAllValues()) {
				if(r.Path.Count == 1) continue; // Skip any 'self' relationships.
				if(!r.ChainBroken) { // Tally negative and positive (direct) relationships. These are compared later.
					if(r.IsNegative) negativeRelationships.Add(new StatusPair(r.SourceStatus, r.TargetStatus), r);
					else positiveRelationships.Add(new StatusPair(r.SourceStatus, r.TargetStatus), r);
				}
				if(r.SourceStatus.Equals(r.TargetStatus) && !r.ChainBroken && !r.IsNegative) {
					if(r.IsConditional) {
						if(includeWarnings) {
							string error = $"CRITICAL WARNING:  Status \"{GetBestName(r.SourceStatus)}\" might feed itself infinitely:";
							error += GetPathString(r.Path, true);
							result.Add(error);
						}
					}
					else {
						string error = $"CRITICAL ERROR:  Status \"{GetBestName(r.SourceStatus)}\" feeds itself infinitely:";
						error += GetPathString(r.Path, false);
						result.Add(error);
					}
				}
				if(r.SourceStatus.Equals(r.TargetStatus) && !r.ChainBroken) {
					switch(r.Relation) {
						case RelationType.Suppresses:
							{
								string error = $"CRITICAL ERROR:  Status \"{GetBestName(r.SourceStatus)}\" suppresses itself. This will always cause an infinite loop:";
								error += GetPathString(r.Path, false);
								result.Add(error);
							}
							break;
						case RelationType.Cancels:
							if(includeWarnings) {
								string error = $"WARNING:  Status \"{GetBestName(r.SourceStatus)}\" cancels itself:";
								error += GetPathString(r.Path, false);
								error += GetErrorLine("(Take a look at the 'Single Source' setting to see if that's what you actually want.)");
								result.Add(error);
							}
							break;
						case RelationType.Prevents:
							if(includeWarnings) {
								string error = $"WARNING:  Status \"{GetBestName(r.SourceStatus)}\" prevents itself:";
								error += GetPathString(r.Path, false);
								result.Add(error);
							}
							break;
					}
				}
				if(r.SourceStatus.Equals(r.TargetStatus) && r.Path.Count >= 4 && r.IsNegative && r.ChainBroken
					&& !r.Path.Where(x => x.Relation == RelationType.Cancels || x.Relation == RelationType.Prevents).Any())
				{
					List<TStatus> negativeList = r.Path.Skip(1)
						.Where(x => x.Relation != RelationType.Feeds && x.Relation != RelationType.Extends)
						.Select(x => x.Status).ToList(); // Remove the extra links...
					if(!visitedRps[negativeList]) { // If this has already been handled as RPS, skip it.
						RecordRpsVisited(negativeList);
						if(r.IsConditional) {
							if(includeWarnings) {
								string error = $"CRITICAL WARNING:  Conditional suppression cycle. This might cause an infinite loop:";
								error += GetPathString(r.Path, false);
								result.Add(error);
							}
						}
						else {
							string error = $"CRITICAL ERROR:  Suppression cycle. This will always cause an infinite loop:";
							error += GetPathString(r.Path, false);
							result.Add(error);
						}
					}
				}
				if(includeWarnings) {
					if(!r.SourceStatus.Equals(r.TargetStatus) && !r.ChainBroken && r.Relation == RelationType.Suppresses) { // Whenever a status suppresses another...
						var otherWay = relationships[r.TargetStatus].ToList()
							.Find(x => x.TargetStatus.Equals(r.SourceStatus) && !x.ChainBroken && x.Relation == RelationType.Suppresses);
						if(otherWay != null) { // ...find out whether they both suppress one another.
							var pair = new StatusPair(r.SourceStatus, r.TargetStatus);
							if(!mutualSuppressions[pair]) { // If it hasn't already been handled...
								mutualSuppressions[pair] = true;
								mutualSuppressions[new StatusPair(r.TargetStatus, r.SourceStatus)] = true;
								string error = $"WARNING:  Mutual suppression. (This warning exists to make certain that you don't expect these 2 statuses to \"cancel each other out\". Instead, whichever status is created first will win. See the docs for more info.):";
								error += GetPathString(r.Path, false);
								error += GetPathString(otherWay.Path, false);
								result.Add(error);
							}
						}
					}
				}
			}

			if(includeWarnings) {
				// Check for feed + extend.
				foreach(var pair in positiveRelationships) {
					var list = pair.Value.ToList();
					var feed = list.Find(x => x.Relation == RelationType.Feeds);
					var extend = list.Find(x => x.Relation == RelationType.Extends);
					if(feed != null && extend != null) {
						string error = $"WARNING:  Possible conflict:  Status \"{GetBestName(pair.Key.status1)}\" extends AND feeds status \"{GetBestName(pair.Key.status2)}\".";
						error += GetPathString(feed.Path, false, "feed");
						error += GetPathString(extend.Path, false, "extend");
						error += GetErrorLine("(Note for feed+extend: the 'feeds' value change is applied before the 'extends' value change.)");
						result.Add(error);
					}
				}
				// Check for positive + negative.
				foreach(var statusPair in negativeRelationships.GetAllKeys().Intersect(positiveRelationships.GetAllKeys())) {
					string error = $"WARNING:  Possible conflict:  Status \"{GetBestName(statusPair.status1)}\"'s relationship to status \"{GetBestName(statusPair.status2)}\" has both negative & positive elements:";
					var allPairRelationships = negativeRelationships[statusPair].Concat(positiveRelationships[statusPair]).ToList();
                    foreach(Relationship r in allPairRelationships) {
						error += GetPathString(r.Path, false, r.Relation.ToString().ToLower());
					}
					if(ContainsBoth(RelationType.Feeds, RelationType.Suppresses, allPairRelationships)) {
						error += GetErrorLine("(Note for feed+suppress: the 'feeds' value change is applied before the suppression.)");
					}
					if(ContainsBoth(RelationType.Extends, RelationType.Suppresses, allPairRelationships)) {
						error += GetErrorLine("(Note for extend+suppress: the suppression is applied before the 'extends' value is propagated. Therefore, this is very unlikely to be useful unless a condition is present.)");
					}
					if(ContainsBoth(RelationType.Extends, RelationType.Cancels, allPairRelationships)) {
						error += GetErrorLine("(Note for extend+cancel: the cancellation is applied before the 'extends' value is propagated. This is very unlikely to be useful.)");
					}
					result.Add(error);
				}
				// Check for inconsistent aggregators.
				foreach(var pair in rules.valueAggs) { // For every status that has an aggregator defined...
					foreach(var otherStatus in rules.statusesThatExtend[pair.Key]) {
						if(rules.valueAggs[pair.Key] != rules.valueAggs[otherStatus]) {
							string error = $"WARNING:  Possibly inconsistent aggregators between extended statuses. Status \"{GetBestName(otherStatus)}\" doesn't seem to use the same aggregator as its parent status \"{GetBestName(pair.Key)}\".";
                            result.Add(error);
						}
					}
				}
				// Check for overlapping values in the enums.
				var allEnumTypes = rules.extraEnumTypes;
				if(typeof(TStatus).IsEnum) allEnumTypes.Add(typeof(TStatus));
				if(allEnumTypes.Count > 1) {
					MultiValueDictionary<int, string> enumNames = new MultiValueDictionary<int, string>();
					MultiValueDictionary<int, Type> recordedIntsPerType = new MultiValueDictionary<int, Type>();
					foreach(var enumType in allEnumTypes) {
						foreach(string enumName in Enum.GetNames(enumType)) {
							object value = Enum.Parse(enumType, enumName);
							enumNames.Add((int)value, $"{enumType.Name}.{enumName}");
							recordedIntsPerType.AddUnique((int)value, enumType); // Note that this enum has a name for this value.
						}
					}
					foreach(var pair in recordedIntsPerType) {
						if(pair.Value.Count() > 1) { // If more than one enum has a name for this value...
							var names = enumNames[pair.Key];
							string error = $"WARNING:  Multiple enums with the same value ({pair.Key}):  ";
							error += GetErrorLine(string.Join(", ", names));
							result.Add(error);
						}
					}
				}
			}
			return result;
		}
		private bool ContainsBoth(RelationType one, RelationType two, List<Relationship> list) {
			return list.Where(x => x.Relation == one).Any() && list.Where(x => x.Relation == two).Any();
		}
		private void CheckRpsErrors(List<string> result, bool includeWarnings) {
			foreach(Relationship r in relationships.GetAllValues()) {
				// This part finds rock-paper-scissors relationships and decides whether they're potentially dangerous or not.
				if(r.SourceStatus.Equals(r.TargetStatus) && r.IsNegative && r.ChainBroken && r.Path.Count == 4) {
					RelationType relation = r.Path[1].Relation; // The first is 'self'. Skip that one.
					if(r.Path.Skip(2).All(x => x.Relation == relation)) { // If the rest are the same, we have a basic rock-paper-scissors relationship.
						var trio = r.Path.Take(3).Select(x => x.Status).ToList();
						if(visitedRps[trio]) continue; // If already visited, skip it.
						RecordRpsVisited(trio);
						bool notDangerous = false;
						DefaultHashSet<RelationType> negatives = new DefaultHashSet<RelationType> {
							RelationType.Cancels, RelationType.Prevents, RelationType.Suppresses };
						for(int i = 0;i<3;++i) { // For each pair (A -> B)...
							TStatus source = r.Path[i].Status;
							TStatus target = r.Path[i+1].Status;
							DefaultHashSet<RelationType> removed = new DefaultHashSet<RelationType>();
							foreach(RelationType presentRelation in negatives) {
								if(!relationships[source]
									.Where(x => x.TargetStatus.Equals(target) && !x.ChainBroken && x.Relation == presentRelation)
									.Any()) { // Note which relation types are present for all statuses in this cycle.
									removed[presentRelation] = true;
									if(presentRelation == RelationType.Suppresses) notDangerous = true; // No suppression means no danger.
								}
							}
							negatives.ExceptWith(removed); // Remove the no-longer-true relationship flags.
							bool cancelled = false;
							if(relationships[source]
								.Where(x => x.TargetStatus.Equals(target) && !x.ChainBroken
									&& x.Relation == RelationType.Cancels && !x.IsConditional)
								.Any()) { // If B is cancelled (unconditionally) by A, that's half of what we need to know.
								cancelled = true;
							}
							HashSet<TStatus> targetRelatives = GetExtendedFamily(target); // Target, any that extend it, and so on.
							var fedRelationships = relationships.GetAllValues();
							fedRelationships = fedRelationships.Where(x => targetRelatives.Contains(x.TargetStatus));
							fedRelationships = fedRelationships.Where(x => x.Relation == RelationType.Feeds && x.Path.Count == 2);
							bool fed = fedRelationships.Any(); // If B can be fed by another value, that's the other half of what we need to know.
							if(cancelled && !fed) { // If its status can actually be cancelled (without still being fed)
								notDangerous = true; // then this one can't be dangerous.
								break;
							}
						}
						string verbs;
						if(negatives.Count == 3) verbs = "foils";
						else {
							verbs = string.Join(" and ", negatives.Select(x => x.ToString().ToLower()));
						}
						if(notDangerous) {
							if(includeWarnings) {
								string error = $"OKAY:  Rock-paper-scissors relationship: Each of these statuses {verbs} the next:";
								error += GetPathString(r.Path, false);
								result.Add(error);
							}
						}
						else {
							if(r.IsConditional) {
								if(includeWarnings) {
									string error = $"CRITICAL WARNING:  Infinite suppression loop likely for this rock-paper-scissors relationship: Each of these statuses {verbs} the next:";
									error += GetPathString(r.Path, false);
									result.Add(error);
								}
							}
							else {
								string error = $"CRITICAL ERROR:  Infinite suppression loop guaranteed for this rock-paper-scissors relationship: Each of these statuses {verbs} the next:";
								error += GetPathString(r.Path, false);
								result.Add(error);
							}
						}
					}
				}
			}
		}
		private void RecordRpsVisited(List<TStatus> path) {
			List<TStatus> temp = new List<TStatus>(path);
			for(int i=0;i<path.Count;++i) {
				visitedRps[new List<TStatus>(temp)] = true;
				temp.Add(temp[0]);
				temp.RemoveAt(0);
			}
		}
		private HashSet<TStatus> GetExtendedFamily(TStatus status) {
			HashSet<TStatus> result = new HashSet<TStatus>();
			Queue<TStatus> q = new Queue<TStatus>();
			q.Enqueue(status);
			while(q.Count > 0) {
				TStatus nextStatus = q.Dequeue();
				if(result.Add(nextStatus)) {
					foreach(var extendingStatus in rules.statusesThatExtend[nextStatus]) {
						q.Enqueue(extendingStatus);
					}
				}
			}
			return result;
		}
		private string GetPathString(List<DirectRelation> path, bool useVerbs, string description = null) {
			string pathStart;
			if(description != null) pathStart = $"  \r\n     Path({description}): ";
			else pathStart = "  \r\n     Path: ";
			if(useVerbs) { //todo: what about representing conditionals here?
				return pathStart + string.Join("", path.Select(x => x.Relation.ToString().ToLower() + " " + GetBestName(x.Status)));
			}
			else {
				return pathStart + string.Join(" -> ", path.Select(x => GetBestName(x.Status)));
			}
		}
		private string GetErrorLine(string s) => "  \r\n     " + s;
		private string GetBestName(TStatus status) {
			if(typeof(TStatus).IsEnum) return status.ToString();
			foreach(Type enumType in rules.extraEnumTypes) {
				try {
					var name = Enum.Parse(enumType, status.ToString());
					return name.ToString();
				}
				catch(ArgumentException) {
					continue;
				}
			}
			return status.ToString();
		}
		private void CheckRules() {
			IEnumerable<KeyValuePair<TStatus, IEnumerable<TStatus>>> allPairs;
			allPairs = rules.statusesFedBy[SourceType.Feed];
			allPairs = allPairs.Concat(rules.statusesFedBy[SourceType.Suppress]);
			allPairs = allPairs.Concat(rules.statusesFedBy[SourceType.Prevent]);
			allPairs = allPairs.Concat(rules.statusesCancelledBy);
			allPairs = allPairs.Concat(rules.statusesExtendedBy);
			foreach(var pair in allPairs) { // For every rule...
				Explore(pair.Key);
				foreach(var target in pair.Value) {
					Explore(target);
				}
			}
		}
		private void Record(Relationship relationship) {
			if(relationship != null) relationships.Add(relationship.SourceStatus, relationship);
		}
		private void Explore(TStatus status) {
			if(completed[status]) return;
			started[status] = true;
			Relationship identity = new Relationship{
				Path = new List<DirectRelation> { new DirectRelation { Status = status, Relation = RelationType.Self } }
			};
			Record(identity); // Record the 'self' relationship for this status.
			foreach(var relationship in GetConnectionsFrom(status)) {
				TStatus targetStatus = relationship.TargetStatus;
				if(!completed[targetStatus] && !started[targetStatus]) {
					Explore(targetStatus);
				}
				if(completed[targetStatus]) {
					MergeExisting(relationship, targetStatus);
				}
				else {
					ExploreManually(status, identity);
					break;
				}
			}
			completed[status] = true;
		}
		private void ExploreManually(TStatus status, Relationship pathSoFar) {
			foreach(var relationship in GetConnectionsFrom(status)) {
				TStatus targetStatus = relationship.TargetStatus;
				Relationship fullPathRelationship = GetMerged(pathSoFar, relationship);
				if(pathSoFar.Path.Where(x => x.Status.Equals(targetStatus)).Any()) { // If targetStatus is already in the path...
					Record(fullPathRelationship); // Record this link, and go no farther.
				}
				else {
					if(completed[targetStatus]) {
						MergeExisting(fullPathRelationship, targetStatus);
					}
					else {
						Record(fullPathRelationship);
						ExploreManually(targetStatus, fullPathRelationship);
					}
				}
			}
		}
		private IEnumerable<Relationship> GetConnectionsFrom(TStatus status) {
			foreach(TStatus targetStatus in rules.statusesExtendedBy[status]) {
				yield return new Relationship {
					Path = new List<DirectRelation> {
						new DirectRelation { Status = status, Relation = RelationType.Self },
						new DirectRelation { Status = targetStatus, Relation = RelationType.Extends }
					}
				};
			}
			foreach(TStatus targetStatus in rules.statusesCancelledBy[status]) {
				var pair = new StatusPair(status, targetStatus);
				bool conditional = rules.cancellationConditions[pair] != null;
				yield return new Relationship {
					Path = new List<DirectRelation> {
						new DirectRelation { Status = status, Relation = RelationType.Self },
						new DirectRelation { Status = targetStatus, Relation = RelationType.Cancels, IsConditional = conditional }
					}
				};
			}
			foreach(SourceType sourceType in new SourceType[] { SourceType.Feed, SourceType.Suppress, SourceType.Prevent }) {
				RelationType relation;
				switch(sourceType) {
					case SourceType.Feed:
						relation = RelationType.Feeds;
						break;
					case SourceType.Suppress:
						relation = RelationType.Suppresses;
						break;
					case SourceType.Prevent:
						relation = RelationType.Prevents;
						break;
					default: throw new NotImplementedException();
				}
				foreach(TStatus targetStatus in rules.statusesFedBy[sourceType][status]) {
					var pair = new StatusPair(status, targetStatus);
					bool conditional = rules.converters[sourceType].ContainsKey(pair);
					yield return new Relationship {
						Path = new List<DirectRelation> {
							new DirectRelation { Status = status, Relation = RelationType.Self },
							new DirectRelation { Status = targetStatus, Relation = relation, IsConditional = conditional }
						}
					};
				}
			}
		}
		private void MergeExisting(Relationship relationship, TStatus targetStatus) {
			TStatus status = relationship.SourceStatus;
			foreach(Relationship targetRelationship in relationships[targetStatus]) {
				Record(GetMerged(relationship, targetRelationship));
			}
		}
		private Relationship GetMerged(Relationship first, Relationship second) {
			if(first == null) return second;
			if(second == null) return first;
			if(!first.TargetStatus.Equals(second.SourceStatus)) throw new InvalidOperationException("The 2nd status must start where the 1st one ends.");
			var shortenedSecondPath = second.Path.Skip(1).ToList(); // Remove the (duplicated) 'self' status that starts the 2nd.
			var shortenedSecondPathStatuses = shortenedSecondPath.Select(x=>x.Status).ToList();
			foreach(TStatus firstPathStatus in first.Path.Select(x=>x.Status)) { // If any status appears twice in the combined path, it must END on the 2nd one.
				int idx = shortenedSecondPathStatuses.IndexOf(firstPathStatus); // If not, this one is extraneous - return null.
				if(idx != -1 && idx != shortenedSecondPathStatuses.Count - 1) return null;
			}
			return new Relationship { Path = new List<DirectRelation>(first.Path.Concat(shortenedSecondPath)) };
		}
		internal enum RelationType { Self, Extends, Feeds, Suppresses, Cancels, Prevents };
		internal struct DirectRelation {
			public TStatus Status;
			public RelationType Relation;
			public bool IsConditional;
			public bool IsNegative => Relation == RelationType.Cancels || Relation == RelationType.Prevents || Relation == RelationType.Suppresses;
		}
		internal class Relationship {
			public List<DirectRelation> Path;
			public TStatus SourceStatus => Path[0].Status;
			public TStatus TargetStatus => Path[Path.Count - 1].Status;
			public RelationType Relation => Path[Path.Count - 1].Relation;
			public bool ChainBroken => !Path.Take(Path.Count - 1).All(x => !x.IsNegative);
			public bool IsNegative => Path.Where(x => x.IsNegative).Count() % 2 != 0;
			public bool IsConditional => Path.Where(x => x.IsConditional).Any();
		}
	}
}
