using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TaxiApplication.Models;
using TaxiApplication.BusinessLogics;
using TaxiApplication.Data;


namespace TaxiApplication.Controllers
{
    public class LaggagesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Laggages
        public ActionResult Index()
        {
            return View(db.laggagees.ToList());
        }

        // GET: Laggages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Laggage laggage = db.laggagees.Find(id);
            if (laggage == null)
            {
                return HttpNotFound();
            }
            return View(laggage);
        }
        //public ActionResult ConfirmReservation(int? LaggageID)
        //{
        //    if (LaggageID == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Laggage laggage = db.laggagees.Find(LaggageID);
        //    if (laggage == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    //Rooms rooms = db.Rooms.Where(x => x.RoomNo == reservation.RoomNo).FirstOrDefault();
        //    Available b = new Available();
            
        //    var rmb = db.Availables.Find(b.schedule.RouteId);
        //    var rmbp = db.Prices.Find(rmb.schedule.RouteId);
        //    var rmbps = db.StopOvers.Find(rmb.schedule.RouteId);


        //    RouteViewModel rvmd = new RouteViewModel();
        //    rvmd.RankName = rmb.schedule.rank.RankName;
        //    rvmd.RouteName = rmb.schedule.route.RouteName;
        //    rvmd.Pricevalue = rmb.Price;
        //    rvmd.StopOvers = rmbps.stop;
        //    rvmd.RegNo = rmb.taxi.RegNo;
        //    rvmd.DriverName = rmb.driver.FirstName;
        //    rvmd.MakeType = rmb.taxi.TaxiMake.MakeType;
        //    rvmd.RouteDuration = 900;
        //    rvmd.TotalPrice = Convert.ToDouble((rmbp.pricevalue * laggage.getPrice()));
        //    rvmd.NumberLaggage = laggage.NumberLaggage;
        //    rvmd.Size = laggage.getSize();
        //    rvmd.LaggagePrice = Convert.ToDouble(laggage.getPrice());
        //    return View(rvmd);
        //}
        

        // GET: Laggages/Create
        public ActionResult Create()
        {
            //var room = db.Routes.Find(id);
            //Session["roomId"] = id;
            return View();
        }

        // POST: Laggages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LaggageID,NumberLaggage,picture,Size,TotalPrice,PassengerName")] Laggage laggage, HttpPostedFileBase img_upload)
        {
            
            byte[] data = null;
            data = new byte[img_upload.ContentLength];
            img_upload.InputStream.Read(data, 0, img_upload.ContentLength);
            laggage.picture = data;

            if (ModelState.IsValid)
            {
                laggage.TotalPrice = Convert.ToDouble(laggage.getPrice());
                laggage.Size = laggage.getSize();
                db.laggagees.Add(laggage);
                db.SaveChanges();
                //return RedirectToAction("Index","RouteViewModels");
                //return RedirectToAction("ConfirmReservation", new { LaggageID = laggage.LaggageID });
                return RedirectToAction("ConfirmDetails", "Laggages", new { id = laggage.LaggageID });


            }

            return View(laggage);
        }
        public ActionResult ConfirmDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            Laggage applicant = db.laggagees.Find(id);
            if (applicant == null)
            {
                return HttpNotFound();
            }
            return View(applicant);
        }
        // GET: Laggages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Laggage laggage = db.laggagees.Find(id);
            if (laggage == null)
            {
                return HttpNotFound();
            }
            return View(laggage);
        }

        // POST: Laggages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LaggageID,NumberLaggage,picture,Size,TotalPrice,PassengerName")] Laggage laggage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(laggage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(laggage);
        }

        // GET: Laggages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Laggage laggage = db.laggagees.Find(id);
            if (laggage == null)
            {
                return HttpNotFound();
            }
            return View(laggage);
        }

        // POST: Laggages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Laggage laggage = db.laggagees.Find(id);
            db.laggagees.Remove(laggage);
            db.SaveChanges();
            return RedirectToAction("Index");
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
