using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameEngine.GameObjects
{
    public abstract class ClickableSpriteObject : SpriteObject , IClickable{
        public event EventHandler MouseClick;
        protected ClickableSpriteObject(GameScreen game) : base(game){
        }

        protected ClickableSpriteObject(GameScreen game, Vector2 position) : base(game, position){
        }

        public void Update(GameTime gametime, Matrix transform){
            base.Update(gametime);
            if (Mouse.GetState().LeftButton == ButtonState.Pressed && Mouse.GetState().LeftButton != ((ScreenManager)GameScreen.ScreenManager).LastMouseState.LeftButton)
            {
                Vector2 mouse = Mouse.GetState().Position.ToVector2();
                Matrix inversTransform = Matrix.Invert(transform);
                Vector2.Transform(ref mouse, ref inversTransform, out mouse);
                if (BoundingBox.Contains(mouse)){
                    MouseClick(this,new EventArgs());
                }
            }
        }
        public override void Update(GameTime gameTime){
            Update(gameTime, CameraDependent ? (GameScreen.MainCam != null ? GameScreen.MainCam.TransformMatrix : Matrix.Identity) : Matrix.Identity);
        }
    }
}
