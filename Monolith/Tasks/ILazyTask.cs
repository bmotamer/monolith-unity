namespace Monolith.Tasks
{
    
    public interface ILazyTask
    {

        void Start();
        bool IsDone { get; }
        float Progress { get; }
        object Result { get; }
        
    }

}