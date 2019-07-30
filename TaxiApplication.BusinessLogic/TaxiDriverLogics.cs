using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxiApplication.Data;
using System.Data.Entity;

namespace TaxiApplication.BusinessLogics
{
   public class TaxiDriverLogics
    {
        private ApplicationDbContext db;
       
        public TaxiDriverLogics()
        {
            this.db = new ApplicationDbContext();
            
        }
        public List<TaxiDriver> GetTaxiDrivers()
        {
            return db.TaxiDrivers.Include(n => n.driver).Include(n => n.taxi).Include(n=>n.owner).ToList();
        }
        public bool Add(TaxiDriver taxiDriver)
        {
            try
            {
                db.TaxiDrivers.Add(taxiDriver);
                db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
            
        }
        public bool Delete(TaxiDriver taxiDriver)
        {
            try
            {
                db.TaxiDrivers.Remove(taxiDriver);
                db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        public TaxiDriver GetTaxiDriver(int? td)
        {
            return db.TaxiDrivers.Find(td);
        }

        public bool addPosition(string taxiNo, int no)
        {
            throw new NotImplementedException();
        }

        public void AddPosition(int position, string taxiNo, int no)
        {
            throw new NotImplementedException();
        }

        public object GetAvail(int? id)
        {
            throw new NotImplementedException();
        }

        public void AvailT(object v)
        {
            throw new NotImplementedException();
        }
        public List<TaxiDriver> TaxiDriversList(string email)
        {
            List<TaxiDriver> taxiDrivers = new List<TaxiDriver>();
            var owner = db.Owners.ToList().Find(n => n.Email == email);
            foreach (var x in db.TaxiDrivers)
            {
                if (x.ownerID == owner.ownerID)
                {
                    taxiDrivers.Add(x);
                }

            }
            return taxiDrivers;
        }
    }
}
