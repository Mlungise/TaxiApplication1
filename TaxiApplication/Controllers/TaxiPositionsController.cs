using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using TaxiApplication.Data;
using TaxiApplication.BusinessLogics;

namespace TaxiApplication.Controllers
{
    public class TaxiPositionsController : Controller
    {
        private TaxiPositionLogics tpLogic;
        private TaxiDriverLogics tdLogic;
        private ScheduleLogics sLogic;
        public TaxiPositionsController()
        {
            this.tpLogic = new TaxiPositionLogics();
            this.tdLogic = new TaxiDriverLogics();
            this.sLogic = new ScheduleLogics();
        }
        // GET: TaxiPositions
        public ActionResult Index()
        {
            return View(tpLogic.GetTaxiPositions());
        }
        public ActionResult Index2()
        {
            return View(tpLogic.GetTaxiPositions());
        }
        public ActionResult Create()
        {
            ViewBag.No = new SelectList(sLogic.GetSchedules(), "No", "Position");
           // ViewBag.td = new SelectList(tdLogic.GetTaxiDrivers(), "td", "TaxiNo");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TaxiPosition taxiPosition)
        {
            if (ModelState.IsValid)
            {

                tpLogic.Add(taxiPosition);
                return RedirectToAction("Index");
            }
            return View(taxiPosition);
        }
        public ActionResult Delete(int? ID)
        {
            if (ID == null)
                return RedirectToAction("Bad_Request", "Error");
            if (tpLogic.GetTaxiPosition(ID) != null)
                return View(tpLogic.GetTaxiPosition(ID));
            else
                return RedirectToAction("Not_Found", "Error");
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int ID)
        {
            tpLogic.RemoveItem(tpLogic.GetTaxiPosition(ID));
            return RedirectToAction("Index");
        }
        public ActionResult Avail(int? ID)
        {
            if (ID == null)
                return RedirectToAction("Bad_Request", "Error");
            if (tpLogic.GetTaxiPosition(ID) != null)
                return View(tpLogic.GetTaxiPosition(ID));
            else
                return RedirectToAction("Not_Found", "Error");
        }
        [HttpPost, ActionName("Avail")]
        [ValidateAntiForgeryToken]
        public ActionResult AvailConfirmed(int ID)
        {
            tpLogic.Avail(tpLogic.GetTaxiPosition(ID));
            return RedirectToAction("Index");
        }

    }
}