using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.GameObjects
{
    public class Layer
    {
        private GameObject[] _objekty;

        protected readonly GameScreen GameScreen;
        protected readonly ScreenManager ScreenManager;

        public List<GameObject> Objekty = new List<GameObject>();
        public bool CameraDependent { get; protected set; }
        public string Name { get; set; }

        public Layer(GameScreen gameScreen,bool cameraDependent = true)
        {
            GameScreen = gameScreen;
            ScreenManager = GameScreen.ScreenManager;
            CameraDependent = cameraDependent;
        }

        public void LoadContent(ContentManager contentManager,string name)
        {
            Name = name;
            if (_objekty == null)
            {
                _objekty = new GameObject[(int)MathHelper.Max(20, Objekty.Count * 1.2f)];
            }
            foreach (GameObject objekt in Objekty)
            {
                objekt.LoadContent(contentManager);
                objekt.Layer = this;
            }
        }
        /// <summary>
        /// Pouzit kdyz se objekt nepridava v metode LoadContent
        /// </summary>
        /// <param name="obj">Objekt k pridani</param>
        public void AddObject(GameObject obj)
        {
            Objekty.Add(obj);
            obj.LoadContent(ScreenManager.Content);
            obj.Layer = this;
        }
        public void UnloadContent()
        {
            foreach (GameObject ob in Objekty)
            {
                ob.UnloadContent();
            }
            _objekty = null;
            Objekty = new List<GameObject>();
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Rectangle view)
        {
            int i = 0;
            while (_objekty.Length > i && _objekty[i] != null)
            {
                if (_objekty[i] is SpriteObject)
                {
                    SpriteObject so = (SpriteObject)_objekty[i];
                    if (view.Intersects(so.BoundingBox))
                        so.Draw(gameTime, spriteBatch);
                }
                i++;
            }

        }
        public void Update(GameTime gameTime)
        {
            if (_objekty != null)
            {
                MoveObjects();
                int objectCount = Objekty.Count;
                if (Name == "Foreground" && GameScreen.ShowUnder != null){
                    Vector2 center = GameScreen.ShowUnder.Position;
                    Rectangle centerRect = new Rectangle((int)center.X - 40, (int)center.Y - 40, 80, 80);
                    for (int i = 0; i < objectCount; i++)
                    {
                        if (_objekty[i] is SpriteObject){
                            if (((SpriteObject)_objekty[i]).BoundingBox.Intersects(centerRect) && ((SpriteObject)_objekty[i]).SpriteColorAlfa > 0.1f)
                            {
                                ((SpriteObject) _objekty[i]).SpriteColorAlfa -= 0.05f;
                            }
                            else if (((SpriteObject)_objekty[i]).SpriteColorAlfa < 1f)
                            {
                                ((SpriteObject)_objekty[i]).SpriteColorAlfa += 0.05f;
                            }
                        }
                        _objekty[i].Update(gameTime);
                    }

                }
                for (int i = 0; i < objectCount; i++)
                {
                    _objekty[i].Update(gameTime);
                }
                SortByY();
            }
        }

        public void SortByY()
        {
            Objekty.Sort();
        }

        public void MoveObjects()
        {
            if (_objekty != null){
                if (_objekty.Length < Objekty.Count){
                    _objekty = new GameObject[(int) (Objekty.Count*1.2f)];
                }
                int objectCount = Objekty.Count;
                for (int i = 0; i < _objekty.Length; i++){
                    if (i < objectCount){
                        _objekty[i] = Objekty[i];
                    }
                    else{
                        _objekty[i] = null;
                    }
                }
            }
        }
    }
}
