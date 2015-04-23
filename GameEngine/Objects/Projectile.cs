using System;
using System.Linq;
using GameEngine.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Objects
{
    class Projectile : SpriteObject{
        private Vector2 _smer;
        private int _zivot = 1000;
        private int _plusG = -1;
        private int _plusR = 2;
        private int _plusB = 1;
        public Projectile(GameScreen game, Vector2 position, string metaData) : base(game, position,metaData){
            Scale = new Vector2(0.2f,0.2f);
            float rychlost = GameHelper.Instance.RandomNext(2f, 4f);
            float angel = GameHelper.Instance.RandomNext(0f, MathHelper.TwoPi);
            _smer = new Vector2(rychlost*(float)Math.Cos(angel),rychlost*(float)Math.Sin(angel));
            SpriteColorG = 100;
            SpriteColorR = 100;
            SpriteColorB = 100;
        }

        public override void LoadContent(ContentManager content){
            Texture = content.Load<Texture2D>("Sprites/hvezda");
        }

        public override void Update(GameTime gameTime){
            base.Update(gameTime);
            _zivot--;
            if (_zivot < 0){
                GameScreen.Layers.Values.Single(s => s.Objekty.Contains(this)).Objekty.Remove(this);
            }
            if (_zivot%50 == 0)
                _smer *= -1;
            Position += _smer;
            SpriteColorG += _plusG;
            SpriteColorR += _plusR;
            SpriteColorB += _plusB;
            if (SpriteColorG < 50 || SpriteColorG > 220)
                _plusG *= -1;
            if (SpriteColorR < 50 || SpriteColorR > 220)
                _plusR *= -1;
            if (SpriteColorB < 50 || SpriteColorB > 220)
                _plusB *= -1;
        }
    }
}
