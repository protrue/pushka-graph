using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PushkaGraph.NewAlgorithms
{
    public abstract class GraphAlgorithm
    {
        public abstract string Name { get; }
        public abstract string Description { get; }
        public abstract Type[] RequiredParameterTypes { get; }
        public abstract Type[] ResultTypes { get; }

        public GraphAlgorithmResult Result { get; private set; }
        public GraphAlgorithmParameters Parameters { get; private set; }

        public bool IsPerforming { get; private set; }
        public bool IsPerformed { get; private set; }

        public event Action<GraphAlgorithmResult> AlgorithmPerformed; 

        private Thread _algorithmThread;
        
        protected abstract GraphAlgorithmResult PerformAlgorithm(GraphAlgorithmParameters parameters);

        public GraphAlgorithmResult PerformAlgorithmAsync(GraphAlgorithmParameters parameters)
        {
            Parameters = parameters;

            _algorithmThread = new Thread(() =>
            {
                IsPerformed = false;
                IsPerforming = true;

                Result = PerformAlgorithm(parameters);

                AlgorithmPerformed?.Invoke(Result);

                IsPerforming = false;
                IsPerformed = true;
            });

            return Result;
        }

        [SecurityPermission(SecurityAction.Demand, ControlThread = true)]
        public void InterruptAlgorithm()
        {
            _algorithmThread.Abort();
        }
    }
}
