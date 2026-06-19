using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Platform
{
    private Vector2 _position;
    private Rectangle _collider;
    private Texture2D _texture;

    public Rectangle Collider => _collider;

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
            _position,
            new Rectangle(104, 122, 867, 176), // null
            Color.White,
            0,
            new Vector2(32, 46),
            new Vector2(0.35f, 0.4f),
            SpriteEffects.None,
            0
        );
    }
}