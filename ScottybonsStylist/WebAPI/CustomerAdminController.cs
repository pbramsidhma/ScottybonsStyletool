using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ScottybonsStylist.Models;
using ScottybonsStylist.Models.ProfileQuestions;

namespace ScottybonsStylist
{
    public class CustomerAdminController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }


        ScottybonsECom28062016Entities _scottybonsEComEntities;
        private readonly StyleIntakeService _styleIntakeService;

        CustomerAdminController()
        {
            _scottybonsEComEntities = new ScottybonsECom28062016Entities();
            _styleIntakeService = new StyleIntakeService(_scottybonsEComEntities);
        }



        // GET api/<controller>/5
        // GET api/<controller>/5
        public IEnumerable<ProfileQuestionAnswerViewModel> Get(int id)
        {
            var profileImageList = new List<ProfileQuestionAnswerViewModel>();
            try
            {
                var orderId = id;

                IEnumerable<OrderDetailsInfo> orderCustomer = _scottybonsEComEntities.GetCustomerDetailsByOrderID(orderId).ToList();


                //Add Order Object o Customer Profile Question Object
                if ((orderCustomer.Any()))
                {
                    var orderObj = orderCustomer.FirstOrDefault();

                    //Add customer Question and Answer Object 
                    var customerObject = _styleIntakeService.PopulateQuestions(orderObj.CustomerID ?? 0).Where(c => c.AnswerControlType.Equals(ProfileQuestionAnswerControlTypes.UploadControl));

                    
                    foreach (var objprofileQAnswer in customerObject)
                    {
                        if (objprofileQAnswer.AnswerControlType == ProfileQuestionAnswerControlTypes.UploadControl)
                        {
                            var custObj = new ProfileQuestionAnswerViewModel
                             {
                                 FirstColumnValue = objprofileQAnswer.QuestionNumber + " " + objprofileQAnswer.Question
                             };

                            if (!ReferenceEquals(objprofileQAnswer.SelectedAnswerImage, null))
                            {
                                custObj.SecondColumnValue = "img";
                                custObj.AnswerImage = objprofileQAnswer.SelectedAnswerImage;
                            }
                            else
                            {
                                custObj.SecondColumnValue = "No Image Uploaded";
                            }

                            profileImageList.Add(custObj);

                        }

                    }

                }
            }
            catch (Exception)
            {

                throw;
            }


            return profileImageList;
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