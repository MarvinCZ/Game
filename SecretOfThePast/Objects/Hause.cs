﻿using GameEngine;
using GameEngine.GameObjects;
using GameEngine.HelpObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SecretOfThePast.Objects
{
    class Hause : SpriteObject
    {
        public Hause(GameScreen game) : base(game)
        {
            Solid = true;
            ColisionBox = new ColisionBox(this);
        }
        public Hause(GameScreen game, Vector2 position,string metaData)
            : base(game, position,metaData)
        {
            Solid = true;
            ColisionBox = new ColisionBox(this);
        }

        public override void LoadContent(ContentManager content)
        {
            if (Texture == null){
                Texture = content.Load<Texture2D>("Sprites/house_1_1");
                Origin = new Vector2(205,924);
            }
        }
        public override Rectangle GetBoundingBoxForColision()
        {
            if(Texture == null)
                return new Rectangle();

            Vector2 spriteSize = new Vector2(630, 400);
            Rectangle r = new Rectangle((int)PositionX, (int)PositionY, (int)(spriteSize.X * Scale.X), (int)(spriteSize.Y * Scale.Y));
            r.Offset(0, (int)(-spriteSize.Y * Scale.Y));
            return r;
        }
    }
}
