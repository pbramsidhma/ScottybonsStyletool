//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ScottybonsStylist.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ErrorLog
    {
        public int LogID { get; set; }
        public System.DateTime ErrorDate { get; set; }
        public string Source { get; set; }
        public string ErrorMessage { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public System.DateTime UpdatedOn { get; set; }
    }
}
