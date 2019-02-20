using System;
using System.Security.Permissions;
using System.Threading;

namespace PushkaGraph.NewAlgorithms.Wrapper
{
    public abstract class GraphAlgorithm
    {
        public abstract string Name { get; }
        public abstract string Description { get; }
        public abstract Tuple<Type, int>[] RequiredParameters { get; }
        public abstract Type[] ResultTypes { get; }

        public GraphAlgorithmResult Result { get; private set; }
        public GraphAlgorithmParameters Parameters { get; private set; }

        public bool IsPerforming { get; private set; }
        public bool IsPerformed { get; private set; }

        public event Action<GraphAlgorithmResult> Performed; 

        private Thread _algorithmThread;
        
        protected abstract GraphAlgorithmResult PerformAlgorithm(GraphAlgorithmParameters parameters);

        public void PerformAlgorithmAsync(GraphAlgorithmParameters parameters)
        {
            IsPerforming = true;
            Parameters = parameters;
            
            _algorithmThread = new Thread(() =>
            {
                Result = PerformAlgorithm(parameters);

                Performed?.Invoke(Result);

                IsPerforming = false;
                IsPerformed = true;
            });
            _algorithmThread.Start();
        }

        [SecurityPermission(SecurityAction.Demand, ControlThread = true)]
        public void InterruptAlgorithm()
        {
            _algorithmThread.Abort();
        }
    }
}
