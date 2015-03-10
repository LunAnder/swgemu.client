using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using SWG.Client.Network;

namespace SWG.Client.Utils
{
    public static class MessageQueueExtensions
    {

        public static List<Message> WaitForMessages(this Queue<Message> queue, params Type[] types)
        {
            return queue.WaitForMessages(1000, types);
        }

        public static List<Message> WaitForMessages(this Queue<Message> queue,  int timeout, params Type[] types)
        {
            List<Message> messages = new List<Message>();

            var neededTypes = types.ToList();
            var timeoutTime = DateTime.Now.AddMilliseconds(timeout);

            while (neededTypes.Count > 0)
            {
                if (timeoutTime <= DateTime.Now)
                {
                    return null;
                }

                while (queue.Count > 0)
                {
                    var msg = queue.Dequeue();
                    messages.Add(msg);

                    var idx = neededTypes.IndexOf(msg.GetType());
                    if (idx >= 0)
                    {
                        neededTypes.RemoveAt(idx);
                    }
                    timeoutTime = DateTime.Now.AddMilliseconds(timeout);
                }
                
                Thread.Sleep(timeout / 10);
            }

            return messages;
        }

        public static List<Message> WaitForMessages(this Queue<Message> queue, Type errorType, params Type[] types)
        {
            return queue.WaitForMessages(1000, errorType, types);
        }

        public static List<Message> WaitForMessages(this Queue<Message> queue, int timeout, Type errorType, params Type[] types)
        {
            List<Message> messages = new List<Message>();

            var neededTypes = types.ToList();
            bool error = false;

            var timeoutTime = DateTime.Now.AddMilliseconds(timeout);

            while (neededTypes.Count > 0 && !error)
            {
                if (timeoutTime <= DateTime.Now)
                {
                    return null;
                }

                while (queue.Count > 0)
                {
                    var msg = queue.Dequeue();
                    messages.Add(msg);
                    if (errorType == msg.GetType())
                    {
                        error = true;
                        break;
                    }

                    var idx = neededTypes.IndexOf(msg.GetType());
                    if (idx >= 0)
                    {
                        neededTypes.RemoveAt(idx);
                    }
                    timeoutTime = DateTime.Now.AddMilliseconds(timeout);
                }
                
                Thread.Sleep(timeout / 10);
            }

            return messages;
        }

    }
}
