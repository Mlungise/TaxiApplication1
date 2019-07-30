using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxiApplication.Data;
using System.Data.Entity;

namespace TaxiApplication.BusinessLogics
{
   public class TaxiPositionLogics
    {
        private ApplicationDbContext db;
        public TaxiPositionLogics()
        {
            this.db = new ApplicationDbContext();
        }
        public List<TaxiPosition> GetTaxiPositions()
        {
            return db.TaxiPosition.Include(n => n.schedule).Include(n => n.TaxiDriver).ToList();
        }
        public bool Add(TaxiPosition taxiPosition)
        {
            try
            {
                db.TaxiPosition.Add(taxiPosition);
                db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        public bool RemoveItem(TaxiPosition taxiPosition)
        {
            try
            {
                db.TaxiPosition.Remove(taxiPosition);
                db.SaveChanges();
                
                return true;
            }
            catch (Exception ex)
            { return false; }
        }
        public TaxiPosition GetTaxiPosition(int? ID)
        {
            return db.TaxiPosition.Find(ID);
        }
        public bool Avail(TaxiPosition taxiPosition)
        {
            try
            {
                Available av = new Available();

                av.ID = taxiPosition.ID;
                av.td = taxiPosition.td;
                av.No = taxiPosition.No;
                av.DriverName = taxiPosition.DriverName;
                av.Week = taxiPosition.Week;
                av.RoutePrice = av.SelectPrice();
                db.Availables.Add(av);
              //  db.TaxiPosition.Remove(taxiPosition);
                db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            { return false; }
        }
    }

}
