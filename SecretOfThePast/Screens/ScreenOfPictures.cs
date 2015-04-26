using System;
using System.Collections.Generic;
using GameEngine;
using GameEngine.Cameras;
using GameEngine.GameObjects;
using GameEngine.HelpObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SecretOfThePast.Objects;

namespace SecretOfThePast.Screens
{
    class ScreenOfPictures : GameScreen
    {
        private TextObject _selectedObject;
        private int _selectedNumber;

        public ScreenOfPictures(ScreenManager screenManager) : base(screenManager)
        {
            
        }

        public override string Name
        {
            get { return "Obrázky"; }
        }

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.GraphicsDevice.Clear(new Color(30,105,0));
            base.Draw(gameTime);
        }

        public override void LoadContent()
        {
            ScreenBuilder sb = new ScreenBuilder(this);
            List<GameObjectPackage> objekty = sb.LoadScreen();
            foreach (GameObjectPackage package in objekty)
            {
                object o = Activator.CreateInstance(Type.GetType(package.Type), this,
                    package.Position, package.MetaData);
                Layers[package.Layer].AddObject((GameObject)o);
            }
            MainCam = new FreeCamera(this,30f);
            _selectedObject = new TextObject(this, "", new Vector2(ScreenManager.GraphicsDevice.Viewport.Bounds.Width - 20, 10))
            {
                HorizontAlignment = TextObject.TextAlignment.Far,
                VerticalAlignment = TextObject.TextAlignment.Near,
                Scale = new Vector2(0.3f, 0.3f)
            };
            Layers["Gui"].Objekty.Add(_selectedObject);
            //Layers["Main"].Objekty.Add(new Player(this,new Vector2()));
            //for (int i = 0; i < 10; i++)
            //    Layers["Main"].Objekty.Add(new Tree(this, new Vector2(800 * i, 0)));
            //Layers["Main"].Objekty.Add(new Hause(this, new Vector2(80, 800)));
            //Layers["Main"].Objekty.Add(new ColidebleMovable(this));
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (Keyboard.GetState().IsKeyDown(Keys.P) && ScreenManager.LastKeyboardState.IsKeyUp(Keys.P) && _selectedNumber < Layers["Background"].Objekty.Count)
                _selectedNumber++;
            if (Keyboard.GetState().IsKeyDown(Keys.O) && ScreenManager.LastKeyboardState.IsKeyUp(Keys.O) && _selectedNumber > 0)
                _selectedNumber--;
            if (_selectedNumber > 0)
            {
                if (Keyboard.GetState().IsKeyDown(GameHelper.Instance.PlayerMoveRight) && ScreenManager.LastKeyboardState.IsKeyUp(GameHelper.Instance.PlayerMoveRight))
                    ((SpriteObject)Layers["Background"].Objekty[_selectedNumber-1]).Position += new Vector2(250f, 0);
                if (Keyboard.GetState().IsKeyDown(GameHelper.Instance.PlayerMoveLeft) && ScreenManager.LastKeyboardState.IsKeyUp(GameHelper.Instance.PlayerMoveLeft))
                    ((SpriteObject)Layers["Background"].Objekty[_selectedNumber-1]).Position += new Vector2(-250f, 0);
                if (Keyboard.GetState().IsKeyDown(GameHelper.Instance.PlayerMoveUp) && ScreenManager.LastKeyboardState.IsKeyUp(GameHelper.Instance.PlayerMoveUp))
                    ((SpriteObject)Layers["Background"].Objekty[_selectedNumber-1]).Position += new Vector2(0, -250f);
                if (Keyboard.GetState().IsKeyDown(GameHelper.Instance.PlayerMoveDown) && ScreenManager.LastKeyboardState.IsKeyUp(GameHelper.Instance.PlayerMoveDown))
                    ((SpriteObject)Layers["Background"].Objekty[_selectedNumber-1]).Position += new Vector2(0, 250f);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left) && ScreenManager.LastKeyboardState.IsKeyUp(Keys.Left)){
                ScreenBuilder sb = new ScreenBuilder(this);
                sb.SaveScreen();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D1) && ScreenManager.LastKeyboardState.IsKeyUp(Keys.D1))
                Layers["Background"].AddObject(new Road(this, "DU"));
            if (Keyboard.GetState().IsKeyDown(Keys.D2) && ScreenManager.LastKeyboardState.IsKeyUp(Keys.D2))
                Layers["Background"].AddObject(new Road(this, "LD"));
            if (Keyboard.GetState().IsKeyDown(Keys.D3) && ScreenManager.LastKeyboardState.IsKeyUp(Keys.D3))
                Layers["Background"].AddObject(new Road(this, "LR"));
            if (Keyboard.GetState().IsKeyDown(Keys.D4) && ScreenManager.LastKeyboardState.IsKeyUp(Keys.D4))
                Layers["Background"].AddObject(new Road(this, "LU"));
            if (Keyboard.GetState().IsKeyDown(Keys.D5) && ScreenManager.LastKeyboardState.IsKeyUp(Keys.D5))
                Layers["Background"].AddObject(new Road(this, "RD"));
            if (Keyboard.GetState().IsKeyDown(Keys.D6) && ScreenManager.LastKeyboardState.IsKeyUp(Keys.D6))
                Layers["Background"].AddObject(new Road(this, "RU"));
            if (Keyboard.GetState().IsKeyDown(Keys.D7) && ScreenManager.LastKeyboardState.IsKeyUp(Keys.D7))
                Layers["Background"].AddObject(new Stain(this, new Vector2(), "1"));
            if (Keyboard.GetState().IsKeyDown(Keys.D8) && ScreenManager.LastKeyboardState.IsKeyUp(Keys.D8))
                Layers["Background"].AddObject(new Stain(this, new Vector2(), "2"));
            if (Keyboard.GetState().IsKeyDown(Keys.D9) && ScreenManager.LastKeyboardState.IsKeyUp(Keys.D9))
                Layers["Background"].AddObject(new Stain(this, new Vector2(), "3"));
            if (Keyboard.GetState().IsKeyDown(Keys.D0) && ScreenManager.LastKeyboardState.IsKeyUp(Keys.D0))
                Layers["Background"].AddObject(new Stain(this, new Vector2(), "4"));
            _selectedObject.Text = "1:DU 2:LD 3:LR 4:LU 5:RD 6:RU 7-9:Stain   Vybrany objekt:" + _selectedNumber;
        }
    }
}
