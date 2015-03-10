using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SWG.Client.Utils;

namespace SWG.Client.Network.Messages.Zone.Cell
{
    [RegisterMessage(MessageOp.UpdateCellPermissionMessage)]
    public class UpdateCellPermissionsMessage : Message
    {
        public byte Permission { get; set; }
        public long CellId { get; set; }

        public UpdateCellPermissionsMessage() { }

        public UpdateCellPermissionsMessage(Message message, bool parseFromData = false)
            : base(message.Data, message.Size, parseFromData)
        {
        }

        public override bool ParseFromData()
        {

            ReadIndex = 6;

            Permission = ReadByte();
            CellId = ReadInt64();


            return true;
        }
    }
}
