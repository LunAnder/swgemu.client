namespace LogAbstraction.Default
{
    public class DefaultLogManagerImplementaion : ILogManager
    {
        public ILogger GetCurrentClassLogger()
        {
            return new DefaultLogger();
        }


        public ILogger GetLogger(string name)
        {
            return new DefaultLogger();
        }
    }
}
