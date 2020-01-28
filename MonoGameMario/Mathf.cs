namespace MonoGameMario
{
    public class Mathf
    {
        public static float Lerp(float a, float b, float by)
        {
            return a * (1 - by) + b * by;
        }

    }
}