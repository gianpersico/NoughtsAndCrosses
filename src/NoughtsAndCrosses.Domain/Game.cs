using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoughtsAndCrosses.Domain
{
    public class Game
    {
        private Game() { }

        public Guid Id { get; private set; }
        public Board Board { get; private set; }
        public ICollection<Player> Players { get; private set; }
        public Player LastPlayer { get; private set; }
        public Player Winner { get { return Board.Winner ?? null; } }

        public static Game StartNew()
        {
            return StartNew(CreateDefaultPlayers());
        }

        public static Game StartNew(ICollection<Player> players)
        {
            var game = new Game();
            game.Id = Guid.NewGuid();
            game.Board = Board.InitialiseForNewGame();
            game.Players = players;
            game.LastPlayer = null;
            return game;
        }

        private static ICollection<Player> CreateDefaultPlayers()
        {
            return new List<Player>
            {
                Player.Create(1, "Player 1", "X"),
                Player.Create(2, "Player 2", "0")
            };
        }

        public void TakeTurn(Player player, BoardSpace boardSpace)
        {
            if (LastPlayer != null && LastPlayer == player) throw new SimultaneousTurnsException(player);
            if (Board.IsStalemate) throw new NoMoreTurnsAllowedException();

            if (Board.Occupy(player, boardSpace)) LastPlayer = player;
            else throw new SpaceAlreadyOccupiedException();
        }

        public bool IsInProgress()
        {
            return Board != null && Board.FreeSpaces > 0 && Winner == null;
        }
    }
}
