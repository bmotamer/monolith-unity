using UnityEngine;
using UnityEngine.InputSystem;
using Physics3D = UnityEngine.Physics;

namespace Monolith.Unity
{

    public abstract class UnityGame : Game
    {
        
        protected UnityGame(IEventListener eventListener) : base(eventListener)
        {
            InputSystem.settings.updateMode = InputSettings.UpdateMode.ProcessEventsManually;

            Physics2D.simulationMode = SimulationMode2D.Script;
            Physics3D.autoSimulation = false;
        }

    }

}