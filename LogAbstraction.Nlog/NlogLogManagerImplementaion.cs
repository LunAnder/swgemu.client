using System.Diagnostics;
using NLog;
using LogAbstraction;

namespace LogAbstraction.Nlog
{
    public class NlogLogManagerImplementaion : ILogManager
    {
        public ILogger GetCurrentClassLogger()
        {
            return new NlogLogger(LogManager.GetLogger(new StackFrame(2, false).GetMethod().DeclaringType.FullName));
        }

        public ILogger GetLogger(string name)
        {
            return new NlogLogger(LogManager.GetLogger(name));
        }
    }
}
