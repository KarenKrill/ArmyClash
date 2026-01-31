using System.Collections.Generic;
using KarenKrill.UniCore.StateSystem.Abstractions;

namespace ArmyClash.GameFlow.Abstractions
{
    public class GameStateGraph : IStateGraph<GameState>
    {
        public GameState InitialState => GameState.Initial;
        public IDictionary<GameState, IList<GameState>> Transitions => _transitions;

        private readonly IDictionary<GameState, IList<GameState>> _transitions = new Dictionary<GameState, IList<GameState>>()
        {
            { GameState.Initial, new List<GameState> { GameState.Loading, GameState.Exit } },
            { GameState.Loading, new List<GameState> { GameState.MainMenu, GameState.TacticalPhase, GameState.Exit } },
            { GameState.MainMenu, new List<GameState> { GameState.Loading, GameState.Exit } },
            { GameState.TacticalPhase, new List<GameState> { GameState.Pause, GameState.BattleEnd, GameState.Exit } },
            { GameState.BattleSimulation, new List<GameState> { GameState.Pause, GameState.BattleSimulation, GameState.BattleEnd, GameState.Exit } },
            { GameState.Pause, new List<GameState> { GameState.TacticalPhase, GameState.BattleSimulation, GameState.BattleEnd, GameState.Loading, GameState.Exit } },
            { GameState.BattleEnd, new List<GameState> { GameState.Loading, GameState.Exit } },
            { GameState.Exit, new List<GameState>() }
        };
    }
}
