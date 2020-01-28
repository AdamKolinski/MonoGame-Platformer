using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace MonoGameMario
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
    
    public class Input
    {
        private static readonly List<Axis> Axes = new List<Axis>();

        public static void Initialize()
        {
            Axis horizontal = new Axis("Horizontal", Keys.D, Keys.A);
            Axes.Add(horizontal);
        }

        public static float GetAxis(string name)
        {
            foreach (var axis in Axes)
            {
                if (axis.name == name)
                    return axis.value;
            }
            return 0;
        }

        public static void Update()
        {
            foreach (var axis in Axes)
            {
                if (Keyboard.GetState().IsKeyDown(axis.positiveKey)) axis.value = 1;
                else if (Keyboard.GetState().IsKeyDown(axis.negativeKey)) axis.value = -1;
                else axis.value = 0;
            }
        }
        
    }
}