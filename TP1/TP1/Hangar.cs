using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1
{
    /// <summary>
    /// Параметризованны класс для хранения набора объектов от интерфейса ITransport
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Hangar<T> where T : class, ITransport
    {
        /// <summary>
        /// Массив объектов, которые храним
        /// </summary>
        private Dictionary<int, T> _places;

        /// <summary>
        /// Максимальное количество мест в ангаре
        /// </summary>
        private int _maxCount;
        
        /// <summary>
        /// Ширина окна отрисовки
        /// </summary>
        private int PictureWidth { get; set; }
        /// <summary>
        /// Высота окна отрисовки
        /// </summary>
        private int PictureHeight { get; set; }
        /// <summary>
        /// Размер места в ангаре (ширина)
        /// </summary>
        private const int _placeSizeWidth = 210;
        /// <summary>
        /// Размер места в ангаре (высота)
        /// </summary>
        private const int _placeSizeHeight = 150;
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="sizes">Количество мест в ангаре</param>
        /// <param name="pictureWidth">Рамзер ангара - ширина</param>
        /// <param name="pictureHeight">Рамзер ангара - высота</param>
        public Hangar(int sizes, int pictureWidth, int pictureHeight)
        {
            _maxCount = sizes;
            _places = new Dictionary<int, T>();
            PictureWidth = pictureWidth;
            PictureHeight = pictureHeight;
            }

        /// <summary>
        /// Перегрузка оператора сложения
        /// Логика действия: в ангар добавляется самолёт
        /// </summary>
        /// <param name="h">Ангар</param>
        /// <param name="plane">Добавляемый самолёт</param>
        /// <returns></returns>
        public static int operator +(Hangar<T> h, T plane)
        {
            if (h._places.Count == h._maxCount)
            {
                return -1;
            }
            for (int i = 0; i < h._maxCount; i++)
            {
                if (h.CheckFreePlace(i))
                {
                    h._places[i] = plane;
                    h._places[i].SetPosition(3 + i / 3 * _placeSizeWidth + 3,
                     i % 3 * _placeSizeHeight + 15, h.PictureWidth,
                    h.PictureHeight);
                    return i;
                }
            }
            return -1;
        }
        /// <summary>
        /// Перегрузка оператора вычитания
        /// Логика действия: из ангара забираем самолёт
        /// </summary>
        /// <param name="h">Ангар</param>
        /// <param name="index">Индекс места, с которого пытаемся извлечь объект</param>
        /// <returns></returns>
        public static T operator -(Hangar<T> h, int index)
        {
            if (!h.CheckFreePlace(index))
            {
                T plane = h._places[index];
                h._places.Remove(index);
                return plane;
            }
            return null;
        }
        /// <summary>
        /// Метод проверки заполнености места в ангаре(ячейки массива)
        /// </summary>
        /// <param name="index">Номер места в ангаре(порядковый номер в массиве)</param>
        /// <returns></returns>
        private bool CheckFreePlace(int index)
        {
            return !_places.ContainsKey(index);
        }
        /// <summary>
        /// Метод отрисовки ангара
        /// </summary>
        /// <param name="g"></param>
        public void Draw(Graphics g)
        {
            DrawMarking(g);
            var keys = _places.Keys.ToList();
            for (int i = 0; i < keys.Count; i++)
            {
                _places[keys[i]].DrawPlane(g);
            }
        }
        /// <summary>
        /// Метод отрисовки разметки мест в ангаре
        /// </summary>
        /// <param name="g"></param>
        private void DrawMarking(Graphics g)
        {
            Pen pen = new Pen(Color.Black, 3);
            //границы ангара
            g.DrawRectangle(pen, 0, 0, (_maxCount / 3) * _placeSizeWidth, 480);
            for (int i = 0; i < _maxCount / 3; i++)
            {//отрисовываем, по 3 места на линии
                for (int j = 0; j < 3; ++j)
                {//линия рамзетки места
                    g.DrawLine(pen, i * _placeSizeWidth, j * _placeSizeHeight,
                    i * _placeSizeWidth + 110, j * _placeSizeHeight);
                }
                g.DrawLine(pen, i * _placeSizeWidth, 0, i * _placeSizeWidth, 480);
            }
        }
    }
}
