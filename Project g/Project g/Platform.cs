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
        private Rectangle _collider;
        private Vector2 _position;
        private Texture2D texture;
        public Platform(Vector2 position, Vector2 size) {
        _position = position;
        }
    }
}
