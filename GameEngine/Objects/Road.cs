using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Objects
{
    class Road : SpriteObject
    {
        private string _smer;

        public Road(GameScreen game, string smer) : base(game)
        {
            _smer = smer;
        }

        public Road(GameScreen game, string smer, Vector2 position) : base(game, position)
        {
            _smer = smer;
        }
        public override void LoadContent(ContentManager content)
        {
            if (Texture == null)
            {
                Texture = content.Load<Texture2D>("Sprites/road_1_"+_smer);
            }
        }
    }
}
