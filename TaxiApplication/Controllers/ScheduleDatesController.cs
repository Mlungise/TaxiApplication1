using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaxiApplication.BusinessLogics;
using TaxiApplication.Data;

namespace TaxiApplication.Controllers
{
    public class ScheduleDatesController : Controller
    {
        private ScheduleDateLogics logic;
        public ScheduleDatesController()
        {
            this.logic = new ScheduleDateLogics();
        }
        // GET: ScheduleDates
        public ActionResult Index()
        {

            return View(logic.GetScheduleDates());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ScheduleDate scheduleDate)
        {
            if (ModelState.IsValid)
            {


                logic.AddDate(scheduleDate);
                return RedirectToAction("Index");
            }
            return View(scheduleDate);
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateManualy(ScheduleDate scheduleDate)
        {
            if (ModelState.IsValid)
            {
                logic.AddDate2(scheduleDate);
                return RedirectToAction("Index");
            }
            return View(scheduleDate);
        }
        public ActionResult CreateManualy()
        {
            return View();
        }
    }
}