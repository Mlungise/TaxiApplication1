using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace TaxiApplication.Data
{
    public class TaxiPosition
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
        
    }
}