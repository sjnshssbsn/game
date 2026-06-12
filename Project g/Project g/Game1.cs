using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Project_g
{
    public class Game1 : Game
    {

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _squareTexture;
        private Vector2 _screenSize;
        private float _ground;

        private Texture2D _background;
        private Texture2D _platformTexture;
        private Texture2D _platformsSpriteSheet;

        private Player _player;

        private Platform[] _platforms;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _screenSize = new Vector2(1280, 720);
            _graphics.PreferredBackBufferWidth = (int)_screenSize.X;
            _graphics.PreferredBackBufferHeight = (int)_screenSize.Y;

            _platforms = new Platform[3];
            _platforms[0] = new Platform(
                new Vector2(200, 600),
                new Vector2(275, 40)
            );
            _platforms[1] = new Platform(
                new Vector2(400, 400),
                new Vector2(275, 40)
            );
            _platforms[2] = new Platform(
                new Vector2(600, 200),
                new Vector2(275, 40)
            );
        }

        protected override void Initialize()
        {
            _ground = _screenSize.Y;

            _player = new Player(
                new Vector2(50, 335),
                new Vector2(40, 65)
            );

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _background = Content.Load<Texture2D>("Images/background");
            _platformTexture = Content.Load<Texture2D>("Images/platform-stone");
            _platformsSpriteSheet = Content.Load<Texture2D>("Images/platforms");

            _squareTexture = new Texture2D(GraphicsDevice, 1, 1);
            _squareTexture.SetData(new[] { Color.Beige });

            Texture2D playerTexture = Content.Load<Texture2D>("Images/main-character-sqr");
            _player.LoadContent(playerTexture);
        }

        protected override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            GamePadState gamePad = GamePad.GetState(PlayerIndex.One);
            KeyboardState keyboard = Keyboard.GetState();

            if (gamePad.Buttons.Back == ButtonState.Pressed
                || keyboard.IsKeyDown(Keys.Escape))
                Exit();

            Vector2 direction = new Vector2();
            if (keyboard.IsKeyDown(Keys.A))
            {
                direction.X = -1;
            }

            if (keyboard.IsKeyDown(Keys.D))
            {
                direction.X = 1;
            }

            if (keyboard.IsKeyDown(Keys.Space) && (_player.Velocity.Y == 0))
            {
                _player.Jump();
            }

            _player.Update(deltaTime);
            _player.SetDirection(direction);

            ResolveCollisions();

            if ((_player.Position.Y + _player.Size.Y) >= _ground)
            {
                _player.Velocity.Y = 0;
                _player.Position.Y = _ground - _player.Size.Y;
                _player.Collider.Location = _player.Position.ToPoint();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _spriteBatch.Draw(
                _background, Vector2.Zero, Color.White);

            for (int i = 0; i < _platforms.Length; ++i)
            {
                _spriteBatch.Draw(
                    _platformsSpriteSheet,
                    _platforms[i],
                    new Rectangle(100, 128, 864, 128),
                    Color.White
                );
            }

            _player.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void ResolveCollisions()
        {
            for (int i = 0; i < _platforms.Length; i++)
            {
                Vector2 collisionData = GetCollisionData(_player.Collider, _platforms[i]);
                if (collisionData == Vector2.Zero)
                    continue;
                _player.Position += collisionData;
                _player.Collider.Location = _player.Position.ToPoint();
                if (collisionData.X != 0)
                {
                    _player.Velocity.X = 0;
                }
                else
                {
                    if (collisionData.Y < 0)
                    {
                        _player.Velocity.Y = 0;
                    }
                    else
                    {
                        _player.Velocity.Y = 0.1f;
                    }
                }
            }
        }

        private Vector2 GetCollisionData(Rectangle a, Rectangle b)
        {
            Vector2 result = Vector2.Zero;
            if (a.Intersects(b))
            {
                Rectangle overlap = Rectangle.Intersect(a, b);
                if (overlap.Width < overlap.Height)
                {
                    int direction = a.Center.X < b.Center.X ? -overlap.Width : overlap.Width;
                    result.X = direction;
                }
                else
                {
                    int direction = a.Center.Y < b.Center.Y ? -overlap.Height : overlap.Height;
                    result.Y = direction;
                }
            }
            return result;

        }
    }
}