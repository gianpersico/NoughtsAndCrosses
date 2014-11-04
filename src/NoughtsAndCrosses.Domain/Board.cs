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
        public Dictionary<BoardReference, Player> Spaces { get; private set; }
        public Player Winner { get; private set; }
        public bool IsStalemate { get { return FreeSpaces == 0; } }

        private Board() { }

        public static Board InitialiseForNewGame()
        {
            var spaces = new Dictionary<BoardReference, Player>();
            spaces.Add(new BoardReference(1, 1), null);
            spaces.Add(new BoardReference(1, 2), null);
            spaces.Add(new BoardReference(1, 3), null);
            spaces.Add(new BoardReference(2, 1), null);
            spaces.Add(new BoardReference(2, 2), null);
            spaces.Add(new BoardReference(2, 3), null);
            spaces.Add(new BoardReference(3, 1), null);
            spaces.Add(new BoardReference(3, 2), null);
            spaces.Add(new BoardReference(3, 3), null);
            var count = spaces.Count();
            return new Board
            {
                TotalSpaces = count,
                FreeSpaces = count,
                Spaces = spaces,
                Winner = null
            };
        }

        public bool Occupy(Player player, BoardReference boardReference)
        {
            var isOccupied = Spaces[boardReference] != null;
            if (isOccupied) return false;

            Spaces[boardReference] = player;
            FreeSpaces--;
            CheckForWinner();
            return true;
        }

        private void CheckForWinner()
        {
            if (FreeSpaces > 4) return;

            Winner = WinnerThroughMiddle();
        }

        private Player WinnerThroughMiddle()
        {
            var centreRef = new BoardReference(2, 2);
            var playerInCentre = PlayerAtReference(centreRef);

            if (playerInCentre == null) return null;
            if (playerInCentre == PlayerAtReference(new BoardReference(1, 1)) && playerInCentre == PlayerAtReference(new BoardReference(3, 3)))
            {
                return playerInCentre;
            }
            if (playerInCentre == PlayerAtReference(new BoardReference(3, 1)) && playerInCentre == PlayerAtReference(new BoardReference(1, 3)))
            {
                return playerInCentre;
            }

            return null;
        }

        private Player PlayerAtReference(BoardReference boardRef)
        {
            return Spaces[boardRef];
        }
    }    
}
