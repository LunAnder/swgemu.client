using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using SWG.Client.Network;
using UnityEngine;

namespace SWG.Client.Utils
{
    public static class MessageListExtensions
    {
        public static List<Message> WaitForMessages(this List<Message> queue, Type errorType, params Type[] types)
        {
            return queue.WaitForMessages(1000, errorType, types);
        }

        public static List<Message> WaitForMessages(this List<Message> queue, int timeout, Type errorType, params Type[] types)
        {
            List<Message> messages = new List<Message>();

            var timeoutTime = DateTime.Now.AddMilliseconds(timeout);

            while (true)
            {
                if (timeoutTime <= DateTime.Now)
                {
                    return null;
                }

                messages.AddRange(queue.Where(cur => !messages.Contains(cur) && (cur.GetType() == errorType || types.Contains(cur.GetType()))));

                if (messages.Count == types.Length || messages.Any(cur => cur.GetType() == errorType))
                {
                    break;
                }

                Thread.Sleep(timeout / 10);
            }

            return messages;
        }

        public static List<Message> WaitForMessages(this List<Message> queue, int timeout, MessageOp errorType, params MessageOp[] types)
        {
            List<Message> messages = new List<Message>();

            var neededTypes = types.ToList();
            bool error = false;

            var timeoutTime = DateTime.Now.AddMilliseconds(timeout);

            while (true)
            {
                if (timeoutTime <= DateTime.Now)
                {
                    return null;
                }

                messages.AddRange(queue.Where(cur => !messages.Contains(cur) && (cur.MessageOpCodeEnum == errorType || types.Contains(cur.MessageOpCodeEnum))));

                if (messages.Count == types.Length || messages.Any(cur => cur.MessageOpCodeEnum == errorType))
                {
                    break;
                }

                Thread.Sleep(timeout / 10);
            }

            return messages;
        }


        public static List<Message> WaitForMessages(this List<Message> queue, int timeout, uint errorType, params uint[] types)
        {
            List<Message> messages = new List<Message>();

            var neededTypes = types.ToList();
            bool error = false;

            var timeoutTime = DateTime.Now.AddMilliseconds(timeout);

            while (true)
            {
                if (timeoutTime <= DateTime.Now)
                {
                    return null;
                }

                messages.AddRange(queue.Where(cur => !messages.Contains(cur) && (cur.MessageOpCode == errorType || types.Contains(cur.MessageOpCode))));

                if (messages.Count == types.Length || messages.Any(cur => cur.MessageOpCode == errorType))
                {
                    break;
                }

                Thread.Sleep(timeout / 10);
            }

            return messages;
        }
    }
}
