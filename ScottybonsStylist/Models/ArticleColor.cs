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
    
    public partial class ArticleColor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ArticleColor()
        {
            this.ArticlePhotos = new HashSet<ArticlePhoto>();
            this.ArticleSKUs = new HashSet<ArticleSKU>();
        }
    
        public int ArticleColorID { get; set; }
        public Nullable<int> ArticleID { get; set; }
        public Nullable<int> ColorSubCategoryID { get; set; }
        public Nullable<int> ColorpalletID { get; set; }
        public Nullable<int> WearMomentID { get; set; }
        public Nullable<int> MoodID { get; set; }
        public Nullable<int> StyleID { get; set; }
        public Nullable<int> ExtravaganceID { get; set; }
        public Nullable<int> SexynessID { get; set; }
        public Nullable<int> GenderID { get; set; }
        public Nullable<int> SeasonID { get; set; }
        public Nullable<int> ArticleColorDescription { get; set; }
        public System.DateTime UpdatedOn { get; set; }
        public System.DateTime CreatedOn { get; set; }
    
        public virtual Article Article { get; set; }
        public virtual ColorPalletMaster ColorPalletMaster { get; set; }
        public virtual ColorSubCategoryMaster ColorSubCategoryMaster { get; set; }
        public virtual ExtravaganceMaster ExtravaganceMaster { get; set; }
        public virtual GenderMaster GenderMaster { get; set; }
        public virtual MoodMaster MoodMaster { get; set; }
        public virtual SeasonMaster SeasonMaster { get; set; }
        public virtual SexynessMaster SexynessMaster { get; set; }
        public virtual StyleMaster StyleMaster { get; set; }
        public virtual WearMomentMaster WearMomentMaster { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ArticlePhoto> ArticlePhotos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ArticleSKU> ArticleSKUs { get; set; }
    }
}
