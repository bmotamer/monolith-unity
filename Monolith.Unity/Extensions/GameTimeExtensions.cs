using UnityTime = UnityEngine.Time;

namespace Monolith.Unity.Extensions
{
    
    public static class GameTimeExtensions
    {

        public static float GetScaledDelta(this GameTime time) => time.Delta * UnityTime.timeScale;
        public static float GetScaledFixedDelta(this GameTime time) => time.FixedDelta * UnityTime.timeScale;

    }
    
}