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
    class ClickTest : ClickableSpriteObject
    {
        public ClickTest(GameScreen game, EventHandler eventHandler = null) : base(game){
            MouseClick += eventHandler;
            SpriteColor = Color.Purple;
        }

        public ClickTest(GameScreen game, Vector2 position, EventHandler eventHandler = null)
            : this(game, eventHandler){
            Position = position;
        }

        public override void LoadContent(ContentManager content){
            Texture = content.Load<Texture2D>("Sprites/ctverecek");
        }
    }
}
