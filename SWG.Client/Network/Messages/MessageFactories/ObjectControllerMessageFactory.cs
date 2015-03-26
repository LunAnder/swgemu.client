using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogAbstraction;
using SWG.Client.Network.Messages.Zone.Object;
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

        public override bool TryParse(uint messageOp, Message baseMessage, out Message parsedMessage)
        {

            var objCMessage = new ObjectControllerMessage(baseMessage, true);
            parsedMessage = objCMessage;
            _logger.Trace("Type: {0:X}, Flags: {1}, ObjectId: {2}, Ticks: {3}.", objCMessage.ControllerType,
                objCMessage.FlagsEnum, objCMessage.ObjectId, objCMessage.Ticks);
            return true;

        }
    }
}
