namespace Monolith
{
    
    public readonly struct GameTime
    {

        public readonly float DeltaTime;
        public readonly float FixedDeltaTime;
        public readonly long UtcTimestamp;
        
        public GameTime(float deltaTime, float fixedDeltaTime, long utcTimestamp)
        {
            DeltaTime = deltaTime;
            FixedDeltaTime = fixedDeltaTime;
            UtcTimestamp = utcTimestamp;
        }
        
    }
    
}