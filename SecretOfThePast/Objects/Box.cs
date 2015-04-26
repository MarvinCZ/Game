using GameEngine;
using GameEngine.GameObjects;
using GameEngine.HelpObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SecretOfThePast.Objects
{
    class Box : SpriteObject
    {
        public Vector2 Point1;
        public Vector2 Point2;
        public Box(GameScreen screen, Vector2 point1, Vector2 point2)
            : base(screen)
        {
            Point1 = point1;
            Point2 = point2;
            Solid = true;
            ColisionBox = new ColisionBox(this, ColisionBox.BoxType.Rectangle);
            //SpriteColorAlfa = 0.7f;
            SpriteColor = Color.Yellow;
        }

        public override void LoadContent(ContentManager content)
        {
            Texture = content.Load<Texture2D>("Sprites/ctverecek");
            ReCalc();
        }

        public void ReCalc()
        {
            float whidth = Point2.X - Point1.X;
            float height = Point2.Y - Point1.Y;
            Scale = new Vector2(
                whidth / Texture.Width,
                height / Texture.Height);
            Position = Point1;
            Origin = new Vector2(0, 0);
        }
    }
}
