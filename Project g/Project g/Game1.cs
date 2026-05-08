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

            _platforms[0] = new Rectangle(200, 590, 150, 30);
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
            _spriteBatch.Draw(
                _squareTexture,
                new Rectangle(
                    (int)_player.Position.X,
                    (int)_player.Position.Y,
                    (int)_player.Size.X,
                    (int)_player.Size.Y),
                Color.Beige);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
