using ScottybonsStylist.AppConstants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScottybonsStylist.Models.Services
{
    public class GiftCardServices
    {
        ScottybonsECom28062016Entities _scottybonsEComEntities;
        public GiftCardServices()
        {
            _scottybonsEComEntities = new ScottybonsECom28062016Entities();
        }
        public List<GiftCardModel> GetGiftCards()
        {
            try
            {
                var items = (from gco in _scottybonsEComEntities.GIftCardsOrders.ToList()
                             join gc in _scottybonsEComEntities.GiftCards.ToList()
                             on gco.OrderNumber equals gc.OrderNumber
                             select new GiftCardModel
                             {
                                 GiftCardId = gc.GiftCardId,
                                 Code = gc.GiftCardCode,
                                 OrderNumber = gc.OrderNumber,
                                 OrderDate = gc.CreatedDate.ToString(),
                                 Value = (Math.Round(gc.GiftCardValue * 100) / 100).ToString("00.00"),
                                 CurrentValue = (Math.Round(gc.CurrentGiftCardValue * 100) / 100).ToString("00.00"),
                                 Email = gc.Email,
                                 ForWho = gc.Name,
                                 Status= GetGiftCardStatusFromEnum(gco.Status==null?0: gco.Status.Value)
                             }).OrderByDescending(o => o.GiftCardId).ToList();
               
                return items;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        private string GetGiftCardStatusFromEnum(int statusId)
        {
            var status = string.Empty;

            switch (statusId)
            {
                case 1:
                    status = GiftCardOrderStatusEnum.Created.ToString();
                    break;
                case 2:
                    status = GiftCardOrderStatusEnum.Completed.ToString();
                    break;
                case 3:
                    status = GiftCardOrderStatusEnum.Error.ToString();
                    break;
                case 4:
                    status = GiftCardOrderStatusEnum.Open.ToString();
                    break;
                default:
                    break;
            }
            return status;
        }

        public GiftCardDetails GetGiftCard(long giftcardId)
        {
            try
            {
                var obj = (from gc in _scottybonsEComEntities.GiftCards.ToList()
                           join gco in _scottybonsEComEntities.GIftCardsOrders.ToList()
                           on gc.OrderNumber equals gco.OrderNumber
                           where gc.GiftCardId == giftcardId
                           select new GiftCardDetails
                           {
                               GiftCardCode = gc.GiftCardCode,
                               OrderNumber = gc.OrderNumber,
                               CreatedDate = gc.CreatedDate.ToString(),
                               Email = gc.Email,
                               GiftCardValue = (Math.Round(gc.GiftCardValue * 100) / 100).ToString("00.00"),
                               CurrentGiftCardValue = (Math.Round(gc.CurrentGiftCardValue * 100) / 100).ToString("00.00"),
                               Name = string.IsNullOrEmpty(gc.Name) ? "N/A" : gc.Name,
                               PersonalMessage = string.IsNullOrEmpty(gc.PersonalMessage) ? "N/A" : gc.PersonalMessage,
                               ShipmentFirstName = gco.ShipmentFirstName,
                               ShipmentLastName = gco.ShipmentLastName,
                               ShipmentStreetName = gco.StreetName,
                               ShipmentHouseNumber = gco.HouseNumber,
                               ShipmentPostalCode = gco.PostalCode,
                               ShipmentCity = gco.Town,
                               ShipmentCountryName = (_scottybonsEComEntities.CountryMasters.FirstOrDefault(cm => cm.CountryID.Equals(gco.Country.Value))).CountryName,
                               ExpirationDate = gc.ExpirationDate.ToString()
                           }).FirstOrDefault();

                return obj;

            }
            catch (Exception)
            {
                return new GiftCardDetails();
            }
        }
        public List<CountryModel> GetCountries()
        {
            var aaa = _scottybonsEComEntities.CountryMasters.Distinct().Where(c => c.CountryID == 2).Select(c => new CountryModel
            {
                CountryId = c.CountryID,
                CountryName = c.CountryName
            }).ToList();
            return aaa;
        }
        public GiftCardOrderNumberStatusModal CheckOrderNumberStatus(string orderNumber)
        {
            var modal = new GiftCardOrderNumberStatusModal();
            try
            {
                if (ReferenceEquals(_scottybonsEComEntities.GIftCardsOrders.ToList().FirstOrDefault(gc => gc.OrderNumber.Trim().Equals(orderNumber)), null))
                {
                    modal.Status = true;
                }
                else
                {
                    modal.OrderNumber = GetAdminGiftCardOrderNumberSequence();
                }
                return modal;
            }
            catch (Exception ex)
            {
                return modal;
            }
        }
        public GiftCardStatusModal HandleGiftCard(GiftCardDetails modal)
        {
            var response = new GiftCardStatusModal();
            try
            {
                var giftCardOrder = new GIftCardsOrder
                {
                    Email = modal.Email,
                    CustomerType = (int)GiftCardCustomerTypeEnum.Admin,
                    PayerFirstName = modal.PayersFirstName,
                    PayerLastName = modal.PayersLastName,
                    ShipmentFirstName = modal.ShipmentFirstName,
                    ShipmentLastName = modal.ShipmentLastName,
                    StreetName = modal.ShipmentStreetName,
                    HouseNumber = modal.ShipmentHouseNumber,
                    OrderNumber = modal.OrderNumber,
                    PostalCode = modal.ShipmentPostalCode,
                    Town = modal.ShipmentCity,
                    Country = modal.ShipmentCountry,
                    Status = (int)GiftCardOrderStatusEnum.Completed,
                    OrderBy = (int)GiftCardOrderByTypeEnum.Admin,
                    OrderTotal = (Math.Round(Convert.ToDouble(modal.GiftCardValue) * 100) / 100),
                };

                _scottybonsEComEntities.GIftCardsOrders.Add(giftCardOrder);
                _scottybonsEComEntities.SaveChanges();

                var giftCardCode = GenerateGiftCardCode(modal.GiftCardValue);
                var giftCardDb = new GiftCard
                {

                    CreatedDate = DateTime.Now,
                    //ExpirationDate = Convert.ToDateTime(modal.ExpirationDate),
                    //ExpirationDate = DateTime.ParseExact(modal.ExpirationDate, "mm/dd/yyyy", null),
                    ExpirationDate = modal.ExpirationDateCtrl,
                    GiftCardCode = giftCardCode,
                    Name = modal.Name,
                    GiftCardValue = (Math.Round(Convert.ToDouble(modal.GiftCardValue) * 100) / 100),
                    CurrentGiftCardValue = (Math.Round(Convert.ToDouble(modal.GiftCardValue) * 100) / 100),
                    PersonalMessage = modal.PersonalMessage,
                    Email = modal.Email,
                    OrderNumber = modal.OrderNumber
                };

                _scottybonsEComEntities.GiftCards.Add(giftCardDb);
                _scottybonsEComEntities.SaveChanges();

                response.Status = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Message = "Error Occured";
                return response;
            }
        }
        public string GetAdminGiftCardOrderNumberSequence()
        {
            var orderBy = (int)GiftCardOrderByTypeEnum.Admin;
            var gcLatestOrderObject = _scottybonsEComEntities.GIftCardsOrders.ToList().OrderBy(gc => gc.GiftCardOrderId).LastOrDefault(gcw => gcw.OrderBy.Value.Equals(orderBy));
            var orderNumber = string.Empty;

            if (!ReferenceEquals(gcLatestOrderObject, null))
            {
                orderNumber = string.Format("{0}", (Convert.ToInt32(gcLatestOrderObject.OrderNumber.Replace(Constants.CustomerGiftCardOrderNumberPrefixer, "").Replace(Constants.AdminGiftCardOrderNumberPrefixer, "")) + 1));
            }
            else
            {
                orderNumber = "1";
            }
            return string.Format("{0}{1}", Constants.AdminGiftCardOrderNumberPrefixer, orderNumber.PadLeft(6, '0'));
        }
        public string GenerateOrderNumber()
        {
            try
            {
                var newOrderNumber = new CommonServices().GenerateCode("G", "").Trim();
                if (!ReferenceEquals(_scottybonsEComEntities.GiftCards.FirstOrDefault(gc => gc.OrderNumber.Trim().Equals(newOrderNumber)), null))
                {
                    newOrderNumber = GenerateOrderNumber();
                }
                return newOrderNumber;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
        public string GenerateGiftCardCode(string giftCardCode)
        {
            try
            {
                var newGiftCardCode = GenerateCode("GC", giftCardCode).Trim();
                if (!ReferenceEquals(_scottybonsEComEntities.GiftCards.FirstOrDefault(gc => gc.GiftCardCode.Trim().Equals(newGiftCardCode)), null))
                {
                    newGiftCardCode = GenerateGiftCardCode(newGiftCardCode);
                }
                return newGiftCardCode;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
        protected string GenerateCode(string startPrefix, string value)
        {
            string alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string small_alphabets = "abcdefghijklmnopqrstuvwxyz";
            string numbers = "1234567890";

            string characters = numbers;
            characters += alphabets + small_alphabets + numbers;

            int length = 5;
            string code = string.Empty;
            for (int i = 0; i < length; i++)
            {
                string character = string.Empty;
                do
                {
                    int index = new Random().Next(0, characters.Length);
                    character = characters.ToCharArray()[index].ToString();
                } while (code.IndexOf(character) != -1);
                code += character;
            }

            //Check the database for duplicate
            return string.Format("{0}{1}{2}", startPrefix, code, (Math.Round(Convert.ToDouble(value) * 100) / 100));
        }

        #region Redemptions

        public List<GiftCardRedemptionModal> GetRedemptions()
        {
            return _scottybonsEComEntities.GiftCardRedemptions.Select(modal => new GiftCardRedemptionModal
            {
                AmounOfRedemption = modal.AmounOfRedemption.Value,
                CustomerId = modal.CustomerId.Value,
                GiftCardCode = modal.GiftCardCode,
                OrderNumber = modal.OrderNumber,
                GiftCardRedemptionId = modal.GiftCardRedemptionId,
                RedemptionDateTime = modal.RedemptionDateTime.Value,
                Status = modal.StatusText==null?"Manual": modal.StatusText
            }).OrderByDescending(o => o.GiftCardRedemptionId).ToList();
        }

        public bool ValidCustomerId(int customerId)
        {
            try
            {
                return _scottybonsEComEntities.Customers.Any(c => c.CustomerID.Equals(customerId));
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public GiftCardDetailsModal CheckGiftCardBalance(string giftCardCode)
        {
            var giftCardModal = new GiftCardDetailsModal();
            try
            {
                giftCardModal = _scottybonsEComEntities.GiftCards.Where(gc => gc.GiftCardCode.ToLower().Trim().Equals(giftCardCode.ToLower().Trim())).Select(gco => new GiftCardDetailsModal
                {
                    OrginalAmount = gco.GiftCardValue,
                    CurrentBalance = gco.CurrentGiftCardValue,
                    ExpireDate = gco.ExpirationDate,
                    Expired = gco.ExpirationDate < DateTime.Today,
                    Exists = true
                }).FirstOrDefault();

                if (ReferenceEquals(giftCardModal, null))
                {
                    giftCardModal = new GiftCardDetailsModal();
                }

                return giftCardModal;
            }
            catch (Exception ex)
            {
                return giftCardModal;
            }
        }

        public OrderNumberGiftCardModal CheckOrderNumberExists(int orderNumber, string giftCardCode)
        {
            var responseModal = new OrderNumberGiftCardModal();

            try
            {
                //responseModal.Valid = _scottybonsEComEntities.Orders.Any(gco => gco.OrderID.Equals(orderNumber) && gco.OrderStatusID != 4);
                responseModal.Valid = _scottybonsEComEntities.Orders.Any(gco => gco.OrderID.Equals(orderNumber));
                if (responseModal.Valid)
                {
                    responseModal.GiftCardExists = _scottybonsEComEntities.GiftCards.Any(gco => gco.GiftCardCode.ToLower().Trim().Equals(giftCardCode.ToLower().Trim()));
                }
                return responseModal;
            }
            catch (Exception ex)
            {
                return responseModal;
            }
        }
        public GiftCardRedemptionStatusModal HandleGiftCardRedemption(GiftCardRedemptionModal giftCardRedemptionModal)
        {
            var responseModal = new GiftCardRedemptionStatusModal();
            try
            {
                var giftCardRedemption = new GiftCardRedemption
                {
                    AmounOfRedemption = giftCardRedemptionModal.AmounOfRedemption,
                    OrderNumber = giftCardRedemptionModal.OrderNumber,
                    GiftCardCode = giftCardRedemptionModal.GiftCardCode,
                    CustomerId = giftCardRedemptionModal.CustomerId,
                    RedemptionDateTime = DateTime.Now
                };

                var giftCardObject = _scottybonsEComEntities.GiftCards.FirstOrDefault(gc => gc.GiftCardCode.ToLower().Trim().Equals(giftCardRedemptionModal.GiftCardCode.ToLower().Trim()));
                if (!ReferenceEquals(giftCardObject, null))
                {
                    giftCardObject.CurrentGiftCardValue = (giftCardObject.CurrentGiftCardValue - giftCardRedemptionModal.AmounOfRedemption);
                    _scottybonsEComEntities.SaveChanges();

                    //Logic to update Orders table [OrderstatusId, PaymentMethodId, Payment Date].
                    int oID = Convert.ToInt32(giftCardRedemptionModal.OrderNumber);
                    var order = _scottybonsEComEntities.Orders.FirstOrDefault(c => c.OrderID == oID);
                    order.OrderStatusID = 4;
                    _scottybonsEComEntities.SaveChanges();
                }
                else
                {
                    responseModal.Status = true;
                    return responseModal;
                }

                _scottybonsEComEntities.GiftCardRedemptions.Add(giftCardRedemption);
                _scottybonsEComEntities.SaveChanges();
                responseModal.Status = true;
                return responseModal;
            }
            catch (Exception ex)
            {
                responseModal.Status = false;
                return responseModal;
            }

        }

        #endregion

    }
}