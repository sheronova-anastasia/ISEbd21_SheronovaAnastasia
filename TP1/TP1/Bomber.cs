using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1
{
    public class Bomber : Plane
    {
        /// <summary>
        /// Дополнительный цвет
        /// </summary>
        public Color DopColor { private set; get; }
        /// <summary>
        /// Признак наличия звезды
        /// </summary>
        public bool Star { private set; get; }
        /// <summary>
        /// Признак наличия бомбы
        /// </summary>
        public bool Bomb { private set; get; }
        /// <summary>
        /// Признак наличия ракеты
        /// </summary>
        public bool Rocket { private set; get; }
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="maxSpeed">Максимальная скорость</param>
        /// <param name="weight">Вес самолёта</param>
        /// <param name="mainColor">Основной цвет фюзеляжа</param>
        /// <param name="dopColor">Дополнительный цвет</param>
        /// <param name="star">Признак наличия звезды</param>
        /// <param name="bomb">Признак наличия бомбы</param>
        /// <param name="rocket">Признак наличия ракеты</param>
        public Bomber(int maxSpeed, float weight, Color mainColor, Color dopColor,
       bool star, bool bomb, bool rocket) :
            base(maxSpeed, weight, mainColor)
        {
            DopColor = dopColor;
            Star = star;
            Bomb = bomb;
            Rocket = rocket;
        }
        /// <summary>
        /// Отрисовка самолёта
        /// </summary>
        /// <param name="g"></param>
        public override void DrawPlane(Graphics g)
        {
            Pen pen = new Pen(Color.Black);
            //рисуем звезду
            if (Star)
            {
                Point pointS1 = new Point((int)_startPosX + 85, (int)_startPosY + 60);
                Point pointS2 = new Point((int)_startPosX + 90, (int)_startPosY + 65);
                Point pointS3 = new Point((int)_startPosX + 95, (int)_startPosY + 60);
                Point pointS4 = new Point((int)_startPosX + 95, (int)_startPosY + 90);
                Point pointS5 = new Point((int)_startPosX + 90, (int)_startPosY + 85);
                Point pointS6 = new Point((int)_startPosX + 85, (int)_startPosY + 90);
                Point[] boardS1 = { pointS1, pointS2, pointS3, pointS4, pointS5, pointS6 };
                g.DrawPolygon(pen, boardS1);
                Point pointS7 = new Point((int)_startPosX + 85, (int)_startPosY + 10);
                Point pointS8 = new Point((int)_startPosX + 90, (int)_startPosY + 15);
                Point pointS9 = new Point((int)_startPosX + 95, (int)_startPosY + 10);
                Point pointS10 = new Point((int)_startPosX + 95, (int)_startPosY + 40);
                Point pointS11 = new Point((int)_startPosX + 90, (int)_startPosY + 35);
                Point pointS12 = new Point((int)_startPosX + 85, (int)_startPosY + 40);
                Point[] boardS2 = { pointS7, pointS8, pointS9, pointS10, pointS11, pointS12 };
                g.DrawPolygon(pen, boardS2);
            }
            //рисуем бомбы
            if (Bomb)
            {
                g.DrawEllipse(pen, _startPosX + 90, _startPosY, 30, 10);
                Brush bomb = new SolidBrush(DopColor);
                g.FillEllipse(bomb, _startPosX + 90, _startPosY, 30, 10);
                g.DrawEllipse(pen, _startPosX + 90, _startPosY + 90, 30, 10);
                g.FillEllipse(bomb, _startPosX + 90, _startPosY + 90, 30, 10);
            }
            base.DrawPlane(g);
            // рисуем ракеты
            if (Rocket)
            {
                g.DrawRectangle(pen, _startPosX + 80, _startPosY + 10, 40, 10);
                Brush rocket = new SolidBrush(DopColor);
                g.FillRectangle(rocket, _startPosX + 80, _startPosY + 10, 40, 10);
                Point pointR1 = new Point((int)_startPosX + 120, (int)_startPosY + 10);
                Point pointR2 = new Point((int)_startPosX + 130, (int)_startPosY + 15);
                Point pointR3 = new Point((int)_startPosX + 120, (int)_startPosY + 20);
                Point[] boardR1 = { pointR1, pointR2, pointR3 };
                g.DrawPolygon(pen, boardR1);
                g.FillPolygon(rocket, boardR1);
                g.DrawRectangle(pen, _startPosX + 80, _startPosY + 80, 40, 10);
                g.FillRectangle(rocket, _startPosX + 80, _startPosY + 80, 40, 10);
                Point pointR4 = new Point((int)_startPosX + 120, (int)_startPosY + 80);
                Point pointR5 = new Point((int)_startPosX + 130, (int)_startPosY + 85);
                Point pointR6 = new Point((int)_startPosX + 120, (int)_startPosY + 90);
                Point[] boardR2 = { pointR4, pointR5, pointR6 };
                g.DrawPolygon(pen, boardR2);
                g.FillPolygon(rocket, boardR2);
            }
        }
    }
}
