using System;
using System.Collections.ObjectModel;

namespace Monolith.Tasks
{
    
    public sealed class LazyTaskSequence : ILazyTask
    {

        public readonly ReadOnlyCollection<ILazyTask> Tasks;
        
        public int TaskIndex { get; private set; }

        public LazyTaskSequence(params ILazyTask[] tasks)
        {
            Tasks = new ReadOnlyCollection<ILazyTask>(tasks);
            TaskIndex = -1;
        }
        
        public bool IsDone => TaskIndex >= Tasks.Count;
        
        public float Progress
        {
            get
            {
                float progress;

                if (IsDone)
                {
                    progress = 1.0F;
                }
                else
                {
                    progress = 0.0F;

                    if (Tasks.Count > 0)
                    {
                        foreach (ILazyTask task in Tasks) progress += task.Progress;

                        progress /= Tasks.Count;
                    }
                }

                return progress;
            }
        }

        public void Start()
        {
            if (TaskIndex >= 0) throw new InvalidOperationException();

            TaskIndex = 0;
            
            if (!IsDone) Tasks[0].Start();
        }
        
        public void Update()
        {
            if (TaskIndex < 0) throw new InvalidOperationException();
            
            while (!IsDone)
            {
                ILazyTask task = Tasks[TaskIndex];
                task.Update();
                
                if (!task.IsDone) break;
                
                ++TaskIndex;

                if (TaskIndex >= Tasks.Count) break;
                    
                Tasks[TaskIndex].Start();
            }
        }
        
    }
    
}