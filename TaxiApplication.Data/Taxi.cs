﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace TaxiApplication.Data
{
   public class Taxi
    {
        [Key]
        public string TaxiNo { get; set; }
        public string Status { get; set; }

        public virtual Owner owner { get; set; }
        public string ownerID { get; set; }

        public virtual TaxiMake TaxiMake { get; set; }
        public string MakeId { get; set; }

        public TaxiModel TaxiModel { get; set; }
        public string TaxiModelID { get; set; }

    }
}
