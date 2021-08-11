using Dal;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll
{
    public class DishTypeInfoBll
    {
        DishTypeInfoDal dtiDal = new DishTypeInfoDal();

        public List<DishTypeInfo> GetList()
        {
            return dtiDal.GetList();
        }

        public bool Add(DishTypeInfo dti)
        {
            return dtiDal.Insert(dti) > 0;
        }

        public bool Edit(DishTypeInfo dti)
        {
            return dtiDal.Update(dti) > 0;
        }

        public bool Remove(int id)
        {
            return dtiDal.Delete(id) > 0;
        }
    }
}
