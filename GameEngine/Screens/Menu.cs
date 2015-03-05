using System;
using GameEngine.GameObjects;
using Microsoft.Xna.Framework;
using System.Linq;

namespace GameEngine.Screens
{
    public class MenuScreen : GameScreen
    {
        public MenuScreen(ScreenManager screenManager) : base(screenManager){
        }

        public override string Name{
            get { return "Menu"; }
        }

        public override void LoadContent()
        {
            int count = ((ScreenManager) Game).Screens.Count;
            count--;
            int vzdalenost = 0;
            if(count != 0)
                vzdalenost = Game.GraphicsDevice.Viewport.Bounds.Height/count;
            for (int i = 0; i < count; i++){
                int y = i*vzdalenost;
                y += vzdalenost/2;
                Vector2 position = new Vector2(60,y);
                ClickableText ct = new ClickableText(this, ((ScreenManager) Game).Screens[i].Name, position, MouseClick){
                    HorizontAlignment = TextObject.TextAlignment.Near
                };
                Layers.Single(s => s.Name == "Gui").Objekty.Add(ct);
            }
            ClickableText end = new ClickableText(this,"Konec", new Vector2(Game.GraphicsDevice.Viewport.Bounds.Width - 20, Game.GraphicsDevice.Viewport.Bounds.Height - 20),EndGame){
                HorizontAlignment = TextObject.TextAlignment.Far,
                VerticalAlignment = TextObject.TextAlignment.Far
            };
            Layers.Single(s => s.Name == "Gui").Objekty.Add(end);
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            Game.GraphicsDevice.Clear(Color.Green);
            base.Draw(gameTime);
        }

        void MouseClick(object sender, EventArgs e){
            ((ScreenManager)Game).ActiveScreen(((ClickableText)sender).Text);
        }

        void EndGame(object sender, EventArgs e){
            Game.Exit();
        }
    }
}
