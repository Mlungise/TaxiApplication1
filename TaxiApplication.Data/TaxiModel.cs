using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace TaxiApplication.Data
{
   public class TaxiModel
    {
        [Key]
        
        public string TaxiModelID { get; set; }
        public string TaxiModelName { get; set; }
        public int Seats { get; set; }

       // public ICollection<Taxi> Taxis { get; set; }
    }
}
