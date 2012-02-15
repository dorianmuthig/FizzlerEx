using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace Fizzler
{
    internal class LRUCache<TInput, TResult>
    {

        private Dictionary<TInput, TResult> data;
        private IndexedLinkedList<TInput> lruList = new IndexedLinkedList<TInput>();
        private Func<TInput, TResult> evalutor;
        private int capacity;

        public LRUCache(Func<TInput, TResult> evalutor, int capacity)
        {
            if (capacity <= 0)
                throw new ArgumentOutOfRangeException();

            this.data = new Dictionary<TInput, TResult>(capacity);
            this.capacity = capacity;
            this.evalutor = evalutor;
        }

        public bool Remove(TInput key)
        {
            bool existed = data.Remove(key);
            lruList.Remove(key);
            return existed;
        }

        public TResult GetValue(TInput key)
        {
            TResult value;
            if (data.TryGetValue(key, out value))
            {
                lruList.Remove(key);
                lruList.Add(key);
            }
            else
            {
                value = evalutor(key);
                data[key] = value;
                lruList.Add(key);

                if (data.Count > capacity)
                {
                    Remove(lruList.First);
                    lruList.RemoveFirst();
                }
            }

            return value;
        }

        public void Clear()
        {
            data.Clear();
            lruList.Clear();
        }

        public int Capacity
        {
            get
            {
                return capacity;
            }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException();
                capacity = value;
                while (data.Count > capacity)
                {
                    Remove(lruList.First);
                    lruList.RemoveFirst();
                }
            }
        }




        private class IndexedLinkedList<T>
        {

            private LinkedList<T> data = new LinkedList<T>();
            private Dictionary<T, LinkedListNode<T>> index = new Dictionary<T, LinkedListNode<T>>();

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
