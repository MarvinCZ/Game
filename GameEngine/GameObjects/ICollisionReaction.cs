using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.GameObjects
{
    interface ICollisionReaction
    {
        void CollisionReaction(GameObject obj);
    }
}
