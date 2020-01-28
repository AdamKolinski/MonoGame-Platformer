﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
 using MonoGameMario;

 namespace MonoGamePlatformer
{
    public class TiledSprite : Sprite
    {
        protected float _tileWidth, _tileHeight;
        private readonly bool _multiTiled;
        public int[,] TileIndexes { get; set; }

        public int TileIndex;

        public int Rows { get; set; }

        public int Columns { get; set; }

        public TiledSprite(SpriteBatch spriteBatch, Texture2D texture, Vector2 position, int rows, int columns, int tileIndex) : base(spriteBatch, texture, position)
        {
            Rows = rows;
            Columns = columns;
            TileIndex = tileIndex;
            _multiTiled = false;
            Initialize();
        }

        public TiledSprite(SpriteBatch spriteBatch, Texture2D texture, Vector2 position, Vector2 scale, int columns, int rows, int tileIndex) : base(spriteBatch, texture, position, scale)
        {
            Rows = rows;
            Columns = columns;
            TileIndex = tileIndex;
            _multiTiled = false;
            Initialize();
        }
        
        public TiledSprite(SpriteBatch spriteBatch, Texture2D texture, Vector2 position, int rows, int columns, int[,] tileIndexes) : base(spriteBatch, texture, position)
        {
            Rows = rows;
            Columns = columns;
            TileIndexes = tileIndexes;
            _multiTiled = true;
            Initialize();
        }

        public TiledSprite(SpriteBatch spriteBatch, Texture2D texture, Vector2 position, Vector2 scale, int columns, int rows, int[,] tileIndexes) : base(spriteBatch, texture, position, scale)
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

        public override void Draw()
        {
            if(!_multiTiled)
                _spriteBatch.Draw(_texture, new Rectangle((int)_position.X, (int)_position.Y, (int)_tileWidth, (int)_tileHeight), new Rectangle((int)(_tileWidth/_scale.X * (TileIndex % Columns)), y: (int)(_tileHeight/_scale.Y *(TileIndex / Columns)), width: (int)(_tileWidth/_scale.X), height: (int)(_tileHeight/_scale.Y)),  Color.White, 0, Vector2.Zero,  s, 0);

            if (_multiTiled)
            {
                for (int y = 0; y < TileIndexes.GetLength(0); y++)
                {
                    for (int x = 0; x < TileIndexes.GetLength(1); x++)
                    {
                        Rectangle destinationRect = new Rectangle((int) _position.X + x * (int) _tileWidth, (int) _position.Y + y * (int) _tileHeight, (int)_tileWidth, (int) _tileHeight);
                        Rectangle sourceRect = new Rectangle((int)(_tileWidth/_scale.X * TileIndexes[y, x] % Columns), y: (int)(_tileHeight/_scale.Y *TileIndexes[y, x] / Columns), width: (int)(_tileWidth/_scale.X), height: (int)(_tileHeight/_scale.Y));
                        
                        _spriteBatch.Draw(_texture, destinationRect , sourceRect,  Color.White);
                    }
                }
            }
            //_spriteBatch.Draw(Helpers.pixel, Rect, new Color(0, 0.3f, 0, 0.2f));
        }
    }
}