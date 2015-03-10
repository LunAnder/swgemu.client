using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SWG.Client.Network.Messages.Zone
{
    public class ClientInactivityMessage : Message
    {
        public static ushort OPCODECOUNT = 2;
        public static MessageOp OPCODE = MessageOp.ClientInactivity;

        public byte Afk { get; set; }

        public ClientInactivityMessage(byte Afk) : base(7)
        {
            this.Afk = Afk;
        }

        public ClientInactivityMessage(bool Afk)
             : this(Afk ? (byte)0x1 : (byte)0x0)
        {
        }

        public override bool AddFieldsToData()
        {
            OpcodeCount = OPCODECOUNT;
            MessageOpCodeEnum = OPCODE;

            WriteIndex = 6;

            AddData(Afk);

            return true;
        }
    }
}
