using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameEngine.Cameras;
using GameEngine.GameObjects;
using GameEngine.Objects;
using GameEngine.Screens;
using GameEngine.HelpObjects;
using Microsoft.Xna.Framework;

namespace GameEngine.Screens
{
    class ScreenKolizeDalsi : GameScreen
    {
        public ScreenKolizeDalsi(ScreenManager screenManager) : base(screenManager){
        }
        public override string Name { get { return "Kolize 2"; } }

        public override void LoadContent()
        {
            Layers.Single(s => s.Name == "SolidObjects").Objekty.Add(new Box(this, new Vector2(-500, -500), new Vector2(500, -498)));
            Layers.Single(s => s.Name == "SolidObjects").Objekty.Add(new Box(this, new Vector2(-500, -500), new Vector2(-498, 500)));
            Layers.Single(s => s.Name == "SolidObjects").Objekty.Add(new Box(this, new Vector2(-500, 498), new Vector2(500, 500)));
            Layers.Single(s => s.Name == "SolidObjects").Objekty.Add(new Box(this, new Vector2(498, -500), new Vector2(500, 500)));
            Layers.Single(s => s.Name == "SolidObjects").Objekty.Add(new Box(this, new Vector2(-250, -30), new Vector2(250, -28)));
            for (int i = 0; i < 50; i++)
            {
                Layers.Single(s => s.Name == "MovebleObjects").Objekty.Add(new Bounci(this));
            }
            Layers.Single(s => s.Name == "MovebleObjects").Objekty.Add(new ColidebleMovable(this));
            MainCam = new FreeCamera(this);
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            Game.GraphicsDevice.Clear(Color.Black);
            base.Draw(gameTime);
        }
    }
}
