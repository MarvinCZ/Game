using GameEngine.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameEngine.Objects
{
    class Controleble : MovableObject
    {
        public Controleble(GameScreen game) : base(game)
        {

        }
        public override void Update(GameTime gameTime)
        {
            float x = 0;
            float y = 0;
            if (Keyboard.GetState().IsKeyDown(GameHelper.Instance.PlayerMoveLeft))
            {
                x = -1;
            }
            if (Keyboard.GetState().IsKeyDown(GameHelper.Instance.PlayerMoveRight))
            {
                x = 1;
            }
            if (Keyboard.GetState().IsKeyDown(GameHelper.Instance.PlayerMoveUp))
            {
                y = -1;
            }
            if (Keyboard.GetState().IsKeyDown(GameHelper.Instance.PlayerMoveDown))
            {
                y = 1;
            }
            Smer = new Vector2(x, y);
            base.Update(gameTime);
        }

        public override void LoadContent(ContentManager content)
        {
            if (Texture == null)
            {
                Texture = content.Load<Texture2D>("Sprites/ctverecek");
            }
        }
    }
}
