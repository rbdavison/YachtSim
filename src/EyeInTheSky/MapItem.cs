using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeInTheSky
{
    internal class MapItem
    {
        internal MapItem(int mapHandle)
        {
            this.Handle = mapHandle;
        }

        internal int Handle { get; private set; }

        public override string ToString()
        {
            return Handle.ToString();
        }
    }
}
