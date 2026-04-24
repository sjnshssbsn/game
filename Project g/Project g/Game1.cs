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
        private Player _player;
        private Vector2 _playerPosition;
        private Vector2 _playerSize;
        private float _ground;
        private float _jumpTimer;


    private Texture2D _backgroung;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _player = new Player(
                new Vector2(100, 100),
                new Vector2(40, 65)
                );

            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 800;
        }

        protected override void Initialize()
        {
            _playerSize = new Vector2(40, 65);
            _ground = 400;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _backgroung = Content.Load<Texture2D>("images/background");
            _squareTexture = new Texture2D(GraphicsDevice, 1, 1);
            _squareTexture.SetData(new[] { Color.Beige });
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
                || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            Vector2 direction = new Vector2();
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                direction.X = -1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                direction.X = 1;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space) && (_jumpTimer <= 0))
            {
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                direction.Y = 1;
            }
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _player.Move(direction, deltaTime);

            if (_player.Position.Y < (_ground - _playerPosition.Y))
                _player.Position.Y++;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _spriteBatch.Draw(_backgroung, Vector2.Zero, Color.White);

            _spriteBatch.Draw(
                _squareTexture,
                new Rectangle(
                    (int)_player.Position.X,
                    (int)_player.Position.Y,
                    (int)_player.Size.X,
                    (int)_player.Size.Y),
                Color.Beige);

            _spriteBatch.Draw (
                _squareTexture,
                new Rectangle(0, (int)_ground, 100, 100 ),
                Color.DarkRed
            );

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
