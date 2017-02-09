using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScottybonsStylist.Models
{
    public partial class GIftCardsOrder : IDisposable
    {
        public void Dispose()
        {            
        }
    }
    public class GiftCardModel
    {
        public long GiftCardId { get; set; }
        public string Code { get; set; }
        public string OrderNumber{ get; set; }
        public string OrderDate { get; set; }
        public string Email { get; set; }
        public string ForWho{ get; set; }
        public string Value { get; set; }
        public string CurrentValue { get; set; }
        public string Status { get; set; }
    }

    public class GiftCardDetails
    {
        public string GiftCardCode { get; set; }
        public string OrderNumber { get; set; }
        public string CreatedDate { get; set; }
        public string Email { get; set; }
        public string GiftCardValue { get; set; }
        public string CurrentGiftCardValue { get; set; }
        public string PayersFirstName { get; set; }
        public string PayersLastName { get; set; }
        public string Name { get; set; }
        public string PersonalMessage { get; set; }
        public string ShipmentFirstName { get; set; }
        public string ShipmentLastName { get; set; }
        public string ShipmentStreetName { get; set; }
        public string ShipmentHouseNumber { get; set; }
        public string ShipmentPostalCode { get; set; }
        public string ShipmentCity { get; set; }
        public int ShipmentCountry { get; set; }
        public string ShipmentCountryName { get; set; }
        public DateTime ExpirationDateCtrl { get; set; }
        public string ExpirationDate { get; set; }

    }

    public class CountryModel
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }
    }
    public class GiftCardStatusModal
    {
        public bool Status { get; set; }
        public string Message { get; set; }
    }

    public class GiftCardOrderNumberStatusModal
    {
        public bool Status { get; set; }
        public string OrderNumber{ get; set; }
    }
}