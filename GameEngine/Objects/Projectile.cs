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
        public Projectile(GameScreen game, Vector2 position) : base(game, position){
            Scale = new Vector2(0.2f,0.2f);
            float rychlost = GameHelper.Instance.RandomNext(2f, 4f);
            float angel = GameHelper.Instance.RandomNext(0f, MathHelper.TwoPi);
            _smer = new Vector2(rychlost*(float)Math.Cos(angel),rychlost*(float)Math.Sin(angel));
            spriteColorG = 100;
            spriteColorR = 100;
            spriteColorB = 100;
        }

        public override void LoadContent(ContentManager content){
            Texture = content.Load<Texture2D>("Sprites/hvezda");
        }

        public override void Update(GameTime gameTime){
            base.Update(gameTime);
            _zivot--;
            if (_zivot < 0){
                gameScreen.Layers.Values.Single(s => s.Objekty.Contains(this)).Objekty.Remove(this);
            }
            if (_zivot%50 == 0)
                _smer *= -1;
            Position += _smer;
            spriteColorG += _plusG;
            spriteColorR += _plusR;
            spriteColorB += _plusB;
            if (spriteColorG < 50 || spriteColorG > 220)
                _plusG *= -1;
            if (spriteColorR < 50 || spriteColorR > 220)
                _plusR *= -1;
            if (spriteColorB < 50 || spriteColorB > 220)
                _plusB *= -1;
        }
    }
}
