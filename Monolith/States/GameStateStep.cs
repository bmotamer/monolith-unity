namespace Monolith.States
{

    public enum GameStateStep : byte
    {

        Uninitialized,
        Loading,
        Loaded,
        Activating,
        Active,
        Deactivating,
        Unloading

    }

}