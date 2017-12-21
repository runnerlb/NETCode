using EF.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.BLL
{
    public class DonationDetailBLL : BaseBLL<DonationDetail>
    {
        DonationDetailDAL dal = new DonationDetailDAL();
    }
}
