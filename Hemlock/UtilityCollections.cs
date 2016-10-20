using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace UtilityCollections {
	public class DefaultHashSet<T> : HashSet<T> {
		public bool this[T t] {
			get { return Contains(t); }
			set {
				if(value) Add(t);
				else Remove(t);
			}
		}
		public DefaultHashSet() { }
		public DefaultHashSet(IEqualityComparer<T> comparer) : base(comparer) { }
		public DefaultHashSet(IEnumerable<T> collection, IEqualityComparer<T> comparer = null) : base(collection, comparer) { }
	}
	public class DefaultValueDictionary<TKey, TValue> : Dictionary<TKey, TValue> {
		new public TValue this[TKey key] {
			get {
				TValue v;
				TryGetValue(key, out v); //TryGetValue sets its out parameter to default if not found.
				return v;
			}
			set {
				base[key] = value;
			}
		}
		public DefaultValueDictionary() { }
		public DefaultValueDictionary(IEqualityComparer<TKey> comparer) : base(comparer) { }
		public DefaultValueDictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer = null)
			: base(dictionary, comparer) { }
	}

	public class MultiValueDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, IEnumerable<TValue>>> {
		private Dictionary<TKey, ICollection<TValue>> d;
		private readonly Func<ICollection<TValue>> createCollection;
		public MultiValueDictionary() {
			d = new Dictionary<TKey, ICollection<TValue>>();
			createCollection = () => new List<TValue>();
		}
		public MultiValueDictionary(IEqualityComparer<TKey> comparer) {
			d = new Dictionary<TKey, ICollection<TValue>>(comparer);
			createCollection = () => new List<TValue>();
		}
		private MultiValueDictionary(Func<ICollection<TValue>> createCollection, IEqualityComparer<TKey> comparer = null) {
			d = new Dictionary<TKey, ICollection<TValue>>(comparer);
			this.createCollection = createCollection;
		}
		public static MultiValueDictionary<TKey, TValue> Create<TCollection>() where TCollection : ICollection<TValue>, new() {
			return new MultiValueDictionary<TKey, TValue>(() => new TCollection());
		}
		public static MultiValueDictionary<TKey, TValue> Create<TCollection>(IEqualityComparer<TKey> comparer)
			where TCollection : ICollection<TValue>, new()
		{
			return new MultiValueDictionary<TKey, TValue>(() => new TCollection(), comparer);
		}
		IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
		//todo: xml note that empty collections can be returned?
		public IEnumerator<KeyValuePair<TKey, IEnumerable<TValue>>> GetEnumerator() {
			foreach(var pair in d) yield return new KeyValuePair<TKey, IEnumerable<TValue>>(pair.Key, pair.Value);
		}
		public IEnumerable<KeyValuePair<TKey, TValue>> GetAllKeyValuePairs() {
			foreach(var pair in d) {
				foreach(var v in pair.Value) {
					yield return new KeyValuePair<TKey, TValue>(pair.Key, v);
				}
			}
		}
		public IEnumerable<TKey> GetAllKeys() => d.Keys;
		public IEnumerable<TValue> GetAllValues() {
			foreach(var collection in d.Values) {
				foreach(var v in collection) {
					yield return v;
				}
			}
		}
		public IEnumerable<TValue> this[TKey key] {
			get {
				if(d.ContainsKey(key)) return d[key];
				else return Enumerable.Empty<TValue>();
			}
			//todo: xml: This one replaces the entire contents of this key.
			set {
				if(value == null) {
					d.Remove(key);
				}
				else {
					ICollection<TValue> coll = createCollection();
					foreach(TValue v in value) {
						coll.Add(v);
					}
					d[key] = coll;
				}
			}
		}
		public void Add(TKey key, TValue value) {
			if(!d.ContainsKey(key)) d.Add(key, createCollection());
			d[key].Add(value);
		}
		public bool Remove(TKey key, TValue value) {
			if(d.ContainsKey(key)) return d[key].Remove(value);
			else return false;
		}
		public void Clear() { d.Clear(); }
		public void Clear(TKey key) { d.Remove(key); }
		public bool Contains(TKey key, TValue value) => d.ContainsKey(key) && d[key].Contains(value);
		public bool Contains(TValue value) {
			foreach(var list in d.Values) {
				if(list.Contains(value)) return true;
			}
			return false;
		}
		public bool AddUnique(TKey key, TValue value) {
			if(Contains(key, value)) return false;
			if(!d.ContainsKey(key)) d.Add(key, createCollection());
			d[key].Add(value);
			return true;
		}
		public bool AnyValues(TKey key) => d.ContainsKey(key) && d[key].Any();
	}
}
