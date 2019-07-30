using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaxiApplication.Data;
using TaxiApplication.BusinessLogics;

namespace TaxiApplication.Controllers
{
    public class TaxisController : Controller
    {
        private ApplicationDbContext db;       
        private TaxiLogics taxiLogic;
        private TaxiModelLogics taximodelLogic;
        private TaxiMakeLogics taximakeLogic;
        private OwnerLogics ownerLogics;
        public TaxisController()
        {
            this.taxiLogic = new TaxiLogics();
            this.taximodelLogic = new TaxiModelLogics();
            this.taximakeLogic = new TaxiMakeLogics();
            this.ownerLogics = new OwnerLogics();
            this.db = new ApplicationDbContext();
        }
        // GET: Taxis
        public ActionResult Index()
        {
            TempData["count"] = db.Taxis.Count();
            var owner = db.Owners.ToList().Find(x => x.Email == User.Identity.Name);
            var taxis = db.Taxis.Where(x => x.ownerID == owner.ownerID);
            return View(taxis.ToList());
        }
        public ActionResult Create()
        {
            ViewBag.TaxiModelID = new SelectList(taximodelLogic.GetTaxiModels(), "TaxiModelID", "TaxiModelName");
            ViewBag.MakeId = new SelectList(taximakeLogic.GetTaxiMakes(), "MakeId", "MakeType");
            ViewBag.ownerID = new SelectList(ownerLogics.GetOwners(), "ownerID", "FirstName");

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Taxi taxi)
        {
            if(ModelState.IsValid)
            {               
                var owner = db.Owners.ToList().Find(x => x.Email == User.Identity.Name);
                taxi.ownerID = owner.ownerID;

                taxiLogic.Add(taxi);
                return RedirectToAction("Index");
            }
            return View("Index");
        }

    }
}