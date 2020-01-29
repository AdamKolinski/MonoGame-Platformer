using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MonoGameMario.Source.Animation
{
    public class Animator
    {
        private Dictionary<string, Animation> _animations;
        private Animation _currentAnimation;
        private bool _play;
        private float _timer;

        public Animator()
        {
            _animations = new Dictionary<string, Animation>();
        }

        public void CreateAnimation(string name, int[] frames, float duration ,bool repeat)
        {
            Animation animation = new Animation(frames, name, duration, repeat);
            _animations.Add(name, animation);
        }

        public void Play(string name)
        {
            if (_animations.TryGetValue(name, out _currentAnimation))
            {
                _play = true;
            }
        }

        public void Stop(string name)
        {
            if (_animations.TryGetValue(name, out _currentAnimation))
            {
                _play = false;
            }
        }

        public void Update(GameTime gameTime, ref int tileIndex)
        {
            if (_play)
            {
                if (_timer >= _currentAnimation.Duration / _currentAnimation.Frames.Length)
                {
                    _currentAnimation.CurrentFrame++;
                    if (_currentAnimation.CurrentFrame >= _currentAnimation.Frames.Length)
                    {
                        if (_currentAnimation.Repeat)
                            _currentAnimation.CurrentFrame = 0;
                        else
                        {
                            _currentAnimation.CurrentFrame = _currentAnimation.Frames.Length - 1;
                            Stop(_currentAnimation.Name);
                        }
                    }

                    _timer = 0;
                }
                else
                {
                    _timer += gameTime.ElapsedGameTime.Milliseconds / 1000f;
                }

                tileIndex = _currentAnimation.Frames[_currentAnimation.CurrentFrame];
            }
        }
    }
}