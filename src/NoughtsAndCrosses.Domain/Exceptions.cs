using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoughtsAndCrosses.Domain
{
    public class SimultaneousTurnsException : Exception
    {
        public SimultaneousTurnsException(Player player)
            : base(string.Format("Unable to take turn, player '{0}' took the last turn", player.Name))
        {

        }
    }

    public class SpaceAlreadyOccupiedException : Exception { }
}
