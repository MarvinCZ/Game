using System;
using GameEngine.GameObjects;
using GameEngine.HelpObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace GameEngine.Objects
{
    class Soundy : SpriteObject
    {
        public Soundy(GameScreen screen)
            : base(screen)
        {
            Position = Vector2.Zero;
        }
        public override void LoadContent(ContentManager content)
        {
            if (Texture == null)
            {
                Texture = content.Load<Texture2D>("Sprites/hvezda");
            }
            SoundEffect snd = content.Load<SoundEffect>("Sounds/zvuk");
            sounds.Add(new Sound(gameScreen, this, snd));
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (updateCount % 180 == 0)
            {
                foreach (Sound snd in sounds)
                {
                    snd.Play();
                }
            }
        }
    }
}
