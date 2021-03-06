﻿using System;
using System.Collections.Generic;
using GameEngine.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameEngine.GuiObjects
{
    public class MessageBox : SpriteObject{
        protected readonly TextObject Message;
        public readonly List<SpriteObject> Buttons = new List<SpriteObject>();
        protected readonly List<GameObject> Objects = new List<GameObject>(); 
        protected readonly bool Storno;
        public Result MessageResult;

        protected event EventHandler OnExit;
        public MessageBox(GameScreen game, String message, bool storno = true) : base(game){
            Message = new TextObject(game,message,Color.Red);
            Buttons.Add(new ClickableText(game, "OK", new Vector2(), Color.Red, MouseOkClick));
            Buttons.Add(new ClickableText(game, "STORNO", new Vector2(), Color.Red, MouseStornoClick));
            Storno = storno;
            Objects.Add(this);
            Objects.Add(Message);
            Objects.Add(Buttons[0]);
            if(storno)
                Objects.Add(Buttons[1]);
        }

        protected virtual void MouseOkClick(object sender, EventArgs e)
        {
            MessageResult = Result.Ok;
            if(OnExit != null)
                OnExit(this,new EventArgs());
        }

        protected virtual void MouseStornoClick(object sender, EventArgs e)
        {
            MessageResult = Result.Storno;
            if (OnExit != null)
                OnExit(this, new EventArgs());
        }

        public void Show(EventHandler ev)
        {
            foreach (GameObject gameObject in Objects){
                GameScreen.Layers["Gui"].Objekty.Add(gameObject);
                gameObject.Layer = GameScreen.Layers["Gui"];
            }
            GameScreen.Pause(Objects);
            GameScreen.Layers["Gui"].MoveObjects();
            OnExit = null;
            OnExit += ev;
        }

        public new void Destroy()
        {
            foreach (GameObject gameObject in Objects)
            {
                GameScreen.Layers["Gui"].Objekty.Remove(gameObject);
            }
            GameScreen.UnPause();
        }

        public override void Update(GameTime gameTime){
            base.Update(gameTime);
            if (Storno){
                if (Keyboard.GetState().IsKeyDown(Keys.Escape)){
                    MouseStornoClick(null,new EventArgs());
                }
            }
            if(Keyboard.GetState().IsKeyDown(Keys.Enter))
                MouseOkClick(null,new EventArgs());
        }

        public override void LoadContent(ContentManager content){
            Message.Font = content.Load<SpriteFont>("SpriteFonts/pismo");
            ((ClickableText)Buttons[0]).Font = content.Load<SpriteFont>("SpriteFonts/pismo");
            ((ClickableText)Buttons[1]).Font = content.Load<SpriteFont>("SpriteFonts/pismo");
            Texture = content.Load<Texture2D>("BackGrounds/MessageBox");
            Reposition();
        }

        protected virtual void Reposition(){
            float x = Message.BoundingBox.Width;
            if (x > Scale.X*Texture.Width)
            {
                Scale = new Vector2((x / Texture.Width) + 0.2f, 1);
            }
            Position = new Vector2(
                ScreenManager.GraphicsDevice.Viewport.Bounds.Width / 2f,
                ScreenManager.GraphicsDevice.Viewport.Bounds.Height / 2f);
            Vector2 position = new Vector2(0f,Texture.Height/4f);
            Message.Position = Position - position;
            float celek = Buttons[0].BoundingBox.Width;
            if(Storno)
                celek += 20f + Buttons[1].BoundingBox.Width;
            celek /= 2;
            position = new Vector2(-celek, Texture.Height / 4f);
            Buttons[0].Position = Position + position;
            ((ClickableText)Buttons[0]).HorizontAlignment = TextObject.TextAlignment.Near;
            position = new Vector2(celek, Texture.Height / 4f);
            Buttons[1].Position = Position + position;
            ((ClickableText)Buttons[1]).HorizontAlignment = TextObject.TextAlignment.Far;

        }

        public enum Result{
            Storno,
            Ok
        }
    }
}
