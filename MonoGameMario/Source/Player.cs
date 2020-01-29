﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameMario.Source.Animation;
using MonoGameMario.Source.InputSystem;
using MonoGameMario.Source.Sprites;

namespace MonoGameMario.Source
{
    public class Player : TiledSprite
    {
        private Animator _animator;
        private Physics _physics;
        private float _movementSpeed, _jumpForce;
        private Vector2 input;

        protected override void Initialize()
        {
            base.Initialize();
            _physics = new Physics(this,90);
            _physics.OnCollisionEnter += OnCollisionEnter;
            
            _jumpForce = 10;
            
            _animator = new Animator();
            int[] walkAnimation = { 2, 1, 3 };
            int[] idleAnimation = { 0 };
            int[] jumpAnimation = { 5 };
            
            _animator.CreateAnimation("Walk", walkAnimation, 0.2f,true);
            _animator.CreateAnimation("Idle", idleAnimation, 0.3f,true);
            _animator.CreateAnimation("Jump", jumpAnimation, 0.3f,true);
            _animator.Play("Idle");
        }

        public override void Update(GameTime gameTime)
        {
            _physics.Update(gameTime);
            _animator.Update(gameTime, ref TileIndex);

            input = new Vector2(Input.GetAxis("Horizontal"), _physics.Velocity.Y);
            
            if (input.X != 0 && _physics.Grounded)
            {
                _animator.Play("Walk");
            }
            else
            {
                _animator.Play("Idle");
            }
            
            if(!_physics.Grounded)
                _animator.Play("Jump");

            if (input.X < 0)
            {
                s = SpriteEffects.FlipHorizontally;
            }
            else if(input.X > 0)
            {
                s = SpriteEffects.None;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space) && _physics.Grounded)
                _physics.Velocity = new Vector2(_physics.Velocity.X, -_jumpForce);
            
            _physics.Velocity = new Vector2(input.X * _movementSpeed, _physics.Velocity.Y);
        }

        private void OnCollisionEnter(Sprite other)
        {
            if (other.Rect.Location.Y < Rect.Location.Y && other is BreakableSprite breakableSprite)
            { 
                _physics.Velocity = new Vector2(_physics.Velocity.X, 0);
                breakableSprite.Destroy();
            }
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