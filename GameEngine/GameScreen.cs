using System.Collections.Generic;
using GameEngine.Cameras;
using GameEngine.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine
{
    /// <summary>
    /// Hlavni logika Druhy rozcesti
    /// </summary>
    public abstract class GameScreen{
        private SpriteBatch _spriteBatch;
        private List<GameObject> _pauseObjects;

        protected bool Paused;
        public readonly ContentManager ContentManager;

        public readonly ScreenManager ScreenManager;
        public IFollowable ShowUnder;
        public abstract string Name { get; }
        public readonly Dictionary<string,Layer> Layers = new Dictionary<string, Layer>();
        public Camera MainCam { get; protected set; }

        protected GameScreen(ScreenManager screenManager){
            ScreenManager = screenManager;
            ContentManager = new ContentManager(screenManager.Services);
            ContentManager.RootDirectory = screenManager.Content.RootDirectory;
            Layers["Background"] = new Layer(this);
            Layers["Main"] = new Layer(this);
            Layers["Foreground"] = new Layer(this);
            Layers["Gui"] = new Layer(this,false);
        }

        /// <summary>
        /// Postara se o vytvoreni pole pro objekty
        /// </summary>
        public virtual void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(ScreenManager.GraphicsDevice);
            foreach (KeyValuePair<string, Layer> layer in Layers)
            {
                layer.Value.LoadContent(ContentManager,layer.Key);
            }
        }

        public void UnloadContent(){
            ContentManager.Unload();
            _pauseObjects = new List<GameObject>();
            Paused = false;
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
            if (Paused){
                foreach (GameObject pauseObject in _pauseObjects){
                    pauseObject.Update(gameTime);
                }
            }
            else{
                foreach (Layer layer in Layers.Values)
                    layer.Update(gameTime);
            }
            if (MainCam != null)
                MainCam.Update();
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public virtual void Draw(GameTime gameTime){
            Rectangle view = ScreenManager.GraphicsDevice.Viewport.Bounds;
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
                view = ScreenManager.GraphicsDevice.Viewport.Bounds;
                _spriteBatch.End();
                _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
            }
            foreach (Layer layer in Layers.Values)
                if (!layer.CameraDependent)
                    layer.Draw(gameTime, _spriteBatch, view);
            _spriteBatch.End();

        }

        public void Pause(List<GameObject> pauseObjects)
        {
            Paused = true;
            _pauseObjects = pauseObjects;
        }

        public void UnPause()
        {
            Paused = false;
            _pauseObjects = null;
        }
    }
}
