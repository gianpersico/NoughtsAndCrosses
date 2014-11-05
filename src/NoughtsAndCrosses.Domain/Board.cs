using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoughtsAndCrosses.Domain
{
    public class Board
    {
        public int TotalSpaces { get; private set; }
        public int FreeSpaces { get; private set; }
        public Dictionary<BoardSpace, Player> Spaces { get; private set; }
        public Player Winner { get; private set; }
        public bool IsStalemate { get { return FreeSpaces == 0; } }
        public List<WinnerDetectionStrategy> WinnerDetectionStrategies { get; private set; }

        private Board() { }

        public static Board InitialiseForNewGame()
        {
            var spaces = new Dictionary<BoardSpace, Player>();
            spaces.Add(new BoardSpace(1, 1), null);
            spaces.Add(new BoardSpace(1, 2), null);
            spaces.Add(new BoardSpace(1, 3), null);
            spaces.Add(new BoardSpace(2, 1), null);
            spaces.Add(new BoardSpace(2, 2), null);
            spaces.Add(new BoardSpace(2, 3), null);
            spaces.Add(new BoardSpace(3, 1), null);
            spaces.Add(new BoardSpace(3, 2), null);
            spaces.Add(new BoardSpace(3, 3), null);
            var count = spaces.Count();
            return new Board
            {
                TotalSpaces = count,
                FreeSpaces = count,
                Spaces = spaces,
                Winner = null,
                WinnerDetectionStrategies = new List<WinnerDetectionStrategy>
                {
                    new WinnerThroughBottomRightSpace(),
                    new WinnerThroughCentreSpace(),
                    new WinnerThroughTopLeftSpace()
                }
            };
        }

        public bool Occupy(Player player, BoardSpace boardSpace)
        {
            var isOccupied = Spaces[boardSpace] != null;
            if (isOccupied) return false;

            Spaces[boardSpace] = player;
            FreeSpaces--;
            CheckForWinner();
            return true;
        }

        private void CheckForWinner()
        {
            if (FreeSpaces > 4) return;

            foreach (var strategy in WinnerDetectionStrategies)
            {
                var strategyWinner = strategy.CheckForWinnerOnBoard(this);
                if (strategyWinner != null)
                {
                    Winner = strategyWinner;
                    break;
                }
            }
        }

        private Player PlayerIn(int x, int y)
        {
            return Spaces[new BoardSpace(x, y)];
        }
    }
}
