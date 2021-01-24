using Monolith;
using UnityEngine;
using UnityEngine.InputSystem;
using Physics3D = UnityEngine.Physics;

namespace Monolith.Unity
{

    public abstract class UnityGame : Game
    {

        public UnityGame(IEventListener eventListener) : base(eventListener)
        {
            InputSystem.settings.updateMode = InputSettings.UpdateMode.ProcessEventsManually;

            Physics2D.autoSimulation = false;
            Physics3D.autoSimulation = false;
        }

    }

}