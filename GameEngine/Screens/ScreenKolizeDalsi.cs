using GameEngine.Cameras;
using GameEngine.Objects;
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
            Layers["Main"].Objekty.Add(new Box(this, new Vector2(-500, -500), new Vector2(500, -498)));
            Layers["Main"].Objekty.Add(new Box(this, new Vector2(-500, -500), new Vector2(-498, 500)));
            Layers["Main"].Objekty.Add(new Box(this, new Vector2(-500, 498), new Vector2(500, 500)));
            Layers["Main"].Objekty.Add(new Box(this, new Vector2(498, -500), new Vector2(500, 500)));
            Layers["Main"].Objekty.Add(new Box(this, new Vector2(-250, -30), new Vector2(250, -28)));
            for (int i = 0; i < 10; i++)
            {
                Layers["Main"].Objekty.Add(new Bounci(this));
            }
            Layers["Main"].Objekty.Add(new ColidebleMovable(this));
            MainCam = new FreeCamera(this);
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.GraphicsDevice.Clear(Color.Black);
            base.Draw(gameTime);
        }
    }
}
