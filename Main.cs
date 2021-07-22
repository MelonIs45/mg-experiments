using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Futura2
{
    public class Main : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Utils _utils;
        private Loader _loader;
        private List<Box> _elements;

        MouseState lastMouseState = Mouse.GetState();
        KeyboardState lastKeyboardState = Keyboard.GetState();

        public Main()
        {
            _graphics = new GraphicsDeviceManager(this);
            _utils = new Utils();
            _loader = new Loader();
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            IsFixedTimeStep = false;
            Window.AllowUserResizing = true;

            _graphics.SynchronizeWithVerticalRetrace = false;
            //TargetElapsedTime = TimeSpan.FromSeconds(1.0f / 2000.0f);

            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            //graphics.ToggleFullScreen();

            _graphics.ApplyChanges();
            base.Initialize();

            ReloadElements();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            MouseState currentMouseState = Mouse.GetState();
            KeyboardState currectKeyboardState = Keyboard.GetState();

            Window.Title = $"Futura2 | Mouse Position: {new Vector2(currentMouseState.X, currentMouseState.Y)} | FPS: {1f / gameTime.ElapsedGameTime.TotalSeconds:0}";

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (lastKeyboardState.IsKeyDown(Keys.Q) && currectKeyboardState.IsKeyUp(Keys.Q))
            {
                ReloadElements();
            }


            if (lastMouseState.LeftButton == ButtonState.Pressed && currentMouseState.LeftButton == ButtonState.Released)
            {
                foreach (Box element in _elements)
                {
                    if (_utils.MouseInBox(element, new Vector2(currentMouseState.X, currentMouseState.Y)))
                    {
                        ReloadElements();
                    }
                }

            }


            lastMouseState = Mouse.GetState();
            lastKeyboardState = Keyboard.GetState();

            base.Update(gameTime);
        }

        private void ReloadElements()
        {
            Random rnd = new Random();
            _elements = new List<Box>();
            _elements = _loader.LoadElements(_graphics, rnd.Next(0, 40));
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            Texture2D texBuffer = new Texture2D(_graphics.GraphicsDevice, 1, 1);

            foreach (Box element in _elements)
            {
                _spriteBatch.GraphicsDevice.BlendState = BlendState.AlphaBlend;
                _spriteBatch.Begin();
                texBuffer = new Texture2D(_graphics.GraphicsDevice, element.X2 - element.X1, element.Y2 - element.Y1);

                Color[] data = new Color[(element.X2 - element.X1) * (element.Y2 - element.Y1)];
                for (int i = 0; i < data.Length; ++i)
                {
                    Vector2 coord = new Vector2(i / (element.X2 - element.X1), i % (element.Y2 - element.Y1));
                    float xrad = (element.X2 - element.X1) / 2;
                    float yrad = (element.Y2 - element.Y1) / 2;
                    Vector2 normal = new Vector2(coord.X - (element.X2 - element.X1) / 2, coord.Y - (element.Y2 - element.Y1) / 2);

                    data[i] = element.Color;

                    //if (Math.Pow(xrad, 2) + (normal.X * normal.Y / Math.Pow(yrad, 2)) <= 1)
                    //{
                    //    data[i] = element.Color;
                    //}
                    //else
                    //{
                    //    data[i] = Color.Black;
                    //}


                    texBuffer.SetData(data);

                    Vector2 coor = new Vector2(element.X1, element.Y1);
                    _spriteBatch.Draw(texBuffer, coor, Color.White);
                    _spriteBatch.End();

                    texBuffer.Dispose();
                }

                base.Draw(gameTime);
                texBuffer.Dispose();

            }
        }
    }
}

