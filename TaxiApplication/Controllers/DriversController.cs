using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaxiApplication.Data;
using TaxiApplication.BusinessLogics;
namespace TaxiApplication.Controllers
{
    public class DriversController : Controller
    {
        private ApplicationDbContext db;
        private DriverLogics logic;
        private OwnerLogics Ownerlogic;

        public DriversController()
        {
            this.logic = new DriverLogics();
            this.Ownerlogic = new OwnerLogics();
            this.db = new ApplicationDbContext();
        }

        // GET: Drivers
        public ActionResult Index()
        {
            var drivers = db.Drivers;
            var Owner = db.Owners.ToList().Find(x => x.Email == User.Identity.Name);
            TempData["count"] = drivers.ToList().Where(x => x.ownerID == Owner.ownerID).Count();
            return View(drivers.ToList().Where(x => x.ownerID == Owner.ownerID));
        }
        public ActionResult Create()
        {
            ViewBag.ownerID = new SelectList(Ownerlogic.GetOwners(), "ownerID", "FirstName");
            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(Driver driver)
        {
            var Driver = db.Drivers.Where(x => x.Email == driver.Email);
            if (Driver.Count() != 0)
            {
                ModelState.AddModelError("", "The Driver already exists");
            }
            if (ModelState.IsValid)
            {
                var Owner = db.Owners.ToList().Find(x => x.Email == User.Identity.Name);
                driver.ownerID = Owner.ownerID;
                logic.AddDriver(driver);
                EmailService es = new EmailService();
                es.SendRegMail(driver.Email, "YMCA Account Activation", "Taxi Driver");

                return RedirectToAction("Index");
            }
            return View(driver);
        }
            
    }
}