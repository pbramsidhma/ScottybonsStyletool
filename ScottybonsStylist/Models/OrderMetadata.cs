using System;
using System.ComponentModel.DataAnnotations;

namespace ScottybonsStylist.Models
{
    [MetadataType(typeof(OrderMetadata))]
    public partial class Order
    {
    }

    public partial class OrderMetadata
    {
        [Required(ErrorMessage = "Please enter : OrderID")]
        [Display(Name = "OrderID")]
        public int OrderID { get; set; }

        [Display(Name = "CustomerID")]
        public int CustomerID { get; set; }

        [Display(Name = "Order Occasion")]
        public int OrderOccasion { get; set; }

        [Display(Name = "Occasion Comments")]
        public string OccasionComments { get; set; }

        [Display(Name = "Wish List")]
        public string WishList { get; set; }

        [Display(Name = "Orderd Date")]
        public DateTime DateOrder { get; set; }

        [Display(Name = "RequiredDate")]
        public DateTime RequiredDate { get; set; }

        [Display(Name = "Payment Method ID")]
        public int PaymentMethodID { get; set; }

        [Display(Name = "Agreed General Conditions")]
        public bool AgreeGeneralConditions { get; set; }

        [Display(Name = "Street")]
        public string OrderStreet { get; set; }

        [Display(Name = "House Number")]
        public string OrderHouseNumber { get; set; }

        [Display(Name = "PostalCode")]
        public string OrderPostalCode { get; set; }

        [Display(Name = "Town")]
        public string OrderTown { get; set; }

        [Display(Name = "OrderCountryID")]
        public int OrderCountryID { get; set; }

        [Display(Name = "OrderComments")]
        public string OrderComments { get; set; }

        [Display(Name = "Deliver Neighbours")]
        public bool DeliverNeighbours { get; set; }

        [Display(Name = "OrderStatusID")]
        public int OrderStatusID { get; set; }

        [Display(Name = "PaymentDate")]
        public DateTime PaymentDate { get; set; }

        [Display(Name = "Is Stylist Contact You")]
        public bool IsStylistContactYou { get; set; }

        [Display(Name = "Contact Number")]
        public string ContactNumber { get; set; }

        [Display(Name = "Order Comment Stylist")]
        public string OrderCommentStylist { get; set; }

        [Display(Name = "OrderStyleAdvice")]
        public string OrderStyleAdvice { get; set; }

        [Display(Name = "NumberOfArticles")]
        public int NumberOfArticles { get; set; }

        [Display(Name = "ReviewOverall")]
        public string ReviewOverall { get; set; }

        [Display(Name = "UpdatedOn")]
        public DateTime UpdatedOn { get; set; }

        [Display(Name = "CreatedOn")]
        public DateTime CreatedOn { get; set; }

        [Display(Name = "PeriodicalScottyBoxID")]
        public int PeriodicalScottyBoxID { get; set; }

        [Display(Name = "NextPerodicalScottyBoxDate")]
        public DateTime NextPerodicalScottyBoxDate { get; set; }

        [Display(Name = "ParentOrderID")]
        public int ParentOrderID { get; set; }

        [Display(Name = "PermissionToCollectFutureInvoices")]
        public bool PermissionToCollectFutureInvoices { get; set; }

        [Display(Name = "OrderComanyName")]
        public string OrderComanyName { get; set; }

        [Display(Name = "CountryMaster")]
        public CountryMaster CountryMaster { get; set; }

        [Display(Name = "Customer")]
        public Customer Customer { get; set; }

        [Display(Name = "OccasionForScottyBoxMaster")]
        public OccasionForScottyBoxMaster OccasionForScottyBoxMaster { get; set; }

        [Display(Name = "OrderStatusMaster")]
        public OrderStatusMaster OrderStatusMaster { get; set; }

        [Display(Name = "PaymentTypeMaster")]
        public PaymentTypeMaster PaymentTypeMaster { get; set; }

    }
}
