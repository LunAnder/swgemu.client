using System;

namespace LogAbstraction
{
    public interface ILogger
    {
        void Debug(object value);
        void Debug(string message);
        void Debug<T>(T value);
        void Debug(string message, Exception exception);
        void Debug(string message, object argument);
        void Debug(string message, params object[] args);
        void Debug<TArgument>(string message, TArgument argument);

        void Error(object value);
        void Error(string message);
        void Error<T>(T value);
        void Error(string message, Exception exception);
        void Error(string message, object argument);
        void Error(string message, params object[] args);
        void Error<TArgument>(string message, TArgument argument);

        void Trace(object value);
        void Trace(string message);
        void Trace<T>(T value);
        void Trace(string message, Exception exception);
        void Trace(string message, object argument);
        void Trace(string message, params object[] args);
        void Trace<TArgument>(string message, TArgument argument);

        void Warn(object value);
        void Warn(string message);
        void Warn<T>(T value);
        void Warn(string message, Exception exception);
        void Warn(string message, object argument);
        void Warn(string message, params object[] args);
        void Warn<TArgument>(string message, TArgument argument);

        void Info(object value);
        void Info(string message);
        void Info<T>(T value);
        void Info(string message, Exception exception);
        void Info(string message, object argument);
        void Info(string message, params object[] args);
        void Info<TArgument>(string message, TArgument argument);

        void Fatal(object value);
        void Fatal(string message);
        void Fatal<T>(T value);
        void Fatal(string message, Exception exception);
        void Fatal(string message, object argument);
        void Fatal(string message, params object[] args);
        void Fatal<TArgument>(string message, TArgument argument);
    }
}
