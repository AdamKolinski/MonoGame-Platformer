﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGamePlatformer;

namespace MonoGameMario
{
    public class Player : TiledSprite
    {
        private Animator _animator;
        private Physics _physics;
        private float _movementSpeed;
        private Vector2 input;
        float tmpVel;

        protected override void Initialize()
        {
            base.Initialize();
            _physics = new Physics(this,90);
            
            _animator = new Animator();
            int[] walkAnimation = { 2, 1, 3 };
            int[] idleAnimation = { 0 };
            int[] jumpAnimation = { 5 };
            
            _animator.CreateAnimation("Walk", walkAnimation, 0.3f,true);
            _animator.CreateAnimation("Idle", idleAnimation, 0.3f,true);
            _animator.CreateAnimation("Jump", jumpAnimation, 0.3f,true);
            _animator.Play("Idle");
        }

        public override void Update(GameTime gameTime)
        {
            _physics.Update(gameTime);
            _animator.Update(gameTime, ref TileIndex);

            Vector2 input = new Vector2(Input.GetAxis("Horizontal"), _physics.Velocity.Y);

            _animator.Play(input.X != 0 ? "Walk" : "Idle");
            if(_physics.Velocity.Y < 0)
                _animator.Play("Jump");

            if (Input.GetAxis("Horizontal") < 0)
            {
                s = SpriteEffects.FlipHorizontally;
            }
            else if(Input.GetAxis("Horizontal") > 0)
            {
                s = SpriteEffects.None;
            }
            
            //tmpVel = Mathf.Lerp(tmpVel, input.X * _movementSpeed, gameTime.ElapsedGameTime.Milliseconds/500f*_movementSpeed);
            
            Console.WriteLine(tmpVel);
            _physics.Velocity = new Vector2(input.X * _movementSpeed, _physics.Velocity.Y);
        }

        #region Constructors
        public Player(SpriteBatch spriteBatch, Texture2D texture, Vector2 position, int rows, int columns, int tileIndex, float movementSpeed = 3) : base(spriteBatch, texture, position, rows, columns, tileIndex)
        {
            _movementSpeed = movementSpeed;
            Initialize();
        }

        public Player(SpriteBatch spriteBatch, Texture2D texture, Vector2 position, Vector2 scale, int columns, int rows, int tileIndex, float movementSpeed = 3) : base(spriteBatch, texture, position, scale, columns, rows, tileIndex)
        {
            _movementSpeed = movementSpeed;
            Initialize();
        }

        public Player(SpriteBatch spriteBatch, Texture2D texture, Vector2 position, int rows, int columns, int[,] tileIndexes, float movementSpeed = 3) : base(spriteBatch, texture, position, rows, columns, tileIndexes)
        {
            _movementSpeed = movementSpeed;
            Initialize();
        }

        public Player(SpriteBatch spriteBatch, Texture2D texture, Vector2 position, Vector2 scale, int columns, int rows, int[,] tileIndexes, float movementSpeed = 3) : base(spriteBatch, texture, position, scale, columns, rows, tileIndexes)
        {
            _movementSpeed = movementSpeed;
            Initialize();
        }
        #endregion
    }
}