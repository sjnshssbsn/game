using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Project_g
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _squareTexture;
        private Vector2 _playerPosition;
        private Vector2 _playerSize;
        private float _ground;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

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

            _squareTexture = new Texture2D(GraphicsDevice, 1, 1);
            _squareTexture.SetData(new[] { Color.Beige });
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
                || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                _playerPosition.X--;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                _playerPosition.X++;
            }

            if (_playerPosition.Y < 400)
            {
                _playerPosition.Y++;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _spriteBatch.Draw(
                _squareTexture,
                new Rectangle(
                    (int)_playerPosition.X,
                    (int)_playerPosition.Y,
                    (int)_playerSize.X,
                    (int)_playerSize.Y),
                Color.Beige);

            _spriteBatch.Draw {
                _squareTexture
                    , new Rectangle( 0, (int) )
            };

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
