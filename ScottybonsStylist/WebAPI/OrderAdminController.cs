using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.Http;
using System.Web.Mvc;
using ScottybonsStylist.Models;
using ScottybonsStylist.Models.ProfileQuestions;

namespace ScottybonsStylist {

    public class OrderAdminController:ApiController {
        ScottybonsECom28062016Entities _scottybonsEComEntities;
        StyleIntakeService _styleIntakeService;

        public OrderAdminController() {
        _scottybonsEComEntities = new ScottybonsECom28062016Entities();
        _styleIntakeService = new StyleIntakeService(_scottybonsEComEntities);
        }


        // GET api/<controller>
        public IEnumerable<OrderListInfo> Get() {

        try {
        var objects = _scottybonsEComEntities.GetOrders().AsEnumerable().Distinct().ToList();
        return objects;

        } catch (Exception ex) {
        //Log Exception
        throw ex;
        }
        return null;
        }

        // GET api/<controller>/5
        public IEnumerable<ProfileQuestionAnswerViewModel> Get(int id) {
        try {


        int orderId = id;

        var profileQuestionList = new List<ProfileQuestionAnswerViewModel>();
        IEnumerable<OrderDetailsInfo> orderCustomer = _scottybonsEComEntities.GetCustomerDetailsByOrderID(orderId).ToList();


        //Add Order Object o Customer Profile Question Object
        if ((orderCustomer.Any())) {
        var orderObj = orderCustomer.FirstOrDefault();

        var custObj = new ProfileQuestionAnswerViewModel {
            FirstColumnValue = "Order Information",
            SecondColumnValue = string.Empty
        };
        profileQuestionList.Add(custObj);

        custObj = new ProfileQuestionAnswerViewModel {
            FirstColumnValue = "Order Number",
            SecondColumnValue = orderObj.OrderId.ToString(CultureInfo.InvariantCulture)
        };
        profileQuestionList.Add(custObj);

        custObj = new ProfileQuestionAnswerViewModel {
            FirstColumnValue = "Order Date",
            SecondColumnValue = orderObj.CreatedOn.ToShortDateString()
        };
        profileQuestionList.Add(custObj);

        custObj = new ProfileQuestionAnswerViewModel {
            FirstColumnValue = "Customer ID",
            SecondColumnValue = orderObj.CustomerID.ToString()
        };
        profileQuestionList.Add(custObj);

        custObj = new ProfileQuestionAnswerViewModel {
            FirstColumnValue = "First Name",
            SecondColumnValue = orderObj.FirstName
        };
        profileQuestionList.Add(custObj);

        custObj = new ProfileQuestionAnswerViewModel {
            FirstColumnValue = "Last Name",
            SecondColumnValue = orderObj.LastName
        };
        profileQuestionList.Add(custObj);

        custObj = new ProfileQuestionAnswerViewModel {
            FirstColumnValue = "Email",
            SecondColumnValue = orderObj.Email
        };
        profileQuestionList.Add(custObj);

        custObj = new ProfileQuestionAnswerViewModel {
            FirstColumnValue = "Occasion",
            SecondColumnValue = orderObj.OccasionForScottyBoxName
        };
        profileQuestionList.Add(custObj);

        custObj = new ProfileQuestionAnswerViewModel {
            FirstColumnValue = "Street Name",
            SecondColumnValue = orderObj.OrderStreet
        };
        profileQuestionList.Add(custObj);

        custObj = new ProfileQuestionAnswerViewModel {
            FirstColumnValue = "Street Number",
            SecondColumnValue = orderObj.OrderHouseNumber
        };
        profileQuestionList.Add(custObj);

        custObj = new ProfileQuestionAnswerViewModel {
            FirstColumnValue = "Postal Code",
            SecondColumnValue = orderObj.OrderPostalCode
        };
        profileQuestionList.Add(custObj);

        custObj = new ProfileQuestionAnswerViewModel {
            FirstColumnValue = "Town",
            SecondColumnValue = orderObj.OrderTown
        };
        profileQuestionList.Add(custObj);

        custObj = new ProfileQuestionAnswerViewModel {
            FirstColumnValue = "Country",
            SecondColumnValue = orderObj.CountryName
        };
        profileQuestionList.Add(custObj);

        custObj = new ProfileQuestionAnswerViewModel {
            FirstColumnValue = "Permission to deliver at neighbours?",
            SecondColumnValue = orderObj.PermissionToDeliverAtNeighbours
        };
        profileQuestionList.Add(custObj);

        custObj = new ProfileQuestionAnswerViewModel {
            FirstColumnValue = "Permission to call?",
            SecondColumnValue = orderObj.PermissionToCall
        };
        profileQuestionList.Add(custObj);

        custObj = new ProfileQuestionAnswerViewModel {
            FirstColumnValue = "Phonenumber",
            SecondColumnValue = orderObj.ContactNumber
        };
        profileQuestionList.Add(custObj);

        custObj = new ProfileQuestionAnswerViewModel {
            FirstColumnValue = "Subscription",
            SecondColumnValue = orderObj.PeriodicalScottyBox
        };
        profileQuestionList.Add(custObj);

        custObj = new ProfileQuestionAnswerViewModel {
            FirstColumnValue = "Period for Subscripion",
            SecondColumnValue = orderObj.PerodicalMonths
        };
        profileQuestionList.Add(custObj);

        //if (!string.IsNullOrEmpty(orderObj.PromoCode)) {
        //custObj = new ProfileQuestionAnswerViewModel {
        //    FirstColumnValue = "Promo code Used",
        //    SecondColumnValue = orderObj.PromoCode
        //};
        //profileQuestionList.Add(custObj);
        //}

        //if (!string.IsNullOrEmpty(orderObj.PromoCode)) {        
        //    var orderPromoDtls =_scottybonsEComEntities.PromoCodeOrders.Where(c => c.OrderId == orderId).FirstOrDefault();
        //    custObj = new ProfileQuestionAnswerViewModel {
        //        FirstColumnValue = "Promo code Type",
        //        SecondColumnValue = (orderPromoDtls.IsOnline)?"Online":"Offline"
        //    };
        //    profileQuestionList.Add(custObj);
        //}
        // if (!string.IsNullOrEmpty(orderObj.PromoCode)) {        
        //    var orderPromoDtls =_scottybonsEComEntities.PromoCodeOrders.Where(c => c.OrderId == orderId).FirstOrDefault();                       
        //    custObj = new ProfileQuestionAnswerViewModel {
        //        FirstColumnValue = "Promo code Is-Percentage?",
        //        SecondColumnValue = ((bool)orderPromoDtls.IsPercentage) ? "True":"False"
        //    };
        //    profileQuestionList.Add(custObj);
        //}
        
        //Add customer Question and Answer Object 
        var customerObject = _styleIntakeService.PopulateQuestions(orderObj.CustomerID ?? 0);

        custObj = new ProfileQuestionAnswerViewModel {
            FirstColumnValue = "Customer - Style Intake",
            SecondColumnValue = string.Empty
        };
        profileQuestionList.Add(custObj);

        foreach (var objprofileQAnswer in customerObject) {
        custObj = new ProfileQuestionAnswerViewModel {
            FirstColumnValue = objprofileQAnswer.QuestionNumber + " " + objprofileQAnswer.Question
        };
        if (objprofileQAnswer.AnswerControlType == ProfileQuestionAnswerControlTypes.UploadControl) {
        if (!ReferenceEquals(objprofileQAnswer.SelectedAnswerImage,null)) {
        custObj.SecondColumnValue = "img";
        custObj.AnswerImage = objprofileQAnswer.SelectedAnswerImage;
        } else {
        custObj.SecondColumnValue = "No Image Uploaded";
        }

        } else if (objprofileQAnswer.AnswerControlType == ProfileQuestionAnswerControlTypes.TextBox) {
        custObj.SecondColumnValue = objprofileQAnswer.SelectedAnswer;
        } else if (objprofileQAnswer.AnswerControlType == ProfileQuestionAnswerControlTypes.Calendar) {
        if (!ReferenceEquals(objprofileQAnswer.CustomerAnswer,null) && !ReferenceEquals(objprofileQAnswer.CustomerAnswer.Answer,null))
            custObj.SecondColumnValue = objprofileQAnswer.CustomerAnswer.Answer;
        } else if (objprofileQAnswer.AnswerControlType == ProfileQuestionAnswerControlTypes.CheckBox) {
        var sb = new StringBuilder();
        var result = string.Empty;
        custObj.SecondColumnValue = objprofileQAnswer.SelectedAnswer + objprofileQAnswer.SelectedOtherAnswer;

        if (!ReferenceEquals(objprofileQAnswer.CheckBoxAnswers,null)) {
        foreach (var objCheckBox in objprofileQAnswer.CheckBoxAnswers) {
        if (objCheckBox.IsSelected)
            sb.Append(objCheckBox.Answer + ":" + objCheckBox.AnswerDescription + ", ");
        }
        }
        if (sb.Length > 0) {
        result = sb.ToString().Substring(0,sb.ToString().Length - 1);
        }
        custObj.SecondColumnValue = result + objprofileQAnswer.SelectedOtherAnswer;

        } else {
        custObj.SecondColumnValue = objprofileQAnswer.SelectedAnswerDescription;
        }
        profileQuestionList.Add(custObj);

        }


        }

        return profileQuestionList;
        } catch (Exception ex) {
        throw ex;
        }
        }


        // POST api/<controller>
        public void Post([FromBody]string value) {
        }

        // PUT api/<controller>/5
        public void Put(int id,[FromBody]string value) {
        }

        // DELETE api/<controller>/5
        public void Delete(int id) {
        }




        ///// <summary>
        ///// Get Order List
        ///// </summary>
        ///// <returns></returns>
        //public IEnumerable<OrderListInfo> GetOrderList()
        //{
        //    var scottybonsEComEntities = new ScottybonsECom28062016Entities();
        //    try
        //    {
        //        return scottybonsEComEntities.GetOrders().AsEnumerable();

        //    }
        //    catch (Exception ex)
        //    {
        //        //Log Exception
        //        throw ex;
        //    }
        //    return null;

        //}
    }
}