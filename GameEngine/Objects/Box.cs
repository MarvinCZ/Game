using GameEngine.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace GameEngine.Objects
{
    class Box : SpriteObject
    {
        public Vector2 point1;
        public Vector2 point2;
        public Box(GameScreen screen, Vector2 point1, Vector2 point2)
            : base(screen)
        {
            this.point1 = point1;
            this.point2 = point2;
            solid = true;
            colisionBox = new HelpObjects.ColisionBox(this, HelpObjects.ColisionBox.BoxType.Rectangle);
            //spriteColorAlfa = 0.7f;
            SpriteColor = Color.Yellow;
        }

        public override void LoadContent(ContentManager content)
        {
            Texture = content.Load<Texture2D>("Sprites/ctverecek");
            recalc();
        }

        public void recalc()
        {
            float whidth = point2.X - point1.X;
            float height = point2.Y - point1.Y;
            Scale = new Vector2(
                whidth / Texture.Width,
                height / Texture.Height);
            Position = point1;
            Origin = new Vector2(0, 0);
        }
    }
}
