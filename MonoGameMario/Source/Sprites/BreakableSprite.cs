using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameMario.Source.Sprites
{
    public class BreakableSprite : TiledSprite, IDestructible
    {
        public void Destroy()
        {
            Console.WriteLine("Destroy handling");
            Game1.CollisionObjects.Remove(this);
        }

        #region Constructors
        public BreakableSprite(SpriteBatch spriteBatch, Texture2D texture, Vector2 position, int rows, int columns, int tileIndex) : base(spriteBatch, texture, position, rows, columns, tileIndex)
        {
            Rows = rows;
            Columns = columns;
            TileIndex = tileIndex;
            _multiTiled = false;
            Initialize();
        }

        public BreakableSprite(SpriteBatch spriteBatch, Texture2D texture, Vector2 position, Vector2 scale, int columns, int rows, int tileIndex) : base(spriteBatch, texture, position, scale, columns, rows, tileIndex)
        {
            Rows = rows;
            Columns = columns;
            TileIndex = tileIndex;
            _multiTiled = false;
            Initialize();
        }
        
        public BreakableSprite(SpriteBatch spriteBatch, Texture2D texture, Vector2 position, int rows, int columns, int[,] tileIndexes) : base(spriteBatch, texture, position, rows, columns, tileIndexes)
        {
            Rows = rows;
            Columns = columns;
            TileIndexes = tileIndexes;
            _multiTiled = true;
            Initialize();
        }

        public BreakableSprite(SpriteBatch spriteBatch, Texture2D texture, Vector2 position, Vector2 scale, int columns, int rows, int[,] tileIndexes) : base(spriteBatch, texture, position, scale, columns, rows, tileIndexes)
        {
            Rows = rows;
            Columns = columns;
            TileIndexes = tileIndexes;
            _multiTiled = true;
            Initialize();
        }

        protected override void Initialize()
        {
            _tileWidth = _texture.Width * _scale.X / Columns;
            _tileHeight = _texture.Height * _scale.Y / Rows;
            Rect = new Rectangle(_position.ToPoint(), new Point((int)_tileWidth, (int)_tileHeight));
        }
        #endregion
    }
}