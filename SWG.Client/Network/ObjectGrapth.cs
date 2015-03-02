using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SWG.Client.Network.Abstracts;
using SWG.Client.Network.Messages;
using SWG.Client.Network.Messages.Zone.Cell;
using SWG.Client.Network.Messages.Zone.Creature;
using SWG.Client.Network.Messages.Zone.Intangible;
using SWG.Client.Network.Messages.Zone.Player;
using SWG.Client.Network.Messages.Zone.Static;
using SWG.Client.Network.Messages.Zone.Tangible;



namespace SWG.Client.Network
{
    public class ObjectGraph : ServiceBase
    {

        private static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        //private  Dictionary<long,Message> messages = new Dictionary<long, Message>();
        public List<Message> messages = new List<Message>(); 

        public Session Session { get; set; }

        protected override void DoWork()
        {
            while (Session.IncomingMessageQueue.Count > 0)
            {
                var msg = Session.IncomingMessageQueue.Dequeue();
                switch (msg?.MessageOpCodeEnum ?? MessageOp.Null)
                {
                    case MessageOp.BaselinesMessage:
                        var baseineParsed = ParseBaselineMessage(msg);
                        if (baseineParsed != null)
                        {
                            //messages.Add(baseineParsed.ObjectId, baseineParsed);
                            messages.Add(baseineParsed);
                            _logger.Trace("{0}{1} BaselineMessage", (MessageOp)baseineParsed.ObjectType, baseineParsed.TypeNumber);
                        }
                    break;
                    case MessageOp.DeltasMessage:
                        var deltaParsed = ParseDeltaMessage(msg);
                        if (deltaParsed != null)
                        {
                            //messages.Add(deltaParsed.ObjectId, deltaParsed);
                            messages.Add(deltaParsed);
                            _logger.Trace("{0}{1} DeltaMessage", (MessageOp)deltaParsed.ObjectType, deltaParsed.TypeNumber); 
                        }
                        break;
                    case MessageOp.ErrorMessage:
                        var errMsg = new ErrorMessage(msg,true);

                        _logger.Error("Recieved error : {0} (Fatal: {1})", errMsg.Message, errMsg.Fatal);
                        break;
                    case MessageOp.Null:
                        _logger.Error("Got null opcode");
                        break;
                    default:
                         _logger.Warn("Got unknown message : {0}", msg);
                        break;
                }
            }

            System.Threading.Thread.Sleep(300);
        }

        #region Parse Baseline

        protected BaselineMessage ParseBaselineMessage(Message Msg)
        {
            Msg.ReadIndex = 14;
            MessageOp primaryType = (MessageOp)Msg.ReadInt32();

            switch (primaryType)
            {
                case MessageOp.CREO:
                    return ParseBaselineCREOMessage(Msg);
                case MessageOp.SCLT:
                    return ParseBaselineSCLTMessage(Msg);
                case MessageOp.PLAY:
                    return ParseBaselinePLAYMessage(Msg);
                case MessageOp.STAO:
                    return ParseBaselineSTAOMessage(Msg);
                case MessageOp.TANO:
                    return ParseBaselineTANOMessage(Msg);
                case MessageOp.ITNO:
                    return ParseBaselineITNOMessage(Msg);
            }

            return null;
        }


        protected BaselineMessage ParseBaselineITNOMessage(Message Msg)
        {
            byte secondayType = Msg.ReadByte();
            switch (secondayType)
            {
                case 0x03:
                    return new IntangibleObjectMessage3(Msg, true);
                case 0x06:
                    return new IntangibleObjectMessage6(Msg, true);
            }
            return null;
        }


        protected BaselineMessage ParseBaselineTANOMessage(Message Msg)
        {
            byte secondayType = Msg.ReadByte();
            switch (secondayType)
            {
                case 0x03:
                    return new TangibleObjectMessage3(Msg, true);
                case 0x06:
                    return new TangibleObjectMessage6(Msg, true);
                case 0x07:
                    return new TangibleObjectMessage7(Msg, true);
            }
            return null;
        }


        protected BaselineMessage ParseBaselineSTAOMessage(Message Msg)
        {
            byte secondayType = Msg.ReadByte();
            switch (secondayType)
            {
                case 0x03:
                    return new StaticObjectMessage3(Msg,true);
            }
            return null;
        }


        protected BaselineMessage ParseBaselinePLAYMessage(Message Msg)
        {
            byte secondayType = Msg.ReadByte();
            switch (secondayType)
            {
                case 0x03:
                    return new PlayerObjectMessage3(Msg,true);
                case 0x06:
                    return new PlayerObjectMessage6(Msg,true);
                case 0x08:
                    return new PlayerObjectMessage8(Msg,true);
                case 0x09:
                    return new PlayerObjectMessage9(Msg,true);
            }
            return null;
        }
        
        protected BaselineMessage ParseBaselineSCLTMessage(Message Msg)
        {
            byte secondayType = Msg.ReadByte();
            switch (secondayType)
            {
                case 0x03:
                    return new CellObjectMessage3(Msg,true);
            }
            return null;
        }


        protected BaselineMessage ParseBaselineCREOMessage(Message Msg)
        {
            byte secondayType = Msg.ReadByte();
            switch (secondayType)
            {
                case 0x01:
                    return new CreatureObjectMessage1(Msg,true);
                case 0x03:
                    return new CreatureObjectMessage3(Msg,true);
                case 0x04:
                    return new CreatureObjectMessage4(Msg,true);
                case 0x06:
                    return new CreatureObjectMessage6(Msg,true);
            }
            return null;
        }
        #endregion

        #region Parse Delta

        protected DeltaMessage ParseDeltaMessage(Message Msg)
        {
            Msg.ReadIndex = 14;
            MessageOp primaryType = (MessageOp)Msg.ReadInt32();

            switch (primaryType)
            {
                case MessageOp.CREO:
                    return ParseDeltaCREOMessage(Msg);
                case MessageOp.SCLT:
                    return ParseDeltaSCLTMessage(Msg);
                case MessageOp.PLAY:
                    return ParseDeltaPLAYMessage(Msg);
                case MessageOp.STAO:
                    return ParseDeltaSTAOMessage(Msg);
                case MessageOp.TANO:
                    return ParseDeltaTANOMessage(Msg);
                case MessageOp.ITNO:
                    return ParseDeltaITNOMessage(Msg);
            }

            return null;
        }


        private DeltaMessage ParseDeltaITNOMessage(Message Msg)
        {
            byte secondayType = Msg.ReadByte();
            switch (secondayType)
            {
                case 0x03:
                    return new IntangibleObjectDeltaMessage3(Msg, true);
            }
            return null;
        }


        private DeltaMessage ParseDeltaTANOMessage(Message Msg)
        {
            byte secondayType = Msg.ReadByte();
            switch (secondayType)
            {
                case 0x03:
                    return new TangibleObjectDeltaMessage3(Msg, true);
                case 0x06:
                    return new TangibleObjectDeltaMessage6(Msg, true);
            }
            return null;
        }


        private DeltaMessage ParseDeltaSTAOMessage(Message Msg)
        {
            byte secondayType = Msg.ReadByte();
            switch (secondayType)
            {
            }
            return null;
        }


        private DeltaMessage ParseDeltaPLAYMessage(Message Msg)
        {
            byte secondayType = Msg.ReadByte();
            switch (secondayType)
            {
                case 0x03:
                    return new PlayerObjectDeltaMessage3(Msg, true);
                case 0x06:
                    return new PlayerObjectDeltaMessage6(Msg, true);
                case 0x08:
                    return new PlayerObjectDeltaMessage8(Msg, true);
                case 0x09:
                    return new PlayerObjectDeltaMessage9(Msg, true);
            }
            return null;
        }


        private DeltaMessage ParseDeltaSCLTMessage(Message Msg)
        {
            byte secondayType = Msg.ReadByte();
            switch (secondayType)
            {
            }
            return null;
        }


        private DeltaMessage ParseDeltaCREOMessage(Message Msg)
        {
            byte secondayType = Msg.ReadByte();
            switch (secondayType)
            {
                case 0x01:
                    return new CreatureObjectDeltaMessage1(Msg, true);
                case 0x03:
                    return new CreatureObjectDeltaMessage3(Msg, true);
                case 0x04:
                    return new CreatureObjectDeltaMessage4(Msg, true);
                case 0x06:
                    return new CreatureObjectDeltaMessage6(Msg, true);
            }
            return null;
        }

        #endregion
    }
}
