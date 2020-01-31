using Microsoft.Xna.Framework;
using MonoGameMario.Source.Sprites;

namespace MonoGameMario.Source
{
    public class Camera
    {
        public Matrix Transform { get; private set; }
        public bool Lock;

        public void Update(Sprite target)
        {
            if (!Lock)
            {
                var position = Matrix.CreateTranslation(
                    -target.Rect.Location.X - (target.Rect.Width / 2),
                    -target.Rect.Location.Y - (target.Rect.Height / 2),
                    0
                );

                var offset = Matrix.CreateTranslation(
                    (float) Game1.ScreenWidth / 2,
                    (float) Game1.ScreenHeight / 2,
                    0
                );

                Transform = position * offset;
            }
        }
    }
}