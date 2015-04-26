using GameEngine;
using GameEngine.GameObjects;
using GameEngine.HelpObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SecretOfThePast.Objects
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
            Sounds.Add(new Sound(GameScreen, this, snd));
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (UpdateCount % 180 == 0)
            {
                foreach (Sound snd in Sounds)
                {
                    snd.Play();
                }
            }
        }
    }
}
