using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ScottybonsStylist.Models;

namespace ScottybonsStylist
{
    public class OrderController : Controller
    {
        private ScottybonsECom28062016Entities db = new ScottybonsECom28062016Entities();

        // GET: Order/OrderIndex
        public ActionResult OrderIndex()
        {
            var order = db.Orders.Include(o => o.CountryMaster).Include(o => o.Customer).Include(o => o.OccasionForScottyBoxMaster).Include(o => o.OrderStatusMaster).Include(o => o.PaymentTypeMaster);
            return View(order.ToList());
        }

        /*
        // GET: Order/OrderDetails/5
        public ActionResult OrderDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }
        */

        // GET: Order/OrderCreate
        public ActionResult OrderCreate()
        {
            ViewBag.OrderCountryID = new SelectList(db.CountryMasters, "CountryID", "CountryName");
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "FirstName");
            ViewBag.OrderOccasion = new SelectList(db.OccasionForScottyBoxMasters, "OccasionForScottyBoxID", "OccasionForScottyBoxName");
            ViewBag.OrderStatusID = new SelectList(db.OrderStatusMasters, "OrderStatusID", "OrderStatusName");
            ViewBag.PaymentMethodID = new SelectList(db.PaymentTypeMasters, "PaymentTypeID", "PaymentTypeName");
            return View();
        }

        // POST: Order/OrderCreate
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OrderCreate([Bind(Include = "OrderID,CustomerID,OrderOccasion,OccasionComments,WishList,DateOrder,RequiredDate,PaymentMethodID,AgreeGeneralConditions,OrderStreet,OrderHouseNumber,OrderPostalCode,OrderTown,OrderCountryID,OrderComments,DeliverNeighbours,OrderStatusID,PaymentDate,IsStylistContactYou,ContactNumber,OrderCommentStylist,OrderStyleAdvice,NumberOfArticles,ReviewOverall,UpdatedOn,CreatedOn,PeriodicalScottyBoxID,NextPerodicalScottyBoxDate,ParentOrderID,PermissionToCollectFutureInvoices,OrderComanyName,CountryMaster,Customer,OccasionForScottyBoxMaster,OrderStatusMaster,PaymentTypeMaster")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                DisplaySuccessMessage("Has append a Order record");
                return RedirectToAction("OrderIndex");
            }

            ViewBag.OrderCountryID = new SelectList(db.CountryMasters, "CountryID", "CountryName", order.OrderCountryID);
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "FirstName", order.CustomerID);
            ViewBag.OrderOccasion = new SelectList(db.OccasionForScottyBoxMasters, "OccasionForScottyBoxID", "OccasionForScottyBoxName", order.OrderOccasion);
            ViewBag.OrderStatusID = new SelectList(db.OrderStatusMasters, "OrderStatusID", "OrderStatusName", order.OrderStatusID);
            ViewBag.PaymentMethodID = new SelectList(db.PaymentTypeMasters, "PaymentTypeID", "PaymentTypeName", order.PaymentMethodID);
            DisplayErrorMessage();
            return View(order);
        }

        // GET: Order/OrderEdit/5
        public ActionResult OrderEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrderCountryID = new SelectList(db.CountryMasters, "CountryID", "CountryName", order.OrderCountryID);
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "FirstName", order.CustomerID);
            ViewBag.OrderOccasion = new SelectList(db.OccasionForScottyBoxMasters, "OccasionForScottyBoxID", "OccasionForScottyBoxName", order.OrderOccasion);
            ViewBag.OrderStatusID = new SelectList(db.OrderStatusMasters, "OrderStatusID", "OrderStatusName", order.OrderStatusID);
            ViewBag.PaymentMethodID = new SelectList(db.PaymentTypeMasters, "PaymentTypeID", "PaymentTypeName", order.PaymentMethodID);
            return View(order);
        }

        // POST: OrderOrder/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OrderEdit([Bind(Include = "OrderID,CustomerID,OrderOccasion,OccasionComments,WishList,DateOrder,RequiredDate,PaymentMethodID,AgreeGeneralConditions,OrderStreet,OrderHouseNumber,OrderPostalCode,OrderTown,OrderCountryID,OrderComments,DeliverNeighbours,OrderStatusID,PaymentDate,IsStylistContactYou,ContactNumber,OrderCommentStylist,OrderStyleAdvice,NumberOfArticles,ReviewOverall,UpdatedOn,CreatedOn,PeriodicalScottyBoxID,NextPerodicalScottyBoxDate,ParentOrderID,PermissionToCollectFutureInvoices,OrderComanyName,CountryMaster,Customer,OccasionForScottyBoxMaster,OrderStatusMaster,PaymentTypeMaster")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                DisplaySuccessMessage("Has update a Order record");
                return RedirectToAction("OrderIndex");
            }
            ViewBag.OrderCountryID = new SelectList(db.CountryMasters, "CountryID", "CountryName", order.OrderCountryID);
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "FirstName", order.CustomerID);
            ViewBag.OrderOccasion = new SelectList(db.OccasionForScottyBoxMasters, "OccasionForScottyBoxID", "OccasionForScottyBoxName", order.OrderOccasion);
            ViewBag.OrderStatusID = new SelectList(db.OrderStatusMasters, "OrderStatusID", "OrderStatusName", order.OrderStatusID);
            ViewBag.PaymentMethodID = new SelectList(db.PaymentTypeMasters, "PaymentTypeID", "PaymentTypeName", order.PaymentMethodID);
            DisplayErrorMessage();
            return View(order);
        }

        // GET: Order/OrderDelete/5
        public ActionResult OrderDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Order/OrderDelete/5
        [HttpPost, ActionName("OrderDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult OrderDeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            DisplaySuccessMessage("Has delete a Order record");
            return RedirectToAction("OrderIndex");
        }

        private void DisplaySuccessMessage(string msgText)
        {
            TempData["SuccessMessage"] = msgText;
        }

        private void DisplayErrorMessage()
        {
            TempData["ErrorMessage"] = "Save changes was unsuccessful.";
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
