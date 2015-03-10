using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SWG.Client.Network
{
    public class DataContainerBase : IDataContainer
    {
        protected byte[] _data = null;

        public byte[] Data
        {
            get { return _data; }
            set { _data = value; }
        }

        protected void ResizeTo(int Size)
        {
            Array.Resize(ref _data, Size);
        }
    }
}
