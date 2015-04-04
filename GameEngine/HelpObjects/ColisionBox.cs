using System.Collections.Generic;
using GameEngine.GameObjects;
using Microsoft.Xna.Framework;

namespace GameEngine.HelpObjects
{
    /// <summary>
    /// Pomocny objek pro reseni kolizi
    /// </summary>
    public class ColisionBox
    {
        protected readonly SpriteObject Objekt;
        public readonly BoxType Type;
        public List<ColisionBox> Boxes;

        /// <summary>
        /// Bez boxu, box bude obdelnik
        /// </summary>
        /// <param name="obj">Objek pro ktery se resi kolize</param>
        public ColisionBox(SpriteObject obj)
        {
            Type = BoxType.Rectangle;
            Objekt = obj;
        }

        /// <summary>
        /// S nastavitelnym boxem
        /// </summary>
        /// <param name="obj">Objek pro ktery se resi kolize</param>
        /// <param name="btype">Typ boxu (obdelnik, kruh)</param>
        public ColisionBox(SpriteObject obj, BoxType btype)
            : this(obj)
        {
            Type = btype;
        }

        /// <summary>
        /// Vyhodnoceni kolize
        /// </summary>
        /// <param name="otherBox">Box pro druhej objekt</param>
        /// <returns>Vrati true kdyz nastane kolize</returns>
        public bool ColideWhith(ColisionBox otherBox)
        {
            if (Objekt.BoundingBox.Intersects(otherBox.Objekt.BoundingBox))
            {
                if (Boxes == null && otherBox.Boxes == null)
                {
                    if (Type == BoxType.Rectangle)
                    {
                        if (otherBox.Type == BoxType.Rectangle)
                        {
                            return true;
                        }
                        if (otherBox.Type == BoxType.Circle)
                            return CirkleInRectangle(otherBox.Objekt.BoundingBox.Center, otherBox.Objekt.BoundingBox.Width / 2, Objekt.BoundingBox);
                    }
                    else
                    {
                        if (otherBox.Type == BoxType.Rectangle)
                        {
                            return CirkleInRectangle(Objekt.BoundingBox.Center, Objekt.BoundingBox.Width / 2, otherBox.Objekt.BoundingBox);                            
                        }
                        if (otherBox.Type == BoxType.Circle)
                        {
                            Point center = otherBox.Objekt.BoundingBox.Center;
                            Point center2 = Objekt.BoundingBox.Center;
                            int radius = otherBox.Objekt.BoundingBox.Width / 2;
                            int radius2 = Objekt.BoundingBox.Width / 2;
                            int x = center.X - center2.X;
                            int y = center.Y - center2.Y;
                            int rad = radius + radius2;
                            x *= x;
                            y *= y;
                            rad *= rad;
                            int vzd = x + y;
                            if (vzd < rad)
                                return true;
                            return false;
                        }
                    }
                }
                if (Boxes != null && otherBox.Boxes == null)
                {
                    for (int i = 0; i < Boxes.Count; i++)
                    {
                        if (otherBox.ColideWhith(Boxes[i]))
                        {
                            return true;
                        }
                    }
                    return false;
                }
                if (Boxes == null && otherBox.Boxes != null)
                {
                    for (int i = 0; i < otherBox.Boxes.Count; i++)
                    {
                        if (ColideWhith(otherBox.Boxes[i]))
                        {
                            return true;
                        }
                    }
                    return false;
                }
                if (Boxes != null && otherBox.Boxes != null)
                {
                    for (int i = 0; i < otherBox.Boxes.Count; i++)
                    {
                        for (int j = 0; j < Boxes.Count; j++)
                        {
                            if (otherBox.Boxes[i].ColideWhith(Boxes[j]))
                            {
                                return true;
                            }
                        }
                    }
                }
                return true;
            }
            return false;
        }
        public enum BoxType
        {
            Rectangle,
            Circle
        }
        bool PointInCirkle(Point pnt, Point center, int radius)
        {
            int x = pnt.X - center.X;
            int y = pnt.Y - center.Y;
            x *= x;
            y *= y;
            int rad = radius * radius;
            int vzdl = x + y;
            if (vzdl < rad)
                return true;
            return false;
        }
        bool CirkleInRectangle(Point center, int radius, Rectangle rect)
        {
            Point topleft = new Point(rect.Left, rect.Top);
            Point topright = new Point(rect.Right, rect.Top);
            Point botleft = new Point(rect.Left, rect.Bottom);
            Point botright = new Point(rect.Right, rect.Bottom);
            if (center.X > topleft.X && center.X < topright.X)
                return true;
            if (center.Y > topleft.Y && center.Y < botright.Y)
                return true;
            if (PointInCirkle(topleft, center, radius))
                return true;
            if (PointInCirkle(topright, center, radius))
                return true;
            if (PointInCirkle(botleft, center, radius))
                return true;
            if (PointInCirkle(botright, center, radius))
                return true;
            return false; 
        }
    }
}
