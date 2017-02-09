using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScottybonsStylist.Controllers
{
    public class GiftCardController : Controller
    {
        // GET: GiftCard
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult GiftCard()
        {
            return View();
        }

        [Authorize]
        public ActionResult Redemption()
        {
            return View();
        }
    }
}