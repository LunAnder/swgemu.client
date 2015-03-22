using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SWG.Client.Utils;
using SWG.Client.Utils.Attribute;

namespace SWG.Client.Network.Messages.Zone
{
    [RegisterMessage(MessageOp.CmdStartScene)]
    public class SceneStart : Message
    {
        public byte IgnoreLayoutFiles { get; set; }
        public long CharacterId { get; set; }
        public string TerrainMap { get; set; }
        public float XCoord { get; set; }
        public float YCoord { get; set; }
        public float ZCoord { get; set; }
        public string SharedRaceTemplate { get; set; }
        public long GalaticTime { get; set; }

        public SceneStart()
        {
            
        }

        public SceneStart(Message message, bool parseFromData = false)
            : base(message.Data, message.Size, parseFromData)
        {

        }

        public override bool ParseFromData()
        {
            ReadIndex = 6;

            IgnoreLayoutFiles = ReadByte();
            CharacterId = ReadInt64();
            TerrainMap = ReadString(Encoding.ASCII);
            XCoord = ReadFloat();
            ZCoord = ReadFloat();
            YCoord = ReadFloat();
            SharedRaceTemplate = ReadString(Encoding.ASCII);
            GalaticTime = ReadInt64();

            return true;
        }
    }
}
