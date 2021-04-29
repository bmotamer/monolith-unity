namespace Monolith.Tasks
{
    
    public interface ILazyTask
    {

        void Start();
        bool IsDone { get; }
        float Progress { get; }
        
    }

    public interface ILazyTask<T> : ILazyTask
    {
        
        T Result { get; }
        
    }

}