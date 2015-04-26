using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine;
using GameEngine.GameObjects;
using GameEngine.HelpObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SecretOfThePast.Objects
{
    class Player : MovableObject
    {
        private int _animace = 0;
        private int _update;
        public Player(GameScreen game, Vector2 position,string metaData="")
            :base(game)
        {
            Position = position;
            MetaData = metaData;
            ColisionBox = new ColisionBox(this, ColisionBox.BoxType.Circle);
            Solid = true;
            SourceRectangle = new Rectangle(0,0,200,320);
        }

        public override void LoadContent(ContentManager content)
        {
            Texture = content.Load<Texture2D>("Sprites/sablona_persp");
            Origin = new Vector2(100, 300);
            for (int i = 1; i < 5; i++)
            {
                SoundEffect snd = content.Load<SoundEffect>("Sounds/grass"+i);
                Sounds.Add(new Sound(GameScreen, this, snd, "grass"+i));
            }
        }
        public override void Update(GameTime gameTime)
        {
            _update++;
            if (_update > 7){
                _update = 1;
                if (_animace != 0){
                    int cislo = GameHelper.Instance.RandomNext(1, 4);
                    foreach (Sound sound in Sounds){
                        if (sound.Name == "grass" + cislo)
                            sound.Play();
                    }
                }
            }
            float x = 0;
            float y = 0;
            if (Keyboard.GetState().IsKeyDown(GameHelper.Instance.PlayerMoveLeft))
            {
                x = -1;
                if (_animace >= 30 && _animace < 40)
                {
                    if (_update == 1){
                        _animace++;
                        if (_animace > 38)
                            _animace = 30;
                    }
                }
                else{
                    _animace = 30;
                }
            }
            if (Keyboard.GetState().IsKeyDown(GameHelper.Instance.PlayerMoveRight))
            {
                x = 1;
                if (_animace >= 10 && _animace < 20)
                {
                    if(_update==1)
                    _animace++;
                    if (_animace > 18)
                        _animace = 10;
                }
                else
                {
                    _animace = 10;
                }
            }
            if (Keyboard.GetState().IsKeyDown(GameHelper.Instance.PlayerMoveUp))
            {
                y = -1;
                if (_animace >= 20 && _animace < 30)
                {
                    if (_update == 1)
                    _animace++;
                    if (_animace > 28)
                        _animace = 20;
                }
                else
                {
                    _animace = 20;
                }
            }
            if (Keyboard.GetState().IsKeyDown(GameHelper.Instance.PlayerMoveDown))
            {
                y = 1;
                if (_animace >= 0 && _animace < 10)
                {
                    if (_update == 1)
                    _animace++;
                    if (_animace > 8)
                        _animace = 0;
                }
                else
                {
                    _animace = 0;
                }
            }
            if (x == 0 & y == 0)
                _animace = 0;
            Smer = new Vector2(x, y);
            int ya = _animace/10;
            int xa = _animace%10;
            SourceRectangle = new Rectangle(xa*200,ya*320+1,200,319);
            base.Update(gameTime);
        }

        public override Rectangle GetBoundingBoxForColision()
        {
            if (Texture == null)
            {
                return new Rectangle();
            }
            Vector2 spriteSize = new Vector2(100, 100);
            Rectangle r = new Rectangle((int)PositionX, (int)PositionY, (int)(spriteSize.X * Scale.X), (int)(spriteSize.Y * Scale.Y));
            r.Offset((int)(-spriteSize.X * Scale.X * 0.5), (int)(-spriteSize.Y * Scale.Y * 0.5));
            return r;
        }
    }
}
