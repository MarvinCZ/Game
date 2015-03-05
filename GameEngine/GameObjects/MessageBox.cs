using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameEngine.GameObjects
{
    public class MessageBox : SpriteObject{
        protected readonly TextObject message;
        public List<SpriteObject> Buttons = new List<SpriteObject>();
        protected bool storno;
        public Result MessageResult;

        protected event EventHandler OnExit;
        public bool Active { get; private set; }
        public MessageBox(GameScreen game, String message, bool storno = true) : base(game){
            this.message = new TextObject(game,message,Color.Red);
            Buttons.Add(new ClickableText(game, "OK", new Vector2(), Color.Red, MouseOkClick));
            Buttons.Add(new ClickableText(game, "STORNO", new Vector2(), Color.Red, MouseStornoClick));
            this.storno = storno;
            Active = true;
        }

        protected virtual void MouseOkClick(object sender, EventArgs e)
        {
            MessageResult = Result.Ok;
            if(OnExit != null)
                OnExit(this,new EventArgs());
            Active = false;
        }

        protected virtual void MouseStornoClick(object sender, EventArgs e)
        {
            MessageResult = Result.Storno;
            if (OnExit != null)
                OnExit(this, new EventArgs());
            Active = false;
        }

        public void Show(EventHandler ev)
        {
            OnExit = null;
            OnExit += ev;
        }

        public override void Update(GameTime gameTime){
            base.Update(gameTime);
            message.Update(gameTime);
            Buttons[0].Update(gameTime);
            if (storno){
                Buttons[1].Update(gameTime);
                if (Keyboard.GetState().IsKeyDown(Keys.Escape)){
                    MouseStornoClick(null,new EventArgs());
                }
            }
            if(Keyboard.GetState().IsKeyDown(Keys.Enter))
                MouseOkClick(null,new EventArgs());
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch){
            base.Draw(gameTime, spriteBatch);
            message.Draw(gameTime,spriteBatch);
            Buttons[0].Draw(gameTime, spriteBatch);
            if(storno)
                Buttons[1].Draw(gameTime, spriteBatch);
        }

        public override void LoadContent(ContentManager content){
            message.Font = content.Load<SpriteFont>("SpriteFonts/pismo");
            ((ClickableText)Buttons[0]).Font = content.Load<SpriteFont>("SpriteFonts/pismo");
            ((ClickableText)Buttons[1]).Font = content.Load<SpriteFont>("SpriteFonts/pismo");
            Texture = content.Load<Texture2D>("BackGrounds/MessageBox");
            Reposition();
        }

        protected virtual void Reposition(){
            float x = message.BoundingBox.Width;
            if (x > Scale.X*Texture.Width)
            {
                Scale = new Vector2((x / Texture.Width) + 0.2f, 1);
            }
            Position = new Vector2(
                game.GraphicsDevice.Viewport.Bounds.Width / 2f,
                game.GraphicsDevice.Viewport.Bounds.Height / 2f);
            Vector2 position = new Vector2(0f,Texture.Height/4f);
            message.Position = Position - position;
            float celek = Buttons[0].BoundingBox.Width;
            if(storno)
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
            Ok,
        }
    }
}
