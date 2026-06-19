using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Enemy
{
    private const float _gravity = 9.8f;

    private float _movementSpeed;
    private Texture2D _texture;

    public Rectangle Collider;
    public Vector2 Position;
    public Vector2 Size;

    public Vector2 Velocity;

    public Enemy(Vector2 position, Vector2 size)
    {
        Position = position;
        Size = size;

        _movementSpeed = 300;
        Collider = new Rectangle(Position.ToPoint(), Size.ToPoint());
    }

    public void LoadContent(Texture2D texture)
    {
        _texture = texture;
    }

    public void Update(float dt, Vector2 targetPosition)
    {
        Vector2 direction = targetPosition - Position;

        if (direction != Vector2.Zero)
        {
            direction.Normalize();
        }

        Velocity.X = direction.X;
        Velocity.Y += _gravity;

        Position.X += Velocity.X * _movementSpeed * dt;
        Position.Y += Velocity.Y * dt;

        Collider.X = (int)Position.X;
        Collider.Y = (int)Position.Y;
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
            Color.Red);
    }
}