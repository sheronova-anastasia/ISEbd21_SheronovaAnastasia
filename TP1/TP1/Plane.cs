using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1
{
    public class Plane : Vehicle
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
        public override void MoveTransport(Direction direction)
        {
            float step = MaxSpeed * 100 / Weight;
            switch (direction)
            {
                // вправо
                case Direction.Right:
                    if (_startPosX + step < _pictureWidth - planeWidth*2)
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
                    if (_startPosY + step < _pictureHeight - planeHeight*2)
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
    }
}
