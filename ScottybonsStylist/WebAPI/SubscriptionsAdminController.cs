using ScottybonsStylist.Models;
using System;
using System.Linq;
using System.Web.Http;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace ScottybonsStylist.WebAPI
{
    public class SubscriptionsAdminController : ApiController
    {
        ScottybonsECom28062016Entities _scottybonsEComEntities;
        private ApplicationUserManager _userManager;
        SubscriptionsAdminController()
        {
            _scottybonsEComEntities = new ScottybonsECom28062016Entities();            
        }

        public SubscriptionsAdminController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;            
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // POST api/<controller>
        [System.Web.Mvc.HttpPost]        
        public object Post([FromBody]dynamic value)
        {
            try
            {
                int orderId = value.OrderId;

                var userId = User.Identity.GetUserId();
                //var userInfo = UserManager.FindById(User.Identity.GetUserId());
                
                //Get Customer Details based on Orderid subscriptionMode
                //IEnumerable<OrderDetailsInfo> orderCustomer = _scottybonsEComEntities.GetCustomerDetailsByOrderID(orderId).ToList();

                //Get order Details
                var order = _scottybonsEComEntities.Orders.FirstOrDefault(c => c.OrderID == orderId);

                //Saving new Subscription
                //int newSubscriptionId = (value.newSubscription != null) ? Convert.ToInt32(value.newSubscription) : 0;
                int newSubscriptionId = (value.newSubscription != null && Convert.ToInt32(value.subscriptionMode) != 0) ? Convert.ToInt32(value.newSubscription) : 0;
                var subscriptionData = _scottybonsEComEntities.PeriodicalScottyBoxMasters.FirstOrDefault(p => p.PeriodicalScottyBoxID.Equals(newSubscriptionId));

                var periodicOrderSubscription = new OrderPeriodicSubscriptionDetail
                {
                    CustomerID = order.CustomerID,
                    CreatedDate = DateTime.Now.Date,
                    OrderId = order.OrderID,
                    PeriodicalScottyBoxID = order.PeriodicalScottyBoxID,
                    NewPeriodicalScottyBoxID = newSubscriptionId,
                    ChangedByAdmin=true,
                    AdminId= userId
                };

                _scottybonsEComEntities.OrderPeriodicSubscriptionDetails.Add(periodicOrderSubscription);
                _scottybonsEComEntities.SaveChanges();

                //Updating order with newly updated subscription
                order.PeriodicalScottyBoxID = (value.newSubscription != null  &&  Convert.ToInt32(value.subscriptionMode) != 0)?Convert.ToInt32(value.newSubscription.Value) : 0;
                order.ContactNumber = (value.contactNumber != null) ? value.contactNumber.Value : order.ContactNumber;
                order.OrderOccasion = (value.orderOccasion!=null)?Convert.ToInt32(value.orderOccasion.Value) : order.OrderOccasion;
                order.OrderStreet = (value.orderStreet!=null)? value.orderStreet.Value : order.OrderStreet;
                order.OrderHouseNumber = (value.orderHouseNumber!= null)? value.orderHouseNumber.Value : order.OrderHouseNumber;
                order.OrderPostalCode = (value.orderPostalCode!=null)? value.orderPostalCode.Value : order.OrderPostalCode;
                order.OrderTown = (value.orderTown!=null)? value.orderTown.Value : order.OrderTown;
                /*order.DeliverNeighbours = (value.deliveryNeighbour!=null)? Convert.ToBoolean(value.deliveryNeighbour.Value): order.DeliverNeighbours;
                order.NextPerodicalScottyBoxDate = (value.newSubscription != null)
                    ? (subscriptionData != null) 
                        ? (order.NextPerodicalScottyBoxDate.Value.Date>DateTime.Now.Date)
                            ? order.NextPerodicalScottyBoxDate.Value.AddMonths(subscriptionData.PerodicalMonths.Value) 
                            : DateTime.Now.Date.AddMonths(subscriptionData.PerodicalMonths.Value)
                        : order.NextPerodicalScottyBoxDate
                    : order.NextPerodicalScottyBoxDate;*/
                order.NextPerodicalScottyBoxDate = (value.newSubscription != null && subscriptionData != null)
                            ? (order.NextPerodicalScottyBoxDate.Value.Date > DateTime.Now.Date)
                                ? (order.PaymentDate.AddMonths(subscriptionData.PerodicalMonths.Value) > DateTime.Now.Date)
                                    ? order.PaymentDate.AddMonths(subscriptionData.PerodicalMonths.Value)
                                    : DateTime.Now.Date.AddDays(1).AddMonths(subscriptionData.PerodicalMonths.Value)
                                : (order.NextPerodicalScottyBoxDate.Value.AddMonths(subscriptionData.PerodicalMonths.Value) > DateTime.Now.Date)
                                    ? order.NextPerodicalScottyBoxDate.Value.AddMonths(subscriptionData.PerodicalMonths.Value)
                                    : DateTime.Now.Date.AddDays(1).AddMonths(subscriptionData.PerodicalMonths.Value)
                            : order.NextPerodicalScottyBoxDate;

                _scottybonsEComEntities.SaveChanges();
                value.result = "Success";

                return value;

            }
            catch (Exception ex)
            {
                value.result = "Something went wrong. Please try again...";
                return value;                
            }
            
        }
    }
    //public object subscriptionFrequency(subscriptionList)
    //{
    //    return subscriptionList;
    //}
}
