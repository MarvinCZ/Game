using System;
using System.Text.RegularExpressions;
using GameEngine.Cameras;
using GameEngine.GameObjects;
using GameEngine.Objects;
using GameEngine.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameEngine
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
                GameObjects.Add(new Hvezda(this));
            }
            _ukazatel = new TextObject(this,"5",new Vector2(Game.GraphicsDevice.Viewport.Bounds.Width - 20, 10)){
                HorizontAlignment = TextObject.TextAlignment.Far,
                VerticalAlignment = TextObject.TextAlignment.Near,
                CameraDependent = false,
                Scale = new Vector2(0.3f,0.3f)
            };
            GuiObjects.Add(_ukazatel);
            _ukaztelCount = new TextObject(this, "", new Vector2(Game.GraphicsDevice.Viewport.Bounds.Width - 20, 30)){
                HorizontAlignment = TextObject.TextAlignment.Far,
                VerticalAlignment = TextObject.TextAlignment.Near,
                CameraDependent = false,
                Scale = new Vector2(0.3f, 0.3f)
            };
            _ukazatelFPS = new TextObject(this, "", new Vector2(Game.GraphicsDevice.Viewport.Bounds.Width - 20, 50))
            {
                HorizontAlignment = TextObject.TextAlignment.Far,
                VerticalAlignment = TextObject.TextAlignment.Near,
                CameraDependent = false,
                Scale = new Vector2(0.3f, 0.3f)
            };
            _ukazatelPozice = new TextObject(this, "", new Vector2(Game.GraphicsDevice.Viewport.Bounds.Width - 20, 70))
            {
                HorizontAlignment = TextObject.TextAlignment.Far,
                VerticalAlignment = TextObject.TextAlignment.Near,
                CameraDependent = false,
                Scale = new Vector2(0.3f, 0.3f)
            };
            GuiObjects.Add(_ukaztelCount);
            GuiObjects.Add(_ukazatelFPS);
            GuiObjects.Add(_ukazatelPozice);
            GameObjects.Add(new Controleble(this));
            GuiObjects.Add(new ClickTest(this, new Vector2(200, 300), OpenMessageBox));
            GameObjects.Add(new Soundy(this));
            base.LoadContent();
        }

        void SwitchScreen(object sender, EventArgs e){
            if (sender is InputMessageBox){
                InputMessageBox imb = (InputMessageBox) sender;
                if (imb.Input == "STORNO")
                    return;
            }
            MessageBox mb = (MessageBox) sender;
            messageBox = null;
            if(mb.MessageResult == MessageBox.Result.Ok)
                ((ScreenManager)Game).ActiveScreen<MenuScreen>();
        }

        void OpenMessageBox(object sender, EventArgs e){
            //messageBox = new InputMessageBox(this,"Write IP");
            messageBox = new MessageBox(this, "Vratit do Menu?",true);
            messageBox.LoadContent(contentManager);
            messageBox.Show(SwitchScreen);
        }

        public override void Draw(GameTime gameTime){
            _updates++;
            if (_oldTime != gameTime.TotalGameTime.Seconds){
                _oldTime = gameTime.TotalGameTime.Seconds;
                _ukazatelFPS.Text = _updates + " FPS";
                _updates = 0;
            }
            Game.GraphicsDevice.Clear(Color.Black);
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime){
            base.Update(gameTime);
            if (messageBox != null && messageBox.Active) { }
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
                    Star s = new Star(this);
                    s.LoadContent(contentManager);
                    GameObjects.Add(s);
                }

                int i = (int)((Camera)MainCam).MoveSpeed;
                _ukazatel.Text = i + " Camera Speed";
                _ukaztelCount.Text = GameObjects.Count + " Objects";
                _ukazatelPozice.Text = "X: " + (int)mousePos.X + " Y: " + (int)mousePos.Y;
            }

        }
    }
}
