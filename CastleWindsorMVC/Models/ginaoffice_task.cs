//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace CastleWindsorMVC.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ginaoffice_task
    {
        public int guid { get; set; }
        public string topic { get; set; }
        public string assigner { get; set; }
        public string accepter { get; set; }
        public string state { get; set; }
        public Nullable<System.DateTime> due_time { get; set; }
        public Nullable<System.DateTime> createtime { get; set; }
        public Nullable<System.DateTime> lastupdate { get; set; }
    }
}
