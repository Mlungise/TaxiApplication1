using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaxiApplication.Data
{
    public class Price
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PriceID { get; set; }
        [Required]

        public decimal pricevalue { get; set; }

        public virtual Route Route { get; set; }
        public int RouteId { get; set; }

        public virtual Season season { get; set; }
        public int SeasonID { get; set; }
      
    }
}