using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourierManagementSystem.Models.Entities
{
    public class TrackingLog
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string TrackingNumber { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public DateTime Date { get; set; }

        
        public int ConsignmentId { get; set; } //Foreign Key for Consignment set in dbcontext


        public Consignment Consignment { get; set; }
    }
}
