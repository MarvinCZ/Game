using System;
using System.Collections.Generic;
using GameEngine.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace GameEngine.GameObjects
{
    public abstract class MovableObject : SpriteObject
    {
        protected bool ColideX;
        protected bool ColideY;
        protected float Rychlost;
        protected Vector2 Smer;
        public List<SpriteObject> KolidedObjects = new List<SpriteObject>();
        public MovableObject(GameScreen game) : base(game){
            SpriteColor = Color.Red;
            Smer = new Vector2(0, 0);
            Rychlost = 1.5f;
            Solid = true;
        }

        public override void Update(GameTime gameTime)
        {
            ColideX = false;
            ColideY = false;
            //TODO: melo by zmizet
            if (Kolize(GameScreen.Layers["SolidObjects"].Objekty) != null)
            {
                Position += Smer * Rychlost;
            }
            else if (Smer.X != 0f || Smer.Y != 0)
            {
                Smer.Normalize();
                Position += new Vector2(Smer.X, 0) * Rychlost;
                KolidedObjects = new List<SpriteObject>();
                SpriteObject kolided;
                while ((kolided = Kolize(GameScreen.Layers["SolidObjects"].Objekty)) != null || (kolided = Kolize(GameScreen.Layers["MovebleObjects"].Objekty)) != null)
                {
                    if(!KolidedObjects.Contains(kolided))
                        KolidedObjects.Add(kolided);
                    ColideX = true;
                    Position -= new Vector2(Smer.X, 0) * 0.5f;
                }
                Position += new Vector2(0, Smer.Y) * Rychlost;
                while ((kolided = Kolize(GameScreen.Layers["SolidObjects"].Objekty)) != null || (kolided = Kolize(GameScreen.Layers["MovebleObjects"].Objekty)) != null)
                {
                    if (!KolidedObjects.Contains(kolided))
                        KolidedObjects.Add(kolided);
                    ColideY = true;
                    Position -= new Vector2(0, Smer.Y) * 0.5f;
                }
                foreach (SpriteObject kolidedObject in KolidedObjects)
                {
                    if (!(kolidedObject is Bounci)){
                        Console.Write("random");
                    }
                    if (!(kolidedObject is MovableObject && ((MovableObject) kolidedObject).KolidedObjects.Contains(this))){
                        if (kolidedObject is ICollisionReaction)
                            ((ICollisionReaction) kolidedObject).CollisionReaction(this);
                        if (this is ICollisionReaction)
                            ((ICollisionReaction) this).CollisionReaction(kolidedObject);
                    }
                }
            }
            base.Update(gameTime);
        }

        public abstract override void LoadContent(ContentManager content);

        SpriteObject Kolize(IReadOnlyList<GameObject> obj)
        {
            for (int i = 0; i < obj.Count; i++)
            {
                if (obj[i] is SpriteObject && ((SpriteObject)obj[i]).IsSolid && obj[i] != this)
                {
                    if (((SpriteObject)obj[i]).ColisionBox.ColideWhith(ColisionBox))
                        return (SpriteObject)obj[i];
                }
            }
            return null;
        }
    }
}
