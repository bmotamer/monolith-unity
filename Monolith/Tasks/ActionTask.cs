using System;

namespace Monolith.Tasks
{
    
    public sealed class ActionTask : ILazyTask
    {

        private readonly Action _action;
        
        public ActionTask(Action action)
        {
            _action = action;
        }

        public void Start()
        {
            if (IsDone) throw new InvalidOperationException();
            
            _action();

            IsDone = true;
        }

        public bool IsDone { get; private set; }
        public float Progress => IsDone ? 1F : 0F;
        
    }
    
}