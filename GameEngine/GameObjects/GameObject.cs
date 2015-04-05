using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.GameObjects
{
    public abstract class GameObject{
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
    }
}
