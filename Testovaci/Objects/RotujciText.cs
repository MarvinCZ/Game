using GameEngine;
using GameEngine.GameObjects;
using Microsoft.Xna.Framework;

namespace Testovaci.Objects
{
    class RotujciText : TextObject
    {
        public RotujciText(GameScreen game, string text, Vector2 pozice) : base(game, text, pozice){
            Scale = new Vector2(0.5f,0.5f);
        }

        public override void Update(GameTime gametime){
            base.Update(gametime);
            Rotation += 0.01f;
        }
    }
}
