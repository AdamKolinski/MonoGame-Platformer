using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameMario.Source.Sprites
{
    public class Sprite
    {
        protected SpriteEffects s = SpriteEffects.FlipHorizontally;
        protected Texture2D _texture;
        protected Vector2 _position, _scale;
        protected SpriteBatch _spriteBatch;
        public Rectangle Rect;

        public void Move(Vector2 translation)
        {
            _position += new Vector2((int)translation.X, (int)translation.Y);
            Rect.Location += translation.ToPoint();
        }

        public Sprite(SpriteBatch spriteBatch, Texture2D texture, Vector2 position)
        {
            _spriteBatch = spriteBatch;
            _texture = texture;
            _position = position;
            _scale = Vector2.One;
        }

        public Sprite(SpriteBatch spriteBatch, Texture2D texture, Vector2 position, Vector2 scale)
        {
            _spriteBatch = spriteBatch;
            _texture = texture;
            _position = position;
            _scale = scale;
            //Texture2D e = new Texture2D();
        }

        protected virtual void Initialize()
        {
            Rect = new Rectangle(_position.ToPoint(), _texture.Bounds.Size * _scale.ToPoint());
        }

        public virtual void Update(GameTime gameTime)
        {
            
        }
        
        public virtual void Draw()
        {
            _spriteBatch.Draw(_texture, new Rectangle((int)_position.X, (int)_position.Y, (int)(_texture.Width*_scale.X), (int)(_texture.Height*_scale.Y)), Color.White);
            
        }
    }
}