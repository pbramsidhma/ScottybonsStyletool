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
    
    public partial class StyleMaster
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StyleMaster()
        {
            this.ArticleColors = new HashSet<ArticleColor>();
        }
    
        public int StyleID { get; set; }
        public string StyleName { get; set; }
        public Nullable<bool> Active { get; set; }
        public int DisplayOrder { get; set; }
        public System.DateTime UpdatedOn { get; set; }
        public System.DateTime CreatedOn { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ArticleColor> ArticleColors { get; set; }
    }
}
