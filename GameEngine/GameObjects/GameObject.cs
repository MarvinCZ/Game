using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

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
            //TODO moc narocny na 60xS
            SpriteObject th = this as SpriteObject;
            SpriteObject ot = other as SpriteObject;
            if (th != null && ot != null)
                return (int)(th.Position.Y - ot.Position.Y);
            if (th != null || ot != null)
                return th != null ? 1 : -1;
            return 0;
        }
    }
}
