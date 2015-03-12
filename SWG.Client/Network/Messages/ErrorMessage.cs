﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SWG.Client.Utils;

namespace SWG.Client.Network.Messages
{
    [RegisterMessage(MessageOp.ErrorMessage)]
    public class ErrorMessage : Message
    {
        public string ErrorType { get; set; }
        public string Message { get; set; }

        public bool Fatal { get; set; }

        public ErrorMessage()
        {
        }

        public ErrorMessage(Message ToCreateFrom, bool ParseFromData = false) 
            :base(ToCreateFrom.Data,ToCreateFrom.Size,ParseFromData)
        {
        }

        public override bool ParseFromData()
        {
            ReadIndex = 6;

            ErrorType = ReadString(Encoding.ASCII);
            Message = ReadString(Encoding.ASCII);
            Fatal = ReadByte() != 0;
            
            return true;
        }
    }
}
