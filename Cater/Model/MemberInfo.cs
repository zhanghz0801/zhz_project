using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class MemberInfo
    {
        public int Mid { get; set; }
        public string MName { get; set; }
        public string MPhone { get; set; }
        public decimal MMoney { get; set; }
        public int MTypeId { get; set; }
        public bool MIsDelete { get; set; }
        //这个属性不是对应这表中的某个列，而是用于做连接查询存储结果
        public string TypeTitle { get; set; }
        public decimal TypeDiscount { get; set; }
    }
}
