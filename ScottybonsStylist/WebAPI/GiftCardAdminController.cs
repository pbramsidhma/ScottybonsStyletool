using System;
using System.Collections.Generic;
using System.Web.Http;
using ScottybonsStylist.Models;
using ScottybonsStylist.Models.Services;

namespace ScottybonsStylist
{

    /// <summary>
    /// 
    /// </summary>
    public class GiftCardAdminController : ApiController
    {
        GiftCardServices giftCardServices;
        
        public GiftCardAdminController()
        {
            giftCardServices = new GiftCardServices();            
        }


        // GET api/<controller>
        public List<GiftCardModel> Get()
        {
            try
            {
                return giftCardServices.GetGiftCards();
            }
            catch (Exception)
            {
                //Log Exception
                return null;
            }    
        }

        public List<GiftCardRedemptionModal> GetRedemptions()
        {
            try
            {
                return giftCardServices.GetRedemptions();
            }
            catch (Exception ex)
            {
                //Log Exception
                return null;
            }
        }

        // GET api/<controller>/5
        public GiftCardDetails Get(long id)
        {
            return giftCardServices.GetGiftCard(id);
        }

        [HttpGet]
        public string GenerateOrderNumber()
        {
            return giftCardServices.GetAdminGiftCardOrderNumberSequence();
        }

        [HttpGet]
        public List<CountryModel> GetCountries()
        {
           return giftCardServices.GetCountries();
        }

        public GiftCardStatusModal HandleGiftCard(GiftCardDetails modal)
        {
            return giftCardServices.HandleGiftCard(modal);
        }


        public GiftCardDetailsModal CheckGiftCardBalance(string giftCardCode)
        {
            return giftCardServices.CheckGiftCardBalance(giftCardCode); 
        }

        public bool ValidCustomerId(int customerId)
        {
            return giftCardServices.ValidCustomerId(customerId);
        }
        
        public OrderNumberGiftCardModal CheckOrderNumberExists(int orderNumber, string giftCardCode) {
            return giftCardServices.CheckOrderNumberExists(orderNumber,giftCardCode);
        }
        public GiftCardRedemptionStatusModal HandleGiftCardRedemption(GiftCardRedemptionModal modal)
        {
            return giftCardServices.HandleGiftCardRedemption(modal);                     
        }

        public GiftCardOrderNumberStatusModal CheckOrderNumberStatus(string orderNumber)
        {
            return giftCardServices.CheckOrderNumberStatus(orderNumber);
        }
        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }        
    }
}