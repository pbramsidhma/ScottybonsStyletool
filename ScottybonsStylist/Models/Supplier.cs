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
    
    public partial class Supplier
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Supplier()
        {
            this.Articles = new HashSet<Article>();
        }
    
        public int SupplierID { get; set; }
        public string PortalURL { get; set; }
        public string PortalUserName { get; set; }
        public string PortalPassword { get; set; }
        public string RecordNames { get; set; }
        public string PaymentMethods { get; set; }
        public int MarkUp { get; set; }
        public int PaymentTerm { get; set; }
        public bool ReturnRight { get; set; }
        public decimal CostPerOrder { get; set; }
        public decimal CostPerReturn { get; set; }
        public decimal Credit { get; set; }
        public string Other { get; set; }
        public string DiscountType { get; set; }
        public string Notes { get; set; }
        public string ScottybonsCustomerID { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
        public System.DateTime UpdatedOn { get; set; }
        public System.DateTime CreatedOn { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Article> Articles { get; set; }
    }
}