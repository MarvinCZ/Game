using Microsoft.Xna.Framework;

namespace GameEngine
{
    public static class Extensions
    {

        public static Vector2 ToVector2(this Point p){
            return new Vector2(p.X,p.Y);
        }

    }
}
