//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace EFORM
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserInfo
    {
        public UserInfo()
        {
            this.LogInfo = new HashSet<LogInfo>();
        }
    
        public int Id { get; set; }
        public string UserName { get; set; }
        public int Sex { get; set; }
        public string Email { get; set; }
        public System.DateTime CreateDate { get; set; }
        public int IsDeleted { get; set; }
    
        public virtual ICollection<LogInfo> LogInfo { get; set; }
    }
}
