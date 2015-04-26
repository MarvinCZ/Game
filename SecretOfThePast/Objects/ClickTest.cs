using System;
using GameEngine;
using GameEngine.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SecretOfThePast.Objects
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
