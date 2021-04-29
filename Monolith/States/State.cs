namespace Monolith.States
{
    
    public abstract class State
    {
        
        protected State(Game game)
        {
        }
        
        protected internal abstract bool Load(Game game);
        
        protected internal abstract void Enter(Game game);
        
        protected internal abstract void Update(Game game);

        protected internal abstract void Exit(Game game);

        protected internal abstract bool Unload(Game game);

    }
    
}