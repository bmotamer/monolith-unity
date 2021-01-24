namespace Monolith.States
{

    public enum StateState : byte
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