using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameEngine.Cameras
{
    class FreeCamera : Camera{
        public FreeCamera(GameScreen game, float moveSpeed = 3f) : base(game){
            MoveSpeed = moveSpeed;
        }

        public override void Update(){
            Vector2 mousePosition = Mouse.GetState().Position.ToVector2();
            float height = game.GraphicsDevice.Viewport.Bounds.Height;
            float width = game.GraphicsDevice.Viewport.Bounds.Width;
            float x = 0;
            float y = 0;
            if (mousePosition.X < 20){
                x = -MoveSpeed;
                reCalc = true;
            }
            if (mousePosition.X > width - 20){
                x = MoveSpeed;
                reCalc = true;
            }
            if (mousePosition.Y < 20){
                y = -MoveSpeed;
                reCalc = true;
            }
            if (mousePosition.Y > height - 20){
                y = MoveSpeed;
                reCalc = true;
            }
            position += new Vector2(x, y);
            base.Update();
        }
    }
}
