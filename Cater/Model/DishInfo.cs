using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class DishInfo
    {
        public int Did { get; set; }
        public string DTitle { get; set; }
        public int DTypeId { get; set; }
        public decimal DPrice { get; set; }
        public string DChar { get; set; }
        public string DIsDelete { get; set; }
         
        public string TypeTitle { get; set; }
    }
}
