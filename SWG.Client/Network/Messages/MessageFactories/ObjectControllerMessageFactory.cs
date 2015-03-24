using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogAbstraction;
using SWG.Client.Utils.Attribute;

namespace SWG.Client.Network.Messages.MessageFactories
{
    [MessageFactory(MessageOp.ObjControllerMessage)]
    public class ObjectControllerMessageFactory : MessageFactoryWithSecondaryOpCodeBase
    {
        private static readonly ILogger _logger = LogManagerFacad.GetCurrentClassLogger();

        protected override ILogger Logger
        {
            get { return _logger; }
        }
    }
}
