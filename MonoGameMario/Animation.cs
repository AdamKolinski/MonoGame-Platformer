using MonoGamePlatformer;

namespace MonoGameMario
{
    public class Animation
    {
        public string Name { get; }
        private TiledSprite _sourceTexture;
        public int[] Frames { get; }
        public int CurrentFrame { get; set; }
        public bool Repeat { get; }
        public float Duration { get; }

        public Animation(int[] frames, string name, float duration, bool repeat)
        {
            Frames = frames;
            Name = name;
            
            CurrentFrame = 0;
            Repeat = repeat;
            Duration = duration;
        }
    }
}