using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SWG.Client.Network.Abstracts
{

    public class TemporaryReadDisposeable : IDisposable
    {
        private IDataContainerRead _dataContainer;
        private int _toResetTo = 1;

        public TemporaryReadDisposeable(IDataContainerRead dataContainer)
        {
            _dataContainer = dataContainer;
            _toResetTo = dataContainer.ReadIndex;
        }

        public void Dispose()
        {
            _dataContainer.ReadIndex = _toResetTo;
        }
    }
}
