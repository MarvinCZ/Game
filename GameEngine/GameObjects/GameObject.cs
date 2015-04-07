using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.GameObjects
{
    public abstract class GameObject : IComparable<GameObject>
    {
        protected readonly ScreenManager ScreenManager;
        protected readonly GameScreen GameScreen;
        public Layer Layer;
        /// <summary>
        /// Vrati pocet volani metody update
        /// </summary>
        protected int UpdateCount;

        protected GameObject(GameScreen game){
            ScreenManager = game.ScreenManager;
            GameScreen = game;
        }

        public virtual void Update(GameTime gameTime){
            UpdateCount++;
        }

        protected virtual void Destroy()
        {
            Layer.Objekty.Remove(this);
            UnloadContent();
        }

        public abstract void LoadContent(ContentManager content);
        public abstract void UnloadContent();
        public int CompareTo(GameObject other)
        {
            if (this is SpriteObject && other is SpriteObject)
                return (int)(((SpriteObject) this).Position.Y - ((SpriteObject) other).Position.Y);
            if (this is SpriteObject || other is SpriteObject)
                return this is SpriteObject ? 1 : -1;
            return 0;
        }
    }
}
