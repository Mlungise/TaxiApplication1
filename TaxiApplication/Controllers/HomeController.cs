using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaxiApplication.Models;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.IO;
using TaxiApplication.BusinessLogics;
using TaxiApplication.Data;


namespace TaxiApplication.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        BusinessLogic bl = new BusinessLogic();

        public ActionResult LandingPage()
        {
            return View();
        }
        [HttpGet]
        public JsonResult GetNotifications()
        {
            ApplicationDbContext DB = new ApplicationDbContext();
            List<DataSubmit> lstDataSubmit = new List<DataSubmit>();
            List<Route> lstDataSubmit1 = DB.Routes.ToList();


            /// Should update from DB
            ///
            ///e.g. Generating Notification manually
            var No = 0;
            //while (No != 0)
            //{

            //    lstDataSubmit.Add(new DataSubmit() { Notification = lstDataSubmit1.ToString() + No, LastUpdated = DateTime.Now.ToString("ss") + " seconds ago..." });
            //    No--;
            //}

            foreach (var ITEM in lstDataSubmit1)
            {
                lstDataSubmit.Add(new DataSubmit() { Notification = ITEM.RouteName + No, LastUpdated = DateTime.Now.ToString("ss") + " seconds ago..." });
                No++;
            }
            return Json(lstDataSubmit, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RouteDetails()
        {
            return View();
        }

        public ActionResult GetRouteDetails(/*string RouteName, */string searchString)
        {

            var name = from s in db.Routes
                       select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                name = name.Where(s => s.RouteName.ToUpper().Contains(searchString.ToUpper()));
            }
            var route = db.Routes.ToList().Find(x => x.RouteName== searchString);
            if (route != null)
            {               
                Session["rid"] = route.RouteId;
                //return RedirectToAction("RouteDetails",name.ToList());
                return RedirectToAction("GetPassengerRoute", name.ToList());

            }
            else
            {
                TempData["AlertMessage"] = "The selected route does not exist in our system";
                return View("Passenger");
                //return RedirectToAction("Not_Found","Error");
            }
            //return RedirectToAction("RouteDetails", name.ToList());
        }
        public ActionResult GetPassengerRoute( int? id)
        {
            return View(db.Availables.ToList().Where(x=>x.schedule.RouteId==id));
        }



        public FileContentResult UserPhotos()
        {
            if (User.Identity.IsAuthenticated)
            {
                String userId = User.Identity.GetUserId();

                if (userId == null)
                {
                    string fileName = HttpContext.Server.MapPath(@"~/Images/noImg.png");

                    byte[] imageData = null;
                    FileInfo fileInfo = new FileInfo(fileName);
                    long imageFileLength = fileInfo.Length;
                    FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);
                    imageData = br.ReadBytes((int)imageFileLength);

                    return File(imageData, "image/png");

                }
                // to get the user details to load user Image    
                var bdUsers = HttpContext.GetOwinContext().Get<ApplicationDbContext>();
                var userImage = bdUsers.Users.Where(x => x.Id == userId).FirstOrDefault();

                return new FileContentResult(userImage.UserPhoto, "image/jpeg");
            }
            else
            {
                string fileName = HttpContext.Server.MapPath(@"~/Images/noimage.png");

                byte[] imageData = null;
                FileInfo fileInfo = new FileInfo(fileName);
                long imageFileLength = fileInfo.Length;
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                imageData = br.ReadBytes((int)imageFileLength);
                return File(imageData, "image/png");

            }
        }


        [Authorize(Roles = "Admin")]
        public ActionResult Admin()
        {
            return View();
        }

        [Authorize(Roles = "RankManager")]
        public ActionResult RankManager()
        {
            return View();
        }

        [Authorize(Roles = "Owner")]
        public ActionResult Owner()
        {
            return View();
        }


        [Authorize(Roles = "Driver")]
        public ActionResult Driver()
        {
            return View();
        }

        //[Authorize(Roles = "Passenger")]
        public ActionResult Passenger()
        {
            //List<Route> CountryList = db.Routes.ToList();
            //ViewBag.CountryList = new SelectList(CountryList, "RouteId", "RouteName");
            return View();
        }
        //public JsonResult GetStateList(int CountryId)
        //{
        //    db.Configuration.ProxyCreationEnabled = false;
        //    List<StopOver> StateList = db.StopOvers.Where(x => x.StopID == CountryId).ToList();
        //    return Json(StateList, JsonRequestBehavior.AllowGet);
        //}

        [AllowAnonymous]
        public ActionResult Error()
        {
            return View();
        }
        public ActionResult Map()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult CreatePassenger(string firstname, string lname, string email, string phone)
        {
            try
            {
                firstname = Request["firstname"];
                lname = Request["lname"];
                email = Request["email"];
                phone = Request["phone"];

                var passenger = db.Passengers.ToList().Find(x => x.EmailAddress == email);
                if (passenger != null)
                {
                    TempData["AlertMessage"] = "User already exists.";
                    return RedirectToAction("LandingPage");
                }

                bl.CreatePassenger(firstname, lname, email, phone);
                EmailService es = new EmailService();
                //es.sendContactMail(email);
                TempData["AlertMessage"] = "Account Created. Please check your emails.";
                return RedirectToAction("LandingPage");
            }
            catch (Exception)
            {
                TempData["AlertMessage"] = "An error occured while processing request. Please try again.";
                return RedirectToAction("LandingPage");
            }

        }


    }
}