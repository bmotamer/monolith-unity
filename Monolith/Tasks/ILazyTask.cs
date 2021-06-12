namespace Monolith.Tasks
{
    
    public interface ILazyTask
    {

        void Start();
        void Update();
        bool IsDone { get; }
        float Progress { get; }
        
    }

    public interface ILazyTask<T> : ILazyTask
    {
        
        T Result { get; }
        
    }

}