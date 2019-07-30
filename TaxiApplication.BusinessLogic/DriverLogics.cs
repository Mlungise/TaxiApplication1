using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxiApplication.Data;
using System.Data.Entity;


namespace TaxiApplication.BusinessLogics
{
    public class DriverLogics
    {
        private ApplicationDbContext db;

        public DriverLogics ()
        {
            this.db = new ApplicationDbContext();
            
        }
        public List<Driver> GetDrivers()
        {
            return db.Drivers.Include(tt=>tt.Owner).ToList();
        }

        public bool AddDriver(Driver driver)
        {
            try
            {
                driver.driverID = Guid.NewGuid().ToString();
                db.Drivers.Add(driver);
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
