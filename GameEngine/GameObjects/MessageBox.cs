using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameEngine.GameObjects
{
    public class MessageBox : SpriteObject{
        protected readonly TextObject message;
        protected readonly ClickableText okButton;
        protected readonly ClickableText stornoButton;
        protected bool storno;
        public Result MessageResult;

        protected event EventHandler OnExit;
        public bool Active { get; private set; }
        public MessageBox(GameScreen game, String message, bool storno = true) : base(game){
            this.message = new TextObject(game,message,Color.Red);
            okButton = new ClickableText(game, "OK", new Vector2(), Color.Red, MouseOkClick);
            stornoButton = new ClickableText(game, "STORNO", new Vector2(), Color.Red, MouseStornoClick);
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
            okButton.Update(gameTime);
            if (storno){
                stornoButton.Update(gameTime);
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
            okButton.Draw(gameTime,spriteBatch);
            if(storno)
                stornoButton.Draw(gameTime,spriteBatch);
        }

        public override void LoadContent(ContentManager content){
            message.Font = content.Load<SpriteFont>("SpriteFonts/pismo");
            okButton.Font = content.Load<SpriteFont>("SpriteFonts/pismo");
            stornoButton.Font = content.Load<SpriteFont>("SpriteFonts/pismo");
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
            float celek = okButton.BoundingBox.Width;
            if(storno)
                celek += 20f + stornoButton.BoundingBox.Width;
            celek /= 2;
            position = new Vector2(-celek, Texture.Height / 4f);
            okButton.Position = Position + position;
            okButton.HorizontAlignment = TextObject.TextAlignment.Near;
            position = new Vector2(celek, Texture.Height / 4f);
            stornoButton.Position = Position + position;
            stornoButton.HorizontAlignment = TextObject.TextAlignment.Far;

        }

        public enum Result{
            Storno,
            Ok,
        }
    }
}
