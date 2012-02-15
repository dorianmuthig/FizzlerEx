using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fizzler
{
    internal class CachingDictionary<TInput, TResult>
    {
        public CachingDictionary(Func<TInput, TResult> evalutor)
            : this(evalutor, 31)
        {
        }

        public CachingDictionary(Func<TInput, TResult> evalutor, int capacity)
        {
            this.capacity = capacity;
            this.dictionary = new Dictionary<TInput, TResult>(capacity);
            this.queue = new PriorityQueue<TInput>(capacity);
            this.evalutor = evalutor;
        }

        private int capacity;
        private Dictionary<TInput, TResult> dictionary;
        private PriorityQueue<TInput> queue;
        private Func<TInput, TResult> evalutor;


        public TResult GetValue(TInput input)
        {
            TResult result;
            if (dictionary.TryGetValue(input, out result))
            {
                return result;
            }
        }


    }
}
