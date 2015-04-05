using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.GameObjects;
using GameEngine.HelpObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Objects
{
    class Tree : SpriteObject
    {
        public Tree(GameScreen game) : base(game)
        {
            ColisionBox = new ColisionBox(this, ColisionBox.BoxType.Circle);
            Solid = true;
        }

        public Tree(GameScreen game, Vector2 position) : base(game, position)
        {
            ColisionBox = new ColisionBox(this, ColisionBox.BoxType.Circle);
            Solid = true;
        }
        public override void LoadContent(ContentManager content)
        {
            if (Texture == null)
            {
                Texture = content.Load<Texture2D>("Sprites/tree_1_" + GameHelper.Instance.RandomNext(1,5));
                Origin = new Vector2(390, 820);
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override Rectangle GetBoundingBoxForColision() 
        {
            if (Texture == null)
            {
                return new Rectangle();
            }
            Vector2 spriteSize = new Vector2(230,100);
            Rectangle r = new Rectangle((int)PositionX, (int)PositionY, (int)(spriteSize.X * Scale.X), (int)(spriteSize.Y * Scale.Y));
            r.Offset((int)(-spriteSize.X * Scale.X * 0.5), (int)(-spriteSize.Y * Scale.Y * 0.5));
            return r;
        }
    }
}
