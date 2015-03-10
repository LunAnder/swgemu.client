using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Configuration;
using System.Runtime.InteropServices;
using System.Text;

using SWG.Client.Network.Abstracts;
using SWG.Client.Network.Messages;
using SWG.Client.Network.Messages.Zone.Cell;
using SWG.Client.Network.Messages.Zone.Creature;
using SWG.Client.Network.Messages.Zone.Intangible;
using SWG.Client.Network.Messages.Zone.Player;
using SWG.Client.Network.Messages.Zone.Static;
using SWG.Client.Network.Messages.Zone.Tangible;
using SWG.Client.Utils;


namespace SWG.Client.Network
{
    public class ObjectGraph : ServiceBase
    {
        public readonly ConcurrentDictionary<uint, Func<Message, Message>> RegisteredObjects =
            new ConcurrentDictionary<uint, Func<Message, Message>>();

        public readonly ConcurrentDictionary<uint, Func<Message, Message>> RegisteredBaselines =
            new ConcurrentDictionary<uint, Func<Message, Message>>();

        public readonly ConcurrentDictionary<uint, Func<Message, Message>> RegisteredDeltas =
            new ConcurrentDictionary<uint, Func<Message, Message>>();

        //private static readonly LogAbstraction.ILogger _logger = LogAbstraction.LogManagerFacad.GetCurrentClassLogger();
        private static readonly LogAbstraction.ILogger _logger = LogAbstraction.LogManagerFacad.GetCurrentClassLogger();

        //private  Dictionary<long,Message> messages = new Dictionary<long, Message>();
        public List<Message> messages = new List<Message>(); 

        public Session Session { get; set; }


        public ObjectGraph() 
            : this(false)
        {
            
        }

        public ObjectGraph(bool registerCreateFromAssemblies)
        {
            if (registerCreateFromAssemblies)
            {
                RegisterCreatesFromAssembly();
            }
        }

        protected override void DoWork()
        {
            while (Session.IncomingMessageQueue.Count > 0)
            {
                var msg = Session.IncomingMessageQueue.Dequeue();
                //switch (msg.MessageOpCodeEnum)
                //{
                //    case MessageOp.BaselinesMessage:
                //        var baseineParsed = ParseBaselineMessage(msg);
                //        if (baseineParsed != null)
                //        {
                //            //messages.Add(baseineParsed.ObjectId, baseineParsed);
                //            messages.Add(baseineParsed);
                //            _logger.Trace("{0}{1} BaselineMessage", (MessageOp)baseineParsed.ObjectType, baseineParsed.TypeNumber);
                //        }
                //        break;
                //    case MessageOp.DeltasMessage:
                //        var deltaParsed = ParseDeltaMessage(msg);
                //        if (deltaParsed != null)
                //        {
                //            //messages.Add(deltaParsed.ObjectId, deltaParsed);
                //            messages.Add(deltaParsed);
                //            _logger.Trace("{0}{1} DeltaMessage", (MessageOp)deltaParsed.ObjectType, deltaParsed.TypeNumber);
                //        }
                //        break;
                //    case MessageOp.ErrorMessage:
                //        var errMsg = new ErrorMessage(msg, true);

                //        _logger.Error("Recieved error : {0} (Fatal: {1})", errMsg.Message, errMsg.Fatal);
                //        break;
                //    case MessageOp.Null:
                //        _logger.Error("Got null opcode");
                //        break;
                //    default:
                //        _logger.Warn("Got unknown message : {0}", msg);
                //        break;
                //}

                Func<Message, Message> createFunc = null;
                Message transformed = null;
                if (RegisteredObjects.TryGetValue(msg.MessageOpCode, out createFunc) &&
                    (transformed = createFunc(msg)) != null)
                {
                    messages.Add(createFunc(msg));
                }
                else
                {
                    _logger.Warn("Unable to find registered message factory for {0}", msg.MessageOpCodeEnum);
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

        public bool RegisterMessageObject(MessageOp opcode, Func<Message, Message> createFunc)
        {
            return RegisterMessageObject((uint)opcode, createFunc);
        }

        public bool RegisterMessageObject(uint opcode, Func<Message, Message> createFunc)
        {
            return RegisteredObjects.TryAdd(opcode, createFunc);
        }

        public bool RegisterMessageObject<T>(uint opcode)
            where T : Message, new()
        {
            return RegisterMessageObject(opcode, (msg) => Message.Create<T>(msg));
        }

        public bool RegisterMessageObject<T>(MessageOp opcode)
            where T : Message, new()
        {
            return RegisterMessageObject<T>((uint) opcode);
        }

        public bool RegisterBaselineObject<T>(uint opcode, uint secondary)
            where T : BaselineMessage, new()
        {
            return RegisteredBaselines.TryAdd(opcode + secondary, (msg) => Message.Create<T>(msg));
        }

        public bool RegisterBaselineObject<T>(MessageOp opcode, uint secondary)
            where T : BaselineMessage, new()
        {
            return RegisterBaselineObject<T>((uint) opcode, secondary);
        }

        public bool RegisterDeltaObject<T>(uint opcode, uint secondary)
            where T : DeltaMessage, new()
        {
            return RegisteredDeltas.TryAdd(opcode + secondary, (msg) => Message.Create<T>(msg));
        }

        public bool RegisterDeltaObject<T>(MessageOp opcode, uint secondary)
            where T : DeltaMessage, new()
        {
            return RegisterDeltaObject<T>((uint)opcode, secondary);
        }


        public void RegisterCreatesFromAssembly()
        {

            RegisterMessageObject(MessageOp.BaselinesMessage, HandleBaselineMessage);
            RegisterMessageObject(MessageOp.DeltasMessage, HandleDeltaMessage);

            var registerMethodInfo = GetType().GetMethod("RegisterMessageObject", new[] { typeof(uint) });

            foreach (
                var type in
                    GetType()
                        .Assembly.GetTypes()
                        .Where(cur => cur.GetCustomAttributes(typeof (RegisterMessageAttribute), false).Length >= 1))
            {
                var attr =
                    (RegisterMessageAttribute)
                        type.GetCustomAttributes(typeof(RegisterMessageAttribute), false).First();

                var genericRegister = registerMethodInfo.MakeGenericMethod(type);
                genericRegister.Invoke(this, new object[] {attr.OpCode});
            }


            registerMethodInfo = GetType().GetMethod("RegisterBaselineObject", new[] { typeof(uint), typeof(uint) });

            foreach (
                var type in
                    GetType()
                        .Assembly.GetTypes()
                        .Where(cur => cur.GetCustomAttributes(typeof(RegisterBaselineMessageAttribute), false).Length >= 1))
            {
                var attr =
                    (RegisterBaselineMessageAttribute)
                        type.GetCustomAttributes(typeof(RegisterBaselineMessageAttribute), false).First();

                var genericRegister = registerMethodInfo.MakeGenericMethod(type);
                genericRegister.Invoke(this, new object[] { attr.OpCode, attr.Secondary.GetValueOrDefault()  });
            }

            registerMethodInfo = GetType().GetMethod("RegisterDeltaObject", new[] { typeof(uint), typeof(uint) });

            foreach (
                var type in
                    GetType()
                        .Assembly.GetTypes()
                        .Where(cur => cur.GetCustomAttributes(typeof(RegisterDeltaMessageAttribute), false).Length >= 1))
            {
                var attr =
                    (RegisterDeltaMessageAttribute)
                        type.GetCustomAttributes(typeof(RegisterDeltaMessageAttribute), false).First();


                var genericRegister = registerMethodInfo.MakeGenericMethod(type);
                genericRegister.Invoke(this, new object[] { attr.OpCode, attr.Secondary.GetValueOrDefault() });
            }


        }

        protected virtual Message HandleBaselineMessage(Message message)
        {
            message.ReadIndex = 14;
            uint opcode = message.ReadUInt32();
            uint secondary = message.ReadByte();
            uint key = opcode + secondary;

            Func<Message, Message> createFactory = null;
            if (RegisteredBaselines.TryGetValue(key, out createFactory))
            {
                return createFactory(message);
            }

            return null;
        }

        protected virtual Message HandleDeltaMessage(Message message)
        {
            message.ReadIndex = 14;
            uint opcode = message.ReadUInt32();
            uint secondary = message.ReadByte();
            uint key = opcode + secondary;

            Func<Message, Message> createFactory = null;
            if (RegisteredDeltas.TryGetValue(key, out createFactory))
            {
                return createFactory(message);
            }

            return null;
        }


    }
}
