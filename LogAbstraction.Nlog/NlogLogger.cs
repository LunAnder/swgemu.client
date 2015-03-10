using System;

namespace LogAbstraction.Nlog
{
    public class NlogLogger : ILogger
    {
        public NLog.Logger Logger { get; set; }

        public NlogLogger()
        {
            
        }

        public NlogLogger(NLog.Logger logger)
        {
            Logger = logger;
        }

        public void Debug(object value)
        {
            Logger.Debug(value);
        }

        public void Debug(string message)
        {
            Logger.Debug(message);
        }

        public void Debug<T>(T value)
        {
            Logger.Debug<T>(value);
        }

        public void Debug(string message, Exception exception)
        {
            Logger.Debug(message, exception);
        }

        public void Debug(string message, object argument)
        {
            Logger.Debug(message, argument);
        }

        public void Debug(string message, params object[] args)
        {
            Logger.Debug(message, args);
        }

        public void Debug<TArgument>(string message, TArgument argument)
        {
            Logger.Debug<TArgument>(message, argument);
        }



        public void Error(object value)
        {
            Logger.Error(value);
        }

        public void Error(string message)
        {
            Logger.Error(message);
        }

        public void Error<T>(T value)
        {
            Logger.Error<T>(value);
        }

        public void Error(string message, Exception exception)
        {
            Logger.Error(message, exception);
        }

        public void Error(string message, object argument)
        {
            Logger.Error(message, argument);
        }

        public void Error(string message, params object[] args)
        {
            Logger.Error(message, args);
        }

        public void Error<TArgument>(string message, TArgument argument)
        {
            Logger.Error<TArgument>(message, argument);
        }




        public void Trace(object value)
        {
            Logger.Trace(value);
        }

        public void Trace(string message)
        {
            Logger.Trace(message);
        }

        public void Trace<T>(T value)
        {
            Logger.Trace<T>(value);
        }

        public void Trace(string message, Exception exception)
        {
            Logger.Trace(message, exception);
        }

        public void Trace(string message, object argument)
        {
            Logger.Trace(message, argument);
        }

        public void Trace(string message, params object[] args)
        {
            Logger.Trace(message, args);
        }

        public void Trace<TArgument>(string message, TArgument argument)
        {
            Logger.Trace<TArgument>(message, argument);
        }




        public void Warn(object value)
        {
            Logger.Warn(value);
        }

        public void Warn(string message)
        {
            Logger.Warn(message);
        }

        public void Warn<T>(T value)
        {
            Logger.Warn<T>(value);
        }

        public void Warn(string message, Exception exception)
        {
            Logger.Warn(message, exception);
        }

        public void Warn(string message, object argument)
        {
            Logger.Warn(message, argument);
        }

        public void Warn(string message, params object[] args)
        {
            Logger.Warn(message, args);
        }

        public void Warn<TArgument>(string message, TArgument argument)
        {
            Logger.Warn<TArgument>(message, argument);
        }



        public void Info(object value)
        {
            Logger.Info(value);
        }

        public void Info(string message)
        {
            Logger.Info(message);
        }

        public void Info<T>(T value)
        {
            Logger.Info<T>(value);
        }

        public void Info(string message, Exception exception)
        {
            Logger.Info(message, exception);
        }

        public void Info(string message, object argument)
        {
            Logger.Info(message, argument);
        }

        public void Info(string message, params object[] args)
        {
            Logger.Info(message, args);
        }

        public void Info<TArgument>(string message, TArgument argument)
        {
            Logger.Info<TArgument>(message, argument);
        }






        public void Fatal(object value)
        {
            Logger.Fatal(value);
        }

        public void Fatal(string message)
        {
            Logger.Fatal(message);
        }

        public void Fatal<T>(T value)
        {
            Logger.Fatal<T>(value);
        }

        public void Fatal(string message, Exception exception)
        {
            Logger.Fatal(message, exception);
        }

        public void Fatal(string message, object argument)
        {
            Logger.Fatal(message, argument);
        }

        public void Fatal(string message, params object[] args)
        {
            Logger.Fatal(message, args);
        }

        public void Fatal<TArgument>(string message, TArgument argument)
        {
            Logger.Fatal<TArgument>(message, argument);
        }
    }
}
