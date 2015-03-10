using NLog;
using LogAbstraction;

namespace LogAbstraction.Nlog
{
    public class NlogLogManagerImplementaion : ILogManager
    {
        public ILogger GetCurrentClassLogger()
        {
            return new NlogLogger(LogManager.GetCurrentClassLogger());
        }

        public ILogger GetLogger(string name)
        {
            return new NlogLogger(LogManager.GetLogger(name));
        }
    }
}
