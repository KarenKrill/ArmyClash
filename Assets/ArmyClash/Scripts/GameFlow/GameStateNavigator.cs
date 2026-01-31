#nullable enable
using KarenKrill.UniCore.StateSystem.Abstractions;

namespace ArmyClash.GameFlow
{
    using Abstractions;

    public class GameStateNavigator : IGameStateNavigator
    {
        public GameState State => _stateSwitcher.State;

        public event StateChangingHandler? StateChanging;
        public event StateChangedHandler? StateChanged;

        public GameStateNavigator(IStateSwitcher<GameState> stateSwitcher)
        {
            _stateSwitcher = stateSwitcher;
        }

        public void LoadMainMenu() => TransitTo(GameState.Loading);
        public void LoadArena(int arenaId) => TransitTo(GameState.Loading, new ArenaLoadContext(arenaId));
        public void Pause()
        {
            _beforePauseState = _stateSwitcher.State;
            TransitTo(GameState.Pause);
        }
        public void Resume()
        {
            if (_beforePauseState is not null)
            {
                TransitTo(_beforePauseState.Value);
            }
        }
        public void StartSimulation() => TransitTo(GameState.BattleSimulation);
        public void FinishSimulation() => TransitTo(GameState.BattleEnd);
        public void Exit() => TransitTo(GameState.Exit);

        private readonly IStateSwitcher<GameState> _stateSwitcher;
        private GameState? _beforePauseState = null;

        private void TransitTo(GameState state, object? context = null)
        {
            StateChanging?.Invoke(_stateSwitcher.State, state);
            _stateSwitcher.TransitTo(state, context);
            StateChanged?.Invoke(state);
        }
    }
}
