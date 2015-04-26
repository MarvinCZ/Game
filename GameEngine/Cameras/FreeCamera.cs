using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameEngine.Cameras
{
    public class FreeCamera : Camera{
        public FreeCamera(GameScreen game, float moveSpeed = 3f) : base(game){
            MoveSpeed = moveSpeed;
        }

        public override void Update(){
            Vector2 mousePosition = Mouse.GetState().Position.ToVector2();
            float height = ScreenManager.GraphicsDevice.Viewport.Bounds.Height;
            float width = ScreenManager.GraphicsDevice.Viewport.Bounds.Width;
            float x = 0;
            float y = 0;
            if (mousePosition.X < 20){
                x = -MoveSpeed;
                NeedRecalculation = true;
            }
            if (mousePosition.X > width - 20){
                x = MoveSpeed;
                NeedRecalculation = true;
            }
            if (mousePosition.Y < 20){
                y = -MoveSpeed;
                NeedRecalculation = true;
            }
            if (mousePosition.Y > height - 20){
                y = MoveSpeed;
                NeedRecalculation = true;
            }
            Position += new Vector2(x, y);
            base.Update();
        }
    }
}
