using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoGameMario.Source.Sprites;

namespace MonoGameMario.Source
{
    public enum Direction
    {
        Horizontal,
        Vertical
    };
    public class Physics
    {
        public Action<Sprite> OnCollisionEnter { get; set; }
        private Vector2 _velocity;

        private readonly float _weight;
        private readonly Sprite _target;
        public float GravityScale { get; set; }
        public bool Grounded { get; set; }
        
        private int _collisionsAmount, _prevCollisionsAmount;
        private List<Sprite> _collidedObjects;

        public Vector2 Velocity
        {
            get => _velocity;
            set => _velocity = value;
        }

        public Physics(Sprite target , float weight, float gravityScale = 1)
        {
            _target = target;
            _weight = weight;
            GravityScale = gravityScale;
            _collidedObjects = new List<Sprite>();
        }

        public void Update(GameTime gameTime)
        {
            _velocity.Y += _weight * gameTime.ElapsedGameTime.Milliseconds/3000.0f * GravityScale;
            Grounded = false;
            _collisionsAmount = 0;
            _collidedObjects.Clear();
            
            _target.Move(new Vector2(0, (int)_velocity.Y));
            CheckCollisions(Direction.Vertical);

            _target.Move(new Vector2((int)_velocity.X, 0));
            CheckCollisions(Direction.Horizontal);

            if (_collisionsAmount > _prevCollisionsAmount)
            {
                foreach (var obj in _collidedObjects)
                {
                    OnCollisionEnter(obj);   
                }
            }

            _prevCollisionsAmount = _collisionsAmount;
        }
        
        

        private void CheckCollisions(Direction direction)
        {
            foreach (var obstacle in Game1.CollisionObjects)
            {
                if(obstacle == _target) continue;

                if (_target.Rect.Intersects(obstacle.Rect))
                {
                    Vector2 depth;

                    if (Helpers.Intersects(_target.Rect, obstacle.Rect, direction, out depth))
                    {
                        if (direction == Direction.Horizontal)
                        {
                            _velocity.X = 0;
                            _target.Move(new Vector2((int)depth.X, 0));
                        }
                        else
                        {
                            _velocity.Y = 1;
                            _target.Move(new Vector2(0, (int)depth.Y));
                            if(depth.Y < 0)Grounded = true;
                        }
                        _collidedObjects.Add(obstacle);
                        _collisionsAmount++;
                    }
                }
            }
        }
    }
}