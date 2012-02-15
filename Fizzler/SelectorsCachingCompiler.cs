namespace Fizzler
{
    #region Imports

    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    #endregion

    /// <summary>
    /// Implementation for a selectors compiler that supports caching.
    /// </summary>
    /// <remarks>
    /// This class is primarily targeted for developers of selection
    /// over an arbitrary document model.
    /// </remarks>
    public static class SelectorsCachingCompiler
    {
        /// <summary>
        /// Creates a caching selectors compiler on top on an existing compiler.
        /// </summary>
        public static Func<string, T> Create<T>(Func<string, T> compiler)
        {
            if (compiler == null)
                throw new ArgumentNullException();
            Debug.Assert(compiler != null);
            var cache = new LRUCache<string, T>(compiler, 30);
            return selector => cache.GetValue(selector);
        }
    }
}