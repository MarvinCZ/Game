using System;
using GameEngine;
using GameEngine.Cameras;
using GameEngine.GameObjects;
using Microsoft.Xna.Framework;
using Testovaci.Objects;

namespace Testovaci.Screens
{
    class TestScreen : GameScreen
    {
        private const int PocetCtvercu = 5000;
        private const int PocetHvezd = 5000;
        private TextObject _ukazatelFPS;
        private double _oldTime;
        private int _updates;
        public TestScreen(ScreenManager screenManager) : base(screenManager){
        }
        public override string Name { get { return "FollowCam"; } }

        public override void LoadContent(){
            for (int i = 0; i < PocetHvezd; i++)
            {
                Layers["Background"].Objekty.Add(new Hvezda(this));
            }
            for (int i = 0; i < PocetCtvercu; i++)
            {
                Layers["Foreground"].Objekty.Add(new Ctverec(this));
            }
            _ukazatelFPS = new TextObject(this, "", new Vector2(ScreenManager.GraphicsDevice.Viewport.Bounds.Width - 20, 10))
            {
                HorizontAlignment = TextObject.TextAlignment.Far,
                VerticalAlignment = TextObject.TextAlignment.Near,
                Scale = new Vector2(0.3f, 0.3f)
            };
            Layers["Gui"].Objekty.Add(_ukazatelFPS);
            IFollowable follRect = new Controleble(this);
            Layers["Main"].Objekty.Add((SpriteObject)follRect);
            MainCam = new FollowingCamera(this, follRect);
            Layers["Main"].Objekty.Add(new MouseFollowing(this));
            Layers["Main"].Objekty.Add(new RotujciText(this, "necum", new Vector2(0, 0)));
            Layers["Gui"].Objekty.Add(new ClickableText(this, "CLICK ME", new Vector2(200, 200), SwitchScreen));
            base.LoadContent();
            
        }

        void SwitchScreen(object sender, EventArgs e)
        {
            ScreenManager.ActiveScreen<MenuScreen>();
        }

        public override void Draw(GameTime gameTime)
        {
            _updates++;
            if (_oldTime != gameTime.TotalGameTime.Seconds)
            {
                _oldTime = gameTime.TotalGameTime.Seconds;
                _ukazatelFPS.Text = _updates + " FPS";
                _updates = 0;
            }
            ScreenManager.GraphicsDevice.Clear(Color.Black);
            base.Draw(gameTime);
        }
    }
}
