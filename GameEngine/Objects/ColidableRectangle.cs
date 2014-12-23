using System;
using GameEngine.GameObjects;
using GameEngine.HelpObjects;
using GameEngine;
using GameEngine.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Objects
{
    class ColidableRectangle : SpriteObject
    {
        private int typ;
        bool Rup;
        bool Gup;
        bool Bup;
        public ColidableRectangle(GameScreen screen) : base(screen) {
            positionX = GameHelper.Instance.RandomNext(-2000f, 2000f);
            positionY = GameHelper.Instance.RandomNext(-2000f, 2000f);
            spriteColorR = GameHelper.Instance.RandomNext(60, 240);
            spriteColorG = GameHelper.Instance.RandomNext(60, 240);
            spriteColorB = GameHelper.Instance.RandomNext(60, 240);
            if (GameHelper.Instance.RandomBool(0.8f))
            {
                float scale = GameHelper.Instance.RandomNext(0.5f, 2f);
                Scale = new Vector2(scale, scale);
                colisionBox = new ColisionBox(this, ColisionBox.BoxType.Circle);
                typ = 0;
            }
            else
            {
                float scale = GameHelper.Instance.RandomNext(0.5f, 5f);
                Scale = new Vector2(GameHelper.Instance.RandomNext(0.5f, 5f), scale);
                colisionBox = new ColisionBox(this);
                typ = 1;
            }
            Rup = GameHelper.Instance.RandomBool(0.5f);
            Gup = GameHelper.Instance.RandomBool(0.5f);
            Bup = GameHelper.Instance.RandomBool(0.5f);
            solid = true;
        }

    
        public override void LoadContent(ContentManager content)
        {
            if (Texture == null)
            {
                if(typ == 1)
                    Texture = content.Load<Texture2D>("Sprites/ctverecek");
                else
                    Texture = content.Load<Texture2D>("Sprites/hvezda");
            }
        }

        public override void Update(GameTime gameTime)
        {
            spriteColorR += Rup ? 1 : -1;
            spriteColorG += Gup ? 1 : -1;
            spriteColorB += Bup ? 1 : -1;
            if (spriteColorR > 240)
                Rup = false;
            if (spriteColorG > 240)
                Gup = false;
            if (spriteColorB > 240)
                Bup = false;
            if (spriteColorR < 60)
                Rup = true;
            if (spriteColorG < 60)
                Gup = true;
            if (spriteColorB < 60)
                Bup = true;
        }

        public void OnColide(SpriteObject obj){
            spriteColorR += 5;
            spriteColorG -= 5;
            spriteColorB += 5;
            if (spriteColorR > 255)
                spriteColorR = 0;
            if (spriteColorG < 0)
                spriteColorG = 255;
            if (spriteColorB > 255)
                spriteColorB = 0;
        }
    }
}
