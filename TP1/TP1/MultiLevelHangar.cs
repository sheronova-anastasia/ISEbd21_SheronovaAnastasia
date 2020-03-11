using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1
{
    /// <summary>
    /// Класс-хранилище ангаров
    /// </summary>
    class MultiLevelHangar
    {
        /// </summary>
        List<Hangar<ITransport>> hangarStages;
        /// <summary>
        /// Сколько мест на каждом уровне
        /// </summary>
        private const int countPlaces = 12;
        /// </summary>
        /// Ширина окна отрисовки
        /// </summary>
        private int pictureWidth;
        /// <summary>
        /// Высота окна отрисовки
        /// </summary>      
        private int pictureHeight;
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="countStages">Количество уровней ангара</param>
        /// <param name="pictureWidth"></param>
        /// <param name="pictureHeight"></param>
        public MultiLevelHangar(int countStages, int pictureWidth, int pictureHeight)
        {
            hangarStages = new List<Hangar<ITransport>>();
            this.pictureWidth = pictureWidth;
            this.pictureHeight = pictureHeight;
            for (int i = 0; i < countStages; ++i)
            {
                hangarStages.Add(new Hangar<ITransport>(countPlaces, pictureWidth,
               pictureHeight));
            }
        }
        /// <summary>
        /// Индексатор
        /// </summary>
        /// <param name="ind"></param>
        /// <returns></returns>
        public Hangar<ITransport> this[int ind]
        {
            get
            {
                if (ind > -1 && ind < hangarStages.Count)
                {
                    return hangarStages[ind];
                }
                return null;
            }
        }

        /// <summary>
        /// Сохранение информации по автомобилям на парковках в файл
        /// </summary>
        /// <param name="filename">Путь и имя файла</param>
        /// <returns></returns>
        public bool SaveData(string filename)
        {
            {
                if (File.Exists(filename))
                {
                    File.Delete(filename);
                }
                using (StreamWriter sw = new StreamWriter(filename))
                {
                    sw.WriteLine("CountLevels:" + hangarStages.Count);
                    foreach (var level in hangarStages)
                    {
                        sw.WriteLine("Level");
                        for (int i = 0; i < countPlaces; i++)
                        {
                            var plane = level[i];
                            if (plane != null)
                            {
                                if (plane.GetType().Name == "Plane")
                                {
                                    sw.WriteLine(i + ":Plane:" + plane);
                                }
                                if (plane.GetType().Name == "Bomber")
                                {
                                    sw.WriteLine(i + ":Bomber:" + plane);
                                }
                            }
                        }
                    }
                }
                return true;
            }
        }
        /// <summary>
        /// Загрузка нформации по автомобилям на парковках из файла
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public bool LoadData(string filename)
        {
            if (!File.Exists(filename))
            {
                return false;
            }
            int counter = -1;
            ITransport plane = null;
            using (StreamReader sr = new StreamReader(filename))
            {
                string line = sr.ReadLine();
                int count;
                bool isValid = line.Contains("CountLevels");
                if (isValid)
                {
                    count = Convert.ToInt32(line.Split(':')[1]);
                    if (hangarStages != null)
                    {
                        hangarStages.Clear();
                    }
                    hangarStages = new List<Hangar<ITransport>>(count);
                }
                else
                {
                    return false;
                }
                while ((line = sr.ReadLine()) != null)
                {
                    if (line == "Level")
                    {
                        counter++;
                        hangarStages.Add(new Hangar<ITransport>(countPlaces, pictureWidth, pictureHeight));
                        continue;
                    }
                    if (string.IsNullOrEmpty(line))
                    {
                        continue;
                    }
                    string[] splitLine = line.Split(':');
                    if (splitLine.Length > 2)
                    {
                        if (splitLine[1] == "Plane")
                        {
                            plane = new Plane(splitLine[2]);
                        }
                        else
                        {
                            plane = new Bomber(splitLine[2]);
                        }
                        hangarStages[counter][Convert.ToInt32(splitLine[0])] = plane;
                    }
                }
                return true;
            }
        }
    }
}

