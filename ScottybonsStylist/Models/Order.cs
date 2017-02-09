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
    
    public partial class Order
    {
        public int OrderID { get; set; }
        public Nullable<int> CustomerID { get; set; }
        public Nullable<int> OrderOccasion { get; set; }
        public string OccasionComments { get; set; }
        public string WishList { get; set; }
        public System.DateTime DateOrder { get; set; }
        public System.DateTime RequiredDate { get; set; }
        public int PaymentMethodID { get; set; }
        public Nullable<bool> AgreeGeneralConditions { get; set; }
        public string OrderStreet { get; set; }
        public string OrderHouseNumber { get; set; }
        public string OrderPostalCode { get; set; }
        public string OrderTown { get; set; }
        public Nullable<int> OrderCountryID { get; set; }
        public string OrderComments { get; set; }
        public Nullable<bool> DeliverNeighbours { get; set; }
        public Nullable<int> OrderStatusID { get; set; }
        public System.DateTime PaymentDate { get; set; }
        public Nullable<bool> IsStylistContactYou { get; set; }
        public string ContactNumber { get; set; }
        public string OrderCommentStylist { get; set; }
        public string OrderStyleAdvice { get; set; }
        public int NumberOfArticles { get; set; }
        public string ReviewOverall { get; set; }
        public System.DateTime UpdatedOn { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public int PeriodicalScottyBoxID { get; set; }
        public Nullable<System.DateTime> NextPerodicalScottyBoxDate { get; set; }
        public Nullable<int> ParentOrderID { get; set; }
        public Nullable<bool> PermissionToCollectFutureInvoices { get; set; }
        public string OrderComanyName { get; set; }
        public Nullable<int> PromoCodeOrderId { get; set; }
        public string PromoCode { get; set; }
    
        public virtual CountryMaster CountryMaster { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual OccasionForScottyBoxMaster OccasionForScottyBoxMaster { get; set; }
        public virtual OrderStatusMaster OrderStatusMaster { get; set; }
        public virtual PaymentTypeMaster PaymentTypeMaster { get; set; }
    }
}
