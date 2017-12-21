using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.DAL
{
    using System.Data.Entity;

    public class BaseDBContext : DbContext
    {
        public BaseDBContext() : base("name=DonationEntities") { }
    }


}
