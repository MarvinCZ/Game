using GameEngine.GameObjects;
using GameEngine.HelpObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Objects
{
    class Bounci : MovableObject, ICollisionReaction
    {
        public Bounci(GameScreen game)
            : base(game)
        {
            ColisionBox = new ColisionBox(this,ColisionBox.BoxType.Circle);
            float scale = 0.5f;
            Scale = new Vector2(scale, scale);
            Position = new Vector2(GameHelper.Instance.RandomNext(-400, 400), GameHelper.Instance.RandomNext(-400, 400));
            Smer = new Vector2(GameHelper.Instance.RandomNext(-400, 400), GameHelper.Instance.RandomNext(-400, 400));
            Smer.Normalize();
            SpriteColor = Color.Red;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (ColideX)
            {
                Smer = new Vector2(-Smer.X,Smer.Y);
            }
            else if (ColideY)
            {
                Smer = new Vector2(Smer.X, -Smer.Y);
            }
        }

        public override void LoadContent(ContentManager content)
        {
            if (Texture == null)
            {
                Texture = content.Load<Texture2D>("Sprites/hvezda");
            }
        }

        public void CollisionReaction(GameObject obj)
        {
            SpriteColor = new Color(SpriteColorB,SpriteColorR,SpriteColorG);
        }
    }
}
