using GameEngine.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Objects
{
    class Hvezda : SpriteObject{
        private readonly float _movespeed;

        public Hvezda(GameScreen gameScreen, float movespeed = 0f)
            : base(gameScreen){
            SpriteColor = Color.CornflowerBlue;
            Scale = new Vector2(0.1f,0.1f);
            _movespeed = movespeed;
            Position = new Vector2(GameHelper.Instance.RandomNext(-ScreenManager.GraphicsDevice.Viewport.Bounds.Width, ScreenManager.GraphicsDevice.Viewport.Bounds.Width), GameHelper.Instance.RandomNext(-ScreenManager.GraphicsDevice.Viewport.Height, ScreenManager.GraphicsDevice.Viewport.Height));
        }

        public override void Update(GameTime gameTime){
            base.Update(gameTime);
            if (_movespeed != 0f){
                PositionX += _movespeed;
                if (PositionX > ScreenManager.GraphicsDevice.Viewport.Bounds.Width && _movespeed > 0f){
                    PositionX = -Texture.Width - GameHelper.Instance.RandomNext(0f, 100f);
                    PositionY = GameHelper.Instance.RandomNext(0, ScreenManager.GraphicsDevice.Viewport.Height);
                }
                if (PositionX + Texture.Width < 0 && _movespeed < 0f){
                    PositionX = ScreenManager.GraphicsDevice.Viewport.Bounds.Width + Texture.Width +
                                GameHelper.Instance.RandomNext(0f, 100f);
                    PositionY = GameHelper.Instance.RandomNext(0, ScreenManager.GraphicsDevice.Viewport.Height);
                }
            }
        }

        public override void LoadContent(ContentManager content){
            if (Texture == null)
            {
                Texture = content.Load<Texture2D>("Sprites/hvezda");
            }
        }
    }
}
