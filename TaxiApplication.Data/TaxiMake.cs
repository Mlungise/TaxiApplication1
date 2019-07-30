using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace TaxiApplication.Data
{
    public class TaxiMake
    {
        [Key]
        public string MakeId { get; set; }
        [Required]

        public string MakeType { get; set; }
        public byte[] Image { get; set; }
        public string ImageType { get; set; }
        //  public ICollection<Taxi> Taxis { get; set; }
    }
}