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
                Winner = null
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

            Winner = WinnerThroughMiddle();
            if (Winner == null)
            {
                Winner = WinnerThroughTopLeft();
                if (Winner == null)
                {
                    Winner = WinnerThroughBottomRight();
                }
            }
        }

        private Player WinnerThroughMiddle()
        {
            var playerInCentre = PlayerIn(2, 2);

            if (playerInCentre != null)
            {
                if (playerInCentre == PlayerIn(1, 1) && playerInCentre == PlayerIn(3, 3))
                {
                    return playerInCentre;
                }
                if (playerInCentre == PlayerIn(3, 1) && playerInCentre == PlayerIn(1, 3))
                {
                    return playerInCentre;
                }
                if (playerInCentre == PlayerIn(2, 1) && playerInCentre == PlayerIn(2, 3))
                {
                    return playerInCentre;
                }
                if (playerInCentre == PlayerIn(1, 2) && playerInCentre == PlayerIn(3, 2))
                {
                    return playerInCentre;
                }
            }

            var playerInTopLeftSpace = PlayerIn(1, 1);
            if (playerInTopLeftSpace != null)
            {
                if (playerInTopLeftSpace == PlayerIn(1, 2) && playerInTopLeftSpace == PlayerIn(1, 3))
                {
                    return playerInTopLeftSpace;
                }
                if (playerInTopLeftSpace == PlayerIn(2, 1) && playerInTopLeftSpace == PlayerIn(3, 1))
                {
                    return playerInTopLeftSpace;
                }
            }

            var playerInBottomRightSpace = PlayerIn(3, 3);
            if (playerInBottomRightSpace != null)
            {
                if (playerInBottomRightSpace == PlayerIn(2, 3) && playerInBottomRightSpace == PlayerIn(1, 3))
                {
                    return playerInBottomRightSpace;
                }
                if (playerInBottomRightSpace == PlayerIn(3, 2) && playerInBottomRightSpace == PlayerIn(3, 1))
                {
                    return playerInBottomRightSpace;
                }
            }

            return null;
        }

        private Player WinnerThroughTopLeft()
        {
            var playerAtTopLeft = PlayerIn(1, 1);
            if (playerAtTopLeft != null)
            {
                if (playerAtTopLeft == PlayerIn(1, 2) && playerAtTopLeft == PlayerIn(1, 3))
                {
                    return playerAtTopLeft;
                }
                if (playerAtTopLeft == PlayerIn(2, 1) && playerAtTopLeft == PlayerIn(3, 1))
                {
                    return playerAtTopLeft;
                }
            }

            return null;
        }

        private Player WinnerThroughBottomRight()
        {
            var playerAtBottomRight = PlayerIn(3, 3);
            if (playerAtBottomRight != null)
            {
                if (playerAtBottomRight == PlayerIn(2, 3) && playerAtBottomRight == PlayerIn(1, 3))
                {
                    return playerAtBottomRight;
                }
                if (playerAtBottomRight == PlayerIn(3, 2) && playerAtBottomRight == PlayerIn(3, 1))
                {
                    return playerAtBottomRight;
                }
            }

            return null;
        }

        private Player PlayerIn(int x, int y)
        {
            return Spaces[new BoardSpace(x, y)];
        }
    }
}
