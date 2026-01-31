#nullable enable

namespace ArmyClash.GameFlow.Abstractions
{
    public delegate void StateChangingHandler(GameState fromState, GameState toState);
    public delegate void StateChangedHandler(GameState state);

    public interface IGameStateNavigator
    {
        GameState State { get; }

        event StateChangingHandler? StateChanging;
        event StateChangedHandler? StateChanged;

        void LoadMainMenu();
        void LoadArena(int arenaId);
        void Pause();
        void Resume();
        void StartSimulation();
        void FinishSimulation();
        void Exit();
    }
}
