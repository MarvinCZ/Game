using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace GameEngine.GameObjects
{
    public abstract class GameObject{
        protected readonly Game ScreenManager;
        protected readonly GameScreen GameScreen;
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

        public abstract void LoadContent(ContentManager content);
        public abstract void UnloadContent();
    }
}
