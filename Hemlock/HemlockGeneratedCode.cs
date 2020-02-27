using System;
using System.IO;
using System.Collections.Generic;

namespace Hemlock {
	// This file contains a lot of generated code. Avert your eyes.
	//
	// If you're wondering whether this is all really necessary - it is, given the constraints I chose for this lib.
	// (If you still can't believe it, feel free to email me and we'll discuss it.)
	//
	// This file also contains the code used to generate the Hemlock code:
	/*public static class HemlockCodeGenerators {
		public static List<string> GenerateHemlockCode(int numParams, List<string> list = null, string filename = null) {
			if(list == null) list = new List<string>();
			GenerateStatusSystemCode(numParams, list, null);
			GenerateStatusTrackerCode(numParams, list, null);
			WriteToFileIfRequested(list, filename);
			return list;
		}
		public static List<string> GenerateStatusSystemCode(int numParams, List<string> list = null, string filename = null) {
			if(list == null) list = new List<string>();
			for(int i=0;i<numParams;++i) {
				GenerateSingleStatusSystem(i+1, list, null);
				list.Add("");
			}
			WriteToFileIfRequested(list, filename);
			return list;
		}
		public static List<string> GenerateSingleStatusSystem(int num, List<string> list = null, string filename = null) {
			if(list == null) list = new List<string>();
			list.Add($@"	public class StatusSystem<TObject{TStatusString(num)}> : StatusSystem<TObject{TStatusString(num - 1)}>{WhereStructString(num)}");
			list.Add($@"	{{");
			list.Add($@"		public StatusSystem() {{ AddConversionCheck<{SingleTStatusString(num)}>(); }}");
			list.Add($@"		/// <summary>Define a new rule for the given status</summary>");
			list.Add($@"		public StatusRules this[{SingleTStatusString(num)} status] => new StatusRules(this, Convert(status));");
			list.Add($@"		/// <summary>");
			list.Add($@"		/// Create a status tracker that'll use the rules already defined.");
			list.Add($@"		/// Rules will be checked for errors when the first tracker is created (unless IgnoreRuleErrors is set to true).");
			list.Add($@"		/// </summary>");
			list.Add($@"		/// <param name=""obj"">The object associated with the newly created tracker (i.e., the tracker's ""owner"")</param>");
			list.Add($@"		new public StatusTracker<TObject{TStatusString(num)}> CreateStatusTracker(TObject obj) => new StatusTracker<TObject{TStatusString(num)}>(obj, this);");
			list.Add($@"	}}");
			WriteToFileIfRequested(list, filename);
			return list;
		}
		public static List<string> GenerateStatusTrackerCode(int numParams, List<string> list = null, string filename = null) {
			if(list == null) list = new List<string>();
			for(int i=0;i<numParams;++i) {
				GenerateSingleStatusTracker(i+1, list, null);
				list.Add("");
			}
			WriteToFileIfRequested(list, filename);
			return list;
		}
		public static List<string> GenerateSingleStatusTracker(int num, List<string> list = null, string filename = null) {
			if(list == null) list = new List<string>();
			list.Add($@"	public class StatusTracker<TObject{TStatusString(num)}> : StatusTracker<TObject{TStatusString(num - 1)}>{WhereStructString(num)}");
			list.Add($@"	{{");
			list.Add($@"		internal StatusTracker(TObject obj, StatusSystem<TObject> rules) : base(obj, rules) {{ }}");
			list.Add($@"		/// <summary>");
			list.Add($@"		/// Retrieve the current int value of the given status.");
			list.Add($@"		/// The status's value can also be directly set, but only if the SingleInstance property is true for this status.");
			list.Add($@"		/// </summary>");
			list.Add($@"		public int this[{SingleTStatusString(num)} status] {{");
			list.Add($@"			get {{ return this[Convert(status)]; }}");
			list.Add($@"			set {{ this[Convert(status)] = value; }}");
			list.Add($@"		}}");
			list.Add($@"	}}");
			WriteToFileIfRequested(list, filename);
			return list;
		}
		public static string SingleTStatusString(int num) => "TStatus" + num.ToString();
		public static string TStatusString(int maxNum) {
			if(maxNum == 0) return "";
			return ", " + string.Join(", ", TList(maxNum));
		}
		public static string[] TList(int count) {
			var result = new string[count];
			for(int i = 0;i<count;++i) result[i] = SingleTStatusString(i+1);
			return result;
		}
		public static string WhereStructString(int count) {
			string result = "";
			for(int i = 0;i<count;++i) result += $" where {SingleTStatusString(i+1)} : struct";
			return result;
		}
		private static void WriteToFileIfRequested(List<string> list, string filename) {
			if(filename == null) return;
			if(File.Exists(filename)) {
				throw new InvalidOperationException($"File '{filename}' already exists.");
			}
			using(StreamWriter file = new StreamWriter(filename)) {
				foreach(string s in list) file.WriteLine(s);
			}
		}
	}*/

	// Now the generated classes:  (You've been warned.)

	public class StatusSystem<TObject, TStatus1> : StatusSystem<TObject> where TStatus1 : struct {
		public StatusSystem() { AddConversionCheck<TStatus1>(); }
		/// <summary>Define a new rule for the given status</summary>
		public StatusRules this[TStatus1 status] => new StatusRules(this, Convert(status));
		/// <summary>
		/// Create a status tracker that'll use the rules already defined.
		/// Rules will be checked for errors when the first tracker is created (unless IgnoreRuleErrors is set to true).
		/// </summary>
		/// <param name="obj">The object associated with the newly created tracker (i.e., the tracker's "owner")</param>
		new public StatusTracker<TObject, TStatus1> CreateStatusTracker(TObject obj) => new StatusTracker<TObject, TStatus1>(obj, this);
	}

	public class StatusSystem<TObject, TStatus1, TStatus2> : StatusSystem<TObject, TStatus1> where TStatus1 : struct where TStatus2 : struct {
		public StatusSystem() { AddConversionCheck<TStatus2>(); }
		/// <summary>Define a new rule for the given status</summary>
		public StatusRules this[TStatus2 status] => new StatusRules(this, Convert(status));
		/// <summary>
		/// Create a status tracker that'll use the rules already defined.
		/// Rules will be checked for errors when the first tracker is created (unless IgnoreRuleErrors is set to true).
		/// </summary>
		/// <param name="obj">The object associated with the newly created tracker (i.e., the tracker's "owner")</param>
		new public StatusTracker<TObject, TStatus1, TStatus2> CreateStatusTracker(TObject obj) => new StatusTracker<TObject, TStatus1, TStatus2>(obj, this);
	}

	public class StatusSystem<TObject, TStatus1, TStatus2, TStatus3> : StatusSystem<TObject, TStatus1, TStatus2> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct {
		public StatusSystem() { AddConversionCheck<TStatus3>(); }
		/// <summary>Define a new rule for the given status</summary>
		public StatusRules this[TStatus3 status] => new StatusRules(this, Convert(status));
		/// <summary>
		/// Create a status tracker that'll use the rules already defined.
		/// Rules will be checked for errors when the first tracker is created (unless IgnoreRuleErrors is set to true).
		/// </summary>
		/// <param name="obj">The object associated with the newly created tracker (i.e., the tracker's "owner")</param>
		new public StatusTracker<TObject, TStatus1, TStatus2, TStatus3> CreateStatusTracker(TObject obj) => new StatusTracker<TObject, TStatus1, TStatus2, TStatus3>(obj, this);
	}

	public class StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4> : StatusSystem<TObject, TStatus1, TStatus2, TStatus3> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct {
		public StatusSystem() { AddConversionCheck<TStatus4>(); }
		/// <summary>Define a new rule for the given status</summary>
		public StatusRules this[TStatus4 status] => new StatusRules(this, Convert(status));
		/// <summary>
		/// Create a status tracker that'll use the rules already defined.
		/// Rules will be checked for errors when the first tracker is created (unless IgnoreRuleErrors is set to true).
		/// </summary>
		/// <param name="obj">The object associated with the newly created tracker (i.e., the tracker's "owner")</param>
		new public StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4> CreateStatusTracker(TObject obj) => new StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4>(obj, this);
	}

	public class StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5> : StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct {
		public StatusSystem() { AddConversionCheck<TStatus5>(); }
		/// <summary>Define a new rule for the given status</summary>
		public StatusRules this[TStatus5 status] => new StatusRules(this, Convert(status));
		/// <summary>
		/// Create a status tracker that'll use the rules already defined.
		/// Rules will be checked for errors when the first tracker is created (unless IgnoreRuleErrors is set to true).
		/// </summary>
		/// <param name="obj">The object associated with the newly created tracker (i.e., the tracker's "owner")</param>
		new public StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5> CreateStatusTracker(TObject obj) => new StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5>(obj, this);
	}

	public class StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6> : StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct {
		public StatusSystem() { AddConversionCheck<TStatus6>(); }
		/// <summary>Define a new rule for the given status</summary>
		public StatusRules this[TStatus6 status] => new StatusRules(this, Convert(status));
		/// <summary>
		/// Create a status tracker that'll use the rules already defined.
		/// Rules will be checked for errors when the first tracker is created (unless IgnoreRuleErrors is set to true).
		/// </summary>
		/// <param name="obj">The object associated with the newly created tracker (i.e., the tracker's "owner")</param>
		new public StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6> CreateStatusTracker(TObject obj) => new StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6>(obj, this);
	}

	public class StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7> : StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct {
		public StatusSystem() { AddConversionCheck<TStatus7>(); }
		/// <summary>Define a new rule for the given status</summary>
		public StatusRules this[TStatus7 status] => new StatusRules(this, Convert(status));
		/// <summary>
		/// Create a status tracker that'll use the rules already defined.
		/// Rules will be checked for errors when the first tracker is created (unless IgnoreRuleErrors is set to true).
		/// </summary>
		/// <param name="obj">The object associated with the newly created tracker (i.e., the tracker's "owner")</param>
		new public StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7> CreateStatusTracker(TObject obj) => new StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7>(obj, this);
	}

	public class StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8> : StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct {
		public StatusSystem() { AddConversionCheck<TStatus8>(); }
		/// <summary>Define a new rule for the given status</summary>
		public StatusRules this[TStatus8 status] => new StatusRules(this, Convert(status));
		/// <summary>
		/// Create a status tracker that'll use the rules already defined.
		/// Rules will be checked for errors when the first tracker is created (unless IgnoreRuleErrors is set to true).
		/// </summary>
		/// <param name="obj">The object associated with the newly created tracker (i.e., the tracker's "owner")</param>
		new public StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8> CreateStatusTracker(TObject obj) => new StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8>(obj, this);
	}

	public class StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9> : StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct {
		public StatusSystem() { AddConversionCheck<TStatus9>(); }
		/// <summary>Define a new rule for the given status</summary>
		public StatusRules this[TStatus9 status] => new StatusRules(this, Convert(status));
		/// <summary>
		/// Create a status tracker that'll use the rules already defined.
		/// Rules will be checked for errors when the first tracker is created (unless IgnoreRuleErrors is set to true).
		/// </summary>
		/// <param name="obj">The object associated with the newly created tracker (i.e., the tracker's "owner")</param>
		new public StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9> CreateStatusTracker(TObject obj) => new StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9>(obj, this);
	}

	public class StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10> : StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct {
		public StatusSystem() { AddConversionCheck<TStatus10>(); }
		/// <summary>Define a new rule for the given status</summary>
		public StatusRules this[TStatus10 status] => new StatusRules(this, Convert(status));
		/// <summary>
		/// Create a status tracker that'll use the rules already defined.
		/// Rules will be checked for errors when the first tracker is created (unless IgnoreRuleErrors is set to true).
		/// </summary>
		/// <param name="obj">The object associated with the newly created tracker (i.e., the tracker's "owner")</param>
		new public StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10> CreateStatusTracker(TObject obj) => new StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10>(obj, this);
	}

	public class StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11> : StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct {
		public StatusSystem() { AddConversionCheck<TStatus11>(); }
		/// <summary>Define a new rule for the given status</summary>
		public StatusRules this[TStatus11 status] => new StatusRules(this, Convert(status));
		/// <summary>
		/// Create a status tracker that'll use the rules already defined.
		/// Rules will be checked for errors when the first tracker is created (unless IgnoreRuleErrors is set to true).
		/// </summary>
		/// <param name="obj">The object associated with the newly created tracker (i.e., the tracker's "owner")</param>
		new public StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11> CreateStatusTracker(TObject obj) => new StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11>(obj, this);
	}

	public class StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12> : StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct {
		public StatusSystem() { AddConversionCheck<TStatus12>(); }
		/// <summary>Define a new rule for the given status</summary>
		public StatusRules this[TStatus12 status] => new StatusRules(this, Convert(status));
		/// <summary>
		/// Create a status tracker that'll use the rules already defined.
		/// Rules will be checked for errors when the first tracker is created (unless IgnoreRuleErrors is set to true).
		/// </summary>
		/// <param name="obj">The object associated with the newly created tracker (i.e., the tracker's "owner")</param>
		new public StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12> CreateStatusTracker(TObject obj) => new StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12>(obj, this);
	}

	public class StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13> : StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct {
		public StatusSystem() { AddConversionCheck<TStatus13>(); }
		/// <summary>Define a new rule for the given status</summary>
		public StatusRules this[TStatus13 status] => new StatusRules(this, Convert(status));
		/// <summary>
		/// Create a status tracker that'll use the rules already defined.
		/// Rules will be checked for errors when the first tracker is created (unless IgnoreRuleErrors is set to true).
		/// </summary>
		/// <param name="obj">The object associated with the newly created tracker (i.e., the tracker's "owner")</param>
		new public StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13> CreateStatusTracker(TObject obj) => new StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13>(obj, this);
	}

	public class StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14> : StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct {
		public StatusSystem() { AddConversionCheck<TStatus14>(); }
		/// <summary>Define a new rule for the given status</summary>
		public StatusRules this[TStatus14 status] => new StatusRules(this, Convert(status));
		/// <summary>
		/// Create a status tracker that'll use the rules already defined.
		/// Rules will be checked for errors when the first tracker is created (unless IgnoreRuleErrors is set to true).
		/// </summary>
		/// <param name="obj">The object associated with the newly created tracker (i.e., the tracker's "owner")</param>
		new public StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14> CreateStatusTracker(TObject obj) => new StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14>(obj, this);
	}

	public class StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15> : StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct {
		public StatusSystem() { AddConversionCheck<TStatus15>(); }
		/// <summary>Define a new rule for the given status</summary>
		public StatusRules this[TStatus15 status] => new StatusRules(this, Convert(status));
		/// <summary>
		/// Create a status tracker that'll use the rules already defined.
		/// Rules will be checked for errors when the first tracker is created (unless IgnoreRuleErrors is set to true).
		/// </summary>
		/// <param name="obj">The object associated with the newly created tracker (i.e., the tracker's "owner")</param>
		new public StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15> CreateStatusTracker(TObject obj) => new StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15>(obj, this);
	}

	public class StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16> : StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct {
		public StatusSystem() { AddConversionCheck<TStatus16>(); }
		/// <summary>Define a new rule for the given status</summary>
		public StatusRules this[TStatus16 status] => new StatusRules(this, Convert(status));
		/// <summary>
		/// Create a status tracker that'll use the rules already defined.
		/// Rules will be checked for errors when the first tracker is created (unless IgnoreRuleErrors is set to true).
		/// </summary>
		/// <param name="obj">The object associated with the newly created tracker (i.e., the tracker's "owner")</param>
		new public StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16> CreateStatusTracker(TObject obj) => new StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16>(obj, this);
	}

	public class StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17> : StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct {
		public StatusSystem() { AddConversionCheck<TStatus17>(); }
		/// <summary>Define a new rule for the given status</summary>
		public StatusRules this[TStatus17 status] => new StatusRules(this, Convert(status));
		/// <summary>
		/// Create a status tracker that'll use the rules already defined.
		/// Rules will be checked for errors when the first tracker is created (unless IgnoreRuleErrors is set to true).
		/// </summary>
		/// <param name="obj">The object associated with the newly created tracker (i.e., the tracker's "owner")</param>
		new public StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17> CreateStatusTracker(TObject obj) => new StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17>(obj, this);
	}

	public class StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18> : StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct {
		public StatusSystem() { AddConversionCheck<TStatus18>(); }
		/// <summary>Define a new rule for the given status</summary>
		public StatusRules this[TStatus18 status] => new StatusRules(this, Convert(status));
		/// <summary>
		/// Create a status tracker that'll use the rules already defined.
		/// Rules will be checked for errors when the first tracker is created (unless IgnoreRuleErrors is set to true).
		/// </summary>
		/// <param name="obj">The object associated with the newly created tracker (i.e., the tracker's "owner")</param>
		new public StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18> CreateStatusTracker(TObject obj) => new StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18>(obj, this);
	}

	public class StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19> : StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct {
		public StatusSystem() { AddConversionCheck<TStatus19>(); }
		/// <summary>Define a new rule for the given status</summary>
		public StatusRules this[TStatus19 status] => new StatusRules(this, Convert(status));
		/// <summary>
		/// Create a status tracker that'll use the rules already defined.
		/// Rules will be checked for errors when the first tracker is created (unless IgnoreRuleErrors is set to true).
		/// </summary>
		/// <param name="obj">The object associated with the newly created tracker (i.e., the tracker's "owner")</param>
		new public StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19> CreateStatusTracker(TObject obj) => new StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19>(obj, this);
	}

	public class StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20> : StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct {
		public StatusSystem() { AddConversionCheck<TStatus20>(); }
		/// <summary>Define a new rule for the given status</summary>
		public StatusRules this[TStatus20 status] => new StatusRules(this, Convert(status));
		/// <summary>
		/// Create a status tracker that'll use the rules already defined.
		/// Rules will be checked for errors when the first tracker is created (unless IgnoreRuleErrors is set to true).
		/// </summary>
		/// <param name="obj">The object associated with the newly created tracker (i.e., the tracker's "owner")</param>
		new public StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20> CreateStatusTracker(TObject obj) => new StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20>(obj, this);
	}

	public class StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21> : StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct {
		public StatusSystem() { AddConversionCheck<TStatus21>(); }
		/// <summary>Define a new rule for the given status</summary>
		public StatusRules this[TStatus21 status] => new StatusRules(this, Convert(status));
		/// <summary>
		/// Create a status tracker that'll use the rules already defined.
		/// Rules will be checked for errors when the first tracker is created (unless IgnoreRuleErrors is set to true).
		/// </summary>
		/// <param name="obj">The object associated with the newly created tracker (i.e., the tracker's "owner")</param>
		new public StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21> CreateStatusTracker(TObject obj) => new StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21>(obj, this);
	}

	public class StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22> : StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct {
		public StatusSystem() { AddConversionCheck<TStatus22>(); }
		/// <summary>Define a new rule for the given status</summary>
		public StatusRules this[TStatus22 status] => new StatusRules(this, Convert(status));
		/// <summary>
		/// Create a status tracker that'll use the rules already defined.
		/// Rules will be checked for errors when the first tracker is created (unless IgnoreRuleErrors is set to true).
		/// </summary>
		/// <param name="obj">The object associated with the newly created tracker (i.e., the tracker's "owner")</param>
		new public StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22> CreateStatusTracker(TObject obj) => new StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22>(obj, this);
	}

	public class StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23> : StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct {
		public StatusSystem() { AddConversionCheck<TStatus23>(); }
		/// <summary>Define a new rule for the given status</summary>
		public StatusRules this[TStatus23 status] => new StatusRules(this, Convert(status));
		/// <summary>
		/// Create a status tracker that'll use the rules already defined.
		/// Rules will be checked for errors when the first tracker is created (unless IgnoreRuleErrors is set to true).
		/// </summary>
		/// <param name="obj">The object associated with the newly created tracker (i.e., the tracker's "owner")</param>
		new public StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23> CreateStatusTracker(TObject obj) => new StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23>(obj, this);
	}

	public class StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24> : StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct where TStatus24 : struct {
		public StatusSystem() { AddConversionCheck<TStatus24>(); }
		/// <summary>Define a new rule for the given status</summary>
		public StatusRules this[TStatus24 status] => new StatusRules(this, Convert(status));
		/// <summary>
		/// Create a status tracker that'll use the rules already defined.
		/// Rules will be checked for errors when the first tracker is created (unless IgnoreRuleErrors is set to true).
		/// </summary>
		/// <param name="obj">The object associated with the newly created tracker (i.e., the tracker's "owner")</param>
		new public StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24> CreateStatusTracker(TObject obj) => new StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24>(obj, this);
	}

	public class StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25> : StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct where TStatus24 : struct where TStatus25 : struct {
		public StatusSystem() { AddConversionCheck<TStatus25>(); }
		/// <summary>Define a new rule for the given status</summary>
		public StatusRules this[TStatus25 status] => new StatusRules(this, Convert(status));
		/// <summary>
		/// Create a status tracker that'll use the rules already defined.
		/// Rules will be checked for errors when the first tracker is created (unless IgnoreRuleErrors is set to true).
		/// </summary>
		/// <param name="obj">The object associated with the newly created tracker (i.e., the tracker's "owner")</param>
		new public StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25> CreateStatusTracker(TObject obj) => new StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25>(obj, this);
	}

	public class StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26> : StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct where TStatus24 : struct where TStatus25 : struct where TStatus26 : struct {
		public StatusSystem() { AddConversionCheck<TStatus26>(); }
		/// <summary>Define a new rule for the given status</summary>
		public StatusRules this[TStatus26 status] => new StatusRules(this, Convert(status));
		/// <summary>
		/// Create a status tracker that'll use the rules already defined.
		/// Rules will be checked for errors when the first tracker is created (unless IgnoreRuleErrors is set to true).
		/// </summary>
		/// <param name="obj">The object associated with the newly created tracker (i.e., the tracker's "owner")</param>
		new public StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26> CreateStatusTracker(TObject obj) => new StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26>(obj, this);
	}

	public class StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27> : StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct where TStatus24 : struct where TStatus25 : struct where TStatus26 : struct where TStatus27 : struct {
		public StatusSystem() { AddConversionCheck<TStatus27>(); }
		/// <summary>Define a new rule for the given status</summary>
		public StatusRules this[TStatus27 status] => new StatusRules(this, Convert(status));
		/// <summary>
		/// Create a status tracker that'll use the rules already defined.
		/// Rules will be checked for errors when the first tracker is created (unless IgnoreRuleErrors is set to true).
		/// </summary>
		/// <param name="obj">The object associated with the newly created tracker (i.e., the tracker's "owner")</param>
		new public StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27> CreateStatusTracker(TObject obj) => new StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27>(obj, this);
	}

	public class StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28> : StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct where TStatus24 : struct where TStatus25 : struct where TStatus26 : struct where TStatus27 : struct where TStatus28 : struct {
		public StatusSystem() { AddConversionCheck<TStatus28>(); }
		/// <summary>Define a new rule for the given status</summary>
		public StatusRules this[TStatus28 status] => new StatusRules(this, Convert(status));
		/// <summary>
		/// Create a status tracker that'll use the rules already defined.
		/// Rules will be checked for errors when the first tracker is created (unless IgnoreRuleErrors is set to true).
		/// </summary>
		/// <param name="obj">The object associated with the newly created tracker (i.e., the tracker's "owner")</param>
		new public StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28> CreateStatusTracker(TObject obj) => new StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28>(obj, this);
	}

	public class StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29> : StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct where TStatus24 : struct where TStatus25 : struct where TStatus26 : struct where TStatus27 : struct where TStatus28 : struct where TStatus29 : struct {
		public StatusSystem() { AddConversionCheck<TStatus29>(); }
		/// <summary>Define a new rule for the given status</summary>
		public StatusRules this[TStatus29 status] => new StatusRules(this, Convert(status));
		/// <summary>
		/// Create a status tracker that'll use the rules already defined.
		/// Rules will be checked for errors when the first tracker is created (unless IgnoreRuleErrors is set to true).
		/// </summary>
		/// <param name="obj">The object associated with the newly created tracker (i.e., the tracker's "owner")</param>
		new public StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29> CreateStatusTracker(TObject obj) => new StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29>(obj, this);
	}

	public class StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30> : StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct where TStatus24 : struct where TStatus25 : struct where TStatus26 : struct where TStatus27 : struct where TStatus28 : struct where TStatus29 : struct where TStatus30 : struct {
		public StatusSystem() { AddConversionCheck<TStatus30>(); }
		/// <summary>Define a new rule for the given status</summary>
		public StatusRules this[TStatus30 status] => new StatusRules(this, Convert(status));
		/// <summary>
		/// Create a status tracker that'll use the rules already defined.
		/// Rules will be checked for errors when the first tracker is created (unless IgnoreRuleErrors is set to true).
		/// </summary>
		/// <param name="obj">The object associated with the newly created tracker (i.e., the tracker's "owner")</param>
		new public StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30> CreateStatusTracker(TObject obj) => new StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30>(obj, this);
	}

	public class StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31> : StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct where TStatus24 : struct where TStatus25 : struct where TStatus26 : struct where TStatus27 : struct where TStatus28 : struct where TStatus29 : struct where TStatus30 : struct where TStatus31 : struct {
		public StatusSystem() { AddConversionCheck<TStatus31>(); }
		/// <summary>Define a new rule for the given status</summary>
		public StatusRules this[TStatus31 status] => new StatusRules(this, Convert(status));
		/// <summary>
		/// Create a status tracker that'll use the rules already defined.
		/// Rules will be checked for errors when the first tracker is created (unless IgnoreRuleErrors is set to true).
		/// </summary>
		/// <param name="obj">The object associated with the newly created tracker (i.e., the tracker's "owner")</param>
		new public StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31> CreateStatusTracker(TObject obj) => new StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31>(obj, this);
	}

	public class StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32> : StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct where TStatus24 : struct where TStatus25 : struct where TStatus26 : struct where TStatus27 : struct where TStatus28 : struct where TStatus29 : struct where TStatus30 : struct where TStatus31 : struct where TStatus32 : struct {
		public StatusSystem() { AddConversionCheck<TStatus32>(); }
		/// <summary>Define a new rule for the given status</summary>
		public StatusRules this[TStatus32 status] => new StatusRules(this, Convert(status));
		/// <summary>
		/// Create a status tracker that'll use the rules already defined.
		/// Rules will be checked for errors when the first tracker is created (unless IgnoreRuleErrors is set to true).
		/// </summary>
		/// <param name="obj">The object associated with the newly created tracker (i.e., the tracker's "owner")</param>
		new public StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32> CreateStatusTracker(TObject obj) => new StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32>(obj, this);
	}

	public class StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33> : StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct where TStatus24 : struct where TStatus25 : struct where TStatus26 : struct where TStatus27 : struct where TStatus28 : struct where TStatus29 : struct where TStatus30 : struct where TStatus31 : struct where TStatus32 : struct where TStatus33 : struct {
		public StatusSystem() { AddConversionCheck<TStatus33>(); }
		/// <summary>Define a new rule for the given status</summary>
		public StatusRules this[TStatus33 status] => new StatusRules(this, Convert(status));
		/// <summary>
		/// Create a status tracker that'll use the rules already defined.
		/// Rules will be checked for errors when the first tracker is created (unless IgnoreRuleErrors is set to true).
		/// </summary>
		/// <param name="obj">The object associated with the newly created tracker (i.e., the tracker's "owner")</param>
		new public StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33> CreateStatusTracker(TObject obj) => new StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33>(obj, this);
	}

	public class StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34> : StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct where TStatus24 : struct where TStatus25 : struct where TStatus26 : struct where TStatus27 : struct where TStatus28 : struct where TStatus29 : struct where TStatus30 : struct where TStatus31 : struct where TStatus32 : struct where TStatus33 : struct where TStatus34 : struct {
		public StatusSystem() { AddConversionCheck<TStatus34>(); }
		/// <summary>Define a new rule for the given status</summary>
		public StatusRules this[TStatus34 status] => new StatusRules(this, Convert(status));
		/// <summary>
		/// Create a status tracker that'll use the rules already defined.
		/// Rules will be checked for errors when the first tracker is created (unless IgnoreRuleErrors is set to true).
		/// </summary>
		/// <param name="obj">The object associated with the newly created tracker (i.e., the tracker's "owner")</param>
		new public StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34> CreateStatusTracker(TObject obj) => new StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34>(obj, this);
	}

	public class StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35> : StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct where TStatus24 : struct where TStatus25 : struct where TStatus26 : struct where TStatus27 : struct where TStatus28 : struct where TStatus29 : struct where TStatus30 : struct where TStatus31 : struct where TStatus32 : struct where TStatus33 : struct where TStatus34 : struct where TStatus35 : struct {
		public StatusSystem() { AddConversionCheck<TStatus35>(); }
		/// <summary>Define a new rule for the given status</summary>
		public StatusRules this[TStatus35 status] => new StatusRules(this, Convert(status));
		/// <summary>
		/// Create a status tracker that'll use the rules already defined.
		/// Rules will be checked for errors when the first tracker is created (unless IgnoreRuleErrors is set to true).
		/// </summary>
		/// <param name="obj">The object associated with the newly created tracker (i.e., the tracker's "owner")</param>
		new public StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35> CreateStatusTracker(TObject obj) => new StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35>(obj, this);
	}

	public class StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36> : StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct where TStatus24 : struct where TStatus25 : struct where TStatus26 : struct where TStatus27 : struct where TStatus28 : struct where TStatus29 : struct where TStatus30 : struct where TStatus31 : struct where TStatus32 : struct where TStatus33 : struct where TStatus34 : struct where TStatus35 : struct where TStatus36 : struct {
		public StatusSystem() { AddConversionCheck<TStatus36>(); }
		/// <summary>Define a new rule for the given status</summary>
		public StatusRules this[TStatus36 status] => new StatusRules(this, Convert(status));
		/// <summary>
		/// Create a status tracker that'll use the rules already defined.
		/// Rules will be checked for errors when the first tracker is created (unless IgnoreRuleErrors is set to true).
		/// </summary>
		/// <param name="obj">The object associated with the newly created tracker (i.e., the tracker's "owner")</param>
		new public StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36> CreateStatusTracker(TObject obj) => new StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36>(obj, this);
	}

	public class StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37> : StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct where TStatus24 : struct where TStatus25 : struct where TStatus26 : struct where TStatus27 : struct where TStatus28 : struct where TStatus29 : struct where TStatus30 : struct where TStatus31 : struct where TStatus32 : struct where TStatus33 : struct where TStatus34 : struct where TStatus35 : struct where TStatus36 : struct where TStatus37 : struct {
		public StatusSystem() { AddConversionCheck<TStatus37>(); }
		/// <summary>Define a new rule for the given status</summary>
		public StatusRules this[TStatus37 status] => new StatusRules(this, Convert(status));
		/// <summary>
		/// Create a status tracker that'll use the rules already defined.
		/// Rules will be checked for errors when the first tracker is created (unless IgnoreRuleErrors is set to true).
		/// </summary>
		/// <param name="obj">The object associated with the newly created tracker (i.e., the tracker's "owner")</param>
		new public StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37> CreateStatusTracker(TObject obj) => new StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37>(obj, this);
	}

	public class StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38> : StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct where TStatus24 : struct where TStatus25 : struct where TStatus26 : struct where TStatus27 : struct where TStatus28 : struct where TStatus29 : struct where TStatus30 : struct where TStatus31 : struct where TStatus32 : struct where TStatus33 : struct where TStatus34 : struct where TStatus35 : struct where TStatus36 : struct where TStatus37 : struct where TStatus38 : struct {
		public StatusSystem() { AddConversionCheck<TStatus38>(); }
		/// <summary>Define a new rule for the given status</summary>
		public StatusRules this[TStatus38 status] => new StatusRules(this, Convert(status));
		/// <summary>
		/// Create a status tracker that'll use the rules already defined.
		/// Rules will be checked for errors when the first tracker is created (unless IgnoreRuleErrors is set to true).
		/// </summary>
		/// <param name="obj">The object associated with the newly created tracker (i.e., the tracker's "owner")</param>
		new public StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38> CreateStatusTracker(TObject obj) => new StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38>(obj, this);
	}

	public class StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39> : StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct where TStatus24 : struct where TStatus25 : struct where TStatus26 : struct where TStatus27 : struct where TStatus28 : struct where TStatus29 : struct where TStatus30 : struct where TStatus31 : struct where TStatus32 : struct where TStatus33 : struct where TStatus34 : struct where TStatus35 : struct where TStatus36 : struct where TStatus37 : struct where TStatus38 : struct where TStatus39 : struct {
		public StatusSystem() { AddConversionCheck<TStatus39>(); }
		/// <summary>Define a new rule for the given status</summary>
		public StatusRules this[TStatus39 status] => new StatusRules(this, Convert(status));
		/// <summary>
		/// Create a status tracker that'll use the rules already defined.
		/// Rules will be checked for errors when the first tracker is created (unless IgnoreRuleErrors is set to true).
		/// </summary>
		/// <param name="obj">The object associated with the newly created tracker (i.e., the tracker's "owner")</param>
		new public StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39> CreateStatusTracker(TObject obj) => new StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39>(obj, this);
	}

	public class StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40> : StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct where TStatus24 : struct where TStatus25 : struct where TStatus26 : struct where TStatus27 : struct where TStatus28 : struct where TStatus29 : struct where TStatus30 : struct where TStatus31 : struct where TStatus32 : struct where TStatus33 : struct where TStatus34 : struct where TStatus35 : struct where TStatus36 : struct where TStatus37 : struct where TStatus38 : struct where TStatus39 : struct where TStatus40 : struct {
		public StatusSystem() { AddConversionCheck<TStatus40>(); }
		/// <summary>Define a new rule for the given status</summary>
		public StatusRules this[TStatus40 status] => new StatusRules(this, Convert(status));
		/// <summary>
		/// Create a status tracker that'll use the rules already defined.
		/// Rules will be checked for errors when the first tracker is created (unless IgnoreRuleErrors is set to true).
		/// </summary>
		/// <param name="obj">The object associated with the newly created tracker (i.e., the tracker's "owner")</param>
		new public StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40> CreateStatusTracker(TObject obj) => new StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40>(obj, this);
	}

	public class StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41> : StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct where TStatus24 : struct where TStatus25 : struct where TStatus26 : struct where TStatus27 : struct where TStatus28 : struct where TStatus29 : struct where TStatus30 : struct where TStatus31 : struct where TStatus32 : struct where TStatus33 : struct where TStatus34 : struct where TStatus35 : struct where TStatus36 : struct where TStatus37 : struct where TStatus38 : struct where TStatus39 : struct where TStatus40 : struct where TStatus41 : struct {
		public StatusSystem() { AddConversionCheck<TStatus41>(); }
		/// <summary>Define a new rule for the given status</summary>
		public StatusRules this[TStatus41 status] => new StatusRules(this, Convert(status));
		/// <summary>
		/// Create a status tracker that'll use the rules already defined.
		/// Rules will be checked for errors when the first tracker is created (unless IgnoreRuleErrors is set to true).
		/// </summary>
		/// <param name="obj">The object associated with the newly created tracker (i.e., the tracker's "owner")</param>
		new public StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41> CreateStatusTracker(TObject obj) => new StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41>(obj, this);
	}

	public class StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42> : StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct where TStatus24 : struct where TStatus25 : struct where TStatus26 : struct where TStatus27 : struct where TStatus28 : struct where TStatus29 : struct where TStatus30 : struct where TStatus31 : struct where TStatus32 : struct where TStatus33 : struct where TStatus34 : struct where TStatus35 : struct where TStatus36 : struct where TStatus37 : struct where TStatus38 : struct where TStatus39 : struct where TStatus40 : struct where TStatus41 : struct where TStatus42 : struct {
		public StatusSystem() { AddConversionCheck<TStatus42>(); }
		/// <summary>Define a new rule for the given status</summary>
		public StatusRules this[TStatus42 status] => new StatusRules(this, Convert(status));
		/// <summary>
		/// Create a status tracker that'll use the rules already defined.
		/// Rules will be checked for errors when the first tracker is created (unless IgnoreRuleErrors is set to true).
		/// </summary>
		/// <param name="obj">The object associated with the newly created tracker (i.e., the tracker's "owner")</param>
		new public StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42> CreateStatusTracker(TObject obj) => new StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42>(obj, this);
	}

	public class StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43> : StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct where TStatus24 : struct where TStatus25 : struct where TStatus26 : struct where TStatus27 : struct where TStatus28 : struct where TStatus29 : struct where TStatus30 : struct where TStatus31 : struct where TStatus32 : struct where TStatus33 : struct where TStatus34 : struct where TStatus35 : struct where TStatus36 : struct where TStatus37 : struct where TStatus38 : struct where TStatus39 : struct where TStatus40 : struct where TStatus41 : struct where TStatus42 : struct where TStatus43 : struct {
		public StatusSystem() { AddConversionCheck<TStatus43>(); }
		/// <summary>Define a new rule for the given status</summary>
		public StatusRules this[TStatus43 status] => new StatusRules(this, Convert(status));
		/// <summary>
		/// Create a status tracker that'll use the rules already defined.
		/// Rules will be checked for errors when the first tracker is created (unless IgnoreRuleErrors is set to true).
		/// </summary>
		/// <param name="obj">The object associated with the newly created tracker (i.e., the tracker's "owner")</param>
		new public StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43> CreateStatusTracker(TObject obj) => new StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43>(obj, this);
	}

	public class StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44> : StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct where TStatus24 : struct where TStatus25 : struct where TStatus26 : struct where TStatus27 : struct where TStatus28 : struct where TStatus29 : struct where TStatus30 : struct where TStatus31 : struct where TStatus32 : struct where TStatus33 : struct where TStatus34 : struct where TStatus35 : struct where TStatus36 : struct where TStatus37 : struct where TStatus38 : struct where TStatus39 : struct where TStatus40 : struct where TStatus41 : struct where TStatus42 : struct where TStatus43 : struct where TStatus44 : struct {
		public StatusSystem() { AddConversionCheck<TStatus44>(); }
		/// <summary>Define a new rule for the given status</summary>
		public StatusRules this[TStatus44 status] => new StatusRules(this, Convert(status));
		/// <summary>
		/// Create a status tracker that'll use the rules already defined.
		/// Rules will be checked for errors when the first tracker is created (unless IgnoreRuleErrors is set to true).
		/// </summary>
		/// <param name="obj">The object associated with the newly created tracker (i.e., the tracker's "owner")</param>
		new public StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44> CreateStatusTracker(TObject obj) => new StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44>(obj, this);
	}

	public class StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45> : StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct where TStatus24 : struct where TStatus25 : struct where TStatus26 : struct where TStatus27 : struct where TStatus28 : struct where TStatus29 : struct where TStatus30 : struct where TStatus31 : struct where TStatus32 : struct where TStatus33 : struct where TStatus34 : struct where TStatus35 : struct where TStatus36 : struct where TStatus37 : struct where TStatus38 : struct where TStatus39 : struct where TStatus40 : struct where TStatus41 : struct where TStatus42 : struct where TStatus43 : struct where TStatus44 : struct where TStatus45 : struct {
		public StatusSystem() { AddConversionCheck<TStatus45>(); }
		/// <summary>Define a new rule for the given status</summary>
		public StatusRules this[TStatus45 status] => new StatusRules(this, Convert(status));
		/// <summary>
		/// Create a status tracker that'll use the rules already defined.
		/// Rules will be checked for errors when the first tracker is created (unless IgnoreRuleErrors is set to true).
		/// </summary>
		/// <param name="obj">The object associated with the newly created tracker (i.e., the tracker's "owner")</param>
		new public StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45> CreateStatusTracker(TObject obj) => new StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45>(obj, this);
	}

	public class StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46> : StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct where TStatus24 : struct where TStatus25 : struct where TStatus26 : struct where TStatus27 : struct where TStatus28 : struct where TStatus29 : struct where TStatus30 : struct where TStatus31 : struct where TStatus32 : struct where TStatus33 : struct where TStatus34 : struct where TStatus35 : struct where TStatus36 : struct where TStatus37 : struct where TStatus38 : struct where TStatus39 : struct where TStatus40 : struct where TStatus41 : struct where TStatus42 : struct where TStatus43 : struct where TStatus44 : struct where TStatus45 : struct where TStatus46 : struct {
		public StatusSystem() { AddConversionCheck<TStatus46>(); }
		/// <summary>Define a new rule for the given status</summary>
		public StatusRules this[TStatus46 status] => new StatusRules(this, Convert(status));
		/// <summary>
		/// Create a status tracker that'll use the rules already defined.
		/// Rules will be checked for errors when the first tracker is created (unless IgnoreRuleErrors is set to true).
		/// </summary>
		/// <param name="obj">The object associated with the newly created tracker (i.e., the tracker's "owner")</param>
		new public StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46> CreateStatusTracker(TObject obj) => new StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46>(obj, this);
	}

	public class StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47> : StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct where TStatus24 : struct where TStatus25 : struct where TStatus26 : struct where TStatus27 : struct where TStatus28 : struct where TStatus29 : struct where TStatus30 : struct where TStatus31 : struct where TStatus32 : struct where TStatus33 : struct where TStatus34 : struct where TStatus35 : struct where TStatus36 : struct where TStatus37 : struct where TStatus38 : struct where TStatus39 : struct where TStatus40 : struct where TStatus41 : struct where TStatus42 : struct where TStatus43 : struct where TStatus44 : struct where TStatus45 : struct where TStatus46 : struct where TStatus47 : struct {
		public StatusSystem() { AddConversionCheck<TStatus47>(); }
		/// <summary>Define a new rule for the given status</summary>
		public StatusRules this[TStatus47 status] => new StatusRules(this, Convert(status));
		/// <summary>
		/// Create a status tracker that'll use the rules already defined.
		/// Rules will be checked for errors when the first tracker is created (unless IgnoreRuleErrors is set to true).
		/// </summary>
		/// <param name="obj">The object associated with the newly created tracker (i.e., the tracker's "owner")</param>
		new public StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47> CreateStatusTracker(TObject obj) => new StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47>(obj, this);
	}

	public class StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48> : StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct where TStatus24 : struct where TStatus25 : struct where TStatus26 : struct where TStatus27 : struct where TStatus28 : struct where TStatus29 : struct where TStatus30 : struct where TStatus31 : struct where TStatus32 : struct where TStatus33 : struct where TStatus34 : struct where TStatus35 : struct where TStatus36 : struct where TStatus37 : struct where TStatus38 : struct where TStatus39 : struct where TStatus40 : struct where TStatus41 : struct where TStatus42 : struct where TStatus43 : struct where TStatus44 : struct where TStatus45 : struct where TStatus46 : struct where TStatus47 : struct where TStatus48 : struct {
		public StatusSystem() { AddConversionCheck<TStatus48>(); }
		/// <summary>Define a new rule for the given status</summary>
		public StatusRules this[TStatus48 status] => new StatusRules(this, Convert(status));
		/// <summary>
		/// Create a status tracker that'll use the rules already defined.
		/// Rules will be checked for errors when the first tracker is created (unless IgnoreRuleErrors is set to true).
		/// </summary>
		/// <param name="obj">The object associated with the newly created tracker (i.e., the tracker's "owner")</param>
		new public StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48> CreateStatusTracker(TObject obj) => new StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48>(obj, this);
	}

	public class StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49> : StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct where TStatus24 : struct where TStatus25 : struct where TStatus26 : struct where TStatus27 : struct where TStatus28 : struct where TStatus29 : struct where TStatus30 : struct where TStatus31 : struct where TStatus32 : struct where TStatus33 : struct where TStatus34 : struct where TStatus35 : struct where TStatus36 : struct where TStatus37 : struct where TStatus38 : struct where TStatus39 : struct where TStatus40 : struct where TStatus41 : struct where TStatus42 : struct where TStatus43 : struct where TStatus44 : struct where TStatus45 : struct where TStatus46 : struct where TStatus47 : struct where TStatus48 : struct where TStatus49 : struct {
		public StatusSystem() { AddConversionCheck<TStatus49>(); }
		/// <summary>Define a new rule for the given status</summary>
		public StatusRules this[TStatus49 status] => new StatusRules(this, Convert(status));
		/// <summary>
		/// Create a status tracker that'll use the rules already defined.
		/// Rules will be checked for errors when the first tracker is created (unless IgnoreRuleErrors is set to true).
		/// </summary>
		/// <param name="obj">The object associated with the newly created tracker (i.e., the tracker's "owner")</param>
		new public StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49> CreateStatusTracker(TObject obj) => new StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49>(obj, this);
	}

	public class StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50> : StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct where TStatus24 : struct where TStatus25 : struct where TStatus26 : struct where TStatus27 : struct where TStatus28 : struct where TStatus29 : struct where TStatus30 : struct where TStatus31 : struct where TStatus32 : struct where TStatus33 : struct where TStatus34 : struct where TStatus35 : struct where TStatus36 : struct where TStatus37 : struct where TStatus38 : struct where TStatus39 : struct where TStatus40 : struct where TStatus41 : struct where TStatus42 : struct where TStatus43 : struct where TStatus44 : struct where TStatus45 : struct where TStatus46 : struct where TStatus47 : struct where TStatus48 : struct where TStatus49 : struct where TStatus50 : struct {
		public StatusSystem() { AddConversionCheck<TStatus50>(); }
		/// <summary>Define a new rule for the given status</summary>
		public StatusRules this[TStatus50 status] => new StatusRules(this, Convert(status));
		/// <summary>
		/// Create a status tracker that'll use the rules already defined.
		/// Rules will be checked for errors when the first tracker is created (unless IgnoreRuleErrors is set to true).
		/// </summary>
		/// <param name="obj">The object associated with the newly created tracker (i.e., the tracker's "owner")</param>
		new public StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50> CreateStatusTracker(TObject obj) => new StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50>(obj, this);
	}

	public class StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50, TStatus51> : StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct where TStatus24 : struct where TStatus25 : struct where TStatus26 : struct where TStatus27 : struct where TStatus28 : struct where TStatus29 : struct where TStatus30 : struct where TStatus31 : struct where TStatus32 : struct where TStatus33 : struct where TStatus34 : struct where TStatus35 : struct where TStatus36 : struct where TStatus37 : struct where TStatus38 : struct where TStatus39 : struct where TStatus40 : struct where TStatus41 : struct where TStatus42 : struct where TStatus43 : struct where TStatus44 : struct where TStatus45 : struct where TStatus46 : struct where TStatus47 : struct where TStatus48 : struct where TStatus49 : struct where TStatus50 : struct where TStatus51 : struct {
		public StatusSystem() { AddConversionCheck<TStatus51>(); }
		/// <summary>Define a new rule for the given status</summary>
		public StatusRules this[TStatus51 status] => new StatusRules(this, Convert(status));
		/// <summary>
		/// Create a status tracker that'll use the rules already defined.
		/// Rules will be checked for errors when the first tracker is created (unless IgnoreRuleErrors is set to true).
		/// </summary>
		/// <param name="obj">The object associated with the newly created tracker (i.e., the tracker's "owner")</param>
		new public StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50, TStatus51> CreateStatusTracker(TObject obj) => new StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50, TStatus51>(obj, this);
	}

	public class StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50, TStatus51, TStatus52> : StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50, TStatus51> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct where TStatus24 : struct where TStatus25 : struct where TStatus26 : struct where TStatus27 : struct where TStatus28 : struct where TStatus29 : struct where TStatus30 : struct where TStatus31 : struct where TStatus32 : struct where TStatus33 : struct where TStatus34 : struct where TStatus35 : struct where TStatus36 : struct where TStatus37 : struct where TStatus38 : struct where TStatus39 : struct where TStatus40 : struct where TStatus41 : struct where TStatus42 : struct where TStatus43 : struct where TStatus44 : struct where TStatus45 : struct where TStatus46 : struct where TStatus47 : struct where TStatus48 : struct where TStatus49 : struct where TStatus50 : struct where TStatus51 : struct where TStatus52 : struct {
		public StatusSystem() { AddConversionCheck<TStatus52>(); }
		/// <summary>Define a new rule for the given status</summary>
		public StatusRules this[TStatus52 status] => new StatusRules(this, Convert(status));
		/// <summary>
		/// Create a status tracker that'll use the rules already defined.
		/// Rules will be checked for errors when the first tracker is created (unless IgnoreRuleErrors is set to true).
		/// </summary>
		/// <param name="obj">The object associated with the newly created tracker (i.e., the tracker's "owner")</param>
		new public StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50, TStatus51, TStatus52> CreateStatusTracker(TObject obj) => new StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50, TStatus51, TStatus52>(obj, this);
	}

	public class StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50, TStatus51, TStatus52, TStatus53> : StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50, TStatus51, TStatus52> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct where TStatus24 : struct where TStatus25 : struct where TStatus26 : struct where TStatus27 : struct where TStatus28 : struct where TStatus29 : struct where TStatus30 : struct where TStatus31 : struct where TStatus32 : struct where TStatus33 : struct where TStatus34 : struct where TStatus35 : struct where TStatus36 : struct where TStatus37 : struct where TStatus38 : struct where TStatus39 : struct where TStatus40 : struct where TStatus41 : struct where TStatus42 : struct where TStatus43 : struct where TStatus44 : struct where TStatus45 : struct where TStatus46 : struct where TStatus47 : struct where TStatus48 : struct where TStatus49 : struct where TStatus50 : struct where TStatus51 : struct where TStatus52 : struct where TStatus53 : struct {
		public StatusSystem() { AddConversionCheck<TStatus53>(); }
		/// <summary>Define a new rule for the given status</summary>
		public StatusRules this[TStatus53 status] => new StatusRules(this, Convert(status));
		/// <summary>
		/// Create a status tracker that'll use the rules already defined.
		/// Rules will be checked for errors when the first tracker is created (unless IgnoreRuleErrors is set to true).
		/// </summary>
		/// <param name="obj">The object associated with the newly created tracker (i.e., the tracker's "owner")</param>
		new public StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50, TStatus51, TStatus52, TStatus53> CreateStatusTracker(TObject obj) => new StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50, TStatus51, TStatus52, TStatus53>(obj, this);
	}

	public class StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50, TStatus51, TStatus52, TStatus53, TStatus54> : StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50, TStatus51, TStatus52, TStatus53> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct where TStatus24 : struct where TStatus25 : struct where TStatus26 : struct where TStatus27 : struct where TStatus28 : struct where TStatus29 : struct where TStatus30 : struct where TStatus31 : struct where TStatus32 : struct where TStatus33 : struct where TStatus34 : struct where TStatus35 : struct where TStatus36 : struct where TStatus37 : struct where TStatus38 : struct where TStatus39 : struct where TStatus40 : struct where TStatus41 : struct where TStatus42 : struct where TStatus43 : struct where TStatus44 : struct where TStatus45 : struct where TStatus46 : struct where TStatus47 : struct where TStatus48 : struct where TStatus49 : struct where TStatus50 : struct where TStatus51 : struct where TStatus52 : struct where TStatus53 : struct where TStatus54 : struct {
		public StatusSystem() { AddConversionCheck<TStatus54>(); }
		/// <summary>Define a new rule for the given status</summary>
		public StatusRules this[TStatus54 status] => new StatusRules(this, Convert(status));
		/// <summary>
		/// Create a status tracker that'll use the rules already defined.
		/// Rules will be checked for errors when the first tracker is created (unless IgnoreRuleErrors is set to true).
		/// </summary>
		/// <param name="obj">The object associated with the newly created tracker (i.e., the tracker's "owner")</param>
		new public StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50, TStatus51, TStatus52, TStatus53, TStatus54> CreateStatusTracker(TObject obj) => new StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50, TStatus51, TStatus52, TStatus53, TStatus54>(obj, this);
	}

	public class StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50, TStatus51, TStatus52, TStatus53, TStatus54, TStatus55> : StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50, TStatus51, TStatus52, TStatus53, TStatus54> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct where TStatus24 : struct where TStatus25 : struct where TStatus26 : struct where TStatus27 : struct where TStatus28 : struct where TStatus29 : struct where TStatus30 : struct where TStatus31 : struct where TStatus32 : struct where TStatus33 : struct where TStatus34 : struct where TStatus35 : struct where TStatus36 : struct where TStatus37 : struct where TStatus38 : struct where TStatus39 : struct where TStatus40 : struct where TStatus41 : struct where TStatus42 : struct where TStatus43 : struct where TStatus44 : struct where TStatus45 : struct where TStatus46 : struct where TStatus47 : struct where TStatus48 : struct where TStatus49 : struct where TStatus50 : struct where TStatus51 : struct where TStatus52 : struct where TStatus53 : struct where TStatus54 : struct where TStatus55 : struct {
		public StatusSystem() { AddConversionCheck<TStatus55>(); }
		/// <summary>Define a new rule for the given status</summary>
		public StatusRules this[TStatus55 status] => new StatusRules(this, Convert(status));
		/// <summary>
		/// Create a status tracker that'll use the rules already defined.
		/// Rules will be checked for errors when the first tracker is created (unless IgnoreRuleErrors is set to true).
		/// </summary>
		/// <param name="obj">The object associated with the newly created tracker (i.e., the tracker's "owner")</param>
		new public StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50, TStatus51, TStatus52, TStatus53, TStatus54, TStatus55> CreateStatusTracker(TObject obj) => new StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50, TStatus51, TStatus52, TStatus53, TStatus54, TStatus55>(obj, this);
	}

	public class StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50, TStatus51, TStatus52, TStatus53, TStatus54, TStatus55, TStatus56> : StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50, TStatus51, TStatus52, TStatus53, TStatus54, TStatus55> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct where TStatus24 : struct where TStatus25 : struct where TStatus26 : struct where TStatus27 : struct where TStatus28 : struct where TStatus29 : struct where TStatus30 : struct where TStatus31 : struct where TStatus32 : struct where TStatus33 : struct where TStatus34 : struct where TStatus35 : struct where TStatus36 : struct where TStatus37 : struct where TStatus38 : struct where TStatus39 : struct where TStatus40 : struct where TStatus41 : struct where TStatus42 : struct where TStatus43 : struct where TStatus44 : struct where TStatus45 : struct where TStatus46 : struct where TStatus47 : struct where TStatus48 : struct where TStatus49 : struct where TStatus50 : struct where TStatus51 : struct where TStatus52 : struct where TStatus53 : struct where TStatus54 : struct where TStatus55 : struct where TStatus56 : struct {
		public StatusSystem() { AddConversionCheck<TStatus56>(); }
		/// <summary>Define a new rule for the given status</summary>
		public StatusRules this[TStatus56 status] => new StatusRules(this, Convert(status));
		/// <summary>
		/// Create a status tracker that'll use the rules already defined.
		/// Rules will be checked for errors when the first tracker is created (unless IgnoreRuleErrors is set to true).
		/// </summary>
		/// <param name="obj">The object associated with the newly created tracker (i.e., the tracker's "owner")</param>
		new public StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50, TStatus51, TStatus52, TStatus53, TStatus54, TStatus55, TStatus56> CreateStatusTracker(TObject obj) => new StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50, TStatus51, TStatus52, TStatus53, TStatus54, TStatus55, TStatus56>(obj, this);
	}

	public class StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50, TStatus51, TStatus52, TStatus53, TStatus54, TStatus55, TStatus56, TStatus57> : StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50, TStatus51, TStatus52, TStatus53, TStatus54, TStatus55, TStatus56> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct where TStatus24 : struct where TStatus25 : struct where TStatus26 : struct where TStatus27 : struct where TStatus28 : struct where TStatus29 : struct where TStatus30 : struct where TStatus31 : struct where TStatus32 : struct where TStatus33 : struct where TStatus34 : struct where TStatus35 : struct where TStatus36 : struct where TStatus37 : struct where TStatus38 : struct where TStatus39 : struct where TStatus40 : struct where TStatus41 : struct where TStatus42 : struct where TStatus43 : struct where TStatus44 : struct where TStatus45 : struct where TStatus46 : struct where TStatus47 : struct where TStatus48 : struct where TStatus49 : struct where TStatus50 : struct where TStatus51 : struct where TStatus52 : struct where TStatus53 : struct where TStatus54 : struct where TStatus55 : struct where TStatus56 : struct where TStatus57 : struct {
		public StatusSystem() { AddConversionCheck<TStatus57>(); }
		/// <summary>Define a new rule for the given status</summary>
		public StatusRules this[TStatus57 status] => new StatusRules(this, Convert(status));
		/// <summary>
		/// Create a status tracker that'll use the rules already defined.
		/// Rules will be checked for errors when the first tracker is created (unless IgnoreRuleErrors is set to true).
		/// </summary>
		/// <param name="obj">The object associated with the newly created tracker (i.e., the tracker's "owner")</param>
		new public StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50, TStatus51, TStatus52, TStatus53, TStatus54, TStatus55, TStatus56, TStatus57> CreateStatusTracker(TObject obj) => new StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50, TStatus51, TStatus52, TStatus53, TStatus54, TStatus55, TStatus56, TStatus57>(obj, this);
	}

	public class StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50, TStatus51, TStatus52, TStatus53, TStatus54, TStatus55, TStatus56, TStatus57, TStatus58> : StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50, TStatus51, TStatus52, TStatus53, TStatus54, TStatus55, TStatus56, TStatus57> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct where TStatus24 : struct where TStatus25 : struct where TStatus26 : struct where TStatus27 : struct where TStatus28 : struct where TStatus29 : struct where TStatus30 : struct where TStatus31 : struct where TStatus32 : struct where TStatus33 : struct where TStatus34 : struct where TStatus35 : struct where TStatus36 : struct where TStatus37 : struct where TStatus38 : struct where TStatus39 : struct where TStatus40 : struct where TStatus41 : struct where TStatus42 : struct where TStatus43 : struct where TStatus44 : struct where TStatus45 : struct where TStatus46 : struct where TStatus47 : struct where TStatus48 : struct where TStatus49 : struct where TStatus50 : struct where TStatus51 : struct where TStatus52 : struct where TStatus53 : struct where TStatus54 : struct where TStatus55 : struct where TStatus56 : struct where TStatus57 : struct where TStatus58 : struct {
		public StatusSystem() { AddConversionCheck<TStatus58>(); }
		/// <summary>Define a new rule for the given status</summary>
		public StatusRules this[TStatus58 status] => new StatusRules(this, Convert(status));
		/// <summary>
		/// Create a status tracker that'll use the rules already defined.
		/// Rules will be checked for errors when the first tracker is created (unless IgnoreRuleErrors is set to true).
		/// </summary>
		/// <param name="obj">The object associated with the newly created tracker (i.e., the tracker's "owner")</param>
		new public StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50, TStatus51, TStatus52, TStatus53, TStatus54, TStatus55, TStatus56, TStatus57, TStatus58> CreateStatusTracker(TObject obj) => new StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50, TStatus51, TStatus52, TStatus53, TStatus54, TStatus55, TStatus56, TStatus57, TStatus58>(obj, this);
	}

	public class StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50, TStatus51, TStatus52, TStatus53, TStatus54, TStatus55, TStatus56, TStatus57, TStatus58, TStatus59> : StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50, TStatus51, TStatus52, TStatus53, TStatus54, TStatus55, TStatus56, TStatus57, TStatus58> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct where TStatus24 : struct where TStatus25 : struct where TStatus26 : struct where TStatus27 : struct where TStatus28 : struct where TStatus29 : struct where TStatus30 : struct where TStatus31 : struct where TStatus32 : struct where TStatus33 : struct where TStatus34 : struct where TStatus35 : struct where TStatus36 : struct where TStatus37 : struct where TStatus38 : struct where TStatus39 : struct where TStatus40 : struct where TStatus41 : struct where TStatus42 : struct where TStatus43 : struct where TStatus44 : struct where TStatus45 : struct where TStatus46 : struct where TStatus47 : struct where TStatus48 : struct where TStatus49 : struct where TStatus50 : struct where TStatus51 : struct where TStatus52 : struct where TStatus53 : struct where TStatus54 : struct where TStatus55 : struct where TStatus56 : struct where TStatus57 : struct where TStatus58 : struct where TStatus59 : struct {
		public StatusSystem() { AddConversionCheck<TStatus59>(); }
		/// <summary>Define a new rule for the given status</summary>
		public StatusRules this[TStatus59 status] => new StatusRules(this, Convert(status));
		/// <summary>
		/// Create a status tracker that'll use the rules already defined.
		/// Rules will be checked for errors when the first tracker is created (unless IgnoreRuleErrors is set to true).
		/// </summary>
		/// <param name="obj">The object associated with the newly created tracker (i.e., the tracker's "owner")</param>
		new public StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50, TStatus51, TStatus52, TStatus53, TStatus54, TStatus55, TStatus56, TStatus57, TStatus58, TStatus59> CreateStatusTracker(TObject obj) => new StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50, TStatus51, TStatus52, TStatus53, TStatus54, TStatus55, TStatus56, TStatus57, TStatus58, TStatus59>(obj, this);
	}

	public class StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50, TStatus51, TStatus52, TStatus53, TStatus54, TStatus55, TStatus56, TStatus57, TStatus58, TStatus59, TStatus60> : StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50, TStatus51, TStatus52, TStatus53, TStatus54, TStatus55, TStatus56, TStatus57, TStatus58, TStatus59> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct where TStatus24 : struct where TStatus25 : struct where TStatus26 : struct where TStatus27 : struct where TStatus28 : struct where TStatus29 : struct where TStatus30 : struct where TStatus31 : struct where TStatus32 : struct where TStatus33 : struct where TStatus34 : struct where TStatus35 : struct where TStatus36 : struct where TStatus37 : struct where TStatus38 : struct where TStatus39 : struct where TStatus40 : struct where TStatus41 : struct where TStatus42 : struct where TStatus43 : struct where TStatus44 : struct where TStatus45 : struct where TStatus46 : struct where TStatus47 : struct where TStatus48 : struct where TStatus49 : struct where TStatus50 : struct where TStatus51 : struct where TStatus52 : struct where TStatus53 : struct where TStatus54 : struct where TStatus55 : struct where TStatus56 : struct where TStatus57 : struct where TStatus58 : struct where TStatus59 : struct where TStatus60 : struct {
		public StatusSystem() { AddConversionCheck<TStatus60>(); }
		/// <summary>Define a new rule for the given status</summary>
		public StatusRules this[TStatus60 status] => new StatusRules(this, Convert(status));
		/// <summary>
		/// Create a status tracker that'll use the rules already defined.
		/// Rules will be checked for errors when the first tracker is created (unless IgnoreRuleErrors is set to true).
		/// </summary>
		/// <param name="obj">The object associated with the newly created tracker (i.e., the tracker's "owner")</param>
		new public StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50, TStatus51, TStatus52, TStatus53, TStatus54, TStatus55, TStatus56, TStatus57, TStatus58, TStatus59, TStatus60> CreateStatusTracker(TObject obj) => new StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50, TStatus51, TStatus52, TStatus53, TStatus54, TStatus55, TStatus56, TStatus57, TStatus58, TStatus59, TStatus60>(obj, this);
	}

	public class StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50, TStatus51, TStatus52, TStatus53, TStatus54, TStatus55, TStatus56, TStatus57, TStatus58, TStatus59, TStatus60, TStatus61> : StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50, TStatus51, TStatus52, TStatus53, TStatus54, TStatus55, TStatus56, TStatus57, TStatus58, TStatus59, TStatus60> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct where TStatus24 : struct where TStatus25 : struct where TStatus26 : struct where TStatus27 : struct where TStatus28 : struct where TStatus29 : struct where TStatus30 : struct where TStatus31 : struct where TStatus32 : struct where TStatus33 : struct where TStatus34 : struct where TStatus35 : struct where TStatus36 : struct where TStatus37 : struct where TStatus38 : struct where TStatus39 : struct where TStatus40 : struct where TStatus41 : struct where TStatus42 : struct where TStatus43 : struct where TStatus44 : struct where TStatus45 : struct where TStatus46 : struct where TStatus47 : struct where TStatus48 : struct where TStatus49 : struct where TStatus50 : struct where TStatus51 : struct where TStatus52 : struct where TStatus53 : struct where TStatus54 : struct where TStatus55 : struct where TStatus56 : struct where TStatus57 : struct where TStatus58 : struct where TStatus59 : struct where TStatus60 : struct where TStatus61 : struct {
		public StatusSystem() { AddConversionCheck<TStatus61>(); }
		/// <summary>Define a new rule for the given status</summary>
		public StatusRules this[TStatus61 status] => new StatusRules(this, Convert(status));
		/// <summary>
		/// Create a status tracker that'll use the rules already defined.
		/// Rules will be checked for errors when the first tracker is created (unless IgnoreRuleErrors is set to true).
		/// </summary>
		/// <param name="obj">The object associated with the newly created tracker (i.e., the tracker's "owner")</param>
		new public StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50, TStatus51, TStatus52, TStatus53, TStatus54, TStatus55, TStatus56, TStatus57, TStatus58, TStatus59, TStatus60, TStatus61> CreateStatusTracker(TObject obj) => new StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50, TStatus51, TStatus52, TStatus53, TStatus54, TStatus55, TStatus56, TStatus57, TStatus58, TStatus59, TStatus60, TStatus61>(obj, this);
	}

	public class StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50, TStatus51, TStatus52, TStatus53, TStatus54, TStatus55, TStatus56, TStatus57, TStatus58, TStatus59, TStatus60, TStatus61, TStatus62> : StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50, TStatus51, TStatus52, TStatus53, TStatus54, TStatus55, TStatus56, TStatus57, TStatus58, TStatus59, TStatus60, TStatus61> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct where TStatus24 : struct where TStatus25 : struct where TStatus26 : struct where TStatus27 : struct where TStatus28 : struct where TStatus29 : struct where TStatus30 : struct where TStatus31 : struct where TStatus32 : struct where TStatus33 : struct where TStatus34 : struct where TStatus35 : struct where TStatus36 : struct where TStatus37 : struct where TStatus38 : struct where TStatus39 : struct where TStatus40 : struct where TStatus41 : struct where TStatus42 : struct where TStatus43 : struct where TStatus44 : struct where TStatus45 : struct where TStatus46 : struct where TStatus47 : struct where TStatus48 : struct where TStatus49 : struct where TStatus50 : struct where TStatus51 : struct where TStatus52 : struct where TStatus53 : struct where TStatus54 : struct where TStatus55 : struct where TStatus56 : struct where TStatus57 : struct where TStatus58 : struct where TStatus59 : struct where TStatus60 : struct where TStatus61 : struct where TStatus62 : struct {
		public StatusSystem() { AddConversionCheck<TStatus62>(); }
		/// <summary>Define a new rule for the given status</summary>
		public StatusRules this[TStatus62 status] => new StatusRules(this, Convert(status));
		/// <summary>
		/// Create a status tracker that'll use the rules already defined.
		/// Rules will be checked for errors when the first tracker is created (unless IgnoreRuleErrors is set to true).
		/// </summary>
		/// <param name="obj">The object associated with the newly created tracker (i.e., the tracker's "owner")</param>
		new public StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50, TStatus51, TStatus52, TStatus53, TStatus54, TStatus55, TStatus56, TStatus57, TStatus58, TStatus59, TStatus60, TStatus61, TStatus62> CreateStatusTracker(TObject obj) => new StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50, TStatus51, TStatus52, TStatus53, TStatus54, TStatus55, TStatus56, TStatus57, TStatus58, TStatus59, TStatus60, TStatus61, TStatus62>(obj, this);
	}

	public class StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50, TStatus51, TStatus52, TStatus53, TStatus54, TStatus55, TStatus56, TStatus57, TStatus58, TStatus59, TStatus60, TStatus61, TStatus62, TStatus63> : StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50, TStatus51, TStatus52, TStatus53, TStatus54, TStatus55, TStatus56, TStatus57, TStatus58, TStatus59, TStatus60, TStatus61, TStatus62> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct where TStatus24 : struct where TStatus25 : struct where TStatus26 : struct where TStatus27 : struct where TStatus28 : struct where TStatus29 : struct where TStatus30 : struct where TStatus31 : struct where TStatus32 : struct where TStatus33 : struct where TStatus34 : struct where TStatus35 : struct where TStatus36 : struct where TStatus37 : struct where TStatus38 : struct where TStatus39 : struct where TStatus40 : struct where TStatus41 : struct where TStatus42 : struct where TStatus43 : struct where TStatus44 : struct where TStatus45 : struct where TStatus46 : struct where TStatus47 : struct where TStatus48 : struct where TStatus49 : struct where TStatus50 : struct where TStatus51 : struct where TStatus52 : struct where TStatus53 : struct where TStatus54 : struct where TStatus55 : struct where TStatus56 : struct where TStatus57 : struct where TStatus58 : struct where TStatus59 : struct where TStatus60 : struct where TStatus61 : struct where TStatus62 : struct where TStatus63 : struct {
		public StatusSystem() { AddConversionCheck<TStatus63>(); }
		/// <summary>Define a new rule for the given status</summary>
		public StatusRules this[TStatus63 status] => new StatusRules(this, Convert(status));
		/// <summary>
		/// Create a status tracker that'll use the rules already defined.
		/// Rules will be checked for errors when the first tracker is created (unless IgnoreRuleErrors is set to true).
		/// </summary>
		/// <param name="obj">The object associated with the newly created tracker (i.e., the tracker's "owner")</param>
		new public StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50, TStatus51, TStatus52, TStatus53, TStatus54, TStatus55, TStatus56, TStatus57, TStatus58, TStatus59, TStatus60, TStatus61, TStatus62, TStatus63> CreateStatusTracker(TObject obj) => new StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50, TStatus51, TStatus52, TStatus53, TStatus54, TStatus55, TStatus56, TStatus57, TStatus58, TStatus59, TStatus60, TStatus61, TStatus62, TStatus63>(obj, this);
	}

	public class StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50, TStatus51, TStatus52, TStatus53, TStatus54, TStatus55, TStatus56, TStatus57, TStatus58, TStatus59, TStatus60, TStatus61, TStatus62, TStatus63, TStatus64> : StatusSystem<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50, TStatus51, TStatus52, TStatus53, TStatus54, TStatus55, TStatus56, TStatus57, TStatus58, TStatus59, TStatus60, TStatus61, TStatus62, TStatus63> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct where TStatus24 : struct where TStatus25 : struct where TStatus26 : struct where TStatus27 : struct where TStatus28 : struct where TStatus29 : struct where TStatus30 : struct where TStatus31 : struct where TStatus32 : struct where TStatus33 : struct where TStatus34 : struct where TStatus35 : struct where TStatus36 : struct where TStatus37 : struct where TStatus38 : struct where TStatus39 : struct where TStatus40 : struct where TStatus41 : struct where TStatus42 : struct where TStatus43 : struct where TStatus44 : struct where TStatus45 : struct where TStatus46 : struct where TStatus47 : struct where TStatus48 : struct where TStatus49 : struct where TStatus50 : struct where TStatus51 : struct where TStatus52 : struct where TStatus53 : struct where TStatus54 : struct where TStatus55 : struct where TStatus56 : struct where TStatus57 : struct where TStatus58 : struct where TStatus59 : struct where TStatus60 : struct where TStatus61 : struct where TStatus62 : struct where TStatus63 : struct where TStatus64 : struct {
		public StatusSystem() { AddConversionCheck<TStatus64>(); }
		/// <summary>Define a new rule for the given status</summary>
		public StatusRules this[TStatus64 status] => new StatusRules(this, Convert(status));
		/// <summary>
		/// Create a status tracker that'll use the rules already defined.
		/// Rules will be checked for errors when the first tracker is created (unless IgnoreRuleErrors is set to true).
		/// </summary>
		/// <param name="obj">The object associated with the newly created tracker (i.e., the tracker's "owner")</param>
		new public StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50, TStatus51, TStatus52, TStatus53, TStatus54, TStatus55, TStatus56, TStatus57, TStatus58, TStatus59, TStatus60, TStatus61, TStatus62, TStatus63, TStatus64> CreateStatusTracker(TObject obj) => new StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50, TStatus51, TStatus52, TStatus53, TStatus54, TStatus55, TStatus56, TStatus57, TStatus58, TStatus59, TStatus60, TStatus61, TStatus62, TStatus63, TStatus64>(obj, this);
	}

	public class StatusTracker<TObject, TStatus1> : StatusTracker<TObject> where TStatus1 : struct {
		internal StatusTracker(TObject obj, StatusSystem<TObject> rules) : base(obj, rules) { }
		/// <summary>
		/// Retrieve the current int value of the given status.
		/// The status's value can also be directly set, but only if the SingleInstance property is true for this status.
		/// </summary>
		public int this[TStatus1 status] {
			get { return this[Convert(status)]; }
			set { this[Convert(status)] = value; }
		}
	}

	public class StatusTracker<TObject, TStatus1, TStatus2> : StatusTracker<TObject, TStatus1> where TStatus1 : struct where TStatus2 : struct {
		internal StatusTracker(TObject obj, StatusSystem<TObject> rules) : base(obj, rules) { }
		/// <summary>
		/// Retrieve the current int value of the given status.
		/// The status's value can also be directly set, but only if the SingleInstance property is true for this status.
		/// </summary>
		public int this[TStatus2 status] {
			get { return this[Convert(status)]; }
			set { this[Convert(status)] = value; }
		}
	}

	public class StatusTracker<TObject, TStatus1, TStatus2, TStatus3> : StatusTracker<TObject, TStatus1, TStatus2> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct {
		internal StatusTracker(TObject obj, StatusSystem<TObject> rules) : base(obj, rules) { }
		/// <summary>
		/// Retrieve the current int value of the given status.
		/// The status's value can also be directly set, but only if the SingleInstance property is true for this status.
		/// </summary>
		public int this[TStatus3 status] {
			get { return this[Convert(status)]; }
			set { this[Convert(status)] = value; }
		}
	}

	public class StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4> : StatusTracker<TObject, TStatus1, TStatus2, TStatus3> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct {
		internal StatusTracker(TObject obj, StatusSystem<TObject> rules) : base(obj, rules) { }
		/// <summary>
		/// Retrieve the current int value of the given status.
		/// The status's value can also be directly set, but only if the SingleInstance property is true for this status.
		/// </summary>
		public int this[TStatus4 status] {
			get { return this[Convert(status)]; }
			set { this[Convert(status)] = value; }
		}
	}

	public class StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5> : StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct {
		internal StatusTracker(TObject obj, StatusSystem<TObject> rules) : base(obj, rules) { }
		/// <summary>
		/// Retrieve the current int value of the given status.
		/// The status's value can also be directly set, but only if the SingleInstance property is true for this status.
		/// </summary>
		public int this[TStatus5 status] {
			get { return this[Convert(status)]; }
			set { this[Convert(status)] = value; }
		}
	}

	public class StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6> : StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct {
		internal StatusTracker(TObject obj, StatusSystem<TObject> rules) : base(obj, rules) { }
		/// <summary>
		/// Retrieve the current int value of the given status.
		/// The status's value can also be directly set, but only if the SingleInstance property is true for this status.
		/// </summary>
		public int this[TStatus6 status] {
			get { return this[Convert(status)]; }
			set { this[Convert(status)] = value; }
		}
	}

	public class StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7> : StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct {
		internal StatusTracker(TObject obj, StatusSystem<TObject> rules) : base(obj, rules) { }
		/// <summary>
		/// Retrieve the current int value of the given status.
		/// The status's value can also be directly set, but only if the SingleInstance property is true for this status.
		/// </summary>
		public int this[TStatus7 status] {
			get { return this[Convert(status)]; }
			set { this[Convert(status)] = value; }
		}
	}

	public class StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8> : StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct {
		internal StatusTracker(TObject obj, StatusSystem<TObject> rules) : base(obj, rules) { }
		/// <summary>
		/// Retrieve the current int value of the given status.
		/// The status's value can also be directly set, but only if the SingleInstance property is true for this status.
		/// </summary>
		public int this[TStatus8 status] {
			get { return this[Convert(status)]; }
			set { this[Convert(status)] = value; }
		}
	}

	public class StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9> : StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct {
		internal StatusTracker(TObject obj, StatusSystem<TObject> rules) : base(obj, rules) { }
		/// <summary>
		/// Retrieve the current int value of the given status.
		/// The status's value can also be directly set, but only if the SingleInstance property is true for this status.
		/// </summary>
		public int this[TStatus9 status] {
			get { return this[Convert(status)]; }
			set { this[Convert(status)] = value; }
		}
	}

	public class StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10> : StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct {
		internal StatusTracker(TObject obj, StatusSystem<TObject> rules) : base(obj, rules) { }
		/// <summary>
		/// Retrieve the current int value of the given status.
		/// The status's value can also be directly set, but only if the SingleInstance property is true for this status.
		/// </summary>
		public int this[TStatus10 status] {
			get { return this[Convert(status)]; }
			set { this[Convert(status)] = value; }
		}
	}

	public class StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11> : StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct {
		internal StatusTracker(TObject obj, StatusSystem<TObject> rules) : base(obj, rules) { }
		/// <summary>
		/// Retrieve the current int value of the given status.
		/// The status's value can also be directly set, but only if the SingleInstance property is true for this status.
		/// </summary>
		public int this[TStatus11 status] {
			get { return this[Convert(status)]; }
			set { this[Convert(status)] = value; }
		}
	}

	public class StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12> : StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct {
		internal StatusTracker(TObject obj, StatusSystem<TObject> rules) : base(obj, rules) { }
		/// <summary>
		/// Retrieve the current int value of the given status.
		/// The status's value can also be directly set, but only if the SingleInstance property is true for this status.
		/// </summary>
		public int this[TStatus12 status] {
			get { return this[Convert(status)]; }
			set { this[Convert(status)] = value; }
		}
	}

	public class StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13> : StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct {
		internal StatusTracker(TObject obj, StatusSystem<TObject> rules) : base(obj, rules) { }
		/// <summary>
		/// Retrieve the current int value of the given status.
		/// The status's value can also be directly set, but only if the SingleInstance property is true for this status.
		/// </summary>
		public int this[TStatus13 status] {
			get { return this[Convert(status)]; }
			set { this[Convert(status)] = value; }
		}
	}

	public class StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14> : StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct {
		internal StatusTracker(TObject obj, StatusSystem<TObject> rules) : base(obj, rules) { }
		/// <summary>
		/// Retrieve the current int value of the given status.
		/// The status's value can also be directly set, but only if the SingleInstance property is true for this status.
		/// </summary>
		public int this[TStatus14 status] {
			get { return this[Convert(status)]; }
			set { this[Convert(status)] = value; }
		}
	}

	public class StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15> : StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct {
		internal StatusTracker(TObject obj, StatusSystem<TObject> rules) : base(obj, rules) { }
		/// <summary>
		/// Retrieve the current int value of the given status.
		/// The status's value can also be directly set, but only if the SingleInstance property is true for this status.
		/// </summary>
		public int this[TStatus15 status] {
			get { return this[Convert(status)]; }
			set { this[Convert(status)] = value; }
		}
	}

	public class StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16> : StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct {
		internal StatusTracker(TObject obj, StatusSystem<TObject> rules) : base(obj, rules) { }
		/// <summary>
		/// Retrieve the current int value of the given status.
		/// The status's value can also be directly set, but only if the SingleInstance property is true for this status.
		/// </summary>
		public int this[TStatus16 status] {
			get { return this[Convert(status)]; }
			set { this[Convert(status)] = value; }
		}
	}

	public class StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17> : StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct {
		internal StatusTracker(TObject obj, StatusSystem<TObject> rules) : base(obj, rules) { }
		/// <summary>
		/// Retrieve the current int value of the given status.
		/// The status's value can also be directly set, but only if the SingleInstance property is true for this status.
		/// </summary>
		public int this[TStatus17 status] {
			get { return this[Convert(status)]; }
			set { this[Convert(status)] = value; }
		}
	}

	public class StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18> : StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct {
		internal StatusTracker(TObject obj, StatusSystem<TObject> rules) : base(obj, rules) { }
		/// <summary>
		/// Retrieve the current int value of the given status.
		/// The status's value can also be directly set, but only if the SingleInstance property is true for this status.
		/// </summary>
		public int this[TStatus18 status] {
			get { return this[Convert(status)]; }
			set { this[Convert(status)] = value; }
		}
	}

	public class StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19> : StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct {
		internal StatusTracker(TObject obj, StatusSystem<TObject> rules) : base(obj, rules) { }
		/// <summary>
		/// Retrieve the current int value of the given status.
		/// The status's value can also be directly set, but only if the SingleInstance property is true for this status.
		/// </summary>
		public int this[TStatus19 status] {
			get { return this[Convert(status)]; }
			set { this[Convert(status)] = value; }
		}
	}

	public class StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20> : StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct {
		internal StatusTracker(TObject obj, StatusSystem<TObject> rules) : base(obj, rules) { }
		/// <summary>
		/// Retrieve the current int value of the given status.
		/// The status's value can also be directly set, but only if the SingleInstance property is true for this status.
		/// </summary>
		public int this[TStatus20 status] {
			get { return this[Convert(status)]; }
			set { this[Convert(status)] = value; }
		}
	}

	public class StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21> : StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct {
		internal StatusTracker(TObject obj, StatusSystem<TObject> rules) : base(obj, rules) { }
		/// <summary>
		/// Retrieve the current int value of the given status.
		/// The status's value can also be directly set, but only if the SingleInstance property is true for this status.
		/// </summary>
		public int this[TStatus21 status] {
			get { return this[Convert(status)]; }
			set { this[Convert(status)] = value; }
		}
	}

	public class StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22> : StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct {
		internal StatusTracker(TObject obj, StatusSystem<TObject> rules) : base(obj, rules) { }
		/// <summary>
		/// Retrieve the current int value of the given status.
		/// The status's value can also be directly set, but only if the SingleInstance property is true for this status.
		/// </summary>
		public int this[TStatus22 status] {
			get { return this[Convert(status)]; }
			set { this[Convert(status)] = value; }
		}
	}

	public class StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23> : StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct {
		internal StatusTracker(TObject obj, StatusSystem<TObject> rules) : base(obj, rules) { }
		/// <summary>
		/// Retrieve the current int value of the given status.
		/// The status's value can also be directly set, but only if the SingleInstance property is true for this status.
		/// </summary>
		public int this[TStatus23 status] {
			get { return this[Convert(status)]; }
			set { this[Convert(status)] = value; }
		}
	}

	public class StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24> : StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct where TStatus24 : struct {
		internal StatusTracker(TObject obj, StatusSystem<TObject> rules) : base(obj, rules) { }
		/// <summary>
		/// Retrieve the current int value of the given status.
		/// The status's value can also be directly set, but only if the SingleInstance property is true for this status.
		/// </summary>
		public int this[TStatus24 status] {
			get { return this[Convert(status)]; }
			set { this[Convert(status)] = value; }
		}
	}

	public class StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25> : StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct where TStatus24 : struct where TStatus25 : struct {
		internal StatusTracker(TObject obj, StatusSystem<TObject> rules) : base(obj, rules) { }
		/// <summary>
		/// Retrieve the current int value of the given status.
		/// The status's value can also be directly set, but only if the SingleInstance property is true for this status.
		/// </summary>
		public int this[TStatus25 status] {
			get { return this[Convert(status)]; }
			set { this[Convert(status)] = value; }
		}
	}

	public class StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26> : StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct where TStatus24 : struct where TStatus25 : struct where TStatus26 : struct {
		internal StatusTracker(TObject obj, StatusSystem<TObject> rules) : base(obj, rules) { }
		/// <summary>
		/// Retrieve the current int value of the given status.
		/// The status's value can also be directly set, but only if the SingleInstance property is true for this status.
		/// </summary>
		public int this[TStatus26 status] {
			get { return this[Convert(status)]; }
			set { this[Convert(status)] = value; }
		}
	}

	public class StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27> : StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct where TStatus24 : struct where TStatus25 : struct where TStatus26 : struct where TStatus27 : struct {
		internal StatusTracker(TObject obj, StatusSystem<TObject> rules) : base(obj, rules) { }
		/// <summary>
		/// Retrieve the current int value of the given status.
		/// The status's value can also be directly set, but only if the SingleInstance property is true for this status.
		/// </summary>
		public int this[TStatus27 status] {
			get { return this[Convert(status)]; }
			set { this[Convert(status)] = value; }
		}
	}

	public class StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28> : StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct where TStatus24 : struct where TStatus25 : struct where TStatus26 : struct where TStatus27 : struct where TStatus28 : struct {
		internal StatusTracker(TObject obj, StatusSystem<TObject> rules) : base(obj, rules) { }
		/// <summary>
		/// Retrieve the current int value of the given status.
		/// The status's value can also be directly set, but only if the SingleInstance property is true for this status.
		/// </summary>
		public int this[TStatus28 status] {
			get { return this[Convert(status)]; }
			set { this[Convert(status)] = value; }
		}
	}

	public class StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29> : StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct where TStatus24 : struct where TStatus25 : struct where TStatus26 : struct where TStatus27 : struct where TStatus28 : struct where TStatus29 : struct {
		internal StatusTracker(TObject obj, StatusSystem<TObject> rules) : base(obj, rules) { }
		/// <summary>
		/// Retrieve the current int value of the given status.
		/// The status's value can also be directly set, but only if the SingleInstance property is true for this status.
		/// </summary>
		public int this[TStatus29 status] {
			get { return this[Convert(status)]; }
			set { this[Convert(status)] = value; }
		}
	}

	public class StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30> : StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct where TStatus24 : struct where TStatus25 : struct where TStatus26 : struct where TStatus27 : struct where TStatus28 : struct where TStatus29 : struct where TStatus30 : struct {
		internal StatusTracker(TObject obj, StatusSystem<TObject> rules) : base(obj, rules) { }
		/// <summary>
		/// Retrieve the current int value of the given status.
		/// The status's value can also be directly set, but only if the SingleInstance property is true for this status.
		/// </summary>
		public int this[TStatus30 status] {
			get { return this[Convert(status)]; }
			set { this[Convert(status)] = value; }
		}
	}

	public class StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31> : StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct where TStatus24 : struct where TStatus25 : struct where TStatus26 : struct where TStatus27 : struct where TStatus28 : struct where TStatus29 : struct where TStatus30 : struct where TStatus31 : struct {
		internal StatusTracker(TObject obj, StatusSystem<TObject> rules) : base(obj, rules) { }
		/// <summary>
		/// Retrieve the current int value of the given status.
		/// The status's value can also be directly set, but only if the SingleInstance property is true for this status.
		/// </summary>
		public int this[TStatus31 status] {
			get { return this[Convert(status)]; }
			set { this[Convert(status)] = value; }
		}
	}

	public class StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32> : StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct where TStatus24 : struct where TStatus25 : struct where TStatus26 : struct where TStatus27 : struct where TStatus28 : struct where TStatus29 : struct where TStatus30 : struct where TStatus31 : struct where TStatus32 : struct {
		internal StatusTracker(TObject obj, StatusSystem<TObject> rules) : base(obj, rules) { }
		/// <summary>
		/// Retrieve the current int value of the given status.
		/// The status's value can also be directly set, but only if the SingleInstance property is true for this status.
		/// </summary>
		public int this[TStatus32 status] {
			get { return this[Convert(status)]; }
			set { this[Convert(status)] = value; }
		}
	}

	public class StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33> : StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct where TStatus24 : struct where TStatus25 : struct where TStatus26 : struct where TStatus27 : struct where TStatus28 : struct where TStatus29 : struct where TStatus30 : struct where TStatus31 : struct where TStatus32 : struct where TStatus33 : struct {
		internal StatusTracker(TObject obj, StatusSystem<TObject> rules) : base(obj, rules) { }
		/// <summary>
		/// Retrieve the current int value of the given status.
		/// The status's value can also be directly set, but only if the SingleInstance property is true for this status.
		/// </summary>
		public int this[TStatus33 status] {
			get { return this[Convert(status)]; }
			set { this[Convert(status)] = value; }
		}
	}

	public class StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34> : StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct where TStatus24 : struct where TStatus25 : struct where TStatus26 : struct where TStatus27 : struct where TStatus28 : struct where TStatus29 : struct where TStatus30 : struct where TStatus31 : struct where TStatus32 : struct where TStatus33 : struct where TStatus34 : struct {
		internal StatusTracker(TObject obj, StatusSystem<TObject> rules) : base(obj, rules) { }
		/// <summary>
		/// Retrieve the current int value of the given status.
		/// The status's value can also be directly set, but only if the SingleInstance property is true for this status.
		/// </summary>
		public int this[TStatus34 status] {
			get { return this[Convert(status)]; }
			set { this[Convert(status)] = value; }
		}
	}

	public class StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35> : StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct where TStatus24 : struct where TStatus25 : struct where TStatus26 : struct where TStatus27 : struct where TStatus28 : struct where TStatus29 : struct where TStatus30 : struct where TStatus31 : struct where TStatus32 : struct where TStatus33 : struct where TStatus34 : struct where TStatus35 : struct {
		internal StatusTracker(TObject obj, StatusSystem<TObject> rules) : base(obj, rules) { }
		/// <summary>
		/// Retrieve the current int value of the given status.
		/// The status's value can also be directly set, but only if the SingleInstance property is true for this status.
		/// </summary>
		public int this[TStatus35 status] {
			get { return this[Convert(status)]; }
			set { this[Convert(status)] = value; }
		}
	}

	public class StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36> : StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct where TStatus24 : struct where TStatus25 : struct where TStatus26 : struct where TStatus27 : struct where TStatus28 : struct where TStatus29 : struct where TStatus30 : struct where TStatus31 : struct where TStatus32 : struct where TStatus33 : struct where TStatus34 : struct where TStatus35 : struct where TStatus36 : struct {
		internal StatusTracker(TObject obj, StatusSystem<TObject> rules) : base(obj, rules) { }
		/// <summary>
		/// Retrieve the current int value of the given status.
		/// The status's value can also be directly set, but only if the SingleInstance property is true for this status.
		/// </summary>
		public int this[TStatus36 status] {
			get { return this[Convert(status)]; }
			set { this[Convert(status)] = value; }
		}
	}

	public class StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37> : StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct where TStatus24 : struct where TStatus25 : struct where TStatus26 : struct where TStatus27 : struct where TStatus28 : struct where TStatus29 : struct where TStatus30 : struct where TStatus31 : struct where TStatus32 : struct where TStatus33 : struct where TStatus34 : struct where TStatus35 : struct where TStatus36 : struct where TStatus37 : struct {
		internal StatusTracker(TObject obj, StatusSystem<TObject> rules) : base(obj, rules) { }
		/// <summary>
		/// Retrieve the current int value of the given status.
		/// The status's value can also be directly set, but only if the SingleInstance property is true for this status.
		/// </summary>
		public int this[TStatus37 status] {
			get { return this[Convert(status)]; }
			set { this[Convert(status)] = value; }
		}
	}

	public class StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38> : StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct where TStatus24 : struct where TStatus25 : struct where TStatus26 : struct where TStatus27 : struct where TStatus28 : struct where TStatus29 : struct where TStatus30 : struct where TStatus31 : struct where TStatus32 : struct where TStatus33 : struct where TStatus34 : struct where TStatus35 : struct where TStatus36 : struct where TStatus37 : struct where TStatus38 : struct {
		internal StatusTracker(TObject obj, StatusSystem<TObject> rules) : base(obj, rules) { }
		/// <summary>
		/// Retrieve the current int value of the given status.
		/// The status's value can also be directly set, but only if the SingleInstance property is true for this status.
		/// </summary>
		public int this[TStatus38 status] {
			get { return this[Convert(status)]; }
			set { this[Convert(status)] = value; }
		}
	}

	public class StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39> : StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct where TStatus24 : struct where TStatus25 : struct where TStatus26 : struct where TStatus27 : struct where TStatus28 : struct where TStatus29 : struct where TStatus30 : struct where TStatus31 : struct where TStatus32 : struct where TStatus33 : struct where TStatus34 : struct where TStatus35 : struct where TStatus36 : struct where TStatus37 : struct where TStatus38 : struct where TStatus39 : struct {
		internal StatusTracker(TObject obj, StatusSystem<TObject> rules) : base(obj, rules) { }
		/// <summary>
		/// Retrieve the current int value of the given status.
		/// The status's value can also be directly set, but only if the SingleInstance property is true for this status.
		/// </summary>
		public int this[TStatus39 status] {
			get { return this[Convert(status)]; }
			set { this[Convert(status)] = value; }
		}
	}

	public class StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40> : StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct where TStatus24 : struct where TStatus25 : struct where TStatus26 : struct where TStatus27 : struct where TStatus28 : struct where TStatus29 : struct where TStatus30 : struct where TStatus31 : struct where TStatus32 : struct where TStatus33 : struct where TStatus34 : struct where TStatus35 : struct where TStatus36 : struct where TStatus37 : struct where TStatus38 : struct where TStatus39 : struct where TStatus40 : struct {
		internal StatusTracker(TObject obj, StatusSystem<TObject> rules) : base(obj, rules) { }
		/// <summary>
		/// Retrieve the current int value of the given status.
		/// The status's value can also be directly set, but only if the SingleInstance property is true for this status.
		/// </summary>
		public int this[TStatus40 status] {
			get { return this[Convert(status)]; }
			set { this[Convert(status)] = value; }
		}
	}

	public class StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41> : StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct where TStatus24 : struct where TStatus25 : struct where TStatus26 : struct where TStatus27 : struct where TStatus28 : struct where TStatus29 : struct where TStatus30 : struct where TStatus31 : struct where TStatus32 : struct where TStatus33 : struct where TStatus34 : struct where TStatus35 : struct where TStatus36 : struct where TStatus37 : struct where TStatus38 : struct where TStatus39 : struct where TStatus40 : struct where TStatus41 : struct {
		internal StatusTracker(TObject obj, StatusSystem<TObject> rules) : base(obj, rules) { }
		/// <summary>
		/// Retrieve the current int value of the given status.
		/// The status's value can also be directly set, but only if the SingleInstance property is true for this status.
		/// </summary>
		public int this[TStatus41 status] {
			get { return this[Convert(status)]; }
			set { this[Convert(status)] = value; }
		}
	}

	public class StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42> : StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct where TStatus24 : struct where TStatus25 : struct where TStatus26 : struct where TStatus27 : struct where TStatus28 : struct where TStatus29 : struct where TStatus30 : struct where TStatus31 : struct where TStatus32 : struct where TStatus33 : struct where TStatus34 : struct where TStatus35 : struct where TStatus36 : struct where TStatus37 : struct where TStatus38 : struct where TStatus39 : struct where TStatus40 : struct where TStatus41 : struct where TStatus42 : struct {
		internal StatusTracker(TObject obj, StatusSystem<TObject> rules) : base(obj, rules) { }
		/// <summary>
		/// Retrieve the current int value of the given status.
		/// The status's value can also be directly set, but only if the SingleInstance property is true for this status.
		/// </summary>
		public int this[TStatus42 status] {
			get { return this[Convert(status)]; }
			set { this[Convert(status)] = value; }
		}
	}

	public class StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43> : StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct where TStatus24 : struct where TStatus25 : struct where TStatus26 : struct where TStatus27 : struct where TStatus28 : struct where TStatus29 : struct where TStatus30 : struct where TStatus31 : struct where TStatus32 : struct where TStatus33 : struct where TStatus34 : struct where TStatus35 : struct where TStatus36 : struct where TStatus37 : struct where TStatus38 : struct where TStatus39 : struct where TStatus40 : struct where TStatus41 : struct where TStatus42 : struct where TStatus43 : struct {
		internal StatusTracker(TObject obj, StatusSystem<TObject> rules) : base(obj, rules) { }
		/// <summary>
		/// Retrieve the current int value of the given status.
		/// The status's value can also be directly set, but only if the SingleInstance property is true for this status.
		/// </summary>
		public int this[TStatus43 status] {
			get { return this[Convert(status)]; }
			set { this[Convert(status)] = value; }
		}
	}

	public class StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44> : StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct where TStatus24 : struct where TStatus25 : struct where TStatus26 : struct where TStatus27 : struct where TStatus28 : struct where TStatus29 : struct where TStatus30 : struct where TStatus31 : struct where TStatus32 : struct where TStatus33 : struct where TStatus34 : struct where TStatus35 : struct where TStatus36 : struct where TStatus37 : struct where TStatus38 : struct where TStatus39 : struct where TStatus40 : struct where TStatus41 : struct where TStatus42 : struct where TStatus43 : struct where TStatus44 : struct {
		internal StatusTracker(TObject obj, StatusSystem<TObject> rules) : base(obj, rules) { }
		/// <summary>
		/// Retrieve the current int value of the given status.
		/// The status's value can also be directly set, but only if the SingleInstance property is true for this status.
		/// </summary>
		public int this[TStatus44 status] {
			get { return this[Convert(status)]; }
			set { this[Convert(status)] = value; }
		}
	}

	public class StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45> : StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct where TStatus24 : struct where TStatus25 : struct where TStatus26 : struct where TStatus27 : struct where TStatus28 : struct where TStatus29 : struct where TStatus30 : struct where TStatus31 : struct where TStatus32 : struct where TStatus33 : struct where TStatus34 : struct where TStatus35 : struct where TStatus36 : struct where TStatus37 : struct where TStatus38 : struct where TStatus39 : struct where TStatus40 : struct where TStatus41 : struct where TStatus42 : struct where TStatus43 : struct where TStatus44 : struct where TStatus45 : struct {
		internal StatusTracker(TObject obj, StatusSystem<TObject> rules) : base(obj, rules) { }
		/// <summary>
		/// Retrieve the current int value of the given status.
		/// The status's value can also be directly set, but only if the SingleInstance property is true for this status.
		/// </summary>
		public int this[TStatus45 status] {
			get { return this[Convert(status)]; }
			set { this[Convert(status)] = value; }
		}
	}

	public class StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46> : StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct where TStatus24 : struct where TStatus25 : struct where TStatus26 : struct where TStatus27 : struct where TStatus28 : struct where TStatus29 : struct where TStatus30 : struct where TStatus31 : struct where TStatus32 : struct where TStatus33 : struct where TStatus34 : struct where TStatus35 : struct where TStatus36 : struct where TStatus37 : struct where TStatus38 : struct where TStatus39 : struct where TStatus40 : struct where TStatus41 : struct where TStatus42 : struct where TStatus43 : struct where TStatus44 : struct where TStatus45 : struct where TStatus46 : struct {
		internal StatusTracker(TObject obj, StatusSystem<TObject> rules) : base(obj, rules) { }
		/// <summary>
		/// Retrieve the current int value of the given status.
		/// The status's value can also be directly set, but only if the SingleInstance property is true for this status.
		/// </summary>
		public int this[TStatus46 status] {
			get { return this[Convert(status)]; }
			set { this[Convert(status)] = value; }
		}
	}

	public class StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47> : StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct where TStatus24 : struct where TStatus25 : struct where TStatus26 : struct where TStatus27 : struct where TStatus28 : struct where TStatus29 : struct where TStatus30 : struct where TStatus31 : struct where TStatus32 : struct where TStatus33 : struct where TStatus34 : struct where TStatus35 : struct where TStatus36 : struct where TStatus37 : struct where TStatus38 : struct where TStatus39 : struct where TStatus40 : struct where TStatus41 : struct where TStatus42 : struct where TStatus43 : struct where TStatus44 : struct where TStatus45 : struct where TStatus46 : struct where TStatus47 : struct {
		internal StatusTracker(TObject obj, StatusSystem<TObject> rules) : base(obj, rules) { }
		/// <summary>
		/// Retrieve the current int value of the given status.
		/// The status's value can also be directly set, but only if the SingleInstance property is true for this status.
		/// </summary>
		public int this[TStatus47 status] {
			get { return this[Convert(status)]; }
			set { this[Convert(status)] = value; }
		}
	}

	public class StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48> : StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct where TStatus24 : struct where TStatus25 : struct where TStatus26 : struct where TStatus27 : struct where TStatus28 : struct where TStatus29 : struct where TStatus30 : struct where TStatus31 : struct where TStatus32 : struct where TStatus33 : struct where TStatus34 : struct where TStatus35 : struct where TStatus36 : struct where TStatus37 : struct where TStatus38 : struct where TStatus39 : struct where TStatus40 : struct where TStatus41 : struct where TStatus42 : struct where TStatus43 : struct where TStatus44 : struct where TStatus45 : struct where TStatus46 : struct where TStatus47 : struct where TStatus48 : struct {
		internal StatusTracker(TObject obj, StatusSystem<TObject> rules) : base(obj, rules) { }
		/// <summary>
		/// Retrieve the current int value of the given status.
		/// The status's value can also be directly set, but only if the SingleInstance property is true for this status.
		/// </summary>
		public int this[TStatus48 status] {
			get { return this[Convert(status)]; }
			set { this[Convert(status)] = value; }
		}
	}

	public class StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49> : StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct where TStatus24 : struct where TStatus25 : struct where TStatus26 : struct where TStatus27 : struct where TStatus28 : struct where TStatus29 : struct where TStatus30 : struct where TStatus31 : struct where TStatus32 : struct where TStatus33 : struct where TStatus34 : struct where TStatus35 : struct where TStatus36 : struct where TStatus37 : struct where TStatus38 : struct where TStatus39 : struct where TStatus40 : struct where TStatus41 : struct where TStatus42 : struct where TStatus43 : struct where TStatus44 : struct where TStatus45 : struct where TStatus46 : struct where TStatus47 : struct where TStatus48 : struct where TStatus49 : struct {
		internal StatusTracker(TObject obj, StatusSystem<TObject> rules) : base(obj, rules) { }
		/// <summary>
		/// Retrieve the current int value of the given status.
		/// The status's value can also be directly set, but only if the SingleInstance property is true for this status.
		/// </summary>
		public int this[TStatus49 status] {
			get { return this[Convert(status)]; }
			set { this[Convert(status)] = value; }
		}
	}

	public class StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50> : StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct where TStatus24 : struct where TStatus25 : struct where TStatus26 : struct where TStatus27 : struct where TStatus28 : struct where TStatus29 : struct where TStatus30 : struct where TStatus31 : struct where TStatus32 : struct where TStatus33 : struct where TStatus34 : struct where TStatus35 : struct where TStatus36 : struct where TStatus37 : struct where TStatus38 : struct where TStatus39 : struct where TStatus40 : struct where TStatus41 : struct where TStatus42 : struct where TStatus43 : struct where TStatus44 : struct where TStatus45 : struct where TStatus46 : struct where TStatus47 : struct where TStatus48 : struct where TStatus49 : struct where TStatus50 : struct {
		internal StatusTracker(TObject obj, StatusSystem<TObject> rules) : base(obj, rules) { }
		/// <summary>
		/// Retrieve the current int value of the given status.
		/// The status's value can also be directly set, but only if the SingleInstance property is true for this status.
		/// </summary>
		public int this[TStatus50 status] {
			get { return this[Convert(status)]; }
			set { this[Convert(status)] = value; }
		}
	}

	public class StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50, TStatus51> : StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct where TStatus24 : struct where TStatus25 : struct where TStatus26 : struct where TStatus27 : struct where TStatus28 : struct where TStatus29 : struct where TStatus30 : struct where TStatus31 : struct where TStatus32 : struct where TStatus33 : struct where TStatus34 : struct where TStatus35 : struct where TStatus36 : struct where TStatus37 : struct where TStatus38 : struct where TStatus39 : struct where TStatus40 : struct where TStatus41 : struct where TStatus42 : struct where TStatus43 : struct where TStatus44 : struct where TStatus45 : struct where TStatus46 : struct where TStatus47 : struct where TStatus48 : struct where TStatus49 : struct where TStatus50 : struct where TStatus51 : struct {
		internal StatusTracker(TObject obj, StatusSystem<TObject> rules) : base(obj, rules) { }
		/// <summary>
		/// Retrieve the current int value of the given status.
		/// The status's value can also be directly set, but only if the SingleInstance property is true for this status.
		/// </summary>
		public int this[TStatus51 status] {
			get { return this[Convert(status)]; }
			set { this[Convert(status)] = value; }
		}
	}

	public class StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50, TStatus51, TStatus52> : StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50, TStatus51> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct where TStatus24 : struct where TStatus25 : struct where TStatus26 : struct where TStatus27 : struct where TStatus28 : struct where TStatus29 : struct where TStatus30 : struct where TStatus31 : struct where TStatus32 : struct where TStatus33 : struct where TStatus34 : struct where TStatus35 : struct where TStatus36 : struct where TStatus37 : struct where TStatus38 : struct where TStatus39 : struct where TStatus40 : struct where TStatus41 : struct where TStatus42 : struct where TStatus43 : struct where TStatus44 : struct where TStatus45 : struct where TStatus46 : struct where TStatus47 : struct where TStatus48 : struct where TStatus49 : struct where TStatus50 : struct where TStatus51 : struct where TStatus52 : struct {
		internal StatusTracker(TObject obj, StatusSystem<TObject> rules) : base(obj, rules) { }
		/// <summary>
		/// Retrieve the current int value of the given status.
		/// The status's value can also be directly set, but only if the SingleInstance property is true for this status.
		/// </summary>
		public int this[TStatus52 status] {
			get { return this[Convert(status)]; }
			set { this[Convert(status)] = value; }
		}
	}

	public class StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50, TStatus51, TStatus52, TStatus53> : StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50, TStatus51, TStatus52> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct where TStatus24 : struct where TStatus25 : struct where TStatus26 : struct where TStatus27 : struct where TStatus28 : struct where TStatus29 : struct where TStatus30 : struct where TStatus31 : struct where TStatus32 : struct where TStatus33 : struct where TStatus34 : struct where TStatus35 : struct where TStatus36 : struct where TStatus37 : struct where TStatus38 : struct where TStatus39 : struct where TStatus40 : struct where TStatus41 : struct where TStatus42 : struct where TStatus43 : struct where TStatus44 : struct where TStatus45 : struct where TStatus46 : struct where TStatus47 : struct where TStatus48 : struct where TStatus49 : struct where TStatus50 : struct where TStatus51 : struct where TStatus52 : struct where TStatus53 : struct {
		internal StatusTracker(TObject obj, StatusSystem<TObject> rules) : base(obj, rules) { }
		/// <summary>
		/// Retrieve the current int value of the given status.
		/// The status's value can also be directly set, but only if the SingleInstance property is true for this status.
		/// </summary>
		public int this[TStatus53 status] {
			get { return this[Convert(status)]; }
			set { this[Convert(status)] = value; }
		}
	}

	public class StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50, TStatus51, TStatus52, TStatus53, TStatus54> : StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50, TStatus51, TStatus52, TStatus53> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct where TStatus24 : struct where TStatus25 : struct where TStatus26 : struct where TStatus27 : struct where TStatus28 : struct where TStatus29 : struct where TStatus30 : struct where TStatus31 : struct where TStatus32 : struct where TStatus33 : struct where TStatus34 : struct where TStatus35 : struct where TStatus36 : struct where TStatus37 : struct where TStatus38 : struct where TStatus39 : struct where TStatus40 : struct where TStatus41 : struct where TStatus42 : struct where TStatus43 : struct where TStatus44 : struct where TStatus45 : struct where TStatus46 : struct where TStatus47 : struct where TStatus48 : struct where TStatus49 : struct where TStatus50 : struct where TStatus51 : struct where TStatus52 : struct where TStatus53 : struct where TStatus54 : struct {
		internal StatusTracker(TObject obj, StatusSystem<TObject> rules) : base(obj, rules) { }
		/// <summary>
		/// Retrieve the current int value of the given status.
		/// The status's value can also be directly set, but only if the SingleInstance property is true for this status.
		/// </summary>
		public int this[TStatus54 status] {
			get { return this[Convert(status)]; }
			set { this[Convert(status)] = value; }
		}
	}

	public class StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50, TStatus51, TStatus52, TStatus53, TStatus54, TStatus55> : StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50, TStatus51, TStatus52, TStatus53, TStatus54> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct where TStatus24 : struct where TStatus25 : struct where TStatus26 : struct where TStatus27 : struct where TStatus28 : struct where TStatus29 : struct where TStatus30 : struct where TStatus31 : struct where TStatus32 : struct where TStatus33 : struct where TStatus34 : struct where TStatus35 : struct where TStatus36 : struct where TStatus37 : struct where TStatus38 : struct where TStatus39 : struct where TStatus40 : struct where TStatus41 : struct where TStatus42 : struct where TStatus43 : struct where TStatus44 : struct where TStatus45 : struct where TStatus46 : struct where TStatus47 : struct where TStatus48 : struct where TStatus49 : struct where TStatus50 : struct where TStatus51 : struct where TStatus52 : struct where TStatus53 : struct where TStatus54 : struct where TStatus55 : struct {
		internal StatusTracker(TObject obj, StatusSystem<TObject> rules) : base(obj, rules) { }
		/// <summary>
		/// Retrieve the current int value of the given status.
		/// The status's value can also be directly set, but only if the SingleInstance property is true for this status.
		/// </summary>
		public int this[TStatus55 status] {
			get { return this[Convert(status)]; }
			set { this[Convert(status)] = value; }
		}
	}

	public class StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50, TStatus51, TStatus52, TStatus53, TStatus54, TStatus55, TStatus56> : StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50, TStatus51, TStatus52, TStatus53, TStatus54, TStatus55> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct where TStatus24 : struct where TStatus25 : struct where TStatus26 : struct where TStatus27 : struct where TStatus28 : struct where TStatus29 : struct where TStatus30 : struct where TStatus31 : struct where TStatus32 : struct where TStatus33 : struct where TStatus34 : struct where TStatus35 : struct where TStatus36 : struct where TStatus37 : struct where TStatus38 : struct where TStatus39 : struct where TStatus40 : struct where TStatus41 : struct where TStatus42 : struct where TStatus43 : struct where TStatus44 : struct where TStatus45 : struct where TStatus46 : struct where TStatus47 : struct where TStatus48 : struct where TStatus49 : struct where TStatus50 : struct where TStatus51 : struct where TStatus52 : struct where TStatus53 : struct where TStatus54 : struct where TStatus55 : struct where TStatus56 : struct {
		internal StatusTracker(TObject obj, StatusSystem<TObject> rules) : base(obj, rules) { }
		/// <summary>
		/// Retrieve the current int value of the given status.
		/// The status's value can also be directly set, but only if the SingleInstance property is true for this status.
		/// </summary>
		public int this[TStatus56 status] {
			get { return this[Convert(status)]; }
			set { this[Convert(status)] = value; }
		}
	}

	public class StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50, TStatus51, TStatus52, TStatus53, TStatus54, TStatus55, TStatus56, TStatus57> : StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50, TStatus51, TStatus52, TStatus53, TStatus54, TStatus55, TStatus56> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct where TStatus24 : struct where TStatus25 : struct where TStatus26 : struct where TStatus27 : struct where TStatus28 : struct where TStatus29 : struct where TStatus30 : struct where TStatus31 : struct where TStatus32 : struct where TStatus33 : struct where TStatus34 : struct where TStatus35 : struct where TStatus36 : struct where TStatus37 : struct where TStatus38 : struct where TStatus39 : struct where TStatus40 : struct where TStatus41 : struct where TStatus42 : struct where TStatus43 : struct where TStatus44 : struct where TStatus45 : struct where TStatus46 : struct where TStatus47 : struct where TStatus48 : struct where TStatus49 : struct where TStatus50 : struct where TStatus51 : struct where TStatus52 : struct where TStatus53 : struct where TStatus54 : struct where TStatus55 : struct where TStatus56 : struct where TStatus57 : struct {
		internal StatusTracker(TObject obj, StatusSystem<TObject> rules) : base(obj, rules) { }
		/// <summary>
		/// Retrieve the current int value of the given status.
		/// The status's value can also be directly set, but only if the SingleInstance property is true for this status.
		/// </summary>
		public int this[TStatus57 status] {
			get { return this[Convert(status)]; }
			set { this[Convert(status)] = value; }
		}
	}

	public class StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50, TStatus51, TStatus52, TStatus53, TStatus54, TStatus55, TStatus56, TStatus57, TStatus58> : StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50, TStatus51, TStatus52, TStatus53, TStatus54, TStatus55, TStatus56, TStatus57> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct where TStatus24 : struct where TStatus25 : struct where TStatus26 : struct where TStatus27 : struct where TStatus28 : struct where TStatus29 : struct where TStatus30 : struct where TStatus31 : struct where TStatus32 : struct where TStatus33 : struct where TStatus34 : struct where TStatus35 : struct where TStatus36 : struct where TStatus37 : struct where TStatus38 : struct where TStatus39 : struct where TStatus40 : struct where TStatus41 : struct where TStatus42 : struct where TStatus43 : struct where TStatus44 : struct where TStatus45 : struct where TStatus46 : struct where TStatus47 : struct where TStatus48 : struct where TStatus49 : struct where TStatus50 : struct where TStatus51 : struct where TStatus52 : struct where TStatus53 : struct where TStatus54 : struct where TStatus55 : struct where TStatus56 : struct where TStatus57 : struct where TStatus58 : struct {
		internal StatusTracker(TObject obj, StatusSystem<TObject> rules) : base(obj, rules) { }
		/// <summary>
		/// Retrieve the current int value of the given status.
		/// The status's value can also be directly set, but only if the SingleInstance property is true for this status.
		/// </summary>
		public int this[TStatus58 status] {
			get { return this[Convert(status)]; }
			set { this[Convert(status)] = value; }
		}
	}

	public class StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50, TStatus51, TStatus52, TStatus53, TStatus54, TStatus55, TStatus56, TStatus57, TStatus58, TStatus59> : StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50, TStatus51, TStatus52, TStatus53, TStatus54, TStatus55, TStatus56, TStatus57, TStatus58> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct where TStatus24 : struct where TStatus25 : struct where TStatus26 : struct where TStatus27 : struct where TStatus28 : struct where TStatus29 : struct where TStatus30 : struct where TStatus31 : struct where TStatus32 : struct where TStatus33 : struct where TStatus34 : struct where TStatus35 : struct where TStatus36 : struct where TStatus37 : struct where TStatus38 : struct where TStatus39 : struct where TStatus40 : struct where TStatus41 : struct where TStatus42 : struct where TStatus43 : struct where TStatus44 : struct where TStatus45 : struct where TStatus46 : struct where TStatus47 : struct where TStatus48 : struct where TStatus49 : struct where TStatus50 : struct where TStatus51 : struct where TStatus52 : struct where TStatus53 : struct where TStatus54 : struct where TStatus55 : struct where TStatus56 : struct where TStatus57 : struct where TStatus58 : struct where TStatus59 : struct {
		internal StatusTracker(TObject obj, StatusSystem<TObject> rules) : base(obj, rules) { }
		/// <summary>
		/// Retrieve the current int value of the given status.
		/// The status's value can also be directly set, but only if the SingleInstance property is true for this status.
		/// </summary>
		public int this[TStatus59 status] {
			get { return this[Convert(status)]; }
			set { this[Convert(status)] = value; }
		}
	}

	public class StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50, TStatus51, TStatus52, TStatus53, TStatus54, TStatus55, TStatus56, TStatus57, TStatus58, TStatus59, TStatus60> : StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50, TStatus51, TStatus52, TStatus53, TStatus54, TStatus55, TStatus56, TStatus57, TStatus58, TStatus59> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct where TStatus24 : struct where TStatus25 : struct where TStatus26 : struct where TStatus27 : struct where TStatus28 : struct where TStatus29 : struct where TStatus30 : struct where TStatus31 : struct where TStatus32 : struct where TStatus33 : struct where TStatus34 : struct where TStatus35 : struct where TStatus36 : struct where TStatus37 : struct where TStatus38 : struct where TStatus39 : struct where TStatus40 : struct where TStatus41 : struct where TStatus42 : struct where TStatus43 : struct where TStatus44 : struct where TStatus45 : struct where TStatus46 : struct where TStatus47 : struct where TStatus48 : struct where TStatus49 : struct where TStatus50 : struct where TStatus51 : struct where TStatus52 : struct where TStatus53 : struct where TStatus54 : struct where TStatus55 : struct where TStatus56 : struct where TStatus57 : struct where TStatus58 : struct where TStatus59 : struct where TStatus60 : struct {
		internal StatusTracker(TObject obj, StatusSystem<TObject> rules) : base(obj, rules) { }
		/// <summary>
		/// Retrieve the current int value of the given status.
		/// The status's value can also be directly set, but only if the SingleInstance property is true for this status.
		/// </summary>
		public int this[TStatus60 status] {
			get { return this[Convert(status)]; }
			set { this[Convert(status)] = value; }
		}
	}

	public class StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50, TStatus51, TStatus52, TStatus53, TStatus54, TStatus55, TStatus56, TStatus57, TStatus58, TStatus59, TStatus60, TStatus61> : StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50, TStatus51, TStatus52, TStatus53, TStatus54, TStatus55, TStatus56, TStatus57, TStatus58, TStatus59, TStatus60> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct where TStatus24 : struct where TStatus25 : struct where TStatus26 : struct where TStatus27 : struct where TStatus28 : struct where TStatus29 : struct where TStatus30 : struct where TStatus31 : struct where TStatus32 : struct where TStatus33 : struct where TStatus34 : struct where TStatus35 : struct where TStatus36 : struct where TStatus37 : struct where TStatus38 : struct where TStatus39 : struct where TStatus40 : struct where TStatus41 : struct where TStatus42 : struct where TStatus43 : struct where TStatus44 : struct where TStatus45 : struct where TStatus46 : struct where TStatus47 : struct where TStatus48 : struct where TStatus49 : struct where TStatus50 : struct where TStatus51 : struct where TStatus52 : struct where TStatus53 : struct where TStatus54 : struct where TStatus55 : struct where TStatus56 : struct where TStatus57 : struct where TStatus58 : struct where TStatus59 : struct where TStatus60 : struct where TStatus61 : struct {
		internal StatusTracker(TObject obj, StatusSystem<TObject> rules) : base(obj, rules) { }
		/// <summary>
		/// Retrieve the current int value of the given status.
		/// The status's value can also be directly set, but only if the SingleInstance property is true for this status.
		/// </summary>
		public int this[TStatus61 status] {
			get { return this[Convert(status)]; }
			set { this[Convert(status)] = value; }
		}
	}

	public class StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50, TStatus51, TStatus52, TStatus53, TStatus54, TStatus55, TStatus56, TStatus57, TStatus58, TStatus59, TStatus60, TStatus61, TStatus62> : StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50, TStatus51, TStatus52, TStatus53, TStatus54, TStatus55, TStatus56, TStatus57, TStatus58, TStatus59, TStatus60, TStatus61> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct where TStatus24 : struct where TStatus25 : struct where TStatus26 : struct where TStatus27 : struct where TStatus28 : struct where TStatus29 : struct where TStatus30 : struct where TStatus31 : struct where TStatus32 : struct where TStatus33 : struct where TStatus34 : struct where TStatus35 : struct where TStatus36 : struct where TStatus37 : struct where TStatus38 : struct where TStatus39 : struct where TStatus40 : struct where TStatus41 : struct where TStatus42 : struct where TStatus43 : struct where TStatus44 : struct where TStatus45 : struct where TStatus46 : struct where TStatus47 : struct where TStatus48 : struct where TStatus49 : struct where TStatus50 : struct where TStatus51 : struct where TStatus52 : struct where TStatus53 : struct where TStatus54 : struct where TStatus55 : struct where TStatus56 : struct where TStatus57 : struct where TStatus58 : struct where TStatus59 : struct where TStatus60 : struct where TStatus61 : struct where TStatus62 : struct {
		internal StatusTracker(TObject obj, StatusSystem<TObject> rules) : base(obj, rules) { }
		/// <summary>
		/// Retrieve the current int value of the given status.
		/// The status's value can also be directly set, but only if the SingleInstance property is true for this status.
		/// </summary>
		public int this[TStatus62 status] {
			get { return this[Convert(status)]; }
			set { this[Convert(status)] = value; }
		}
	}

	public class StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50, TStatus51, TStatus52, TStatus53, TStatus54, TStatus55, TStatus56, TStatus57, TStatus58, TStatus59, TStatus60, TStatus61, TStatus62, TStatus63> : StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50, TStatus51, TStatus52, TStatus53, TStatus54, TStatus55, TStatus56, TStatus57, TStatus58, TStatus59, TStatus60, TStatus61, TStatus62> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct where TStatus24 : struct where TStatus25 : struct where TStatus26 : struct where TStatus27 : struct where TStatus28 : struct where TStatus29 : struct where TStatus30 : struct where TStatus31 : struct where TStatus32 : struct where TStatus33 : struct where TStatus34 : struct where TStatus35 : struct where TStatus36 : struct where TStatus37 : struct where TStatus38 : struct where TStatus39 : struct where TStatus40 : struct where TStatus41 : struct where TStatus42 : struct where TStatus43 : struct where TStatus44 : struct where TStatus45 : struct where TStatus46 : struct where TStatus47 : struct where TStatus48 : struct where TStatus49 : struct where TStatus50 : struct where TStatus51 : struct where TStatus52 : struct where TStatus53 : struct where TStatus54 : struct where TStatus55 : struct where TStatus56 : struct where TStatus57 : struct where TStatus58 : struct where TStatus59 : struct where TStatus60 : struct where TStatus61 : struct where TStatus62 : struct where TStatus63 : struct {
		internal StatusTracker(TObject obj, StatusSystem<TObject> rules) : base(obj, rules) { }
		/// <summary>
		/// Retrieve the current int value of the given status.
		/// The status's value can also be directly set, but only if the SingleInstance property is true for this status.
		/// </summary>
		public int this[TStatus63 status] {
			get { return this[Convert(status)]; }
			set { this[Convert(status)] = value; }
		}
	}

	public class StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50, TStatus51, TStatus52, TStatus53, TStatus54, TStatus55, TStatus56, TStatus57, TStatus58, TStatus59, TStatus60, TStatus61, TStatus62, TStatus63, TStatus64> : StatusTracker<TObject, TStatus1, TStatus2, TStatus3, TStatus4, TStatus5, TStatus6, TStatus7, TStatus8, TStatus9, TStatus10, TStatus11, TStatus12, TStatus13, TStatus14, TStatus15, TStatus16, TStatus17, TStatus18, TStatus19, TStatus20, TStatus21, TStatus22, TStatus23, TStatus24, TStatus25, TStatus26, TStatus27, TStatus28, TStatus29, TStatus30, TStatus31, TStatus32, TStatus33, TStatus34, TStatus35, TStatus36, TStatus37, TStatus38, TStatus39, TStatus40, TStatus41, TStatus42, TStatus43, TStatus44, TStatus45, TStatus46, TStatus47, TStatus48, TStatus49, TStatus50, TStatus51, TStatus52, TStatus53, TStatus54, TStatus55, TStatus56, TStatus57, TStatus58, TStatus59, TStatus60, TStatus61, TStatus62, TStatus63> where TStatus1 : struct where TStatus2 : struct where TStatus3 : struct where TStatus4 : struct where TStatus5 : struct where TStatus6 : struct where TStatus7 : struct where TStatus8 : struct where TStatus9 : struct where TStatus10 : struct where TStatus11 : struct where TStatus12 : struct where TStatus13 : struct where TStatus14 : struct where TStatus15 : struct where TStatus16 : struct where TStatus17 : struct where TStatus18 : struct where TStatus19 : struct where TStatus20 : struct where TStatus21 : struct where TStatus22 : struct where TStatus23 : struct where TStatus24 : struct where TStatus25 : struct where TStatus26 : struct where TStatus27 : struct where TStatus28 : struct where TStatus29 : struct where TStatus30 : struct where TStatus31 : struct where TStatus32 : struct where TStatus33 : struct where TStatus34 : struct where TStatus35 : struct where TStatus36 : struct where TStatus37 : struct where TStatus38 : struct where TStatus39 : struct where TStatus40 : struct where TStatus41 : struct where TStatus42 : struct where TStatus43 : struct where TStatus44 : struct where TStatus45 : struct where TStatus46 : struct where TStatus47 : struct where TStatus48 : struct where TStatus49 : struct where TStatus50 : struct where TStatus51 : struct where TStatus52 : struct where TStatus53 : struct where TStatus54 : struct where TStatus55 : struct where TStatus56 : struct where TStatus57 : struct where TStatus58 : struct where TStatus59 : struct where TStatus60 : struct where TStatus61 : struct where TStatus62 : struct where TStatus63 : struct where TStatus64 : struct {
		internal StatusTracker(TObject obj, StatusSystem<TObject> rules) : base(obj, rules) { }
		/// <summary>
		/// Retrieve the current int value of the given status.
		/// The status's value can also be directly set, but only if the SingleInstance property is true for this status.
		/// </summary>
		public int this[TStatus64 status] {
			get { return this[Convert(status)]; }
			set { this[Convert(status)] = value; }
		}
	}
}
