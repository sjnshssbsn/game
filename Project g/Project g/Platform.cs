using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Project_g
{
    public class Platform
    {
        private Vector2 _position;
        private Rectangle _collider;
        private Texture2D _texture;

        public Platform(Vector2 position, Vector2 size)
        {
            _position = position;
            _collider = new Rectangle(
                position.ToPoint(),
                size.ToPoint()
            );
        }

        public void LoadContent(Texture2D texture)
        {
            _texture = texture;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                _texture,
                new Rectangle(
                    (int)Position.X,
                    (int)Position.Y,
                    (int)Size.X,
                    (int)Size.Y),
                Color.Beige);
        }
    }
}
