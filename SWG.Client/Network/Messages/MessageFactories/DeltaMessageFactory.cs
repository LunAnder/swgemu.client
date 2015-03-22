using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogAbstraction;
using SWG.Client.Network.Messages.Base;
using SWG.Client.Utils;
using SWG.Client.Utils.Attribute;

namespace SWG.Client.Network.Messages.MessageFactories
{
    

    [MessageFactory(MessageOp.DeltasMessage)]
    public class DeltaMessageFactory : MessageFactoryWithSecondaryOpCodeBase
    {
        private static readonly ILogger _logger = LogAbstraction.LogManagerFacad.GetCurrentClassLogger();

        protected override ILogger Logger
        {
            get { return _logger; }
        }
    }
}
