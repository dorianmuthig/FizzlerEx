using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fizzler
{
    internal class CachingDictionary<TInput, TResult>
    {
        public CachingDictionary(Func<TInput, TResult> evalutor)
            : this(evalutor, 30)
        {
        }

        public CachingDictionary(Func<TInput, TResult> evalutor, int capacity)
        {
            this.capacity = capacity;
        }

        private int capacity;
        private Dictionary<TInput, TResult> dictionary = new Dictionary<TInput, TResult>();
        private Queue<TInput> queue = new Queue<TInput>();


        //public TResult GetValue(TInput input)
        //{
        //    TResult result;
        //    if (dictionary.TryGetValue(input, out result))
        //    {
        //        return result;
        //    }
        //}


    }
}
