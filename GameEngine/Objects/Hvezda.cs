using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            Position = new Vector2(GameHelper.Instance.RandomNext(-game.GraphicsDevice.Viewport.Bounds.Width, game.GraphicsDevice.Viewport.Bounds.Width), GameHelper.Instance.RandomNext(-game.GraphicsDevice.Viewport.Height, game.GraphicsDevice.Viewport.Height));
        }

        public override void Update(GameTime gameTime){
            base.Update(gameTime);
            if (_movespeed != 0f){
                positionX += _movespeed;
                if (positionX > game.GraphicsDevice.Viewport.Bounds.Width && _movespeed > 0f){
                    positionX = -Texture.Width - GameHelper.Instance.RandomNext(0f, 100f);
                    positionY = GameHelper.Instance.RandomNext(0, game.GraphicsDevice.Viewport.Height);
                }
                if (positionX + Texture.Width < 0 && _movespeed < 0f){
                    positionX = game.GraphicsDevice.Viewport.Bounds.Width + Texture.Width +
                                GameHelper.Instance.RandomNext(0f, 100f);
                    positionY = GameHelper.Instance.RandomNext(0, game.GraphicsDevice.Viewport.Height);
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
