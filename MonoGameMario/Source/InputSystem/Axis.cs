using Microsoft.Xna.Framework.Input;

namespace MonoGameMario.Source.InputSystem
{
    public class Axis
    {
        public string name;
        public float value;
        public Keys positiveKey, negativeKey;

        public Axis(string name, Keys positiveKey, Keys negativeKey)
        {
            this.name = name;
            this.positiveKey = positiveKey;
            this.negativeKey = negativeKey;
        }
    }
}