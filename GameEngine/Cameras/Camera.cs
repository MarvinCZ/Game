using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameEngine.Cameras
{
    public abstract class Camera{
        private float _moveSpeed;
        protected bool NeedRecalculation;
        private float _rotation;
        private float _zoom;
        private Matrix _transformMatrix;
        public Vector2 Position { get; protected set; }
        protected readonly ScreenManager ScreenManager;

        public float MoveSpeed
        {
            get { return _moveSpeed; }
            protected set { _moveSpeed = value < 0 ? 0 : value; }
        }
        /// <summary>
        /// urcuje rotaci kamery v radianech
        /// </summary>
        public float Rotation
        {
            get { return _rotation; }
            set
            {
                _rotation = value;
                while (_rotation > MathHelper.Pi)
                {
                    _rotation -= MathHelper.TwoPi;
                }
                while (_rotation < -MathHelper.Pi)
                {
                    _rotation += MathHelper.TwoPi;
                }
                NeedRecalculation = true;
            }
        }
        /// <summary>
        /// priblizeni/oddaleni kamery
        /// </summary>
        public float Zoom
        {
            get { return _zoom; }
            set
            {
                _zoom = value;
                if (_zoom < 0.1f)
                    _zoom = 0.1f;
                NeedRecalculation = true;
            }
        }
        /// <summary>
        /// matice pro transofmaci vykreslovaci plochy
        /// </summary>
        public Matrix TransformMatrix { get { return _transformMatrix; } }
        /// <summary>
        /// vrati souradnice centra
        /// </summary>
        public Vector2 ScreenCenter{
            get
            {
                return new Vector2(
                    ScreenManager.GraphicsDevice.Viewport.Bounds.Width / 2f,
                    ScreenManager.GraphicsDevice.Viewport.Bounds.Height / 2f);
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
            Position = new Vector2();
            Rotation = 0f;
            Zoom = 1f;
            ScreenManager = game.ScreenManager;
            NeedRecalculation = true;
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
            if (NeedRecalculation){
                _transformMatrix = Matrix.Identity*
                                   Matrix.CreateTranslation(-Position.X, -Position.Y, 0)*
                                   Matrix.CreateRotationZ(_rotation)*
                                   Matrix.CreateTranslation(Origin.X, Origin.Y, 0)*
                                   Matrix.CreateScale(_zoom, _zoom, _zoom);

                Vector2 from;
                Vector2 to;
                Rectangle view = ScreenManager.GraphicsDevice.Viewport.Bounds;
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
                NeedRecalculation = false;
            }
        }
    }
}
