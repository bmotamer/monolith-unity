using System;

namespace Monolith.Tasks
{
    
    public sealed class LazyActionTask : ILazyTask
    {

        private readonly Action _action;
        
        public LazyActionTask(Action action)
        {
            _action = action;
        }
        
        public bool IsDone { get; private set; }
        public float Progress => IsDone ? 1F : 0F;

        public void Start()
        {
            if (IsDone) throw new InvalidOperationException();
            
            _action();

            IsDone = true;
        }

        public void Update()
        {
        }

    }
    
}