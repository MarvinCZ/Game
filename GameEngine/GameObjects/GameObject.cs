using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace GameEngine.GameObjects
{
    public abstract class GameObject{
        protected Game game;
        protected GameScreen gameScreen;
        /// <summary>
        /// Vrati pocet volani metody update
        /// </summary>
        protected int updateCount;

        protected GameObject(GameScreen game){
            this.game = game.Game;
            gameScreen = game;
        }

        public virtual void Update(GameTime gameTime){
            updateCount++;
        }

        public abstract void LoadContent(ContentManager content);
        public abstract void UnloadContent();
    }
}
