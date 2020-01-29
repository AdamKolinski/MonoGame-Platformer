using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGameMario.Source;
using MonoGameMario.Source.InputSystem;
using MonoGameMario.Source.Sprites;

public class Camera
{
    public Matrix Transform { get; private set; }
    public bool Lock;
    
    public void Update(Sprite _target)
    {
        if(Lock) return;
        
        var position = Matrix.CreateTranslation(
            -_target.Rect.Location.X - (_target.Rect.Width / 2),
            -_target.Rect.Location.Y - (_target.Rect.Height / 2),
            0
            );

        var offset = Matrix.CreateTranslation(
            Game1.ScreenWidth / 2,
            Game1.ScreenHeight /2,
            0
            );

        Transform = position * offset;
    }
}