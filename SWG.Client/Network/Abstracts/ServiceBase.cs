using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using LogAbstraction;


namespace SWG.Client.Network.Abstracts
{
    public enum ServiceState
    {
        Stopped,
        Starting,
        Running,
        Stopping,
        Suspending,
        Suspended,
        Resuming,
    }



    public abstract class ServiceBase
    {
        private static ILogger _logger = LogManagerFacad.GetCurrentClassLogger();

        public ServiceState ServiceStatus { get; protected set; }

        protected bool _Run = true;

        protected Thread ServiceThread;

        public string ServiceThreadName { get; protected set; }

        protected ManualResetEvent wait = new ManualResetEvent(true);

        public virtual void Suspend()
        {
            ServiceStatus = ServiceState.Suspending;
            wait.Reset();
        }

        public virtual void Resume()
        {
            ServiceStatus = ServiceState.Resuming;
            wait.Set();
        }

        public virtual void Stop(bool Wait = true, TimeSpan? Timeout = null)
        {
           _Run = false;
            if (ServiceStatus == ServiceState.Suspended || ServiceStatus == ServiceState.Suspending)
            {
                wait.Set();
            }

            ServiceStatus = ServiceState.Stopping;
            
            if (Wait)
            {
                ServiceThread.Join(Timeout.HasValue ? Timeout.Value : TimeSpan.FromMilliseconds(0));    
            }
        }


        public virtual EventWaitHandle Start()
        {
            if (ServiceStatus == ServiceState.Stopped)
            {
                ServiceThread = new Thread(InternalRun)
                {
                    Name = ServiceThreadName
                };
                ServiceStatus = ServiceState.Starting;
                EventWaitHandle wait = new ManualResetEvent(false);

                ServiceThread.Start(wait);

                return wait;
            }

            return new ManualResetEvent(true);
        }


        protected void InternalRun(object sync)
        {
            _logger.Debug("Service {0} started", ServiceThreadName);
            ServiceStatus = ServiceState.Running;

            var waitHandle = sync as EventWaitHandle;
            if (waitHandle != null)
            {
                waitHandle.Set();
            }

            while (_Run)
            {
                if (ServiceStatus == ServiceState.Suspending)
                {
                    ServiceStatus = ServiceState.Suspended;
                    Console.WriteLine("{0} suspended", ServiceThreadName);
                    wait.WaitOne();
                    Console.WriteLine("{0} resumed", ServiceThreadName);
                    ServiceStatus = ServiceState.Running;
                }

                DoWork();
            }
            ServiceStatus = ServiceState.Stopped;
            Console.WriteLine("Service {0} stopped", ServiceThreadName);
        }
        
        protected abstract void DoWork();
    }
}
