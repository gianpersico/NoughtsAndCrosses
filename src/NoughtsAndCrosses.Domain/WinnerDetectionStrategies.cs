using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoughtsAndCrosses.Domain
{
    public abstract class WinnerDetectionStrategy
    {
        public abstract Player CheckForWinnerOnBoard(Board board);
        public Player PlayerIn(Board board, int x, int y)
        {
            return board.Spaces[new BoardSpace(x, y)];
        }
    }

    public class WinnerThroughCentreSpace : WinnerDetectionStrategy
    {
        public override Player CheckForWinnerOnBoard(Board board)
        {
            var playerInCentre = PlayerIn(board, 2, 2);

            if (playerInCentre != null)
            {
                if (playerInCentre == PlayerIn(board, 1, 1) && playerInCentre == PlayerIn(board, 3, 3))
                {
                    return playerInCentre;
                }
                if (playerInCentre == PlayerIn(board, 3, 1) && playerInCentre == PlayerIn(board, 1, 3))
                {
                    return playerInCentre;
                }
                if (playerInCentre == PlayerIn(board, 2, 1) && playerInCentre == PlayerIn(board, 2, 3))
                {
                    return playerInCentre;
                }
                if (playerInCentre == PlayerIn(board, 1, 2) && playerInCentre == PlayerIn(board, 3, 2))
                {
                    return playerInCentre;
                }
            }

            return null;
        }
    }

    public class WinnerThroughTopLeftSpace : WinnerDetectionStrategy
    {
        public override Player CheckForWinnerOnBoard(Board board)
        {
            var playerAtTopLeft = PlayerIn(board, 1, 1);
            if (playerAtTopLeft != null)
            {
                if (playerAtTopLeft == PlayerIn(board, 1, 2) && playerAtTopLeft == PlayerIn(board, 1, 3))
                {
                    return playerAtTopLeft;
                }
                if (playerAtTopLeft == PlayerIn(board, 2, 1) && playerAtTopLeft == PlayerIn(board, 3, 1))
                {
                    return playerAtTopLeft;
                }
            }

            return null;
        }
    }

    public class WinnerThroughBottomRightSpace : WinnerDetectionStrategy
    {
        public override Player CheckForWinnerOnBoard(Board board)
        {
            var playerAtBottomRight = PlayerIn(board, 3, 3);
            if (playerAtBottomRight != null)
            {
                if (playerAtBottomRight == PlayerIn(board, 2, 3) && playerAtBottomRight == PlayerIn(board, 1, 3))
                {
                    return playerAtBottomRight;
                }
                if (playerAtBottomRight == PlayerIn(board, 3, 2) && playerAtBottomRight == PlayerIn(board, 3, 1))
                {
                    return playerAtBottomRight;
                }
            }

            return null;
        }
    }
}
