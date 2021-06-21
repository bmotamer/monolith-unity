using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Physics3D = UnityEngine.Physics;
using UnityTime = UnityEngine.Time;

namespace Monolith.Unity
{

    public abstract class UnityGame : Game
    {
        
        protected UnityGame(GameEngineListener engineListener, IGameBootOptions bootOptions) : base(engineListener, bootOptions)
        {
            InputSystem.settings.updateMode = InputSettings.UpdateMode.ProcessEventsManually;

            Physics2D.simulationMode = SimulationMode2D.Script;
            Physics3D.autoSimulation = false;
        }

        protected override GameTime CaptureTime()
        {
            return new GameTime(
                UnityTime.unscaledDeltaTime,
                UnityTime.fixedUnscaledDeltaTime,
                DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
            );
        }

    }

}