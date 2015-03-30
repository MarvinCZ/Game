using GameEngine.GameObjects;
using GameEngine.HelpObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameEngine.Objects
{
    class ColidebleMovable : MovableObject
    {
        public ColidebleMovable(GameScreen game)
            : base(game)
        {
            ColisionBox = new ColisionBox(this,ColisionBox.BoxType.Circle);
            float scale = 0.6f;
            Scale = new Vector2(scale, scale);
            Solid = true;
            SpriteColor = new Color(255, 0, 255);
        }

        public override void Update(GameTime gameTime)
        {
            float x  = 0;
            float y = 0;
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                x = -1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                x = 1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                y = -1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                y = 1;
            }
            smer = new Vector2(x, y);
            base.Update(gameTime);
        }

        public override void LoadContent(ContentManager content)
        {
            if (Texture == null)
            {
                Texture = content.Load<Texture2D>("Sprites/hvezda");
            }
        }
    }
}
