using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1
{
    public abstract class Vehicle : ITransport
    {
        /// <summary>
        /// Левая координата отрисовки самолёта
        /// </summary>
        protected float _startPosX;
        /// <summary>
        /// Правая кооридната отрисовки самолёта
        /// </summary>
        protected float _startPosY;
        /// <summary>
        /// Ширина окна отрисовки
        /// </summary>
        protected int _pictureWidth;
        //Высота окна отрисовки
        protected int _pictureHeight;
        /// <summary>
        /// Максимальная скорость
        /// </summary>
        public int MaxSpeed { protected set; get; }
        /// <summary>
        /// Вес самолёта
        /// </summary>
        public float Weight { protected set; get; }
        /// <summary>
        /// Основной цвет фюзеляжа
        /// </summary>
        public Color MainColor { protected set; get; }
        public void SetPosition(int x, int y, int width, int height)
        {
            _startPosX = x;
            _startPosY = y;
            _pictureWidth = width;
            _pictureHeight = height;
        }
        public abstract void DrawPlane(Graphics g);
        public abstract void MoveTransport(Direction direction);
    }
}
