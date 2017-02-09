using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScottybonsStylist.Models
{
    public class GiftCardRedemptionStatusModal
    {
        public bool Status { get; set; }
        public string Message { get; set; }
    }

    public class GiftCardDetailsModal
    {
        public bool Expired { get; set; }
        public bool Exists { get; set; }
        public double OrginalAmount { get; set; }
        public double CurrentBalance { get; set; }
        public DateTime ExpireDate { get; set; }
    }

    public class GiftCardRedemptionModal
    {
        public int GiftCardRedemptionId { get; set; }
        public string OrderNumber { get; set; }
        public string GiftCardCode { get; set; }
        public double AmounOfRedemption { get; set; }
        public int CustomerId { get; set; }
        public DateTime RedemptionDateTime { get; set; }
        public bool InValid { get; set; }
        public bool ValidOrderNumber { get; set; }
        public bool ValidGiftCardCode { get; set; }
        public double OrginalBalance { get; set; }
        public double CurrentBalance { get; set; }
        public string Status { get; set; }
    }
    
    public class OrderNumberGiftCardModal
    {
        public bool Valid { get; set; }
        public bool GiftCardExists { get; set; }

    }
}