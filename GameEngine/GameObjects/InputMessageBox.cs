using System;
using System.Text.RegularExpressions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameEngine.GameObjects
{
    public class InputMessageBox : MessageBox{
        protected readonly TextObject input;
        private readonly Regex _regex;
        private KeyboardState _oldKeyboardState;
        private KeyboardState _curentKeyboardState;
        public string Input;

        public InputMessageBox(GameScreen game, string message, bool storno = true) : base(game, message, storno){
            input = new TextObject(game,"",Color.Green);
            Input = "";
            this.Objects.Add(input);
        }

        public InputMessageBox(GameScreen game, string message, Regex rex, bool storno = true) : this(game, message, storno){
            _regex = rex;
        }

        protected override void MouseOkClick(object sender, EventArgs e)
        {
            if (_regex == null || _regex.IsMatch(input.Text))
                base.MouseOkClick(sender, e);
            else{
                if (!message.Text.Contains("špatný formát"))
                {
                    message.Text += " špatný formát";
                    message.SpriteColor = Color.Blue;
                }
            }
        }

        public override void Update(GameTime gameTime){
            base.Update(gameTime);
            _oldKeyboardState = _curentKeyboardState;
            _curentKeyboardState = Keyboard.GetState();
            Keys[] pressed = _curentKeyboardState.GetPressedKeys();
            bool shift = (_curentKeyboardState.IsKeyDown(Keys.LeftShift) ||
                          _curentKeyboardState.IsKeyDown(Keys.RightShift)) ||
                         _curentKeyboardState.IsKeyDown(Keys.CapsLock);
            for (int i = 0; i < pressed.Length; i++){
                if (pressed[i] == Keys.Back && _oldKeyboardState.IsKeyUp(Keys.Back) && Input.Length != 0){
                    Input = Input.Remove(Input.Length - 1, 1);
                }
                else if (pressed[i] == Keys.Enter){
                    MouseOkClick(null,new EventArgs());
                }
                else if (_oldKeyboardState.IsKeyUp(pressed[i])){
                    string inpt = GameHelper.Instance.TextFromKey(pressed[i], shift);
                    if (inpt != null)
                        Input += inpt;
                }
            }
            //input.Update(gameTime);
            input.Text = Input;
            float lengh = input.BoundingBox.Width;
            if (lengh < 100f)
                lengh = 100f;
            lengh += Buttons[0].BoundingBox.Width;
            lengh += 15f;
            if (storno)
                lengh += Buttons[1].BoundingBox.Width;
            if (lengh < message.BoundingBox.Width)
                lengh = message.BoundingBox.Width;
            Scale = new Vector2((lengh / Texture.Width) + 0.2f, 1);
            Reposition();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch){
            base.Draw(gameTime, spriteBatch);
            input.Draw(gameTime,spriteBatch);
        }

        public override void LoadContent(ContentManager content)
        {
            message.Font = content.Load<SpriteFont>("SpriteFonts/pismo");
            ((ClickableText)Buttons[0]).Font = content.Load<SpriteFont>("SpriteFonts/pismo");
            Texture = content.Load<Texture2D>("BackGrounds/MessageBox");
            input.Font = content.Load<SpriteFont>("SpriteFonts/pismo");
            ((ClickableText)Buttons[1]).Font = content.Load<SpriteFont>("SpriteFonts/pismo");
            Reposition();
        }

        protected override void Reposition(){
            base.Reposition();
            Position = new Vector2(
                ScreenManager.GraphicsDevice.Viewport.Bounds.Width / 2f,
                ScreenManager.GraphicsDevice.Viewport.Bounds.Height / 2f);
            float width = (Texture.Width*Scale.X)/2f;
            Vector2 position = new Vector2(width - 10f, Texture.Height/4f);
            ((ClickableText)Buttons[0]).Position = Position + position;
            ((ClickableText)Buttons[0]).HorizontAlignment = TextObject.TextAlignment.Far;
            position = new Vector2(width - 20f - ((ClickableText)Buttons[0]).BoundingBox.Width, Texture.Height / 4f);
            ((ClickableText)Buttons[1]).Position = Position + position;
            ((ClickableText)Buttons[1]).HorizontAlignment = TextObject.TextAlignment.Far;
            position = new Vector2(-width + 10f, Texture.Height/4f);
            input.Position = Position + position;
            input.HorizontAlignment = TextObject.TextAlignment.Near;
        }
    }
}
