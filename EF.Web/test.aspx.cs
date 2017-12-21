using EF.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EF.Web
{
    public partial class Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CompanyBLL bll = new CompanyBLL();

                //添加
                Company modelAdd = new Company()
                {
                    CName = "马化腾"
                };
                bll.Add(modelAdd);

                ////修改数据
                //var modelEdit = bll.Query(m => m.cID == 8).FirstOrDefault();
                //modelEdit.CName = "马云云";

                Company modelAdd2 = new Company()
                {
                    CName = "马化腾 码云",
                    cID = 8
                };
                bll.Edit(modelAdd2, new string[] { "CName" });

                Company modelDel1 = new Company() { cID = 9 };
                bll.Delete(modelDel1,false);

                var modelDel2 = bll.Query(m => m.cID == 10).FirstOrDefault();
                bll.Delete(modelDel2,true);

                bll.SaveChanges();
                var list = bll.Query(m => true);

                DataList1.DataSource = list;
                DataList1.DataBind();
            }
        }
    }
}