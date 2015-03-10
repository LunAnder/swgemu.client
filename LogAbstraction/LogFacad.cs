using LogAbstraction.Default;

namespace LogAbstraction
{
    public static class LogManagerFacad
    {

        public static ILogManager ManagerImplementaiton { get; set; }

        static LogManagerFacad()
        {
            ManagerImplementaiton = new DefaultLogManagerImplementaion();
        }

        public static ILogger GetCurrentClassLogger()
        {
            return ManagerImplementaiton.GetCurrentClassLogger();
        }
    }
}
