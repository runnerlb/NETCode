using EF.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.BLL
{
    public class CompanyBLL : BaseBLL<Company>
    {
        CompanyDAL dal = new CompanyDAL();
    }
}
