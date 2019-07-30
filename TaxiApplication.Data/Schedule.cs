using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TaxiApplication.Data
{
   public class Schedule
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int No { get; set; }

        [Required]

        public int Position { get; set; }

        // public int Row { get; set; }
        public string ownerID { get; set; }
        public virtual Owner owner { get; set; }
        public string RankId { get; set; }
        public virtual Rank rank { get; set; }
        public int RouteId { get; set; }
        public virtual Route route { get; set; }

        public int id { get; set; }
        public virtual ScheduleDate ScheduleDate { get; set; }

        public virtual ICollection<TaxiPosition> TaxiPositions { get; set; }

    }
}
