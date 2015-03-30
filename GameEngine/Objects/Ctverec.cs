using System;
using GameEngine.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Objects
{
    class Ctverec : SpriteObject
    {
        private float _rotationSpeed;
        private float _moveSpeed;

        //private Box _box;

        public Ctverec(GameScreen game)
            : base(game){
            RestrartMe();
            //_box = new Box(ScreenManager,new Vector2(),new Vector2());
        }

        public float MoveSpeed{
            get { return _moveSpeed; }
        }

        public override void Update(GameTime gameTime){
            base.Update(gameTime);
                Rotation += MathHelper.ToRadians(_rotationSpeed);
                Position += new Vector2(0,MoveSpeed);
                _moveSpeed += 0.15f;
                Scale = new Vector2(Scale.X*0.99f, Scale.Y*0.99f);
                SpriteColorAlfa -= 0.005f;

            if ((PositionY - Texture.Height > ScreenManager.GraphicsDevice.Viewport.Bounds.Height && MoveSpeed > 0f) ||
                SpriteColorAlfa <= 0f){
                RestrartMe();
                }
            //_box.point1 = new Vector2(BoundingBox.X, BoundingBox.Y);
            //_box.point2 = new Vector2(BoundingBox.X+BoundingBox.Height, BoundingBox.Y+BoundingBox.Width);
            //_box.recalc();
        }

        //public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        //{
        //    base.Draw(gameTime, spriteBatch);
        //    _box.Draw(gameTime, spriteBatch);
        //}

        void RestrartMe(){
            SpriteColorAlfa = 1f;
            Scale = new Vector2(5f, 5f);
            PositionX = GameHelper.Instance.RandomNext(-2 * ScreenManager.GraphicsDevice.Viewport.Bounds.Width, 2 * ScreenManager.GraphicsDevice.Viewport.Bounds.Width);
            SpriteColor = new Color(0, GameHelper.Instance.RandomNext(70, 220), 0);

            do{
                _rotationSpeed = GameHelper.Instance.RandomNext(-2.5f, 2.5f);
            } while (Math.Abs(_rotationSpeed)<1f);

            do{
                _moveSpeed = GameHelper.Instance.RandomNext(-25f, -5f);
            } while (Math.Abs(MoveSpeed) < 5f);


            if(Texture != null)
            {
                if (MoveSpeed < 0){
                    PositionY = Texture.Height + ScreenManager.GraphicsDevice.Viewport.Height;
                } else{
                    PositionY = -Texture.Height;
                }
            }
        }

        public override void LoadContent(ContentManager content){
            if (Texture == null)
            {
                Texture = content.Load<Texture2D>("Sprites/ctverecek");
            }
            //_box.LoadContent(content);
        }
    }
}
