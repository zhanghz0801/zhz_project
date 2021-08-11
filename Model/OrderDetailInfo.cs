using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class OrderDetailInfo
    {
        public int Oid { get; set; }
        public int OrderId { get; set; }
        public int DishId { get; set; }
        public int Count { get; set; }

        public string DishTitle { get; set; }
        public decimal DishPrice { get; set; }
    }
}
