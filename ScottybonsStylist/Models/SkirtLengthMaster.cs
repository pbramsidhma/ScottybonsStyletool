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
    
    public partial class SkirtLengthMaster
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SkirtLengthMaster()
        {
            this.SubCategoryProperties = new HashSet<SubCategoryProperty>();
        }
    
        public int SkirtLengthID { get; set; }
        public string SkirtLengthName { get; set; }
        public Nullable<bool> Active { get; set; }
        public int DisplayOrder { get; set; }
        public System.DateTime UpdatedOn { get; set; }
        public System.DateTime CreatedOn { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SubCategoryProperty> SubCategoryProperties { get; set; }
    }
}
