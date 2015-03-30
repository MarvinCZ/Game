using System.Collections.Generic;
using GameEngine.GameObjects;
using GameEngine.Cameras;
using GameEngine.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using System.Windows.Forms.VisualStyles;


namespace GameEngine
{
    /// <summary>
    /// Hlavni logika Druhy rozcesti
    /// </summary>
    public abstract class GameScreen{
        private SpriteBatch _spriteBatch;

        public MessageBox messageBox;
        protected readonly ContentManager contentManager;
        public readonly Game Game;
        public abstract string Name { get; }

        public readonly Dictionary<string,Layer> Layers = new Dictionary<string, Layer>();
        public Camera MainCam { get; protected set; }

        protected GameScreen(ScreenManager screenManager){
            Game = screenManager;
            contentManager = new ContentManager(screenManager.Services);
            contentManager.RootDirectory = screenManager.Content.RootDirectory;
            Layers["Background"] = new Layer();
            Layers["SolidObjects"] = new Layer();
            Layers["MovebleObjects"] = new Layer();
            Layers["Foreground"] = new Layer();
            Layers["Gui"] = new Layer(false);
        }

        /// <summary>
        /// Postara se o vytvoreni pole pro objekty
        /// </summary>
        public virtual void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            foreach (KeyValuePair<string, Layer> layer in Layers)
            {
                layer.Value.LoadContent(contentManager,layer.Key);
            }
        }

        public void UnloadContent(){
            contentManager.Unload();
            foreach (Layer layer in Layers.Values)
            {
                layer.UnloadContent();
            }
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public virtual void Update(GameTime gameTime)
        {
            if (messageBox != null && messageBox.Active)
                messageBox.Update(gameTime);
            else
            {

                foreach (Layer layer in Layers.Values)
                    layer.Update(gameTime);
                if (MainCam != null)
                    MainCam.Update();
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public virtual void Draw(GameTime gameTime){
            Rectangle view = Game.GraphicsDevice.Viewport.Bounds;
            if (MainCam != null)
            {
                view = MainCam.ViewRectangle;
                _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, null, null, null, null,
                    MainCam.TransformMatrix);
            }
            else
                _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
            foreach (Layer layer in Layers.Values)
                if(layer.CameraDependent)
                    layer.Draw(gameTime,_spriteBatch,view);
            if (MainCam != null)
            {
                view = Game.GraphicsDevice.Viewport.Bounds;
                _spriteBatch.End();
                _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
            }
            foreach (Layer layer in Layers.Values)
                if (!layer.CameraDependent)
                    layer.Draw(gameTime, _spriteBatch, view);
            if (messageBox != null && messageBox.Active)
                messageBox.Draw(gameTime, _spriteBatch);
            _spriteBatch.End();

        }
    }
}
