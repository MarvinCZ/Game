using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.GameObjects
{
    public class Layer
    {
        private GameObject[] _objekty;
        public List<GameObject> Objekty = new List<GameObject>();
        public bool CameraDependent { get; protected set; }
        public string Name { get; set; }
        public Layer(bool cameraDependent = true)
        {
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
            }
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
                if (_objekty.Length < Objekty.Count)
                {
                    _objekty = new GameObject[(int)(Objekty.Count * 1.2f)];
                }
                int objectCount = Objekty.Count;
                for (int i = 0; i < _objekty.Length; i++)
                {
                    if (i < objectCount)
                    {
                        _objekty[i] = Objekty[i];
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
            }
        }
    }
}
