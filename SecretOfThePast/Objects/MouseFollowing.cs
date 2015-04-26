using GameEngine;
using GameEngine.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SecretOfThePast.Objects
{
    class MouseFollowing : SpriteObject{
        public MouseFollowing(GameScreen game) : base(game){
        }

        public override void Update(GameTime gameTime){
            Vector2 mouse = Mouse.GetState().Position.ToVector2();
            Matrix inversTransform =
                Matrix.Invert(GameScreen.MainCam != null ? GameScreen.MainCam.TransformMatrix : Matrix.Identity);
            Vector2.Transform(ref mouse, ref inversTransform, out mouse);
            Position = mouse;
            base.Update(gameTime);
        }

        public override void LoadContent(ContentManager content){
            if (Texture == null)
            {
                Texture = content.Load<Texture2D>("Sprites/ctverecek");
            }
        }
    }
}
