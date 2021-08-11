using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class TableInfo
    {
        public int Tid { get; set; }
        public string  TTitle { get; set; }
        public int THallId { get; set; }
        public bool TIsFree { get; set; }
        public bool TIsDelete { get; set; }

        public string HallTitle { get; set; }
        public int IsFreeSearch { get; set; }
    }
}
