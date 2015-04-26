using GameEngine;
using GameEngine.GameObjects;
using GameEngine.HelpObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SecretOfThePast.Objects
{
    class ColidableRectangle : SpriteObject
    {
        private int typ;
        bool Rup;
        bool Gup;
        bool Bup;
        public ColidableRectangle(GameScreen screen,Color color) : base(screen) {
            PositionX = GameHelper.Instance.RandomNext(-2000f, 2000f);
            PositionY = GameHelper.Instance.RandomNext(-2000f, 2000f);
            SpriteColor = color;
            if (GameHelper.Instance.RandomBool(0.8f))
            {
                float scale = GameHelper.Instance.RandomNext(0.5f, 2f);
                Scale = new Vector2(scale, scale);
                ColisionBox = new ColisionBox(this, ColisionBox.BoxType.Circle);
                typ = 0;
            }
            else
            {
                float scale = GameHelper.Instance.RandomNext(0.5f, 5f);
                Scale = new Vector2(GameHelper.Instance.RandomNext(0.5f, 5f), scale);
                ColisionBox = new ColisionBox(this);
                typ = 1;
            }
            Rup = GameHelper.Instance.RandomBool(0.5f);
            Gup = GameHelper.Instance.RandomBool(0.5f);
            Bup = GameHelper.Instance.RandomBool(0.5f);
            Solid = true;
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
            //SpriteColorR += Rup ? 1 : -1;
            //SpriteColorG += Gup ? 1 : -1;
            //SpriteColorB += Bup ? 1 : -1;
            if (SpriteColorR > 240)
                Rup = false;
            if (SpriteColorG > 240)
                Gup = false;
            if (SpriteColorB > 240)
                Bup = false;
            if (SpriteColorR < 60)
                Rup = true;
            if (SpriteColorG < 60)
                Gup = true;
            if (SpriteColorB < 60)
                Bup = true;
        }

        public void OnColide(SpriteObject obj){
            SpriteColorR += 5;
            SpriteColorG -= 5;
            SpriteColorB += 5;
            if (SpriteColorR > 255)
                SpriteColorR = 0;
            if (SpriteColorG < 0)
                SpriteColorG = 255;
            if (SpriteColorB > 255)
                SpriteColorB = 0;
        }
    }
}
