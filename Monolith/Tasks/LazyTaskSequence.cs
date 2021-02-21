using System;
using System.Collections.ObjectModel;
using Monolith.Extensions;

namespace Monolith.Tasks
{
    
    public sealed class LazyTaskSequence
    {

        public readonly ReadOnlyCollection<ILazyTask> Tasks;
        
        public int TaskIndex { get; private set; }
        
        public LazyTaskSequence(params ILazyTask[] tasks)
        {
            if (tasks.HasNulls() || tasks.HasDuplicates()) throw new ArgumentException(nameof(tasks));

            Tasks = new ReadOnlyCollection<ILazyTask>(tasks);
            TaskIndex = -1;
        }

        public bool Update()
        {
            bool isDone = false;

            if (TaskIndex < 0)
            {
                if (Tasks.Count == 0)
                {
                    isDone = true;
                }
                else
                {
                    Tasks[0].Start();
                    TaskIndex = 0;   
                }
            }
            else
            {
                if (Tasks[TaskIndex].IsDone)
                {
                    if (TaskIndex == (Tasks.Count - 1))
                    {
                        isDone = true;
                    }
                    else
                    {
                        ++TaskIndex;
                        Tasks[TaskIndex].Start();
                    }
                }
            }

            return isDone;
        }

    }
    
}