using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxiApplication.Data;
using System.Data.Entity;
using Microsoft.AspNet.Identity;

namespace TaxiApplication.BusinessLogics
{
   public class TaxiLogics
    {
        private ApplicationDbContext db;
        public TaxiLogics()
        {
            this.db = new ApplicationDbContext();
        }
        public List<Taxi> GetTaxis()
        {
            return db.Taxis.Include(n =>n.owner).Include(n =>n.TaxiModel).Include(n =>n.TaxiMake).ToList();
        }
        public bool Add(Taxi taxi)
        {
            try
            {
                //taxi.TaxiNo = Guid.NewGuid().ToString();
                db.Taxis.Add(taxi);
                db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
