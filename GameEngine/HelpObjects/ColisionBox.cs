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
        protected SpriteObject objekt;
        public BoxType Type;
        public List<ColisionBox> Boxes;

        /// <summary>
        /// Bez boxu, box bude obdelnik
        /// </summary>
        /// <param name="obj">Objek pro ktery se resi kolize</param>
        public ColisionBox(SpriteObject obj)
        {
            Type = BoxType.Rectangle;
            objekt = obj;
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
        /// <param name="cbox">Box pro druhej objekt</param>
        /// <returns>Vrati true kdyz nastane kolize</returns>
        public bool ColideWhith(ColisionBox cbox)
        {
            if (objekt.BoundingBox.Intersects(cbox.objekt.BoundingBox))
            {
                if (Boxes == null && cbox.Boxes == null)
                {
                    if (Type == BoxType.Rectangle)
                    {
                        if (cbox.Type == BoxType.Rectangle)
                        {
                            return true;
                        }
                        if (cbox.Type == BoxType.Circle)
                            return cirkleInRectangle(cbox.objekt.BoundingBox.Center, cbox.objekt.BoundingBox.Width / 2, objekt.BoundingBox);
                    }
                    else
                    {
                        if (cbox.Type == BoxType.Rectangle)
                        {
                            return cirkleInRectangle(objekt.BoundingBox.Center, objekt.BoundingBox.Width / 2, cbox.objekt.BoundingBox);                            
                        }
                        if (cbox.Type == BoxType.Circle)
                        {
                            Point center = cbox.objekt.BoundingBox.Center;
                            Point center2 = objekt.BoundingBox.Center;
                            int radius = cbox.objekt.BoundingBox.Width / 2;
                            int radius2 = objekt.BoundingBox.Width / 2;
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
                if (Boxes != null && cbox.Boxes == null)
                {
                    for (int i = 0; i < Boxes.Count; i++)
                    {
                        if (cbox.ColideWhith(Boxes[i]))
                        {
                            return true;
                        }
                    }
                    return false;
                }
                if (Boxes == null && cbox.Boxes != null)
                {
                    for (int i = 0; i < cbox.Boxes.Count; i++)
                    {
                        if (ColideWhith(cbox.Boxes[i]))
                        {
                            return true;
                        }
                    }
                    return false;
                }
                if (Boxes != null && cbox.Boxes != null)
                {
                    for (int i = 0; i < cbox.Boxes.Count; i++)
                    {
                        for (int j = 0; j < Boxes.Count; j++)
                        {
                            if (cbox.Boxes[i].ColideWhith(Boxes[j]))
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
        bool pointInCirkle(Point pnt, Point center, int radius)
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
        bool cirkleInRectangle(Point center, int radius, Rectangle rect)
        {
            Point topleft = new Point(rect.Left, rect.Top);
            Point topright = new Point(rect.Right, rect.Top);
            Point botleft = new Point(rect.Left, rect.Bottom);
            Point botright = new Point(rect.Right, rect.Bottom);
            if (center.X > topleft.X && center.X < topright.X)
                return true;
            if (center.Y > topleft.Y && center.Y < botright.Y)
                return true;
            if (pointInCirkle(topleft, center, radius))
                return true;
            if (pointInCirkle(topright, center, radius))
                return true;
            if (pointInCirkle(botleft, center, radius))
                return true;
            if (pointInCirkle(botright, center, radius))
                return true;
            return false; 
        }
    }
}
