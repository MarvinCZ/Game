using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine;
using GameEngine.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SecretOfThePast.Objects
{
    class Stain : SpriteObject
    {
        public Stain(GameScreen game, Vector2 position) : base(game)
        {
            Position = position;
            MetaData = GameHelper.Instance.RandomNext(1, 4).ToString();
        }
        public Stain(GameScreen game, Vector2 position, string metaData) : base(game, position, metaData) {}
        public override void LoadContent(ContentManager content)
        {
            Texture = content.Load<Texture2D>("Sprites/stain_1_"+MetaData);
        }
    }
}
