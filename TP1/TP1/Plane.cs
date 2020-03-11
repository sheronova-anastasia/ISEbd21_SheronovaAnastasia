using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1
{
    public class Plane : Vehicle, IComparable<Plane>, IEquatable<Plane>
    {
        /// <summary>
        /// Ширина отрисовки самолёта
        /// </summary>
        protected const int planeWidth = 100;
        /// <summary>
        /// Ширина отрисовки самолёта
        /// </summary>
        protected const int planeHeight = 60;
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="maxSpeed">Максимальная скорость</param>
        /// <param name="weight">Вес самолёта</param>
        /// <param name="mainColor">Основной цвет фюзеляжа</param>
        public Plane(int maxSpeed, float weight, Color mainColor)
        {
            MaxSpeed = maxSpeed;
            Weight = weight;
            MainColor = mainColor;
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="info">Информация по объекту</param>
        public Plane(string info)
        {
            string[] strs = info.Split(';');
            if (strs.Length == 3)
            {
                MaxSpeed = Convert.ToInt32(strs[0]);
                Weight = Convert.ToInt32(strs[1]);
                MainColor = Color.FromName(strs[2]);
            }
        }

        public override void MoveTransport(Direction direction)
        {
            float step = MaxSpeed * 100 / Weight;
            switch (direction)
            {
                // вправо
                case Direction.Right:
                    if (_startPosX + step < _pictureWidth - planeWidth)
                    {
                        _startPosX += step;
                    }
                    break;
                //влево
                case Direction.Left:
                    if (_startPosX - step > 0)
                    {
                        _startPosX -= step;
                    }
                    break;
                //вверх
                case Direction.Up:
                    if (_startPosY - step > 0)
                    {
                        _startPosY -= step;
                    }
                    break;
                //вниз
                case Direction.Down:
                    if (_startPosY + step < _pictureHeight - planeHeight)
                    {
                        _startPosY += step;
                    }
                    break;
            }
        }
        public override void DrawPlane(Graphics g)
        {
            Pen pen = new Pen(Color.Black);
            // теперь отрисуем фюзеляж самолета
            //границы самолета
            g.DrawRectangle(pen, _startPosX, _startPosY + 40, 130, 20);
            g.DrawRectangle(pen, _startPosX + 75, _startPosY - 10, 25, 120);
            Point pointT1 = new Point((int)_startPosX + 5, (int)_startPosY + 20);
            Point pointT2 = new Point((int)_startPosX + 25, (int)_startPosY + 50);
            Point pointT3 = new Point((int)_startPosX + 5, (int)_startPosY + 80);
            Point[] boardT = { pointT1, pointT2, pointT3 };
            g.DrawPolygon(pen, boardT);
            Brush brRed = new SolidBrush(MainColor);
            g.FillRectangle(brRed, _startPosX, _startPosY + 40, 130, 20);
            Brush brGreen = new SolidBrush(MainColor);
            g.FillRectangle(brGreen, _startPosX + 75, _startPosY - 10, 25, 120);
            g.FillPolygon(brGreen, boardT);
            Brush brBrown = new SolidBrush(MainColor);
            g.FillEllipse(brBrown, _startPosX + 130, _startPosY + 35, 10, 30);
            g.DrawEllipse(pen, _startPosX + 130, _startPosY + 35, 10, 30);
            g.FillEllipse(brBrown, _startPosX + 95, _startPosY, 10, 30);
            g.DrawEllipse(pen, _startPosX + 95, _startPosY, 10, 30);
            g.FillEllipse(brBrown, _startPosX + 95, _startPosY + 70, 10, 30);
            g.DrawEllipse(pen, _startPosX + 95, _startPosY + 70, 10, 30);
            Brush brBlue = new SolidBrush(MainColor);
            g.FillEllipse(brBlue, _startPosX + 70, _startPosY + 42, 30, 15);
            g.DrawEllipse(pen, _startPosX + 70, _startPosY + 42, 30, 15);
            Brush brPink = new SolidBrush(Color.Brown);
            g.FillEllipse(brPink, _startPosX + 133, _startPosY + 55, 5, 17);
            g.DrawEllipse(pen, _startPosX + 133, _startPosY + 55, 5, 17);
            g.FillEllipse(brPink, _startPosX + 133, _startPosY + 25, 5, 17);
            g.DrawEllipse(pen, _startPosX + 133, _startPosY + 25, 5, 17);
            g.FillEllipse(brPink, _startPosX + 133, _startPosY + 45, 7, 7);
            g.DrawEllipse(pen, _startPosX + 133, _startPosY + 45, 7, 7);
        }

        public override string ToString()
        {
            return MaxSpeed + ";" + Weight + ";" + MainColor.Name;
        }

        /// <summary>
        /// Метод интерфейса IComparable для класса Plane
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(Plane other)
        {
            if (other == null)
            {
                return 1;
            }
            if (MaxSpeed != other.MaxSpeed)
            {
                return MaxSpeed.CompareTo(other.MaxSpeed);
            }
            if (Weight != other.Weight)
            {
                return Weight.CompareTo(other.Weight);
            }
            if (MainColor != other.MainColor)
            {
                MainColor.Name.CompareTo(other.MainColor.Name);
            }
            return 0;
        }

        /// <summary>
        /// Метод интерфейса IEquatable для класса Plane
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Plane other)
        {
            if (other == null)
            {
                return false;
            }
            if (GetType().Name != other.GetType().Name)
            {
                return false;
            }
            if (MaxSpeed != other.MaxSpeed)
            {
                return false;
            }
            if (Weight != other.Weight)
            {
                return false;
            }
            if (MainColor != other.MainColor)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Перегрузка метода от object
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(Object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (!(obj is Plane planeObj))
            {
                return false;
            }
            else
            {
                return Equals(planeObj);
            }
        }

        /// <summary>
        /// Перегрузка метода от object
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
