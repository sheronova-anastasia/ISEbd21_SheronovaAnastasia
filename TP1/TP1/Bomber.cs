using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1
{
    public class Bomber
    {
        /// <summary>
        /// Левая координата отрисовки самолёта
        /// </summary>
        private float _startPosX;
        /// <summary>
        /// Правая кооридната отрисовки самолёта
        /// </summary>
        private float _startPosY;
        /// <summary>
        /// Ширина окна отрисовки 
        /// </summary>
        private int _pictureWidth;
        /// <summary>
        /// Высота окна отрисовки
        /// </summary>
        private int _pictureHeight;
        /// <summary>
        /// Ширина отрисовки самолёта
        /// </summary>
        private const int planeWidth = 100;
        /// <summary>
        /// Высота отрисовки самолёта
        /// </summary>
        private const int planeHeight = 60;
        /// <summary>
        /// Максимальная скорость
        /// </summary>
        public int MaxSpeed { private set; get; }
        /// <summary>
        /// Вес самолёта
        /// </summary>
        public float Weight { private set; get; }
        /// <summary>
        /// Основной цвет фюзеляжа
        /// </summary>
        public Color MainColor { private set; get; }
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
       bool star, bool bomb, bool rocket)
        {
            MaxSpeed = maxSpeed;
            Weight = weight;
            MainColor = mainColor;
            DopColor = dopColor;
            Star = star;
            Bomb = bomb;
            Rocket = rocket;
        }
        /// <summary>
        /// Установка позиции самолёта
        /// </summary>
        /// <param name="x">Координата X</param>
        /// <param name="y">Координата Y</param>
        /// <param name="width">Ширина картинки</param>
        /// <param name="height">Высота картинки</param>
        public void SetPosition(int x, int y, int width, int height)
        {
            _startPosX = x;
            _startPosY = y;
            _pictureWidth = width;
            _pictureHeight = height;
        }
        /// <summary>
        /// Изменение направления пермещения
        /// </summary>
        /// <param name="direction">Направление</param>
        public void MoveTransport(Direction direction)
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
        /// <summary>
        /// Отрисовка самолёта
        /// </summary>
        /// <param name="g"></param>
        public void DrawPlane(Graphics g)
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
            Brush brPink = new SolidBrush(DopColor);
            g.FillEllipse(brPink, _startPosX + 133, _startPosY + 55, 5, 17);
            g.DrawEllipse(pen, _startPosX + 133, _startPosY + 55, 5, 17);
            g.FillEllipse(brPink, _startPosX + 133, _startPosY + 25, 5, 17);
            g.DrawEllipse(pen, _startPosX + 133, _startPosY + 25, 5, 17);
            g.FillEllipse(brPink, _startPosX + 133, _startPosY + 45, 7, 7);
            g.DrawEllipse(pen, _startPosX + 133, _startPosY + 45, 7, 7);
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
