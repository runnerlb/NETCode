//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

public partial class DonationDetail
{
    public int dId { get; set; }
    public string dUserName { get; set; }
    public int dcID { get; set; }
    public decimal dAmount { get; set; }
    public System.DateTime dDate { get; set; }
    public int disdelete { get; set; }
    public System.DateTime dCreateTime { get; set; }

    public virtual Company Company { get; set; }
}
