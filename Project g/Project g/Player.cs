using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


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

        public void Move(Vector2 direction) 
        {
            Position += direction;
        }
    }
}
