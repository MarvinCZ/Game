using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameEngine.Cameras
{
    public abstract class Camera{
        private float _moveSpeed;
        protected bool reCalc;
        protected float rotation;
        protected float zoom;
        protected Matrix transformMatrix;
        protected Vector2 position;
        protected readonly Game game;

        public float MoveSpeed
        {
            get { return _moveSpeed; }
            protected set { _moveSpeed = value < 0 ? 0 : value; }
        }
        /// <summary>
        /// pozice puuzita pro rotaci a zoom
        /// </summary>
        public Vector2 Position;
        /// <summary>
        /// urcuje rotaci kamery v radianech
        /// </summary>
        public float Rotation
        {
            get { return rotation; }
            set
            {
                rotation = value;
                while (rotation > MathHelper.Pi)
                {
                    rotation -= MathHelper.TwoPi;
                }
                while (rotation < -MathHelper.Pi)
                {
                    rotation += MathHelper.TwoPi;
                }
                reCalc = true;
            }
        }
        /// <summary>
        /// priblizeni/oddaleni kamery
        /// </summary>
        public float Zoom
        {
            get { return zoom; }
            set
            {
                zoom = value;
                if (zoom < 0.1f)
                    zoom = 0.1f;
                reCalc = true;
            }
        }
        /// <summary>
        /// matice pro transofmaci vykreslovaci plochy
        /// </summary>
        public Matrix TransformMatrix { get { return transformMatrix; } }
        /// <summary>
        /// vrati souradnice centra
        /// </summary>
        public Vector2 ScreenCenter{
            get
            {
                return new Vector2(
                    game.GraphicsDevice.Viewport.Bounds.Width / 2f,
                    game.GraphicsDevice.Viewport.Bounds.Height / 2f);
            }
        }
        /// <summary>
        /// posunuti kamery (pozice bude v centru)
        /// </summary>
        public Vector2 Origin{
            get { return (ScreenCenter/Zoom); }
        }

        public Rectangle ViewRectangle { get; protected set; }

        protected Camera(GameScreen game){
            Rotation = 0f;
            Zoom = 1f;
            this.game = game.Game;
            reCalc = true;
        }
        /// <summary>
        /// prepocitani matice
        /// </summary>
        public virtual void Update()
        {
            if (Keyboard.GetState().IsKeyDown(GameHelper.Instance.RotateLeft))
                Rotation -= 0.01f;
            if (Keyboard.GetState().IsKeyDown(GameHelper.Instance.RotateRight))
                Rotation += 0.01f;
            if (Keyboard.GetState().IsKeyDown(GameHelper.Instance.ZoomOut))
                Zoom += 0.01f;
            if (Keyboard.GetState().IsKeyDown(GameHelper.Instance.ZoomIn))
                Zoom -= 0.01f;
            if (Keyboard.GetState().IsKeyDown(GameHelper.Instance.CamMoveSpeedPlus))
                MoveSpeed += 0.01f;
            if (Keyboard.GetState().IsKeyDown(GameHelper.Instance.CamMoveSpeedMinus))
                MoveSpeed -= 0.01f;
            if (reCalc){
                transformMatrix = Matrix.Identity*
                                  Matrix.CreateTranslation(-position.X, -position.Y, 0)*
                                  Matrix.CreateRotationZ(rotation)*
                                  Matrix.CreateTranslation(Origin.X, Origin.Y, 0)*
                                  Matrix.CreateScale(zoom, zoom, zoom);

                Vector2 from;
                Vector2 to;
                Rectangle view = game.GraphicsDevice.Viewport.Bounds;
                if (Rotation == 0){
                    from = GameHelper.Instance.RealVector2(new Vector2(-200, -200), TransformMatrix);
                    to = GameHelper.Instance.RealVector2(new Vector2(view.Width + 200, view.Height + 200),
                        TransformMatrix);
                }
                else{
                    Vector2[] kraje = new Vector2[4];
                    kraje[0] = GameHelper.Instance.RealVector2(new Vector2(-200, -200), TransformMatrix);
                    kraje[1] = GameHelper.Instance.RealVector2(new Vector2(view.Width + 200, -200), TransformMatrix);
                    kraje[2] = GameHelper.Instance.RealVector2(new Vector2(-200, view.Height + 200), TransformMatrix);
                    kraje[3] = GameHelper.Instance.RealVector2(new Vector2(view.Width + 200, view.Height + 200),
                        TransformMatrix);
                    float minX = kraje[0].X;
                    float minY = kraje[0].Y;
                    float maxX = kraje[0].X;
                    float maxY = kraje[0].Y;
                    for (int k = 1; k < kraje.Length; k++){
                        if (kraje[k].X < minX)
                            minX = kraje[k].X;
                        if (kraje[k].X > maxX)
                            maxX = kraje[k].X;
                        if (kraje[k].Y < minY)
                            minY = kraje[k].Y;
                        if (kraje[k].Y > maxY)
                            maxY = kraje[k].Y;
                    }
                    from = new Vector2(minX, minY);
                    to = new Vector2(maxX, maxY);

                }
                ViewRectangle = new Rectangle((int) from.X, (int) from.Y, (int) to.X - (int) from.X,
                    (int) to.Y - (int) from.Y);
                reCalc = false;
            }
        }
    }
}
