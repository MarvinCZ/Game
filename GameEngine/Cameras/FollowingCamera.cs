using Microsoft.Xna.Framework;

namespace GameEngine.Cameras
{
    /// <summary>
    /// Kamera, ktera se centruje na urcity objekt
    /// </summary>
    public class FollowingCamera : Camera{

        private readonly IFollowable _followable;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="game">Isntance tridy GhameHost</param>
        /// <param name="followable">Objekt ke sledovani</param>
        /// <param name="moveSpeed">Rychlost kamery vuci objektu 0f-1f (nepohybliva-bez zpozdeni)</param>
        public FollowingCamera(GameScreen game,IFollowable followable,float moveSpeed = 0.03f) : base(game){
            MoveSpeed = moveSpeed;
            _followable = followable;
            NeedRecalculation = true;
        }

        public override void Update()
        {
            base.Update();
            Vector2 difference = _followable.Position - Position;
            if (difference.Length() <= 1f){
                Position = _followable.Position;
            }
            else{
                difference *= MoveSpeed;
                Position += difference;
            }
            NeedRecalculation = difference.Length() != 0;
        }
    }
}
