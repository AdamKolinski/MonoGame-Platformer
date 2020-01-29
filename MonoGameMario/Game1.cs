﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameMario;

namespace MonoGamePlatformer
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        
        private TiledSprite _multiTiledTest, _obstacle, _obstacle2;
        private Player _player;

        private KeyboardState _currentKeyboardState, _previousKeyboardState;

        public static List<Sprite> CollisionObjects;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this) {PreferMultiSampling = false};

            IsFixedTimeStep = false;
            IsMouseVisible = true;
            Content.RootDirectory = "Content";
        }
        
        protected override void Initialize()
        {
            CollisionObjects = new List<Sprite>();
            Input.Initialize();
            
            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Helpers.pixel = Content.Load<Texture2D>("pixel");
            
            _player = new Player(_spriteBatch, Content.Load<Texture2D>("mario"), new Vector2(320, 100), new Vector2(1, 1), 21, 2, 2);
            _obstacle = new TiledSprite(_spriteBatch, Content.Load<Texture2D>("mario_tileset"), new Vector2(200, 320), new Vector2(30, 2), 23, 6, 2);
            _obstacle2 = new TiledSprite(_spriteBatch, Content.Load<Texture2D>("mario_tileset"), new Vector2(360, 360), new Vector2(2, 2), 23, 6, 2);
            int[,] tiles =
            {
                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            };
            _multiTiledTest = new TiledSprite(_spriteBatch, Content.Load<Texture2D>("mario_tileset"), new Vector2(0, 0), new Vector2(2,2), 23, 6, tiles);
            
            CollisionObjects.Add(_player);
            CollisionObjects.Add(_obstacle);
            CollisionObjects.Add(_obstacle2);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            Input.Update();
            
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _previousKeyboardState = _currentKeyboardState;
            _player.Update(gameTime);
            base.Update(gameTime);
        }

        public bool GetKeyDown(Keys key)
        {
            return _currentKeyboardState.IsKeyDown(key) && !_previousKeyboardState.IsKeyDown(key);
        }

        protected override void Draw(GameTime gameTime)
        {
            //GraphicsDevice.Clear(Color.CornflowerBlue);
            GraphicsDevice.Clear(new Color(4, 156, 216));
            
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise);
            
            // _multiTiledTest.Draw();
            //
            _obstacle.Draw();
            _obstacle2.Draw();
            _player.Draw();

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
