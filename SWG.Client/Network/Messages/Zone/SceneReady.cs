using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SWG.Client.Network.Messages.Zone
{
    public class SceneReady : Message
    {
        public static ushort OPCODECOUNT = 1;
        public static MessageOp OPCODE = MessageOp.CmdSceneReady;

        public byte Afk { get; set; }

        public SceneReady()
            : base(6)
        {
        }

        public override bool AddFieldsToData()
        {
            OpcodeCount = OPCODECOUNT;
            MessageOpCodeEnum = OPCODE;

            WriteIndex = 6;

            return true;
        }
    }
}
