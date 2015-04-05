using GameEngine.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace GameEngine.HelpObjects
{
    public class Sound
    {
        private const int MIN_VZDAL = 10000;
        private const int ROZMEZI = 990000;
        public string Name { get; protected set; }
        protected readonly SoundEffectInstance SndEffect;
        protected SpriteObject Parrent;
        protected GameScreen Screen;
        public Sound(GameScreen screen,SpriteObject obj, SoundEffect snd, string name = "null"){
            Screen = screen;
            Parrent = obj;
            Name = name;
            SndEffect = snd.CreateInstance();
        }
        public void Update()
        {
            if (Screen.MainCam != null)
            {
                Vector2 rozdil = Screen.MainCam.Position - Parrent.Position;
                float vzdalenost = rozdil.LengthSquared();
                vzdalenost -= MIN_VZDAL;
                if (vzdalenost < 0)
                    vzdalenost = 0;
                SndEffect.Volume = vzdalenost < ROZMEZI ? 1 - (vzdalenost / ROZMEZI) : 0;
            }
        }
        public void Play()
        {
            SndEffect.Play();
        }
        public void Stop()
        {
            SndEffect.Stop();
        }
    }
}
