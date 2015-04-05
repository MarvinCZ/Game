using System;
using System.Text.RegularExpressions;
using GameEngine.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameEngine.GuiObjects
{
    public class InputMessageBox : MessageBox{
        protected readonly TextObject Input;
        private readonly Regex _regex;
        private KeyboardState _oldKeyboardState;
        private KeyboardState _curentKeyboardState;
        public string InputText;

        public InputMessageBox(GameScreen game, string message, bool storno = true) : base(game, message, storno){
            Input = new TextObject(game,"",Color.Green);
            InputText = "";
            Objects.Add(Input);
        }

        public InputMessageBox(GameScreen game, string message, Regex rex, bool storno = true) : this(game, message, storno){
            _regex = rex;
        }

        protected override void MouseOkClick(object sender, EventArgs e)
        {
            if (_regex == null || _regex.IsMatch(Input.Text))
                base.MouseOkClick(sender, e);
            else{
                if (!Message.Text.Contains("špatný formát"))
                {
                    Message.Text += " špatný formát";
                    Message.SpriteColor = Color.Blue;
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
                if (pressed[i] == Keys.Back && _oldKeyboardState.IsKeyUp(Keys.Back) && InputText.Length != 0){
                    InputText = InputText.Remove(InputText.Length - 1, 1);
                }
                else if (pressed[i] == Keys.Enter){
                    MouseOkClick(null,new EventArgs());
                }
                else if (_oldKeyboardState.IsKeyUp(pressed[i])){
                    string inpt = GameHelper.Instance.TextFromKey(pressed[i], shift);
                    if (inpt != null)
                        InputText += inpt;
                }
            }
            //input.Update(gameTime);
            Input.Text = InputText;
            float lengh = Input.BoundingBox.Width;
            if (lengh < 100f)
                lengh = 100f;
            lengh += Buttons[0].BoundingBox.Width;
            lengh += 15f;
            if (Storno)
                lengh += Buttons[1].BoundingBox.Width;
            if (lengh < Message.BoundingBox.Width)
                lengh = Message.BoundingBox.Width;
            Scale = new Vector2((lengh / Texture.Width) + 0.2f, 1);
            Reposition();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch){
            base.Draw(gameTime, spriteBatch);
            Input.Draw(gameTime,spriteBatch);
        }

        public override void LoadContent(ContentManager content)
        {
            Message.Font = content.Load<SpriteFont>("SpriteFonts/pismo");
            ((ClickableText)Buttons[0]).Font = content.Load<SpriteFont>("SpriteFonts/pismo");
            Texture = content.Load<Texture2D>("BackGrounds/MessageBox");
            Input.Font = content.Load<SpriteFont>("SpriteFonts/pismo");
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
            Input.Position = Position + position;
            Input.HorizontAlignment = TextObject.TextAlignment.Near;
        }
    }
}
