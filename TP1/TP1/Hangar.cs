using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace TP1
{
    /// <summary>
    /// Параметризованны класс для хранения набора объектов от интерфейса ITransport
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Hangar<T> : IEnumerator<T>, IEnumerable<T>, IComparable<Hangar<T>>
        where T : class, ITransport
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
        /// Текущий элемент для вывода через IEnumerator (будет обращаться по своему индексу к ключу словаря, по которму будет возвращаться запись)
        /// </summary>
 private int _currentIndex;

        /// <summary>
        /// Получить порядковое место на парковке
        /// </summary>
        public int GetKey
        {
            get
            {
                return _places.Keys.ToList()[_currentIndex];
            }
        }

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
            _currentIndex = -1;
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
                throw new HangarOverflowException();
            }
            if (h._places.ContainsValue(plane))
            {
                throw new HangarAlreadyHaveException();
            }
            for (int i = 0; i < h._maxCount; i++)
            {
                if (h.CheckFreePlace(i))
                {
                    h._places.Add(i, plane);
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
            throw new HangarNotFoundException(index);
        }

        /// <summary>
        /// Индексатор
        /// </summary>
        /// <param name="ind"></param>
        /// <returns></returns>
        public T this[int ind]
        {
            get
            {
                if (_places.ContainsKey(ind))
                {
                    return _places[ind];
                }
                throw new HangarNotFoundException(ind);
            }
            set
            {
                if (CheckFreePlace(ind))
                {
                    _places.Add(ind, value);
                    _places[ind].SetPosition(3 + ind / 3 * _placeSizeWidth + 3, ind % 3 
                    * _placeSizeHeight + 15, PictureWidth, PictureHeight);
                }
                else
                {
                    throw new HangarOccupiedPlaceException(ind);
                }
            }
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

        public T GetPlaneByKey(int key)
        {
            return _places.ContainsKey(key) ? _places[key] : null;
        }
        /// <summary>
        /// Метод отрисовки ангара
        /// </summary>
        /// <param name="g"></param>
        public void Draw(Graphics g)
        {
            DrawMarking(g);
            foreach (var plane in _places)
            {
                plane.Value.DrawPlane(g);
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

        /// <summary>
        /// Метод интерфейса IEnumerator для получения текущего элемента
        /// </summary>
        public T Current
        {
            get
            {
                return _places[_places.Keys.ToList()[_currentIndex]];
            }
        }

        /// <summary>
        /// Метод интерфейса IEnumerator для получения текущего элемента
        /// </summary>
        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        /// <summary>
        /// Метод интерфейса IEnumerator, вызываемый при удалении объекта
        /// </summary>
        public void Dispose()
        {
            _places.Clear();
        }
        /// <summary>
        /// Метод интерфейса IEnumerator для перехода к следующему элементу или началу коллекции
 /// </summary>
 /// <returns></returns>
 public bool MoveNext()
        {
            if (_currentIndex + 1 >= _places.Count)
            {
                Reset();
                return false;
            }
            _currentIndex++;
            return true;
        }
        /// <summary>
        /// Метод интерфейса IEnumerator для сброса и возврата к началу коллекции
        /// </summary>
        public void Reset()
        {
            _currentIndex = -1;
        }
        /// <summary>
        /// Метод интерфейса IEnumerable
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            return this;
        }
        /// <summary>
        /// Метод интерфейса IEnumerable
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        /// <summary>
        /// Метод интерфейса IComparable
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(Hangar<T> other)
        {
            if (_places.Count > other._places.Count)
            {
                return -1;
            }
            else if (_places.Count < other._places.Count)
            {
                return 1;
            }
            else if (_places.Count > 0)
            {
                var thisKeys = _places.Keys.ToList();
                var otherKeys = other._places.Keys.ToList();
                for (int i = 0; i < _places.Count; ++i)
                {
                    if (_places[thisKeys[i]] is Plane && other._places[thisKeys[i]] is
                   Bomber)
                    {
                        return 1;
                    }
                    if (_places[thisKeys[i]] is Bomber && other._places[thisKeys[i]]
                    is Plane)
                    {
                        return -1;
                    }
                    if (_places[thisKeys[i]] is Plane && other._places[thisKeys[i]] is
                    Plane)
                    {
                        return (_places[thisKeys[i]] is
                       Plane).CompareTo(other._places[thisKeys[i]] is Plane);
                    }
                    if (_places[thisKeys[i]] is Bomber && other._places[thisKeys[i]]
                    is Bomber)
                    {
                        return (_places[thisKeys[i]] is
                       Bomber).CompareTo(other._places[thisKeys[i]] is Bomber);
                    }
                }
            }
            return 0;
        }
    }
}
