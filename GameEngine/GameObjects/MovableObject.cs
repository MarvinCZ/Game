using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace GameEngine.GameObjects
{
    public abstract class MovableObject : SpriteObject
    {
        protected bool colideX;
        protected bool colideY;
        protected float rychlost;
        protected Vector2 smer;
        public MovableObject(GameScreen game) : base(game){
            SpriteColor = Color.Red;
            smer = new Vector2(0, 0);
            rychlost = 5f;
            Solid = true;
        }

        public override void Update(GameTime gameTime)
        {
            colideX = false;
            colideY = false;
            //TODO: melo by zmizet
            if (kolize(GameScreen.Layers["SolidObjects"].Objekty))
            {
                Position += smer * rychlost;
            }
            else if (smer.X != 0 || smer.Y != 0)
            {
                smer.Normalize();
                Position += new Vector2(smer.X, 0) * rychlost;
                while (kolize(GameScreen.Layers["SolidObjects"].Objekty) || kolize(GameScreen.Layers["MovebleObjects"].Objekty))
                {
                    colideX = true;
                    Position -= new Vector2(smer.X, 0)*0.5f;
                }
                Position += new Vector2(0, smer.Y) * rychlost;
                while (kolize(GameScreen.Layers["SolidObjects"].Objekty) || kolize(GameScreen.Layers["MovebleObjects"].Objekty))
                {
                    colideY = true;
                    Position -= new Vector2(0, smer.Y) * 0.5f;
                }
            }
            base.Update(gameTime);
        }

        public abstract override void LoadContent(ContentManager content);
        bool kolize(List<GameObject> obj){
            for (int i = 0; i < obj.Count; i++)
            {
                if (obj[i] is SpriteObject && ((SpriteObject)obj[i]).IsSolid && obj[i] != this)
                {
                    if (((SpriteObject)obj[i]).ColisionBox.ColideWhith(ColisionBox))
                        return true;
                }
            }
            return false;
        }
    }
}
