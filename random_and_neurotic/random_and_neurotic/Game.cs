using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace random_and_neurotic
{  
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game : Microsoft.Xna.Framework.Game
    {
        Network network;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        static Texture2D pixel;
        public static KeyboardData keyboard = new KeyboardData();

        public Game()
        {
            network = new Network(Settings.WIDTH, Settings.HEIGHT);

            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferMultiSampling = true;
            graphics.PreferredBackBufferWidth = Settings.WIDTH * Settings.SCALE;
            graphics.PreferredBackBufferHeight = Settings.HEIGHT * Settings.SCALE;

            Content.RootDirectory = "Content";   
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            IsMouseVisible = true;
            IsFixedTimeStep = false;

            pixel = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            pixel.SetData(new[] { Color.White });

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {


            //
            if (keyboard.key_pressed(Keys.Escape))
                this.Exit();

            //
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                int x = (int)(Mouse.GetState().X / Settings.SCALE);
                int y = (int)(Mouse.GetState().Y / Settings.SCALE);
                if (x >= 0 && y >= 0 && x < Settings.WIDTH && y < Settings.HEIGHT)
                    network.neurons[x, y].set_charge(20);
            }

            //
            network.update();

            //
            keyboard.update();

            //
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkGray);

            spriteBatch.Begin();
            foreach (Neuron neuron in network.neurons)
            {
                float charge = neuron.get_charge();
                Color color = new Color(charge, charge, charge);
                spriteBatch.Draw(pixel, neuron.get_position() * Settings.SCALE, null, color, 0, Vector2.Zero, Settings.SCALE, SpriteEffects.None, 0);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
