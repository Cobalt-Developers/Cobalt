using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace Cobalt.Api.Helpers
{
    public class OrderedDictionary<T, TK>
    {
        private OrderedDictionary UnderlyingCollection { get; } = new OrderedDictionary();

        public TK this[T key]
        {
            get => (TK)UnderlyingCollection[key];
            set => UnderlyingCollection[key] = value;
        }

        public TK this[int index]
        {
            get => (TK)UnderlyingCollection[index];
            set => UnderlyingCollection[index] = value;
        }
        public ICollection<T> Keys => UnderlyingCollection.Keys.OfType<T>().ToList();
        public ICollection<TK> Values => UnderlyingCollection.Values.OfType<TK>().ToList();
        public bool IsReadOnly => UnderlyingCollection.IsReadOnly;
        public int Count => UnderlyingCollection.Count;
        public IDictionaryEnumerator GetEnumerator() => UnderlyingCollection.GetEnumerator();
        public void Insert(int index, T key, TK value) => UnderlyingCollection.Insert(index, key, value);
        public void RemoveAt(int index) => UnderlyingCollection.RemoveAt(index);
        public bool Contains(T key) => UnderlyingCollection.Contains(key);
        public void Add(T key, TK value) => UnderlyingCollection.Add(key, value);
        public void Clear() => UnderlyingCollection.Clear();
        public void Remove(T key) => UnderlyingCollection.Remove(key);
        public void CopyTo(Array array, int index) => UnderlyingCollection.CopyTo(array, index);
    }
}