using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace ScottybonsStylist.AppConstants
{
    public static class Constants
    {
       
        #region Gift Card Properties
        public static string CustomerGiftCardOrderNumberPrefixer
        {
            get
            {

                return WebConfigurationManager.AppSettings["CustomerGiftCardOrderNumberPrefixer"];
            }
        }

        public static string AdminGiftCardOrderNumberPrefixer
        {
            get
            {

                return WebConfigurationManager.AppSettings["AdminGiftCardOrderNumberPrefixer"];
            }
        }
        #endregion
    }
}