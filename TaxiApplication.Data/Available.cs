using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace TaxiApplication.Data
{
    public class Available
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int td { get; set; }
        public virtual TaxiDriver TaxiDriver { get; set; }
        public virtual Schedule schedule { get; set; }
        public int No { get; set; }
        public string DriverName { get; set; }
        public string Week { get; set; }
        public decimal RoutePrice { get; set; }

        ApplicationDbContext db = new ApplicationDbContext();

        public decimal SelectPrice()
        {
            var x = (from d in db.Prices
                     where d.Route.RouteName==schedule.route.RouteName
                     select d.pricevalue).FirstOrDefault();
            return x;
        }
        //public int SelectMaxNumOfP()
        //{
        //    var po = db.reservedTaxis.Where(n => n.TaxiNo == TaxiNo).Select(n => n.Count).FirstOrDefault();
        //    if (po > 0)
        //    {
        //        po = db.reservedTaxis.Where(n => n.TaxiNo == TaxiNo).Select(n => n.Count).Max();
        //    }
        //    else
        //    {
        //        po = 0;
        //    }
        //    //var v = (from i in db.reservedtaxis
        //    //         where i.TaxiNo == TaxiNo 
        //    //         select i.TotalAmount).FirstOrDefault();
        //    return po;
        //}
        //public decimal CalcTotal()
        //{
        //    return SelectPrie() * SelectMaxNumOfP();
        //}
        //public int CalcSeats()
        //{
        //    return 15 - SelectMaxNumOfP();
        //}

    }
}