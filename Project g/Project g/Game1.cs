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

        private Player _player;

        private Rectangle[] _platforms;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _screenSize = new Vector2(1280, 720);
            _graphics.PreferredBackBufferWidth = (int)_screenSize.X;
            _graphics.PreferredBackBufferHeight = (int)_screenSize.Y;

            _platforms = new Rectangle[3];
            _platforms[0] = new Rectangle(200, 600, 275, 40);
            _platforms[1] = new Rectangle(400, 400, 275, 40);
            _platforms[2] = new Rectangle(600, 200, 275, 40);
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

            _squareTexture = new Texture2D(GraphicsDevice, 1, 1);
            _squareTexture.SetData(new[] { Color.Beige });

            Texture2D playerTexture = new Texture2D(GraphicsDevice, 1, 1);
            playerTexture.SetData(new[] { Color.Beige });
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
                    _squareTexture,
                    _platforms[i],
                    Color.RosyBrown);
            }

            _player.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void ResolveCollisions()
        {
            for (int i = 0; i < _platforms.Length; i++)
            {
                bool isCollidingLeft = (_player.Position.X + _player.Size.X)
                    > _platforms[i].Left;
                bool isCollidingTop = (_player.Position.Y + _player.Size.Y)
                    > _platforms[i].Top;
                bool isCollidingRight = _player.Position.X < _platforms[i].Right;
                bool isCollidingBottom = _player.Position.Y
                    < _platforms[i].Bottom;
                bool isColliding = isCollidingLeft
                    && isCollidingTop
                    && isCollidingRight
                    && isCollidingBottom;

                if (isColliding)
                {
                    if ((isCollidingLeft || isCollidingRight)
                        && (!isCollidingTop && !isCollidingBottom))
                    {
                        _player.Velocity.X *= -1;
                    }

                    if (isCollidingBottom)
                    {
                        _player.Velocity.Y *= -1;
                    }

                    if (isCollidingTop)
                    {
                        _player.Velocity.Y = 0;
                        _player.Position.Y = _platforms[i].Top - _player.Size.Y;
                    }
                }
            }
        }
    }
}
