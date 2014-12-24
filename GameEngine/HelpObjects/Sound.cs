using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using GameEngine.GameObjects;

namespace GameEngine.HelpObjects
{
    public class Sound
    {
        private const int _minVzdal = 10000;
        private const int _rozmezi = 990000;
        public string Name { get; protected set; }
        protected SoundEffectInstance sndEffect;
        protected SpriteObject parrent;
        protected GameScreen screen;
        public Sound(GameScreen screen,SpriteObject obj, SoundEffect snd, string name = "null"){
            this.screen = screen;
            parrent = obj;
            Name = name;
            sndEffect = snd.CreateInstance();
        }
        public void Update()
        {
            if (screen.MainCam != null)
            {
                Vector2 rozdil = screen.MainCam.Position - parrent.Position;
                float vzdalenost = rozdil.LengthSquared();
                vzdalenost -= _minVzdal;
                if (vzdalenost < 0)
                    vzdalenost = 0;
                sndEffect.Volume = vzdalenost < _rozmezi ? 1 - (vzdalenost / _rozmezi) : 0;
            }
        }
        public void Play()
        {
            sndEffect.Play();
        }
        public void Stop()
        {
            sndEffect.Stop();
        }
    }
}
