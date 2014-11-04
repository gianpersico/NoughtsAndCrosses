using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoughtsAndCrosses.Domain
{
    public class Player
    {
        private Player() { }

        public int Number { get; private set; }
        public string Name { get; private set; }
        public string Tag { get; private set; }

        public static Player Create(int number, string name, string tag)
        {
            var player = new Player
            {
                Name = name,
                Number = number,
                Tag = tag
            };
            return player;
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as Player);
        }

        public bool Equals(Player p)
        {
            // If parameter is null, return false. 
            if (Object.ReferenceEquals(p, null))
            {
                return false;
            }

            // Optimization for a common success case. 
            if (Object.ReferenceEquals(this, p))
            {
                return true;
            }

            // If run-time types are not exactly the same, return false. 
            if (this.GetType() != p.GetType())
                return false;

            // Return true if the fields match. 
            // Note that the base class is not invoked because it is 
            // System.Object, which defines Equals as reference equality. 
            return (Number == p.Number) && (Name == p.Name) && (Tag == p.Tag);
        }

        public override int GetHashCode()
        {
            return Number * 0x00010000;
        }
    }
}
