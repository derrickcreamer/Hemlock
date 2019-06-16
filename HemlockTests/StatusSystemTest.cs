using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using NUnit.Framework;
using Hemlock;

namespace HemlockTests {
	[TestFixture] public class StatusSystemTest {
		public enum TestStatus { A = 2, B, C, D = 7, E = -3, F = -999, AlsoD = 7 };
		public class TestObj { }

		protected StatusSystem<TestObj, TestStatus> rules;
		protected TestObj testObj;
		protected StatusTracker<TestObj, TestStatus> tracker;

		[SetUp] public void Initialize() {
			StatusConverter<TestStatus, int>.Convert = x => (int)x;
			rules = new StatusSystem<TestObj, TestStatus>();
			testObj = new TestObj();
			tracker = rules.CreateStatusTracker(testObj);
		}

		[TestFixture] public class Init : StatusSystemTest {
			[TestCase] public void InitialValuesZero() {
				tracker = rules.CreateStatusTracker(null);
				for(int i = -1000;i<50;++i) Assert.AreEqual(0, tracker[(TestStatus)i]);
			}
			[TestCase] public void InitializationExceptions() {
				StatusConverter<TestStatus, int>.Convert = null;
				Assert.Throws<InvalidOperationException>(() => new StatusSystem<TestObj, TestStatus>().CreateStatusTracker(testObj));
				Assert.Throws<InvalidOperationException>(() => new StatusSystem<TestObj, TestStatus>()[TestStatus.A].Feeds(TestStatus.B));
				StatusConverter<TestStatus, int>.Convert = x => (int)x; // Reset it, because I'm not sure NUnit will.
			}
		}
		[TestFixture] public class Sources : StatusSystemTest {
			[TestCase] public void BasicSourceOperations() {
				Assert.Throws<ArgumentNullException>(()=> tracker.AddSource(null));
				Assert.Throws<ArgumentNullException>(() => tracker.RemoveSource(null));
				Source<TestObj, int> s = new Source<TestObj, int>((int)TestStatus.E, value: 2); // (method #1 of creating Sources)
				Assert.AreEqual(0, tracker[TestStatus.E]);
				tracker.AddSource(s);
				Assert.AreEqual(2, tracker[TestStatus.E]);
				s.Value = 7;
				Assert.AreEqual(7, tracker[TestStatus.E]);
				Source<TestObj, int> s2 = tracker.CreateSource(TestStatus.E, value: 6); // (method #2 of creating Sources)
				tracker.AddSource(s2);
				Assert.AreEqual(13, tracker[TestStatus.E]);
				tracker.RemoveSource(s);
				Assert.AreEqual(6, tracker[TestStatus.E]);
				tracker.Cancel(TestStatus.E);
				Assert.AreEqual(0, tracker[TestStatus.E]);
			}
			[TestCase] public void SingleSource() {
				rules[TestStatus.C].SingleSource = true;
				var s = tracker.CreateSource(TestStatus.C, value: 2); // (method #3 of creating Sources)
				var s2 = tracker.CreateSource(TestStatus.C, value: 7);
				tracker.AddSource(s);
				tracker.AddSource(s2);
				Assert.AreEqual(7, tracker[TestStatus.C]);

				rules[TestStatus.B].SingleSource = true;
				tracker[TestStatus.B] = -4;
				Assert.AreEqual(-4, tracker[TestStatus.B]);
				var s3 = tracker.CreateSource(TestStatus.B, value: 1);
				tracker.AddSource(s3);
				Assert.AreEqual(1, tracker[TestStatus.B]);
				tracker.Cancel(TestStatus.B);
				Assert.AreEqual(0, tracker[TestStatus.B]);

				Assert.Throws<InvalidOperationException>(()=> tracker[TestStatus.E] = 5 );
			}
			[TestCase] public void SameEnumValues() {
				rules[TestStatus.AlsoD].SingleSource = true; //D and AlsoD share their enum value (7).
				Assert.True(rules[TestStatus.D].SingleSource);
				tracker[TestStatus.D] = 11;
				Assert.AreEqual(11, tracker[TestStatus.AlsoD]);
			}
			[TestCase] public void NonIntBaseStatus() {
				BaseStatusSystem<TestObj, char> charRules = new BaseStatusSystem<TestObj, char>();
				BaseStatusTracker<TestObj, char> charTracker = charRules.CreateStatusTracker(testObj);
				charTracker.Add('@');
				Assert.AreEqual(1, charTracker['@']);
			}
			[TestCase] public void TryGetValue() {
				var source = new Source<TestObj, int>(7); // 7 is defined for TestStatus
				TestStatus status;
				Assert.True(source.TryGetStatus(out status));
				Assert.AreEqual(TestStatus.D, status);

				var source2 = new Source<TestObj, int>(8); // 8 is not defined for TestStatus
				Assert.False(source2.TryGetStatus(out status)); // Returns false, no exception
			}
			[TestCase]
			public void GetValue() {
				var source = new Source<TestObj, int>(7); // 7 is defined for TestStatus
				Assert.AreEqual(TestStatus.D, source.GetStatus<TestStatus>());

				var source2 = new Source<TestObj, int>(8); // 8 is not defined for TestStatus
				Assert.AreEqual((TestStatus)8, source2.GetStatus<TestStatus>());
			}
		}
		[TestFixture] public class Aggregators : StatusSystemTest {
			[TestCase] public void BasicAggOperations() {
				Assert.Throws<ArgumentException>(() => {
					rules[TestStatus.D].Aggregator = ints => 4;
				});
				rules[TestStatus.A].Aggregator = rules.Total;
				rules[TestStatus.B].Aggregator = rules.Bool;
				rules[TestStatus.C].Aggregator = rules.MaximumOrZero;
				for(int i=0;i<2;++i) {
					tracker.Add(TestStatus.A, value:i+3);
					tracker.Add(TestStatus.B, value: i+3);
					tracker.Add(TestStatus.C, value: i+3);
				}
				Assert.AreEqual(7, tracker[TestStatus.A]);
				Assert.AreEqual(1, tracker[TestStatus.B]);
				Assert.AreEqual(4, tracker[TestStatus.C]);

				Func<IEnumerable<int>, int> safeAgg = ints => {
					foreach(int i in ints) {
						return 4;
					}
					return 0;
				};
				rules[TestStatus.E].Aggregator = safeAgg;
				tracker.Add(TestStatus.E, value:25);
				Assert.AreEqual(4, tracker[TestStatus.E]);
			}
			[TestCase] public void NullAggs() {
				Assert.Throws<ArgumentNullException>(() => rules.DefaultValueAggregator = null);
				rules.DefaultValueAggregator = ints => { foreach(int i in ints) return 7;   return 0; };
				tracker.Add(TestStatus.A);
				rules[TestStatus.A].Aggregator = null;
				Assert.AreEqual(7, tracker[TestStatus.A]);
				tracker.Cancel(TestStatus.A); // remove and re-add, because 'live' agg changes aren't supported
				rules[TestStatus.A].Aggregator = ints => { foreach(int i in ints) return 5;   return 0; };
				tracker.Add(TestStatus.A);
				Assert.AreEqual(5, tracker[TestStatus.A]);
				Assert.Null(rules[TestStatus.B].Aggregator);
				tracker.Cancel(TestStatus.A);
				rules[TestStatus.A].Aggregator = rules[TestStatus.B].Aggregator; // null again
				tracker.Add(TestStatus.A);
				Assert.AreEqual(7, tracker[TestStatus.A]);
			}
		}
		[TestFixture] public class Verbs : StatusSystemTest {
			[TestCase] public void Feeds() {
				rules[TestStatus.A].Feeds(TestStatus.B);
				tracker.Add(TestStatus.A, value:3);
				Assert.AreEqual(3, tracker[TestStatus.B]);
			}
			[TestCase] public void FeedsCustom() {
				rules[TestStatus.A].Feeds(6, i => i%2 != 0, TestStatus.B); // "Feed 6 into B if A is odd."
				tracker.Add(TestStatus.A, value: 4);
				Assert.AreEqual(0, tracker[TestStatus.B]);
				tracker.Add(TestStatus.A, value: -1);
				Assert.AreEqual(6, tracker[TestStatus.B]);
			}
			[TestCase] public void Suppresses() {
				rules[TestStatus.A].Suppresses(TestStatus.B);
				tracker.Add(TestStatus.B, value: 4);
				tracker.Add(TestStatus.A, value: 3);
				Assert.AreEqual(0, tracker[TestStatus.B]);
				tracker.Cancel(TestStatus.A);
				Assert.AreEqual(4, tracker[TestStatus.B]);
			}
			[TestCase] public void Prevents() {
				rules[TestStatus.A].Prevents(TestStatus.B);
				tracker.Add(TestStatus.B, value: 4);
				tracker.Add(TestStatus.A, value: 3);
				tracker.Add(TestStatus.B, value: 55);
				Assert.AreEqual(4, tracker[TestStatus.B]);
			}
			[TestCase] public void PreventedWhen() {
				rules[TestStatus.A].PreventedWhen((obj, status) => obj != testObj); // only testObj can receive status.A
				TestObj testObj2 = new TestObj();
				var tracker2 = rules.CreateStatusTracker(testObj2);
				tracker.AddSource(new Source<TestObj, int>((int)TestStatus.A, 3));
				Assert.AreEqual(3, tracker[TestStatus.A]); // not prevented for testObj
				tracker2.AddSource(new Source<TestObj, int>((int)TestStatus.A, 3));
				Assert.AreEqual(0, tracker2[TestStatus.A]); // prevented for testObj2
			}
			[TestCase] public void Cancels() {
				rules[TestStatus.A].Cancels(TestStatus.B);
				tracker.Add(TestStatus.A, value: 3);
				tracker.Add(TestStatus.B, value: 55);
				Assert.AreEqual(55, tracker[TestStatus.B]); // B not prevented
				tracker.Add(TestStatus.A, value: -8);
				Assert.AreEqual(55, tracker[TestStatus.B]); // B not cancelled: A did not increase
				tracker.Add(TestStatus.A, value: 2);
				Assert.AreEqual(0, tracker[TestStatus.B]); // B cancelled
			}
			[TestCase] public void CancelsCustom() {
				// "whenever A increases to a value between 5 and 7, cancel B" :
				rules[TestStatus.A].Cancels(i => i>=5 && i<=7, TestStatus.B);
				tracker.Add(TestStatus.B, value: 55);
				tracker.Add(TestStatus.A, value: 3);
				Assert.AreEqual(55, tracker[TestStatus.B]); // B not cancelled
				tracker.Add(TestStatus.A, value: 4);
				Assert.AreEqual(0, tracker[TestStatus.B]); // B cancelled
				tracker.Add(TestStatus.B, value: 99);
				tracker.Add(TestStatus.A, value: 1);
				Assert.AreEqual(99, tracker[TestStatus.B]); // B not cancelled
			}
			[TestCase] public void CancelsPriority() {
				rules[TestStatus.A].Aggregator = rules.Bool;
				string message = null;
				rules[TestStatus.A].Messages.Decreased = (obj, st, ov, nv) => {
					message = "Status A is no longer true";
				};
				var s = new Source<TestObj, int>[4];
				for(int i=0;i<4;++i) {
					int ii = i;
					s[i] = new Source<TestObj, int>((int)TestStatus.A, priority: ii*ii, overrideSetIndex: i); //0, 1, 4, & 9 priority
					
					rules.GetOverrideSet(i).Overrides(TestStatus.A).Messages.Decreased = (obj, st, ov, nv) => {
						message = $"Status A is no longer true: Source {ii}";
					};
				}
				tracker.AddSource(s[1]); // Order of addition doesn't matter
				tracker.AddSource(s[3]);
				tracker.AddSource(s[0]);
				tracker.AddSource(s[2]);
				tracker.Cancel(TestStatus.A);
				Assert.AreEqual("Status A is no longer true: Source 3", message);
				// Highest priority was last to be removed, and was the only one to actually change the
				// value (because of the boolean aggregator)
			}
			[TestCase] public void Extends() {
				rules[TestStatus.A].Prevents(TestStatus.C);
				rules[TestStatus.D].Extends(TestStatus.C);
				tracker.Add(TestStatus.C, value: 44);
				Assert.AreEqual(0, tracker[TestStatus.D]); // C does not add to D
				tracker.Add(TestStatus.D, value: 2);
				Assert.AreEqual(46, tracker[TestStatus.C]); // D adds to C
				tracker.Cancel(TestStatus.D);
				Assert.AreEqual(44, tracker[TestStatus.C]); // C is not cancelled with D
				tracker.Add(TestStatus.D, value: 5);
				tracker.Cancel(TestStatus.C);
				Assert.AreEqual(0, tracker[TestStatus.D]); // D is cancelled with C
				tracker.Add(TestStatus.A, value: 8);
				tracker.Add(TestStatus.D, value: 5);
				Assert.AreEqual(0, tracker[TestStatus.D]); // D is prevented with C
			}
			[TestCase] public void PreventedWhenExtends() {
				rules[TestStatus.A].PreventedWhen((obj, status) => true); // A is always prevented
				rules[TestStatus.B].Extends(TestStatus.A);
				tracker.Add(TestStatus.B, 3);
				Assert.AreEqual(0, tracker[TestStatus.B]); // B is prevented because it extends A
			}
		}
		[TestFixture] public class OnChanged : StatusSystemTest {
			[TestCase] public void BasicOnChangedOperations() {
				string message = null;
				int num = 0;
				rules[TestStatus.A].Messages.Changed = (obj, status, oldValue, newValue) =>
				{
					message = "Status A changed";
				};
				rules[TestStatus.A].Effects.Increased = (obj, status, oldValue, newValue) => {
					num = newValue;
				};
				tracker.Add(TestStatus.A);
				Assert.AreEqual("Status A changed", message);
				Assert.AreEqual(1, num);
				message = null;
				num = 0;
				tracker.Cancel(TestStatus.A);
				Assert.AreEqual("Status A changed", message); // Message changes on increase & decrease.
				Assert.AreEqual(0, num); // Num changes only on increase.
			}
			[TestCase] public void StatusOverrides() {
				string message = null;
				int num = 0;
				rules[TestStatus.A].Messages.Changed = (obj, status, oldValue, newValue) => {
					message = "Status A changed";
				};
				rules[TestStatus.A].Effects.Increased = (obj, status, oldValue, newValue) => {
					num = newValue;
				};
				rules[TestStatus.B].Feeds(TestStatus.A);
				rules[TestStatus.C].Feeds(TestStatus.B);
				rules[TestStatus.B].Overrides(TestStatus.A).Messages.Increased = (obj, status, oldValue, newValue) => {
					message = "Status A changed, by way of status B";
				};
				rules[TestStatus.C].Overrides(TestStatus.A).Messages.Increased = rules.DoNothing;
				tracker.Add(TestStatus.C); // C feeds B which feeds A
				Assert.AreEqual(null, message); // Increase message was overridden by the first link in the chain (C)
				Assert.AreEqual(1, num); // Increase effect was not overridden
				tracker.Cancel(TestStatus.C);
				Assert.AreEqual("Status A changed", message); // Decrease message was not overridden
			}
			[TestCase] public void SourceOverrides() {
				string message = null;
				string messagePart2 = null;
				int num = 0;
				rules[TestStatus.A].Messages.Changed = (obj, status, oldValue, newValue) => {
					message = "Status A changed";
					messagePart2 = "(etc.)";
				};
				rules[TestStatus.A].Effects.Increased = (obj, status, oldValue, newValue) => {
					num = newValue;
				};
				rules[TestStatus.B].Feeds(TestStatus.A);
				rules.GetOverrideSet(0).Overrides(TestStatus.A).Messages.Changed = (obj, status, oldValue, newValue) => {
					message = "Status A changed because of status B changing";
				};
				var s = new Source<TestObj, int>((int)TestStatus.B, overrideSetIndex: 0);
				tracker.AddSource(s);
				Assert.AreEqual("Status A changed because of status B changing", message);
				Assert.AreEqual(null, messagePart2); // The original change message for A did not happen at all
				Assert.AreEqual(1, num); // Increase effect was not overridden
			}
			[TestCase] public void StatusOverridesWithOverrideSet() {
				bool increased = false;
				rules.GetOverrideSet(19).Overrides(TestStatus.C).Effects.Increased = (obj, st, ov, nv) => {
					increased = true;
				};
				rules[TestStatus.C].UsesOverrideSet(19);
				rules[TestStatus.C].Effects.Increased = (obj, st, ov, nv) => {
					throw new Exception("should not happen"); // Since TestStatus.C is using an override set, its normal handlers are ignored
				};
				tracker.Add(TestStatus.C);
				Assert.IsTrue(increased); // Only the handler from the override set was applied
			}
			[TestCase] public void SourceOverridesWithOverrideSet() {
				int n = 0;
				rules.GetOverrideSet(1).Overrides(TestStatus.E).Effects.Changed = (obj, st, ov, nv) => {
					n = 5;
				};
				rules.GetOverrideSet(2).Overrides(TestStatus.E).Effects.Changed = (obj, st, ov, nv) => {
					n = 6;
				};
				rules[TestStatus.E].Effects.Changed = (obj, st, ov, nv) => {
					n = 7;
				};
				rules[TestStatus.E].UsesOverrideSet(1);
				tracker.Add(TestStatus.E, overrideSetIndex: 2);
				Assert.AreEqual(6, n);
			}
			[TestCase] public void ToggleGenerateOptions() {
				int i = 0, j = 0;
				rules[TestStatus.A].Messages.Changed = (obj, st, ov, nv) => {
					i = 3;
				};
				rules[TestStatus.A].Effects.Changed = (obj, st, ov, nv) => {
					j = 7;
				};
				tracker.GenerateNoEffects = true;
				tracker.Add(TestStatus.A);
				Assert.AreEqual(3, i);
				Assert.AreEqual(0, j);
				i = 0;
				tracker.GenerateNoEffects = false;
				tracker.GenerateNoMessages = true;
				tracker.Add(TestStatus.A);
				Assert.AreEqual(0, i);
				Assert.AreEqual(7, j);
			}
		}
		[TestFixture] public class MultipleEnums : StatusSystemTest {
			public enum OtherStatus { One = 8, Two }; // (Carefully) start at 8 because TestStatus ends at 7.
			[TestCase] public void BasicMultipleEnumOperations() {
				StatusConverter<OtherStatus, int>.Convert = x => (int)x;
				var mRules = new StatusSystem<TestObj, OtherStatus, TestStatus>(); // Int base. Using OtherStatus and TestStatus.
				mRules[TestStatus.A].SingleSource = true;
				mRules[OtherStatus.One].Cancels(TestStatus.A);
				mRules[TestStatus.F].Feeds(OtherStatus.One);
				mRules[OtherStatus.Two].Feeds(3, TestStatus.F);
				var mTracker = mRules.CreateStatusTracker(testObj);
				mTracker[TestStatus.A] = 22;
				Assert.AreEqual(22, mTracker[TestStatus.A]);
				mTracker.Add(OtherStatus.Two, 5);
				Assert.AreEqual(5, mTracker[OtherStatus.Two]);
				Assert.AreEqual(3, mTracker[TestStatus.F]);
				Assert.AreEqual(3, mTracker[OtherStatus.One]);
				Assert.AreEqual(0, mTracker[TestStatus.A]);
			}
		}
		[TestFixture] public class RuleChecker : StatusSystemTest {
			[TestCase] public void InfiniteFeedIllegal() {
				var checkedRules = new StatusSystem<TestObj, TestStatus>();
				checkedRules[TestStatus.A].Feeds(TestStatus.A);
				Assert.Throws<InvalidDataException>(() => checkedRules.CreateStatusTracker(testObj));
			}
			[TestCase] public void SelfSuppressionIllegal() {
				var checkedRules = new StatusSystem<TestObj, TestStatus>();
				checkedRules[TestStatus.A].Feeds(TestStatus.B);
				checkedRules[TestStatus.B].Extends(TestStatus.C);
				checkedRules[TestStatus.C].Suppresses(TestStatus.A);
				Assert.Throws<InvalidDataException>(() => checkedRules.CreateStatusTracker(testObj));
			}
			[TestCase] public void SuppressionCycleIllegal() {
				var checkedRules = new StatusSystem<TestObj, TestStatus>();
				checkedRules[TestStatus.A].Suppresses(TestStatus.B);
				checkedRules[TestStatus.B].Suppresses(TestStatus.C);
				checkedRules[TestStatus.C].Suppresses(TestStatus.A);
				Assert.Throws<InvalidDataException>(() => checkedRules.CreateStatusTracker(testObj));
			}
			[TestCase] public void ConditionalSuppressionCycleWarning() {
				var checkedRules = new StatusSystem<TestObj, TestStatus>();
				checkedRules[TestStatus.A].Suppresses(i => i < 4, TestStatus.B); // With a condition.
				checkedRules[TestStatus.B].Suppresses(TestStatus.C);
				checkedRules[TestStatus.C].Suppresses(TestStatus.A);
				var errors = checkedRules.GetRuleErrorsAndWarnings();
				Assert.AreEqual(1, errors.Count); // Just one warning.
				Assert.IsTrue(errors[0].StartsWith("CRITICAL WARNING"));
				checkedRules.CreateStatusTracker(testObj); // No exception.
			}
			[TestCase] public void BrokenSuppressionCycleOkay() {
				var checkedRules = new StatusSystem<TestObj, TestStatus>();
				checkedRules[TestStatus.A].Suppresses(TestStatus.B);
				checkedRules[TestStatus.B].Suppresses(TestStatus.C);
				checkedRules[TestStatus.C].Suppresses(TestStatus.A);
				checkedRules[TestStatus.C].Cancels(TestStatus.A);
				var errors = checkedRules.GetRuleErrorsAndWarnings();
				Assert.AreEqual(1, errors.Count); // Just one warning.
				Assert.IsTrue(errors[0].StartsWith("OKAY"));
				checkedRules.CreateStatusTracker(testObj); // No exception.
			}
			[TestCase] public void ReversedSuppressionCycleDetected() {
				var checkedRules = new StatusSystem<TestObj, TestStatus>();
				checkedRules[TestStatus.A].Prevents(TestStatus.B);
				checkedRules[TestStatus.B].Prevents(TestStatus.C); // One cycle in one direction.
				checkedRules[TestStatus.C].Prevents(TestStatus.A);
				checkedRules[TestStatus.A].Cancels(TestStatus.C);
				checkedRules[TestStatus.B].Cancels(TestStatus.A); // Another in the opposite direction.
				checkedRules[TestStatus.C].Cancels(TestStatus.B);
				Assert.AreEqual(2, checkedRules.GetRuleErrorsAndWarnings().Count); // One warning for each.
				checkedRules.CreateStatusTracker(testObj); // No exception.
			}
			[TestCase] public void SingleErrorForLargeSuppressionCycle() {
				var checkedRules = new StatusSystem<TestObj, TestStatus>();
				checkedRules[TestStatus.A].Suppresses(TestStatus.B);
				checkedRules[TestStatus.B].Suppresses(TestStatus.C);
				checkedRules[TestStatus.C].Suppresses(TestStatus.D);
				checkedRules[TestStatus.D].Extends(TestStatus.E);
				checkedRules[TestStatus.E].Suppresses(TestStatus.F); // 5 suppressions in 7 links.
				checkedRules[TestStatus.F].Feeds((TestStatus)55);
				checkedRules[55].Suppresses(TestStatus.A);
				var errors = checkedRules.GetRuleErrorsAndWarnings();
				Assert.AreEqual(1, errors.Count); // Just one error.
			}
			[TestCase] public void FeedPlusExtendWarning() {
				var checkedRules = new StatusSystem<TestObj, TestStatus>();
				checkedRules[TestStatus.A].Feeds(TestStatus.B);
				checkedRules[TestStatus.A].Extends(TestStatus.B);
				var errors = checkedRules.GetRuleErrorsAndWarnings();
				Assert.AreEqual(1, errors.Count); // Just one warning.
				Assert.IsTrue(errors[0].StartsWith("WARNING"));
				checkedRules.CreateStatusTracker(testObj); // No exception.
			}
			[TestCase] public void NegativePlusPositiveWarning() {
				var checkedRules = new StatusSystem<TestObj, TestStatus>();
				checkedRules[TestStatus.A].Feeds(TestStatus.B);
				checkedRules[TestStatus.A].Cancels(TestStatus.B);
				checkedRules[TestStatus.A].Suppresses(TestStatus.B);
				var errors = checkedRules.GetRuleErrorsAndWarnings();
				Assert.AreEqual(1, errors.Count); // Just one warning.
				Assert.IsTrue(errors[0].StartsWith("WARNING"));
				checkedRules.CreateStatusTracker(testObj); // No exception.
			}
			[TestCase] public void MutualSuppressionWarning() {
				var checkedRules = new StatusSystem<TestObj, TestStatus>();
				checkedRules[TestStatus.A].Suppresses(TestStatus.B);
				checkedRules[TestStatus.B].Suppresses(TestStatus.A);
				var errors = checkedRules.GetRuleErrorsAndWarnings();
				Assert.AreEqual(1, errors.Count); // Just one warning.
				Assert.IsTrue(errors[0].StartsWith("WARNING"));
				checkedRules.CreateStatusTracker(testObj); // No exception.
			}
			[TestCase] public void InconsistentAggregatorWarning() {
				var checkedRules = new StatusSystem<TestObj, TestStatus>();
				checkedRules[TestStatus.B].Extends(TestStatus.A);
				checkedRules[TestStatus.A].Aggregator = checkedRules.MaximumOrZero;
				var errors = checkedRules.GetRuleErrorsAndWarnings();
				Assert.AreEqual(1, errors.Count); // Just one warning.
				Assert.IsTrue(errors[0].StartsWith("WARNING"));
				checkedRules.CreateStatusTracker(testObj); // No exception.
			}
			public enum OverlapOne { Red = 4, Green = 4, Blue = 3 };
			public enum OverlapTwo { Red = 5, Green = 5, Blue = 4 };
			[TestCase] public void OverlappingEnumValueWarning() {
				StatusConverter<OverlapOne, int>.Convert = i => (int)i;
				StatusConverter<OverlapTwo, int>.Convert = i => (int)i;
				var checkedRules = new StatusSystem<TestObj, OverlapOne, OverlapTwo>();
				var errors = checkedRules.GetRuleErrorsAndWarnings();
				Assert.AreEqual(1, errors.Count); // Just one warning: Shared value (4).
				Assert.IsTrue(errors[0].StartsWith("WARNING"));
				checkedRules.CreateStatusTracker(testObj); // No exception.
			}
		}
		[TestFixture] public class Parser : StatusSystemTest {
			public enum RPS { Rock, Paper, Scissors, NumChoices };
			public enum RGB { Red = RPS.NumChoices, Green, Blue };
			public static readonly List<string> rps1 = new List<string> {
				"rock foils scissors",
				"scissors foils paper",
				"paper foils rock"
			};
			public static readonly List<string> rgb1 = new List<string> {
				"",
				"red{ # a block and a comment",
				" feeds blue; cancels green",
				"}",
				"green == 4 feeds blue 2"
			};
			public static readonly List<string> rps_rgb1 = new List<string> {
				"paper>2 suppresses green, blue;  red feeds paper"
			};
			[TestCase] public void BasicParserOperations() {
				Assert.Throws<System.IO.InvalidDataException>(() => new StatusSystem<TestObj>().ParseRulesText(rps1));
				StatusConverter<RPS, int>.Convert = i => (int)i;
				StatusConverter<RGB, int>.Convert = i => (int)i;
				var pRules = new StatusSystem<TestObj, RPS, RGB>();
				pRules.ParseRulesText(rps1.Concat(rgb1).Concat(rps_rgb1));
				var pTracker = pRules.CreateStatusTracker(testObj);
				pTracker.Add(RPS.Rock);
				pTracker.Add(RGB.Red);
				Assert.AreEqual(0, pTracker[RPS.Rock]); // Red fed Paper which cancelled Rock.
			}
			[TestCase] public void SingleStatusParserOperations() {
				var baseRules = new BaseStatusSystem<TestObj, RGB>();
				baseRules.ParseRulesText(rgb1);
				var baseTracker = baseRules.CreateStatusTracker(testObj);
				baseTracker.Add(RGB.Green, 3);
				baseTracker.Add(RGB.Red);
				baseTracker.Add(RGB.Green, 4);
				Assert.AreEqual(3, baseTracker[RGB.Blue]); // 1 from red, 2 from green.
			}
			[TestCase] public void LoneStatusSetBasedOnModes() {
				var pRules = new StatusSystem<TestObj, TestStatus>();
				List<string> ruleText = new List<string> {
					"a; b; c",
					"d",
					"bool:",
					"e; f",
					"a"
				};
				pRules.ParseRulesText(ruleText);
				Assert.AreSame(pRules.Bool, pRules[TestStatus.E].Aggregator);
				Assert.AreSame(pRules.Bool, pRules[TestStatus.F].Aggregator);
				Assert.AreSame(pRules.Bool, pRules[TestStatus.A].Aggregator); // All 3 set to bool.
				pRules.CreateStatusTracker(testObj); // No exception.
			}
			[TestCase] public void ParserConditions() {
				var pRules = new StatusSystem<TestObj, TestStatus>();
				List<string> ruleTextAB = new List<string> {
					"a==1 feeds b",
					"a!=1 feeds b",
					"a>=1 feeds b",
					"a<=1 feeds b",
					"a>1 feeds b",
					"a<1 feeds b"
				};
				List<string> ruleTextCD = new List<string> {
					"c==5 feeds d 3",
					"c>=5 feeds d 3",
					"c>5 feeds d 3",
				};
				pRules.ParseRulesText(ruleTextAB); // Works.
				pRules.ParseRulesText(ruleTextCD); // Works.
				//These all throw because they try to feed 3 even when c is 0:
				Assert.Throws<InvalidDataException>(() => pRules.ParseRulesText(new List<string> { "c!=5 feeds d 3" }));
				Assert.Throws<InvalidDataException>(() => pRules.ParseRulesText(new List<string> { "c<=5 feeds d 3" }));
				Assert.Throws<InvalidDataException>(() => pRules.ParseRulesText(new List<string> { "c<5 feeds d 3" }));
			}
		}
	}
}
