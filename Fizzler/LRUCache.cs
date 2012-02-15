using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace Fizzler
{
    internal class LRUCache<TKey, TValue>
    {

        Dictionary<TKey, TValue> data;
        IndexedLinkedList<TKey> lruList = new IndexedLinkedList<TKey>();
        ICollection<KeyValuePair<TKey, TValue>> dataAsCollection;
        int capacity;

        public LRUCache(int capacity)
        {

            if (capacity <= 0)
            {
                throw new ArgumentException("capacity should always be bigger than 0");
            }

            data = new Dictionary<TKey, TValue>(capacity);
            dataAsCollection = data;
            this.capacity = capacity;
        }

        public bool Remove(TKey key)
        {
            bool existed = data.Remove(key);
            lruList.Remove(key);
            return existed;
        }

        public TValue GetValue(TKey key)
        {
            var value = data[key];
            lruList.Remove(key);
            lruList.Add(key);
            return value;
        }

        private void SetValue(TKey key, TValue value)
        {
            data[key] = value;
            lruList.Remove(key);
            lruList.Add(key);

            if (data.Count > capacity)
            {
                Remove(lruList.First);
                lruList.RemoveFirst();
            }
        }

        public void Clear()
        {
            data.Clear();
            lruList.Clear();
        }




        private class IndexedLinkedList<T>
        {

            LinkedList<T> data = new LinkedList<T>();
            Dictionary<T, LinkedListNode<T>> index = new Dictionary<T, LinkedListNode<T>>();

            public void Add(T value)
            {
                index[value] = data.AddLast(value);
            }

            public void RemoveFirst()
            {
                index.Remove(data.First.Value);
                data.RemoveFirst();
            }

            public void Remove(T value)
            {
                LinkedListNode<T> node;
                if (index.TryGetValue(value, out node))
                {
                    data.Remove(node);
                    index.Remove(value);
                }
            }

            public int Count
            {
                get
                {
                    return data.Count;
                }
            }

            public void Clear()
            {
                data.Clear();
                index.Clear();
            }

            public T First
            {
                get
                {
                    return data.First.Value;
                }
            }
        }




    }



}
