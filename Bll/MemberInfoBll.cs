using Dal;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll
{
    public class MemberInfoBll
    {
        MemberInfoDal miDal = new MemberInfoDal();

        public List<MemberInfo> GetList(MemberInfo mi)
        {
            return miDal.GetList(mi);
        }

        public bool Add(MemberInfo mi)
        {
            return miDal.Insert(mi)>0;
        }

        public bool Edit(MemberInfo mi)
        {
            return miDal.Update(mi) > 0;
        }

        public bool Remove(int id)
        {
            return miDal.Delete(id) > 0;
        }
    }
}
