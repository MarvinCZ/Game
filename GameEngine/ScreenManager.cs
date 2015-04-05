using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using GameEngine.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace GameEngine
{
    public class ScreenManager : Game
    {
        private GameScreen _activeScreen;
        private bool _fullscreen;
        private readonly GraphicsDeviceManager _graphicDeviceManager;

        public readonly List<GameScreen> Screens = new List<GameScreen>();
        public MouseState LastMouseState;
        public KeyboardState LastKeyboardState;

        public ScreenManager()
        {
            _graphicDeviceManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Fullscreen(true, _graphicDeviceManager);
            Screens.Add(new TestScreen(this));
            Screens.Add(new ScreenKolize(this));
            Screens.Add(new ScreenKolizeDalsi(this));
            Screens.Add(new Screen2(this));
            Screens.Add(new ScreenOfPictures(this));
            Screens.Add(new MenuScreen(this));

        }

        protected override void LoadContent(){
            base.LoadContent();
            ActiveScreen<MenuScreen>();
        }

        public void ActiveScreen<T>() where T : GameScreen{
            if (!(_activeScreen is T))
            {
                LastMouseState = Mouse.GetState();
                for (int i = 0; i < Screens.Count; i++)
                {
                    if (Screens[i] is T)
                    {
                        _activeScreen = Screens[i];
                        Screens[i].LoadContent();
                    }
                }
            }
        }
        public void ActiveScreen(String screen)
        {
            if (_activeScreen.Name != screen)
            {
                LastMouseState = Mouse.GetState();
                for (int i = 0; i < Screens.Count; i++)
                {
                    if (Screens[i].Name == screen)
                    {
                        _activeScreen = Screens[i];
                        Screens[i].LoadContent();
                    }
                }
            }
        }

        protected override void Draw(GameTime gameTime){
            base.Draw(gameTime);
            _activeScreen.Draw(gameTime);
        }

        protected override void Update(GameTime gameTime){
            base.Update(gameTime);
            if (Keyboard.GetState().IsKeyDown(Keys.Escape) && LastKeyboardState.IsKeyUp(Keys.Escape))
            {
                if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
                    Fullscreen(null, _graphicDeviceManager);
                else
                    ActiveScreen<MenuScreen>();
            }
            for (int i = 0; i < Screens.Count; i++)
            {
                if (Screens[i] != _activeScreen)
                {
                    Screens[i].UnloadContent();
                }
            }
            _activeScreen.Update(gameTime);
            LastMouseState = Mouse.GetState();
            LastKeyboardState = Keyboard.GetState();
        }

        public void Fullscreen(bool? fullscreen, GraphicsDeviceManager graphics){
            if (fullscreen != _fullscreen){
                if(fullscreen == null)
                    _fullscreen = ! _fullscreen;
                else{
                    _fullscreen = (bool)fullscreen;
                }
                if (_fullscreen){
                    var screen = Screen.AllScreens.First(e => e.Primary);
                    Window.IsBorderless = true;
                    Window.Position = new Point(screen.Bounds.X, screen.Bounds.Y);
                    graphics.PreferredBackBufferHeight = screen.Bounds.Height;
                    graphics.PreferredBackBufferWidth = screen.Bounds.Width;
                    graphics.ApplyChanges();
                }
                else
                {
                    Window.IsBorderless = false;
                    Window.Position = new Point(200, 200);
                    graphics.PreferredBackBufferHeight = 600;
                    graphics.PreferredBackBufferWidth = 800;
                    graphics.ApplyChanges();
                }
            }
        } 
    }
}
