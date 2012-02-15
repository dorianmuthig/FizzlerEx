//**********************************************************
//* PriorityQueue                                          *
//* Copyright (c) Julian M Bucknall 2004                   *
//* All rights reserved.                                   *
//* This code can be used in your applications, providing  *
//*    that this copyright comment box remains as-is       *
//**********************************************************
//* .NET priority queue class (heap algorithm)             *
//**********************************************************

using System;
using System.Collections;
using System.Security.Permissions;
using System.Runtime.Serialization;

namespace Fizzler
{

    [Serializable]
    internal struct HeapEntry<T>
    {
        private T item;
        private IComparable priority;
        public HeapEntry(T item, IComparable priority)
        {
            this.item = item;
            this.priority = priority;
        }
        public T Item
        {
            get { return item; }
        }
        public IComparable Priority
        {
            get { return priority; }
        }
        public void Clear()
        {
            item = default(T);
            priority = null;
        }
    }

    [Serializable]
    internal class PriorityQueue<T>
    {
        private int count;
        private int capacity;
        private HeapEntry<T>[] heap;

        public PriorityQueue()
        {
            capacity = 15; // 15 is equal to 4 complete levels
            heap = new HeapEntry<T>[capacity];
        }

        public T Dequeue()
        {
            if (count == 0)
                throw new InvalidOperationException();

            T result = heap[0].Item;
            count--;
            TrickleDown(0, heap[count]);
            heap[count].Clear();
            return result;
        }

        public void Enqueue(T item, IComparable priority)
        {
            if (priority == null)
                throw new ArgumentNullException("priority");
            if (count == capacity)
                GrowHeap();
            count++;
            BubbleUp(count - 1, new HeapEntry<T>(item, priority));
        }

        private void BubbleUp(int index, HeapEntry<T> he)
        {
            int parent = GetParent(index);
            // note: (index > 0) means there is a parent
            while ((index > 0) &&
                  (heap[parent].Priority.CompareTo(he.Priority) < 0))
            {
                heap[index] = heap[parent];
                index = parent;
                parent = GetParent(index);
            }
            heap[index] = he;
        }

        private int GetLeftChild(int index)
        {
            return (index * 2) + 1;
        }

        private int GetParent(int index)
        {
            return (index - 1) / 2;
        }

        private void GrowHeap()
        {
            capacity = (capacity * 2) + 1;
            var newHeap = new HeapEntry<T>[capacity];
            System.Array.Copy(heap, 0, newHeap, 0, count);
            heap = newHeap;
        }

        private void TrickleDown(int index, HeapEntry<T> he)
        {
            int child = GetLeftChild(index);
            while (child < count)
            {
                if (((child + 1) < count) &&
                    (heap[child].Priority.CompareTo(heap[child + 1].Priority) < 0))
                {
                    child++;
                }
                heap[index] = heap[child];
                index = child;
                child = GetLeftChild(index);
            }
            BubbleUp(index, he);
        }


    }
}
