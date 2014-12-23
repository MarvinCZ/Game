using Microsoft.Xna.Framework;

namespace GameEngine.Cameras
{
    interface IFollowable
    {
        /// <summary>
        /// Vrati pozici, kterou kamera sleduje
        /// </summary>
        Vector2 Position { get; }
        /// <summary>
        /// Vrati origin sledovaneho objektu
        /// </summary>
        Vector2 Origin { get; }
    }
}
