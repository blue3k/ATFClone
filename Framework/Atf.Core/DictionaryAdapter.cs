//Copyright © 2018 Kyungkun Ko

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Sce.Atf
{
    /// <summary>
    /// IDictionary adapter to other types
    /// Note: ValueS and ValueT should be safe to be casted forcedly
    /// </summary>
    /// <typeparam name="KeyType">Underlying dictionary key type</typeparam>
    /// <typeparam name="ValueS">Underlying dictionary value type</typeparam>
    /// <typeparam name="ValueT">Adapted dictionary value type</typeparam>
    public class DictionaryAdapter<KeyType, ValueT, ValueS> : IDictionary<KeyType, ValueT>
            //where ValueT : class
            //where ValueS : class
    {
        /// <summary>Constructor</summary>
        /// <param name="list">List to adapt, must be non-null</param>
        public DictionaryAdapter(IDictionary<KeyType, ValueS> src)
        {
            if (src == null)
                throw new ArgumentNullException("src");
            m_SrcDictionary = src;
        }


        public ICollection<KeyType> Keys
        {
            get { return m_SrcDictionary.Keys.Cast<KeyType>().ToList<KeyType>(); }
        }

        public ICollection<ValueT> Values => m_SrcDictionary.Values.Cast<ValueT>().ToList<ValueT>();

        public int Count => m_SrcDictionary.Count;

        public bool IsReadOnly => m_SrcDictionary.IsReadOnly;

        public ValueT this[KeyType key] { get => (ValueT)(object)m_SrcDictionary[key]; set => m_SrcDictionary[key] = (ValueS)(object)value; }

        public bool ContainsKey(KeyType key)
        {
            return m_SrcDictionary.ContainsKey(key);
        }

        public void Add(KeyType key, ValueT value)
        {
            m_SrcDictionary.Add(key, (ValueS)(object)value);
        }

        public bool Remove(KeyType key)
        {
            return m_SrcDictionary.Remove(key);
        }

        public bool TryGetValue(KeyType key, out ValueT value)
        {
            ValueS orgValue;
            if(m_SrcDictionary.TryGetValue(key, out orgValue))
            {
                value = (ValueT)(object)orgValue;
                return true;
            }

            value = default(ValueT);
            return false;
        }

        public void Add(KeyValuePair<KeyType, ValueT> item)
        {
            m_SrcDictionary.Add(item.Key, (ValueS)(object)item.Value);
        }

        public void Clear()
        {
            m_SrcDictionary.Clear();
        }

        public bool Contains(KeyValuePair<KeyType, ValueT> item)
        {
            ValueS orgValue;
            if (m_SrcDictionary.TryGetValue(item.Key, out orgValue))
            {
                return item.Value.Equals(orgValue);
            }

            return false;
        }

        public void CopyTo(KeyValuePair<KeyType, ValueT>[] array, int arrayIndex)
        {
            m_SrcDictionary.ToArray().Cast<KeyValuePair<KeyType, ValueT>>().ToList().CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<KeyType, ValueT> item)
        {
            ValueS orgValue;
            if (m_SrcDictionary.TryGetValue(item.Key, out orgValue))
            {
                if(item.Value.Equals(orgValue))
                {
                    m_SrcDictionary.Remove(item.Key);
                    return true;
                }
            }

            return false;
        }

        public IEnumerator<KeyValuePair<KeyType, ValueT>> GetEnumerator()
        {
            return m_SrcDictionary.ToArray().Cast<KeyValuePair<KeyType, ValueT>>().ToList().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return m_SrcDictionary.GetEnumerator();
        }

        private readonly IDictionary<KeyType, ValueS> m_SrcDictionary;

    }
}
