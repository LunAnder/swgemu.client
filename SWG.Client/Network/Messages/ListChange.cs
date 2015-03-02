using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SWG.Client.Network.Messages
{
    public class ListChange<T>
    {
        private int _index = -1;

        public ListChangeOperation Operation { get; set; }
        public T Value { get; set; }
        public T[] Values { get; set; }

        public int Index
        {
            get { return _index; }
            set { _index = value; }
        }
    }
}
