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
    
    public partial class GiftCard
    {
        public long GiftCardId { get; set; }
        public string OrderNumber { get; set; }
        public string GiftCardCode { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string Email { get; set; }
        public double GiftCardValue { get; set; }
        public double CurrentGiftCardValue { get; set; }
        public string Name { get; set; }
        public string PersonalMessage { get; set; }
        public System.DateTime ExpirationDate { get; set; }
    }
}
