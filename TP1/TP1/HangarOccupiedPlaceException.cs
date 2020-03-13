using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1
{
    public class HangarOccupiedPlaceException : Exception
    {
        public HangarOccupiedPlaceException(int i) : base("На месте " + i + " уже стоит автомобиль")
            { }
    }
}
