using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoughtsAndCrosses.Domain
{
    public struct BoardReference
    {
        public int x, y;

        public BoardReference(int p1, int p2)
        {
            x = p1;
            y = p2;
        }
    }
}
