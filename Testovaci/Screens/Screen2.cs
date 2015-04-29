using System;
using System.Linq;
using GameEngine;
using GameEngine.Cameras;
using GameEngine.GameObjects;
using GameEngine.GuiObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Testovaci.Objects;

namespace Testovaci.Screens
{
    class Screen2 : GameScreen{
        private TextObject _ukazatel;
        private TextObject _ukaztelCount;
        private TextObject _ukazatelFPS;
        private TextObject _ukazatelPozice;
        private double _oldTime;
        private int _updates;
        public Screen2(ScreenManager screenManager) : base(screenManager){
        }

        public override string Name{ get { return "FreeCam"; }}

        public override void LoadContent(){
            MainCam = new FreeCamera(this);
            for (int i = 0; i < 0; i++)
            {
                Layers["Main"].Objekty.Add(new Hvezda(this));
            }
            _ukazatel = new TextObject(this,"5",new Vector2(ScreenManager.GraphicsDevice.Viewport.Bounds.Width - 20, 10)){
                HorizontAlignment = TextObject.TextAlignment.Far,
                VerticalAlignment = TextObject.TextAlignment.Near,
                Scale = new Vector2(0.3f,0.3f)
            };
            Layers["Gui"].Objekty.Add(_ukazatel);
            _ukaztelCount = new TextObject(this, "", new Vector2(ScreenManager.GraphicsDevice.Viewport.Bounds.Width - 20, 30)){
                HorizontAlignment = TextObject.TextAlignment.Far,
                VerticalAlignment = TextObject.TextAlignment.Near,
                Scale = new Vector2(0.3f, 0.3f)
            };
            _ukazatelFPS = new TextObject(this, "", new Vector2(ScreenManager.GraphicsDevice.Viewport.Bounds.Width - 20, 50))
            {
                HorizontAlignment = TextObject.TextAlignment.Far,
                VerticalAlignment = TextObject.TextAlignment.Near,
                Scale = new Vector2(0.3f, 0.3f)
            };
            _ukazatelPozice = new TextObject(this, "", new Vector2(ScreenManager.GraphicsDevice.Viewport.Bounds.Width - 20, 70))
            {
                HorizontAlignment = TextObject.TextAlignment.Far,
                VerticalAlignment = TextObject.TextAlignment.Near,
                Scale = new Vector2(0.3f, 0.3f)
            };
            Layers["Gui"].Objekty.Add(_ukaztelCount);
            Layers["Gui"].Objekty.Add(_ukazatelFPS);
            Layers["Gui"].Objekty.Add(_ukazatelPozice);
            Layers["Main"].Objekty.Add(new Controleble(this));
            Layers["Gui"].Objekty.Add(new ClickTest(this, new Vector2(200, 300), OpenMessageBox));
            Layers["Main"].Objekty.Add(new Soundy(this));
            base.LoadContent();
        }

        void SwitchScreen(object sender, EventArgs e){
            if (sender is InputMessageBox){
                InputMessageBox imb = (InputMessageBox) sender;
                if (imb.InputText == "STORNO")
                    return;
            }
            MessageBox mb = (MessageBox) sender;
            mb.Destroy();
            if(mb.MessageResult == MessageBox.Result.Ok)
                ScreenManager.ActiveScreen<MenuScreen>();
        }

        void OpenMessageBox(object sender, EventArgs e){
            MessageBox mb = new InputMessageBox(this,"Write IP",GameHelper.Instance.RegIp(),false);
            //MessageBox mb = new MessageBox(this, "Vratit do Menu?",true);
            mb.LoadContent(ContentManager);
            mb.Show(SwitchScreen);
        }

        public override void Draw(GameTime gameTime){
            _updates++;
            if (_oldTime != gameTime.TotalGameTime.Seconds){
                _oldTime = gameTime.TotalGameTime.Seconds;
                _ukazatelFPS.Text = _updates + " FPS";
                _updates = 0;
            }
            ScreenManager.GraphicsDevice.Clear(Color.Black);
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime){
            base.Update(gameTime);
            if (Paused) { }
            else
            {
                Vector2 mousePos = GameHelper.Instance.RealVector2(Mouse.GetState().Position.ToVector2(), MainCam.TransformMatrix);
                //if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                //{
                //    for (int j = 0; j < 50; j++)
                //    {
                //        Projectile p = new Projectile(this, mousePos);
                //        p.LoadContent(contentManager);
                //        GameObjects.Add(p);
                //    }
                //}
                for (int j = 0; j < 20; j++)
                {
                    Star star = new Star(this);
                    Layers["Main"].AddObject(star);
                }

                int i = (int)MainCam.MoveSpeed;
                _ukazatel.Text = i + " Camera Speed";
                int count = 0;
                foreach (Layer layer in Layers.Values)
                    count += layer.Objekty.Count();
                _ukaztelCount.Text = count + " Objects";
                _ukazatelPozice.Text = "X: " + (int)mousePos.X + " Y: " + (int)mousePos.Y;
            }

        }
    }
}
