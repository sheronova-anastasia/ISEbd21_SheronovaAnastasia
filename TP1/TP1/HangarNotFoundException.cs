using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1
{
    public class HangarNotFoundException : Exception
    {
        public HangarNotFoundException(int i) : base("Не найден самолёт по месту "
   + i)
        { }
    }
}
