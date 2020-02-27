using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Reflection;

namespace Hemlock {

	using TBaseStatus = System.Int32;

	public static class StatusParser {
		/// <summary>
		/// Open "<paramref name="filename"/>" and parse its text as rules. Apply the parsed rules to "<paramref name="rules"/>".
		/// </summary>
		public static StatusSystem<TObject> ParseRulesText<TObject>(this StatusSystem<TObject> rules, string filename) {
			if(!File.Exists(filename)) throw new FileNotFoundException($"{filename} cannot be found here.");
			List<string> text = new List<string>();
			using(StreamReader stream = new StreamReader(filename)) {
				while(true) {
					string line = stream.ReadLine();
					if(line == null) break;
					text.Add(line);
				}
			}
			return rules.ParseRulesText(text);
		}
		/// <summary>
		/// Parse "<paramref name="text"/>" as rules. Apply the parsed rules to "<paramref name="rules"/>".
		/// </summary>
		public static StatusSystem<TObject> ParseRulesText<TObject>(this StatusSystem<TObject> rules, IEnumerable<string> text) {
			new StatusParserInternal<TObject>(rules, Tokenize(text)).Parse();
			return rules;
		}
		private static List<string> Tokenize(IEnumerable<string> text) {
			var result = new List<string>();
			var separators = new[] { ' ', '\t' };
			var regexSeparators = new[] { "==", "!=", "<=", ">=", "<", ">", "{", "}", ":", ",", ";" };
			var regexPattern = "(" + string.Join("|", regexSeparators.Select(x => Regex.Escape(x))) + ")";
			foreach(string rawLine in text) {
				string line = rawLine;
				int commentIdx = line.IndexOf('#');
				if(commentIdx != -1) line = line.Substring(0, commentIdx);
				foreach(string section in Regex.Split(line, regexPattern)) {
					result.AddRange(section.Split(separators, StringSplitOptions.RemoveEmptyEntries));
				}
				result.Add("\r\n");
			}
			return result;
		}
		internal static bool IsBasicVerb(this string token) {
			return new List<string> { "feeds", "suppresses", "prevents", "cancels", "extends", "foils" }.Contains(token);
		}
		internal static bool IsInvertedVerb(this string token) {
			return new List<string> { "fed", "suppressed", "prevented", "canceled", "cancelled", "extended", "foiled" }.Contains(token);
		}
		internal static bool IsComparisonOperator(this string token) {
			return new List<string> { "==", "!=", "<", "<=", ">", ">=" }.Contains(token);
		}
		internal static bool IsNumber(this string token) {
			int tokenInt;
			return int.TryParse(token, out tokenInt);
		}
		internal static bool IsAggregator(this string token) {
			return new List<string> { "total", "bool", "max" }.Contains(token);
		}
		internal static bool IsInstanceLimiter(this string token) {
			return new List<string> { "single", "multiple" }.Contains(token);
		}
	}

	internal class StatusParserInternal<TObject> {
		StatusSystem<TObject> rules;
		List<string> tokens;
		int idx;
		string rulesDefaultAggregator;
		string parserDefaultAggregator;
		string parserDefaultInstanceLimit;
		List<Type> extraEnums;
		internal StatusParserInternal(StatusSystem<TObject> rules, List<string> tokens) {
			this.rules = rules;
			this.tokens = tokens;
			extraEnums = rules.extraEnumTypes;
			if(rules.DefaultValueAggregator == rules.Total) rulesDefaultAggregator = "total";
			else if(rules.DefaultValueAggregator == rules.Bool) rulesDefaultAggregator = "bool";
			else if(rules.DefaultValueAggregator == rules.MaximumOrZero) rulesDefaultAggregator = "max";
			else rulesDefaultAggregator = null;
			parserDefaultAggregator = rulesDefaultAggregator;
			parserDefaultInstanceLimit = "multiple";
		}
		internal void Parse() {
			for(idx = 0;idx<tokens.Count;) {
				string token = tokens[idx];
				switch(token) {
					case "\r\n":
					case ";":
						++idx; //consume & ignore newlines & semicolons
						break;
					case "total":
					case "max":
					case "bool":
						parserDefaultAggregator = token;
						++idx;
						ConsumeOrError(":");
						break;
					case "single":
					case "multiple":
						parserDefaultInstanceLimit = token;
						++idx;
						ConsumeOrError(":");
						break;
					default: {
							if(token.IsNumber()) throw new InvalidDataException("Syntax error: Expected status or mode, got number instead.");
							TBaseStatus status;
							if(!TryParse(token, out status)) throw new InvalidDataException($"Error: {token} not recognized as any given type.");
							++idx;
							SetAggregator(status, parserDefaultAggregator);
							SetInstanceLimit(status, parserDefaultInstanceLimit);
							if(TryConsume(";")) break;
							else if(TryConsume("\r\n")) break;
							else if(TryConsume("{")) BeginBlock(status);
							else ReadRule(status);
						}
						break;
				}
			}
		}
		private static MethodInfo baseTryParse = null;
		private bool TryParse(string token, out TBaseStatus status) {
			if(typeof(TBaseStatus).IsEnum && Enum.TryParse(token, true, out status)) return true;
			if(extraEnums != null) {
				if(baseTryParse == null) {
					foreach(var method in typeof(Enum).GetMethods()) {
						var parameters = method.GetParameters();
						if(method.Name == "TryParse" && parameters.Length == 3 && method.IsGenericMethod
							&& parameters[0].ParameterType == typeof(string) && parameters[1].ParameterType == typeof(bool)
							&& parameters[2].ParameterType.GetElementType() == method.GetGenericArguments()[0]) {
							baseTryParse = method;
							break;
						}
					}
					if(baseTryParse == null) throw new MissingMethodException("Enum.TryParse<TEnum>(string, bool, TEnum) seems to be missing.");
				}
				foreach(Type t in extraEnums) {
					MethodInfo m = baseTryParse.MakeGenericMethod(t);
					object[] mParams = new object[] { token, true, null };
					bool success = (bool)m.Invoke(null, mParams);
					if(success) {
						status = (TBaseStatus)mParams[2];
						return true;
					}
				}
			}
			status = default(TBaseStatus);
			return false;
		}
		private void SetAggregator(TBaseStatus status, string aggString) {
			Func<IEnumerable<int>, int> agg = null;
			if(rulesDefaultAggregator == null || rulesDefaultAggregator != aggString) { //if the rules default agg is something unknown,
				switch(aggString) { // or just doesn't match the current one, set it.
					case "total":
						agg = rules.Total;
						break;
					case "bool":
						agg = rules.Bool;
						break;
					case "max":
						agg = rules.MaximumOrZero;
						break;
				}
			}
			rules[status].Aggregator = agg;
		}
		private void SetInstanceLimit(TBaseStatus status, string limitString) {
			rules[status].SingleInstance = (limitString == "single");
		}
		private void ConsumeOrError(string targetToken) {
			string temp = null;
			ConsumeOrError(s => s == targetToken, ref temp);
		}
		private void ConsumeOrError(string targetToken, ref string consumed) {
			ConsumeOrError(s => s == targetToken, ref consumed);
		}
		private void ConsumeOrError(Func<string, bool> condition) {
			string temp = null;
			ConsumeOrError(condition, ref temp);
		}
		private void ConsumeOrError(Func<string, bool> condition, ref string consumed) {
			try {
				if(!TryConsume(condition, ref consumed)) {
					throw new InvalidDataException($"Syntax error: Unexpected {tokens[idx]}");
				}
			}
			catch(IndexOutOfRangeException) {
				throw new EndOfStreamException($"Unexpected end of file.");
			}
		}
		private bool TryConsume(string targetToken) {
			string temp = null;
			return TryConsume(s => s == targetToken, ref temp);
		}
		private bool TryConsume(string targetToken, ref string consumed) {
			return TryConsume(s => s == targetToken, ref consumed);
		}
		private bool TryConsume(Func<string, bool> condition) {
			string temp = null;
			return TryConsume(condition, ref temp);
		}
		private bool TryConsume(Func<string, bool> condition, ref string consumed) {
			if(idx >= tokens.Count) return false;
			if(condition(tokens[idx])) {
				consumed = tokens[idx];
				++idx;
				return true;
			}
			else return false;
		}
		private void BeginBlock(TBaseStatus status) {
			while(idx < tokens.Count) {
				string token = tokens[idx];
				switch(token) {
					case "}":
						++idx; //consume the closing brace
						return; // then we're done with this block.
					case ";":
					case "\r\n": //ignore semicolons and newlines.
						++idx;
						break;
					default:
						ReadRule(status); //Otherwise, it must be the start of a rule.
						break;
				}
			}
			throw new EndOfStreamException("Unexpected end of file: Expected closing brace '}'.");
		}
		private void ReadRule(TBaseStatus status) {
			string token = tokens[idx];
			if(token.IsBasicVerb() || token.IsComparisonOperator()) ReadBasicRule(status);
			else if(token.IsInvertedVerb() || token.IsNumber()) ReadInvertedRule(status);
			else if(token.IsAggregator() || token.IsInstanceLimiter()) ReadOtherRule(status);
			else throw new InvalidDataException($"Syntax error: Unexpected {token}.");
		}
		private void ReadBasicRule(TBaseStatus status){
			string comparisonOperator = null, comparisonValue = null, verb = null, otherStatus = null, fedValue = null;
			if(TryConsume(StatusParser.IsComparisonOperator, ref comparisonOperator)) {
				ConsumeOrError(StatusParser.IsNumber, ref comparisonValue);
			}
			ConsumeOrError(StatusParser.IsBasicVerb, ref verb);
			while(true) {
				ConsumeOrError(IsStatus, ref otherStatus);
				TryConsume(StatusParser.IsNumber, ref fedValue);
				SetRule(false, status, comparisonOperator, comparisonValue, verb, otherStatus, fedValue);
				if(!TryConsume(",")) {
					break;
				}
			}
		}
		private void ReadInvertedRule(TBaseStatus status){
			string comparisonOperator = null, comparisonValue = null, verb = null, otherStatus = null, fedValue = null;
			TryConsume(StatusParser.IsNumber, ref fedValue);
			ConsumeOrError(StatusParser.IsInvertedVerb, ref verb);
			ConsumeOrError("by");
			while(true) {
				ConsumeOrError(IsStatus, ref otherStatus);
				if(TryConsume(StatusParser.IsComparisonOperator, ref comparisonOperator)) {
					ConsumeOrError(StatusParser.IsNumber, ref comparisonValue);
				}
				SetRule(true, status, comparisonOperator, comparisonValue, verb, otherStatus, fedValue);
				if(!TryConsume(",")) {
					break;
				}
			}
		}
		private bool IsStatus(string token){
			if(token.IsNumber()) return false;
			TBaseStatus status;
			return TryParse(token, out status);
		}
		private void ReadOtherRule(TBaseStatus status) {
			string token = tokens[idx];
			switch(token) {
				case "total":
				case "max":
				case "bool":
					SetAggregator(status, token);
					break;
				case "single":
				case "multiple":
					SetInstanceLimit(status, token);
					break;
			}
			++idx;
		}
		private string GetBasicVerb(string invertedVerb) {
			switch(invertedVerb) {
				case "fed": return "feeds";
				case "suppressed": return "suppresses";
				case "prevented": return "prevents";
				case "cancelled":
				case "canceled": return "cancels";
				case "extended": return "extends";
				case "foiled": return "foils";
				default: return null;
			}
		}
		private void SetRule(bool inverted, TBaseStatus status, string comparisonOperatorString,
			string comparisonValueString, string verbString, string otherStatusString, string fedValueString)
		{
			TBaseStatus otherStatus;
			if(!TryParse(otherStatusString, out otherStatus)) throw new InvalidDataException($"Error: {otherStatusString} not recognized as any given type.");
			TBaseStatus sourceStatus, targetStatus;
			if(inverted) {
				sourceStatus = otherStatus;
				targetStatus = status;
				verbString = GetBasicVerb(verbString);
			}
			else {
				sourceStatus = status;
				targetStatus = otherStatus;
			}
			Func<int, bool> condition = GetCondition(comparisonOperatorString, comparisonValueString);
			if(verbString != "feeds") { // Only feeds can have fed values.
				if(fedValueString != null) throw new InvalidDataException($"Unexpected value with {verbString}.");
				if(verbString == "extends") {
					if(comparisonOperatorString != null || comparisonValueString != null) { // and no conditions for this one.
						throw new InvalidDataException($"Unexpected condition with {verbString}.");
					}
				}
			}
			try {
				switch(verbString) {
					case "foils":
						rules[sourceStatus].Foils(condition, targetStatus);
						break;
					case "cancels":
						rules[sourceStatus].Cancels(condition, targetStatus);
						break;
					case "extends":
						rules[sourceStatus].Extends(targetStatus);
						break;
					default:
						if(fedValueString == null) {
							switch(verbString) {
								case "feeds":
									rules[sourceStatus].Feeds(condition, targetStatus);
									break;
								case "suppresses":
									rules[sourceStatus].Suppresses(condition, targetStatus);
									break;
								case "prevents":
									rules[sourceStatus].Prevents(condition, targetStatus);
									break;
							}
						}
						else {
							int fedValue = int.Parse(fedValueString);
							switch(verbString) {
								case "feeds":
									rules[sourceStatus].Feeds(fedValue, condition, targetStatus);
									break;
								/*case "suppresses":
									rules[sourceStatus].Suppresses(fedValue, condition, targetStatus);
									break;
								case "prevents":
									rules[sourceStatus].Prevents(fedValue, condition, targetStatus);
									break;*/
							}
						}
						break;
				}
			}
			catch(ArgumentException e) {
				throw new InvalidDataException("Likely illegal condition. See inner exception.", e);
			}
		}
		private Func<int, bool> GetCondition(string opString, string valueString) {
			if(opString == null && valueString == null) return null;
			int value = int.Parse(valueString);
			switch(opString) {
				case "==": return i => i == value; //todo: should these be cached?
				case "!=": return i => i != value;
				case ">": return i => i > value;
				case "<": return i => i < value;
				case ">=": return i => i >= value;
				case "<=": return i => i <= value;
				default: return null;
			}
		}
	}
}
