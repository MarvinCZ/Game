using System.Collections.Generic;
using GameEngine.GameObjects;
using GameEngine.Cameras;
using GameEngine.Screens;
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
        private GameObject[] _objekty;
        protected MessageBox messageBox;
        protected readonly ContentManager contentManager;
        public readonly Game Game;
        public abstract string Name { get; }
        public List<GameObject> GameObjects = new List<GameObject>();
        public Camera MainCam { get; protected set; }

        protected GameScreen(ScreenManager screenManager){
            Game = screenManager;
            contentManager = new ContentManager(screenManager.Services);
            contentManager.RootDirectory = screenManager.Content.RootDirectory;
        }

        /// <summary>
        /// Postara se o vytvoreni pole pro objekty
        /// </summary>
        public virtual void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            if (_objekty == null)
            {
                _objekty = new GameObject[(int)MathHelper.Max(20, GameObjects.Count * 1.2f)];
            }
        }

        public void UnloadContent(){
            contentManager.Unload();
            GameObjects = new List<GameObject>();
            _objekty = null;
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
            else if (_objekty != null)
            {
                if (_objekty.Length < GameObjects.Count)
                {
                    _objekty = new GameObject[(int)(GameObjects.Count * 1.2f)];
                }
                int objectCount = GameObjects.Count;
                for (int i = 0; i < _objekty.Length; i++)
                {
                    if (i < objectCount)
                    {
                        _objekty[i] = GameObjects[i];
                    }
                    else
                    {
                        _objekty[i] = null;
                    }
                }
                for (int i = 0; i < objectCount; i++)
                {
                    _objekty[i].Update(gameTime);
                }
                if (MainCam != null)
                    MainCam.Update();
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public virtual void Draw(GameTime gameTime){
            Rectangle view;
            if (MainCam != null){
                view = MainCam.ViewRectangle;
                _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, null, null, null, null,
                    MainCam.TransformMatrix);
                int j = 0;
                while (_objekty.Length > j && _objekty[j] != null){
                    if (_objekty[j] is SpriteObject && ((SpriteObject) _objekty[j]).CameraDependent){
                        SpriteObject so = (SpriteObject) _objekty[j];
                        if(view.Intersects(so.BoundingBox))
                            so.Draw(gameTime, _spriteBatch);
                    }
                    j++;
                }
                _spriteBatch.End();
            }
            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied);
            int i = 0;
            while (_objekty.Length > i && _objekty[i] != null){
                if (_objekty[i] is SpriteObject &&
                    (MainCam == null || !((SpriteObject) _objekty[i]).CameraDependent)){
                    ((SpriteObject) _objekty[i]).Draw(gameTime, _spriteBatch);
                }
                i++;
            }
            if (messageBox != null && messageBox.Active)
                messageBox.Draw(gameTime, _spriteBatch);
            _spriteBatch.End();

        }
    }
}
