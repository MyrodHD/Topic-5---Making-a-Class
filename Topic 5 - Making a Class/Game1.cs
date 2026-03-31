using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using static System.Net.WebRequestMethods;

namespace Topic_5___Making_a_Class
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        enum Screen
        {
            Title,
            House,
            End
        }

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        Ghost ghost1;

        List<Texture2D> ghostTextures;

        Texture2D marioTexture;
        Texture2D titleScreenTexture;
        Texture2D endScreenTexture;
        Texture2D mainBackgroundTexture;

        MouseState mouseState;
        KeyboardState keyboardState;

        Random generator;

        Screen screen;
        
        Rectangle window;
        Rectangle marioRect;

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            ghostTextures = new List<Texture2D>();

            window = new Rectangle(0,0,800,600);
            _graphics.PreferredBackBufferWidth = window.Width;
            _graphics.PreferredBackBufferHeight = window.Height;
            _graphics.ApplyChanges();

            ghost1 = new Ghost(ghostTextures, new Rectangle(150,250,40,40));
            marioRect = new Rectangle(0, 0, 20,20);

            screen = Screen.Title;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            titleScreenTexture = Content.Load<Texture2D>("Images/haunted-title");
            mainBackgroundTexture = Content.Load<Texture2D>("Images/haunted-background");
            endScreenTexture = Content.Load<Texture2D>("Images/haunted-end-screen");
            marioTexture = Content.Load<Texture2D>("Images/mario");

            for (int i = 1; i <= 8; i++)
                ghostTextures.Add(Content.Load<Texture2D>("Images/boo-move-" + i));
        }

        protected override void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();

            marioRect.Location = mouseState.Position;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            if (screen == Screen.Title)
            {
                if (keyboardState.IsKeyDown(Keys.Enter))
                    screen = Screen.House;
            }
            else if (screen == Screen.House)
            {
                ghost1.Update(gameTime, mouseState);
                if (ghost1.Contains(mouseState.Position) && mouseState.LeftButton == ButtonState.Pressed)
                    screen = Screen.End;
            }

                base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();

            if (screen == Screen.Title)
                _spriteBatch.Draw(titleScreenTexture, window, Color.White);

            if (screen == Screen.House)
            {
                _spriteBatch.Draw(mainBackgroundTexture, window, Color.White);

                ghost1.Draw(_spriteBatch);

                _spriteBatch.Draw(marioTexture, marioRect, Color.White);
            }
            else
                _spriteBatch.Draw(endScreenTexture, window, Color.White);

                _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
