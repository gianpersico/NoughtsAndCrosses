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
                     if (Object.ReferenceEquals(p, null))
            {
                return false;
            }

            if (Object.ReferenceEquals(this, p))
            {
                return true;
            }

            if (this.GetType() != p.GetType())
                return false;

            return (Number == p.Number) && (Name == p.Name) && (Tag == p.Tag);
        }

        public override int GetHashCode()
        {
            return Number * 0x00010000;
        }
    }
}
