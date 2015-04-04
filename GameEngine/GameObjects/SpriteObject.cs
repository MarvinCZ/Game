using System.Collections.Generic;
using System.Linq;
using GameEngine.Cameras;
using GameEngine.HelpObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.GameObjects
{
    public abstract class SpriteObject : GameObject, IFollowable
    {

        protected float PositionX;
        protected float PositionY;
        protected float OriginX;
        protected float OriginY;

        protected int SpriteColorR;
        protected int SpriteColorG;
        protected int SpriteColorB;
        public float SpriteColorAlfa = 1f;
        private Texture2D _texture;
        protected bool Solid;
        private Rectangle _boundingBox;

        public ColisionBox ColisionBox { protected set; get; }

        protected List<Sound> Sounds;

        public bool IsSolid
        {
            get
            {
                return Solid;
            }
        }

        /// <summary>
        /// textura pro vykresleni
        /// </summary>
        public Texture2D Texture{
            get{
               return _texture;
            }
            set{
                _texture = value;
                Origin = new Vector2(
                    _texture.Width/2f,
                    _texture.Height / 2f);
                UpdateBoundingBox();
            }
        }
        /// <summary>
        /// Origin(stred) pro rotaci a scale
        /// </summary>
        public Vector2 Origin{
            get{
                return new Vector2(OriginX, OriginY);
            }
            set
            {
                OriginX = value.X;
                OriginY = value.Y;
                UpdateBoundingBox();
            }
        }
        /// <summary>
        /// pozice obektu
        /// </summary>
        public Vector2 Position{
            get{
                return new Vector2(PositionX,PositionY);
            }
            set
            {
                PositionX = value.X;
                PositionY = value.Y;
                UpdateBoundingBox();
            }
        }
        /// <summary>
        /// Meni se pozice vykresleni v zavislosti na kamere
        /// </summary>
        public bool CameraDependent {
            get
            {
                return Layer.CameraDependent;
            }
        }
        /// <summary>
        /// rotace objektu v radianech
        /// </summary>
        public float Rotation { get ; set; }
        /// <summary>
        /// barva pouzita k vykreslovani (bez alfy)
        /// </summary>
        public Color SpriteColor{
            get { return new Color(SpriteColorR,SpriteColorG,SpriteColorB);}
            set{
                SpriteColorR = value.R;
                SpriteColorG = value.G;
                SpriteColorB = value.B;
            }
        }
        /// <summary>
        /// barva pouzita k vykreslovani (s alfou)
        /// </summary>
        public Color SpriteColorA{
            get { return new Color(SpriteColorR, SpriteColorG, SpriteColorB) * SpriteColorAlfa; }
            set
            {
                SpriteColorR = value.R;
                SpriteColorG = value.G;
                SpriteColorB = value.B;
                SpriteColorAlfa = value.A;
            }

        }
        /// <summary>
        /// scale(zvetseni/zmenseni) objektu
        /// </summary>
        public Vector2 Scale { get; set; }
        /// <summary>
        /// urcuje kde(vpred/vzad) se bude objekt nachzet
        /// </summary>
        public float LayerDepth { get; set; }
        /// <summary>
        /// urcuje vyrez k vykresleni, pokud neni definovat vykresli se cely
        /// </summary>
        public Rectangle SourceRectangle { get; set; }
        /// <summary>
        /// Vrati obdelnik okolo objektu
        /// </summary>
        public virtual Rectangle BoundingBox{
            get{
                if (Texture != null)
                {
                    return _boundingBox;
                }
                return new Rectangle();
            }
        }

        #region Constructors

        protected SpriteObject(GameScreen game) : base(game){
            Scale = Vector2.One;
            SpriteColor = Color.White;
            Sounds = new List<Sound>();
        }

        protected SpriteObject(GameScreen game,Vector2 position)
            : this(game){
            Position = position;
        }
        #endregion
        
        /// <summary>
        /// Vykresli objekt
        /// </summary>
        /// <param name="gameTime">herni cas</param>
        /// <param name="spriteBatch">sprite batch</param>
        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch){
            if (Texture != null){
                if (SourceRectangle.IsEmpty){
                    spriteBatch.Draw(Texture, Position, null, SpriteColorA, Rotation, Origin, Scale, SpriteEffects.None, LayerDepth);
                }
                else{
                    spriteBatch.Draw(Texture, Position, SourceRectangle, SpriteColorA, Rotation, Origin, Scale, SpriteEffects.None, LayerDepth);
                }
            }
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            foreach (Sound snd in Sounds)
                snd.Update();
        }

        public override void UnloadContent(){
            _texture = null;
            foreach (Sound snd in Sounds)
                snd.Stop();
            Sounds = new List<Sound>();
        }
        protected void UpdateBoundingBox()
        {
            if (Texture == null)
            {
                _boundingBox = new Rectangle();
                return;
            }
            Vector2 spriteSize = SourceRectangle.IsEmpty ? new Vector2(Texture.Width, Texture.Height) : new Vector2(SourceRectangle.Width, SourceRectangle.Height);
            Rectangle r = new Rectangle((int)PositionX, (int)PositionY, (int)(spriteSize.X * Scale.X), (int)(spriteSize.Y * Scale.Y));
            r.Offset((int)(-OriginX * Scale.X), (int)(-OriginY * Scale.Y));
            _boundingBox = r;
        }
    }
}
