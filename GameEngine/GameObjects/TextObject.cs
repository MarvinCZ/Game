using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.GameObjects
{
    public class TextObject : SpriteObject{
        private SpriteFont _font;
        private String _text;

        public enum TextAlignment{
            /// <summary>
            /// Zarovnani urceno kodem
            /// </summary>
            Manual,
            /// <summary>
            /// Zarovnano do leva/na horu
            /// </summary>
            Near,
            /// <summary>
            /// Zarovnano na stred
            /// </summary>
            Center,
            /// <summary>
            /// Zarovnano do prava/dolu
            /// </summary>
            Far,
        }

        private TextAlignment _verticalAlignment = TextAlignment.Center;
        private TextAlignment _horizontAlignment = TextAlignment.Center;

        /// <summary>
        /// urcuje vertikalni pozici originu pro stringu
        /// </summary>
        public TextAlignment VerticalAlignment{
            get { return _verticalAlignment; }
            set{
                if (_verticalAlignment != value){
                    _verticalAlignment = value;
                    ReCalculate();
                }
            }
        }

        /// <summary>
        /// urcuje horizontalni pozici originu pro string
        /// </summary>
        public TextAlignment HorizontAlignment{
            get { return _horizontAlignment; }
            set{
                if (_horizontAlignment != value){
                    _horizontAlignment = value;
                    ReCalculate();
                }
            }
        }

        /// <summary>
        /// urcuje text, ktery bude vykreslen
        /// </summary>
        public string Text{
            get { return _text; }
            set{
                if (_text != value){
                    _text = value;
                    ReCalculate();
                }
            }
        }

        public override Rectangle BoundingBox{
            get
            {
                Vector2 textSize = SourceRectangle.IsEmpty ? Font.MeasureString(Text) : new Vector2(SourceRectangle.Width, SourceRectangle.Height);
                Rectangle r = new Rectangle((int)positionX, (int)positionY, (int)(textSize.X * Scale.X), (int)(textSize.Y * Scale.Y));
                r.Offset((int)(-originX * Scale.X), (int)(-originY * Scale.Y));
                return r;
            }
        }

        /// <summary>
        /// SpriteFont vyuzity k vykresleni
        /// </summary>
        public SpriteFont Font{
            get { return _font; }
            set{
                if (_font != value){
                    _font = value;
                    ReCalculate();
                }
            }
        }

        /// <summary>
        /// Slouzi k prepocitani originu po zmene hodnot
        /// </summary>
        private void ReCalculate(){
            if (HorizontAlignment == TextAlignment.Manual && VerticalAlignment == TextAlignment.Manual)
                return;
            if(Font == null || Text == null || Text.Length == 0)
                return;
            Vector2 size = Font.MeasureString(Text);
            switch (HorizontAlignment)
            {
                case TextAlignment.Near:
                    originX = 0;
                    break;
                case TextAlignment.Center:
                    originX = size.X / 2;
                    break;
                case TextAlignment.Far:
                    originX = size.X;
                    break;
            }
            switch (VerticalAlignment)
            {
                case TextAlignment.Near:
                    originY = 0;
                    break;
                case TextAlignment.Center:
                    originY = size.Y / 2;
                    break;
                case TextAlignment.Far:
                    originY = size.Y;
                    break;
            }

        }

        #region Construktor

        public TextObject(GameScreen game, String text, Vector2 pozice, Color color, SpriteFont font = null)
            : this(game, text, color, font){
            Position = pozice;
        }

        public TextObject(GameScreen game, String text, Color color, SpriteFont font = null)
            : base(game){
            _text = text;
            _font = font;
            SpriteColor = color;
            ReCalculate();
        }

        public TextObject(GameScreen game, String text, Vector2 pozice, SpriteFont font = null)
            : this(game, text, font){
            Position = pozice;
        }

        public TextObject(GameScreen game, String text, SpriteFont font = null)
            : base(game){
            _text = text;
            _font = font;
            ReCalculate();
        }

        #endregion

        /// <summary>
        /// pokud muze tak vykresli text
        /// </summary>
        /// <param name="gameTime">herni cas</param>
        /// <param name="spriteBatch">sprite batch</param>
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch){
            if (Font != null && !string.IsNullOrEmpty(Text)){
                spriteBatch.DrawString(_font, _text, Position, SpriteColorA, Rotation,Origin,Scale,SpriteEffects.None,LayerDepth);
            }
        }

        public override void LoadContent(ContentManager content){
            if(_font == null)
                Font = content.Load<SpriteFont>("SpriteFonts/pismo");
        }

        public override void UnloadContent(){
            base.UnloadContent();
            Font = null;
        }
    }
}
