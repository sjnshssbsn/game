using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Project_g
{
    public class Player
    {
        public Vector2 Position;
        public Vector2 Size;

        public Player() { }
        public Player(Vector2 position, Vector2 size)
        {
            Position = position;
            Size = size;
        }

        public void Draw() { }
    }
}
