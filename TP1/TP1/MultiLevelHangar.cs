﻿using System;
using System.Collections.Generic;
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
        
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="countStages">Количество уровней ангара</param>
        /// <param name="pictureWidth"></param>
        /// <param name="pictureHeight"></param>
        public MultiLevelHangar(int countStages, int pictureWidth, int pictureHeight)
        {
            hangarStages = new List<Hangar<ITransport>>();
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
    }
}
