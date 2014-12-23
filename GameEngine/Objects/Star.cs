using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameEngine.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Objects
{
    class Star : SpriteObject{
        private Vector2 _smer;
        private float _rychlost = 15f;
        private int _zivot = 200;
        private Vector2 scaling = new Vector2(1.03f,1.03f);
        public Star(GameScreen game) : this(game, new Vector2(0, 0))
        {

        }

        public Star(GameScreen game, Vector2 position) : base(game, position){
            Scale = new Vector2(0.05f,0.05f);
            _smer = new Vector2(
                GameHelper.Instance.RandomNext(-1f,1f),
                GameHelper.Instance.RandomNext(-1f, 1f));
            SpriteColor = new Color(
                GameHelper.Instance.RandomNext(0.4f, 1f),
                GameHelper.Instance.RandomNext(0.4f, 1f),
                GameHelper.Instance.RandomNext(0.4f, 1f)
                );
            //_smer.Normalize();
        }

        public override void LoadContent(ContentManager content){
            Texture = content.Load<Texture2D>("Sprites/hvezda");
        }

        public override void Update(GameTime gameTime){
            _zivot--;
            if (_zivot < 0){
                gameScreen.GameObjects.Remove(this);
            }
            Position += _smer*_rychlost;
            Scale *= scaling;
            if (Scale.X > 2f){
                scaling = new Vector2(0.95f,0.95f);
            }
            base.Update(gameTime);
        }
    }
}
