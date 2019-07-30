using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaxiApplication.Data;
using TaxiApplication.BusinessLogics;

namespace TaxiApplication.Controllers
{
    public class TaxiModelsController : Controller
    {
        private TaxiModelLogics tmLogic;
        public TaxiModelsController()
        {
            this.tmLogic = new TaxiModelLogics();
        }
        // GET: TaxiModels
        public ActionResult Index()
        {
            return View(tmLogic.GetTaxiModels());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TaxiModel taxiMode)
        {
            if(ModelState.IsValid)
            {
                tmLogic.Add(taxiMode);
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}