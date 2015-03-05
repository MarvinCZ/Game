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

        private GameObject[] _guiObjekty;
        private GameObject[] _backgroundObjekty;
        private GameObject[] _foregroundObjekty;
        private GameObject[] _objekty;

        public MessageBox messageBox;
        protected readonly ContentManager contentManager;
        public readonly Game Game;
        public abstract string Name { get; }
        public List<GameObject> GuiObjects = new List<GameObject>();
        public List<GameObject> ForegroundGameObjects = new List<GameObject>();
        public List<GameObject> GameObjects = new List<GameObject>();
        public List<GameObject> BackgroundGameObjects = new List<GameObject>();
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
            layerLoadContent(ref _backgroundObjekty, BackgroundGameObjects);
            layerLoadContent(ref _objekty, GameObjects);
            layerLoadContent(ref _foregroundObjekty, ForegroundGameObjects);
            layerLoadContent(ref _guiObjekty, GuiObjects);
        }

        public void UnloadContent(){
            contentManager.Unload();
            layerUnloadContent(ref _backgroundObjekty,ref BackgroundGameObjects);
            layerUnloadContent(ref _objekty,ref GameObjects);
            layerUnloadContent(ref _foregroundObjekty,ref ForegroundGameObjects);
            layerUnloadContent(ref _guiObjekty,ref GuiObjects);
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
                layerUpdate(ref _guiObjekty, GuiObjects, gameTime);
                layerUpdate(ref _foregroundObjekty,ForegroundGameObjects, gameTime);
                layerUpdate(ref _objekty,GameObjects, gameTime);
                layerUpdate(ref _backgroundObjekty,BackgroundGameObjects, gameTime);
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
            layerDraw(_spriteBatch, _backgroundObjekty, view, gameTime);
            layerDraw(_spriteBatch, _objekty, view, gameTime);
            layerDraw(_spriteBatch, _foregroundObjekty, view, gameTime);
            if (MainCam != null)
            {
                view = Game.GraphicsDevice.Viewport.Bounds;
                _spriteBatch.End();
                _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
            }
            layerDraw(_spriteBatch, _guiObjekty, view, gameTime);
            if (messageBox != null && messageBox.Active)
                messageBox.Draw(gameTime, _spriteBatch);
            _spriteBatch.End();

        }
        private void layerUpdate(ref GameObject[] objekty,List<GameObject> obj, GameTime gameTime)
        {
            if (objekty != null)
            {
                if (objekty.Length < obj.Count)
                {
                    objekty = new GameObject[(int)(obj.Count * 1.2f)];
                }
                int objectCount = obj.Count;
                for (int i = 0; i < objekty.Length; i++)
                {
                    if (i < objectCount)
                    {
                        objekty[i] = obj[i];
                    }
                    else
                    {
                        objekty[i] = null;
                    }
                }
                for (int i = 0; i < objectCount; i++)
                {
                    objekty[i].Update(gameTime);
                }
            }
        }
        private void layerDraw(SpriteBatch sprBatch,GameObject[] objekty,Rectangle view,GameTime gameTime)
        {
            int i = 0;
            while(objekty.Length > i && objekty[i] != null){
                if(objekty[i] is SpriteObject){
                    SpriteObject so = (SpriteObject)objekty[i];
                    if (view.Intersects(so.BoundingBox))
                        so.Draw(gameTime, sprBatch);
                }
                i++;
            }
        }
        private void layerLoadContent(ref GameObject[] objekty, List<GameObject> obj)
        {
            if (objekty == null)
            {
                objekty = new GameObject[(int)MathHelper.Max(20, obj.Count * 1.2f)];
            }
            foreach (GameObject ob in obj)
            {
                ob.LoadContent(contentManager);
            }
        }
        private void layerUnloadContent(ref GameObject[] objekty,ref List<GameObject> obj)
        {
            foreach (GameObject ob in obj)
            {
                ob.UnloadContent();
            }
            objekty = null;
            obj = new List<GameObject>();
        }
    }
}
