using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameEngine.GameObjects
{
    public class ClickableText : TextObject , IClickable{
        /// <summary>
        /// Zavola se po kliknuti levim tlacitkem na text
        /// </summary>
        public event EventHandler MouseClick;

        public ClickableText(GameScreen game, string text, Vector2 pozice, Color color, EventHandler mouseClick = null)
            : base(game, text, pozice, color){
            if (mouseClick != null)
                MouseClick += mouseClick;
            CameraDependent = false;
        }

        public ClickableText(GameScreen game, string text, Vector2 pozice, EventHandler mouseClick = null)
            : base(game, text, pozice){
            if (mouseClick != null)
                MouseClick += mouseClick;
            CameraDependent = false;
        }

        public override void Update(GameTime gameTime){
            Update(gameTime, CameraDependent ? gameScreen.MainCam.TransformMatrix : Matrix.Identity);
        }

        public void Update(GameTime gameTime, Matrix transform){
            base.Update(gameTime);
                if (Mouse.GetState().LeftButton == ButtonState.Pressed && ((ScreenManager)this.gameScreen.Game).LastMouseState.LeftButton != ButtonState.Pressed && MouseClick != null){
                    Vector2 mouse = Mouse.GetState().Position.ToVector2();
                    Matrix inversTransform = Matrix.Invert(transform);
                    Vector2.Transform(ref mouse, ref inversTransform, out mouse);
                    if (BoundingBox.Contains(mouse)){
                        MouseClick(this, new EventArgs());
                    }
                }
        }
    }
}
