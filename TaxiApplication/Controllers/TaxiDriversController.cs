using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaxiApplication.Data;
using TaxiApplication.BusinessLogics;

namespace TaxiApplication.Controllers
{
    public class TaxiDriversController : Controller
    {
        private ApplicationDbContext db;
        private TaxiDriverLogics txDriverLogic;
        private DriverLogics dLogic;
        private TaxiLogics tlogic;
        
        public TaxiDriversController()
        {
            this.txDriverLogic = new TaxiDriverLogics();
            this.tlogic = new TaxiLogics();
            this.dLogic = new DriverLogics();
            this.db = new ApplicationDbContext();
        }
        // GET: TaxiDrivers
        public ActionResult Index()
        {
            var owner = db.Owners.ToList().Find(x => x.Email == User.Identity.Name);
            var taxis = db.TaxiDrivers.Where(x => x.ownerID == owner.ownerID);
            return View(taxis.ToList());
        }
        public ActionResult Create()
        {
            ViewBag.driverID = new SelectList(dLogic.GetDrivers(), "driverID", "FirstName");
            ViewBag.TaxiNo = new SelectList(tlogic.GetTaxis(), "TaxiNo", "TaxiNo");
            ViewBag.ownerID = new SelectList(tlogic.GetTaxis(), "ownerID", "FirstName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TaxiDriver taxiDriver)
        {
            if(ModelState.IsValid)
            {
                var owner = db.Owners.ToList().Find(x => x.Email == User.Identity.Name);
                taxiDriver.ownerID = owner.ownerID;

                txDriverLogic.Add(taxiDriver);
                return RedirectToAction("Index");
            }
            return View("Index");
        }
        public ActionResult Delete(int? td)
        {
            if (td == null)
                return RedirectToAction("NotFound", "");
            if (txDriverLogic.GetTaxiDriver(td) != null)
                return View(txDriverLogic.GetTaxiDriver(td));
            else
                return RedirectToAction("Error");
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int td)
        {
            txDriverLogic.Delete(txDriverLogic.GetTaxiDriver(td));
            return RedirectToAction("Index");
        }


    }
}