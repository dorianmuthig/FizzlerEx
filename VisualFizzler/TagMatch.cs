using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualFizzler
{
    public struct TagMatch
    {
        private int _start;
        private int _length;

        public TagMatch(int start, int length)
        {
            this._start = start;
            this._length = length;
        }

        public int Index { get { return _start; } }
        public int Length { get { return _length; } }
    }
}
