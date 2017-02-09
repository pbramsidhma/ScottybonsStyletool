using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScottybonsStylist.Models.ProfileQuestions
{
    public class ProfileQuestionAnswerViewModel
    {
       

        public int QuestionId { get; set; }
        public string Question { get; set; }
        public string AnswerDescription { get; set; }

        public byte[] AnswerImage { get; set; }

        public int SerialNumber { get; set; }

        public string FirstColumnValue { get; set; }
        public object SecondColumnValue { get; set; }
    }



    public class ProfileQuestionsViewModel
    {

        public int QuestionId { get; set; }
        public string Question { get; set; }
        public string QuestionSubDescription { get; set; }
        public bool IsRequired { get; set; }
        public bool IsImageTobeShown { get; set; }
        public string ImagePath { get; set; }
        public int DisplayOrder { get; set; }
        public string LanguageOfQuestion { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int NextQuestionNumber { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int PreviousQuestionNumber { get; set; }
        public string ValidationMessage { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int NumberOfQuestions { get; set; }
        public IList<StyleProfileAnswer> Answers { get; set; }
        public ProfileQuestionAnswerControlTypes AnswerControlType { get; set; }
        public int QuestionNumber { get; set; }
        public int AnswerControlId { get; set; }

        public bool IsDisablePrevious { get; set; }
        public bool IsDisableNext { get; set; }

        public string Percentage { get; set; }

        public string SelectedAnswer { get; set; }
        public string SelectedAnswerDescription { get; set; }
        public byte[] SelectedAnswerImage { get; set; }

        public string Flag { get; set; }
        public string SelectedOtherAnswer { get; set; }


        public CustomerAnswer CustomerAnswer { get; set; }

        public IList<AnswerControlViewModel> CheckBoxAnswers { get; set; }

        public string ErrorMessage { get; set; }
    }

    public class AnswerControlViewModel
    {
        public bool IsSelected { get; set; }
        public int AnswerId { get; set; }
        public int QuestionId { get; set; }
        public string Answer { get; set; }
        public int DependentQuestion { get; set; }
        public string AnswerDescription { get; set; }
        public string AnswerImagePath { get; set; }
        public string Flag { get; set; }
        public string QuestionNumber { get; set; }
    }

    public enum ProfileQuestionAnswerControlTypes
    {
        TextBox = 2,
        TextArea = 3,
        CheckBox = 4,
        RadioButton = 5,
        DropdownList = 6,
        CheckBoxList = 7,
        RadioButtonList = 8,
        None = 9,
        Button = 10,
        LinkButton = 11,
        UploadControl = 12,
        Calendar = 13,
    }
}