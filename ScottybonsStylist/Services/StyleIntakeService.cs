using System.Collections.Generic;
using System.Linq;
using ScottybonsStylist.Models;
using ScottybonsStylist.Models.ProfileQuestions;

namespace ScottybonsStylist
{
    public class StyleIntakeService
    {
        private ScottybonsECom28062016Entities _scottybonsEComEntities;

        public StyleIntakeService(ScottybonsECom28062016Entities scottybonsEComEntities)
        {
            _scottybonsEComEntities = scottybonsEComEntities;
        }

        /// <summary>
        /// Populate List of Questions for Edit/View
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ProfileQuestionsViewModel> PopulateQuestions(int customerId )
        {


            var objQuestionId = _scottybonsEComEntities.CustomerAnswers.Where(c => c.CustomerId == customerId)
                .OrderByDescending(c => c.CreatedOn).FirstOrDefault().QuestionId;

            var languageId = _scottybonsEComEntities.StyleProfileQuestions.FirstOrDefault(c => c.QuestionID == objQuestionId).Language;

            var profQuestions = _scottybonsEComEntities.StyleProfileQuestions.Where(c => c.Language == languageId)
                .OrderBy(c => c.DisplayOrder);

            var allProfileQuestionsViewModel = new List<ProfileQuestionsViewModel>();

            foreach (var objQuestions in profQuestions)
            {
                var profileQuestionsViewModel = GenerateSingleQuestion(objQuestions.QuestionNumber, customerId, objQuestions.QuestionID);
                allProfileQuestionsViewModel.Add(profileQuestionsViewModel);
            }
            return allProfileQuestionsViewModel;

        }

        private IEnumerable<ProfileQuestionsViewModel> PopulateQuestionUsingAnswers(int customerId)
        {
            var allProfileQuestionsViewModel = new List<ProfileQuestionsViewModel>();
            var customerAnswerObjList = _scottybonsEComEntities.CustomerAnswers.Where(c => c.CustomerId == customerId).OrderByDescending(c => c.CreatedOn).Take(49).Join(_scottybonsEComEntities.StyleProfileQuestions, x => x.QuestionId, y => y.QuestionID, (x, y) => x);


            foreach (var customerAnswerObj in customerAnswerObjList)
            {

                var profileQuestionsViewModel = GenerateCustomerAnswer(customerAnswerObj);
                allProfileQuestionsViewModel.Add(profileQuestionsViewModel);
            }
            return allProfileQuestionsViewModel.OrderBy(c => c.QuestionNumber);
        }

        private ProfileQuestionsViewModel GenerateCustomerAnswer(CustomerAnswer customerAnswerObj)
        {
            var profileQuestionsViewModel = new ProfileQuestionsViewModel();

            StyleProfileQuestion objQuestion =
                _scottybonsEComEntities.StyleProfileQuestions.FirstOrDefault(
                    q => q.QuestionID == customerAnswerObj.QuestionId && q.Language == "nl");


            profileQuestionsViewModel.CustomerAnswer = customerAnswerObj;

            profileQuestionsViewModel.Question = objQuestion.Question;
            profileQuestionsViewModel.QuestionId = objQuestion.QuestionID;
            profileQuestionsViewModel.QuestionNumber = objQuestion.QuestionNumber ?? 0;
            profileQuestionsViewModel.QuestionSubDescription = objQuestion.QuestionDescription;
            profileQuestionsViewModel.IsRequired = objQuestion.isRequired ?? false;
            profileQuestionsViewModel.IsImageTobeShown = objQuestion.IsImageTobeShown ?? false;
            profileQuestionsViewModel.ImagePath = objQuestion.ImagePath ?? string.Empty;
            profileQuestionsViewModel.DisplayOrder = objQuestion.DisplayOrder ?? 0;
            profileQuestionsViewModel.LanguageOfQuestion = objQuestion.Language;
            profileQuestionsViewModel.ValidationMessage = objQuestion.ValidationMessage;

            //Answer Information
            profileQuestionsViewModel.AnswerControlId = objQuestion.AnswerControlID ?? 0;
            profileQuestionsViewModel.AnswerControlType =
                (ProfileQuestionAnswerControlTypes)profileQuestionsViewModel.AnswerControlId;
            profileQuestionsViewModel.Answers =
                _scottybonsEComEntities.StyleProfileAnswers.Where(a => a.QuestionID == objQuestion.QuestionID).ToList();

            if (
                profileQuestionsViewModel.AnswerControlType.Equals(ProfileQuestionAnswerControlTypes.RadioButton) &&
                (!ReferenceEquals(profileQuestionsViewModel.CustomerAnswer.AnswerId, null)))
            {
                profileQuestionsViewModel.SelectedAnswer = profileQuestionsViewModel.CustomerAnswer.AnswerId.ToString();


                profileQuestionsViewModel.SelectedAnswerDescription =
                    profileQuestionsViewModel.Answers.FirstOrDefault(
                        o => o.AnswerID == profileQuestionsViewModel.CustomerAnswer.AnswerId).Answer + " " +
                    profileQuestionsViewModel.Answers.FirstOrDefault(
                        o => o.AnswerID == profileQuestionsViewModel.CustomerAnswer.AnswerId).AnswerDescription;
                if (!string.IsNullOrEmpty(profileQuestionsViewModel.CustomerAnswer.Answer))
                {
                    profileQuestionsViewModel.SelectedOtherAnswer = profileQuestionsViewModel.CustomerAnswer.Answer;
                    profileQuestionsViewModel.SelectedAnswerDescription = " " +
                                                                          profileQuestionsViewModel.SelectedOtherAnswer;
                }
            }
            else if (
                profileQuestionsViewModel.AnswerControlType.Equals(ProfileQuestionAnswerControlTypes.TextBox) &&
                (!ReferenceEquals(profileQuestionsViewModel.CustomerAnswer.Answer, null)))
                profileQuestionsViewModel.SelectedAnswer = profileQuestionsViewModel.CustomerAnswer.Answer;
            else if (
                profileQuestionsViewModel.AnswerControlType.Equals(
                    ProfileQuestionAnswerControlTypes.UploadControl) &&
                (!ReferenceEquals(profileQuestionsViewModel.CustomerAnswer.CustomerImage, null)))
                profileQuestionsViewModel.SelectedAnswerImage =
                    profileQuestionsViewModel.CustomerAnswer.CustomerImage;
            else if (profileQuestionsViewModel.AnswerControlType.Equals(ProfileQuestionAnswerControlTypes.CheckBox) &&
                     (!ReferenceEquals(profileQuestionsViewModel.CustomerAnswer.Answer, null)))
            {
                if (!string.IsNullOrEmpty(profileQuestionsViewModel.CustomerAnswer.OtherAnswer))
                {
                    profileQuestionsViewModel.SelectedOtherAnswer = profileQuestionsViewModel.CustomerAnswer.OtherAnswer;
                }
                profileQuestionsViewModel.SelectedAnswer = profileQuestionsViewModel.CustomerAnswer.Answer;

                var answerCheckBoxViewModel = profileQuestionsViewModel.Answers.Select(ans => new AnswerControlViewModel
                {
                    IsSelected =
                        !ReferenceEquals(profileQuestionsViewModel.SelectedAnswer, null) &&
                        profileQuestionsViewModel.SelectedAnswer.Contains(ans.AnswerID.ToString()),
                    AnswerId = ans.AnswerID,
                    QuestionId = ans.QuestionID ?? 1,
                    Answer = ans.Answer,
                    AnswerDescription = ans.AnswerDescription,
                    AnswerImagePath = ans.AnswerImagePath,
                    Flag = ans.FLAG,
                    QuestionNumber = ans.QuestionNumber,
                    DependentQuestion = ans.DependentQuestion ?? 1
                }).ToList();
                profileQuestionsViewModel.CheckBoxAnswers = answerCheckBoxViewModel;
            }
            return profileQuestionsViewModel;
        }


        /// <summary>
        /// Generate Single Questions
        /// </summary>
        /// <param name="questionNumber"></param>
        /// <param name="customerNumber"></param>
        /// <param name="questionId"></param>
        /// <returns></returns>
        private ProfileQuestionsViewModel GenerateSingleQuestion(int? questionNumber, int customerNumber, int questionId)
        {
            var profileQuestionsViewModel = new ProfileQuestionsViewModel();
            StyleProfileQuestion objQuestion =
                _scottybonsEComEntities.StyleProfileQuestions.FirstOrDefault(q => q.QuestionID == questionId);

            if (ReferenceEquals(objQuestion, null)) return profileQuestionsViewModel;


            //Question Information
            profileQuestionsViewModel.Question = objQuestion.Question;
            profileQuestionsViewModel.QuestionId = questionId;
            profileQuestionsViewModel.QuestionNumber = objQuestion.QuestionNumber ?? 0;
            profileQuestionsViewModel.QuestionSubDescription = objQuestion.QuestionDescription;
            profileQuestionsViewModel.IsRequired = objQuestion.isRequired ?? false;
            profileQuestionsViewModel.IsImageTobeShown = objQuestion.IsImageTobeShown ?? false;
            profileQuestionsViewModel.ImagePath = objQuestion.ImagePath ?? string.Empty;
            profileQuestionsViewModel.DisplayOrder = objQuestion.DisplayOrder ?? 0;
            profileQuestionsViewModel.LanguageOfQuestion = objQuestion.Language;
            profileQuestionsViewModel.ValidationMessage = objQuestion.ValidationMessage;

            //Answer Information
            profileQuestionsViewModel.AnswerControlId = objQuestion.AnswerControlID ?? 0;
            profileQuestionsViewModel.AnswerControlType = (ProfileQuestionAnswerControlTypes)profileQuestionsViewModel.AnswerControlId;
            profileQuestionsViewModel.Answers = _scottybonsEComEntities.StyleProfileAnswers.Where(a => a.QuestionID == objQuestion.QuestionID).ToList();


            var customerAnswerObj = _scottybonsEComEntities.CustomerAnswers.Where(c => c.QuestionId == profileQuestionsViewModel.QuestionId && c.CustomerId == customerNumber).OrderByDescending(c => c.CreatedOn).FirstOrDefault();

            if (ReferenceEquals(customerAnswerObj, null))
                return profileQuestionsViewModel;
            profileQuestionsViewModel.CustomerAnswer = customerAnswerObj;

            if (
                profileQuestionsViewModel.AnswerControlType.Equals(ProfileQuestionAnswerControlTypes.RadioButton) &&
                (!ReferenceEquals(profileQuestionsViewModel.CustomerAnswer.AnswerId, null)))
            {
                profileQuestionsViewModel.SelectedAnswer = profileQuestionsViewModel.CustomerAnswer.AnswerId.ToString();




                profileQuestionsViewModel.SelectedAnswerDescription = profileQuestionsViewModel.Answers.FirstOrDefault(o => o.AnswerID == profileQuestionsViewModel.CustomerAnswer.AnswerId).Answer + " " + profileQuestionsViewModel.Answers.FirstOrDefault(o => o.AnswerID == profileQuestionsViewModel.CustomerAnswer.AnswerId).AnswerDescription;
                if (!string.IsNullOrEmpty(profileQuestionsViewModel.CustomerAnswer.OtherAnswer))
                {
                    profileQuestionsViewModel.SelectedOtherAnswer = profileQuestionsViewModel.CustomerAnswer.OtherAnswer;
                    profileQuestionsViewModel.SelectedAnswerDescription = " " +
                                                                          profileQuestionsViewModel.SelectedOtherAnswer;
                }

            }
            else if (
                profileQuestionsViewModel.AnswerControlType.Equals(ProfileQuestionAnswerControlTypes.TextBox) &&
                (!ReferenceEquals(profileQuestionsViewModel.CustomerAnswer.Answer, null)))
                profileQuestionsViewModel.SelectedAnswer = profileQuestionsViewModel.CustomerAnswer.Answer;
            else if (
                profileQuestionsViewModel.AnswerControlType.Equals(
                    ProfileQuestionAnswerControlTypes.UploadControl) &&
                (!ReferenceEquals(profileQuestionsViewModel.CustomerAnswer.CustomerImage, null)))
                profileQuestionsViewModel.SelectedAnswerImage =
                    profileQuestionsViewModel.CustomerAnswer.CustomerImage;
            else if (profileQuestionsViewModel.AnswerControlType.Equals(ProfileQuestionAnswerControlTypes.CheckBox) && (!ReferenceEquals(profileQuestionsViewModel.CustomerAnswer.Answer, null)))
            {
                if (!string.IsNullOrEmpty(profileQuestionsViewModel.CustomerAnswer.OtherAnswer))
                {
                    profileQuestionsViewModel.SelectedOtherAnswer = profileQuestionsViewModel.CustomerAnswer.OtherAnswer;
                }
                profileQuestionsViewModel.SelectedAnswer = profileQuestionsViewModel.CustomerAnswer.Answer;

                var answerCheckBoxViewModel = profileQuestionsViewModel.Answers.Select(ans => new AnswerControlViewModel
                {
                    IsSelected = !ReferenceEquals(profileQuestionsViewModel.SelectedAnswer, null) && profileQuestionsViewModel.SelectedAnswer.Contains(ans.AnswerID.ToString()),
                    AnswerId = ans.AnswerID,
                    QuestionId = ans.QuestionID ?? 1,
                    Answer = ans.Answer,
                    AnswerDescription = ans.AnswerDescription,
                    AnswerImagePath = ans.AnswerImagePath,
                    Flag = ans.FLAG,
                    QuestionNumber = ans.QuestionNumber,
                    DependentQuestion = ans.DependentQuestion ?? 1
                }).ToList();
                profileQuestionsViewModel.CheckBoxAnswers = answerCheckBoxViewModel;
            }
            return profileQuestionsViewModel;
        }
    }
}