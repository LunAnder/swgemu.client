namespace LogAbstraction
{
    public interface ILogManager
    {
        ILogger GetCurrentClassLogger();
        ILogger GetLogger(string name);
    }
}
