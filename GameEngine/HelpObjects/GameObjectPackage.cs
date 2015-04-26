using System;
using GameEngine.GameObjects;
using Microsoft.Xna.Framework;

namespace GameEngine.HelpObjects
{
    [Serializable]
    public class GameObjectPackage
    {
        public Vector2 Position;
        public string Type;
        public string Layer;
        public string MetaData;

        public GameObjectPackage(GameObject objekt)
        {
            Type = objekt.GetType().FullName;
            SpriteObject spriteObject = objekt as SpriteObject;
            if (spriteObject != null){
                Position = spriteObject.Position;
            }
            MetaData = objekt.MetaData;
            Layer = objekt.Layer.Name;
        }
        public GameObjectPackage() { }
    }
}
