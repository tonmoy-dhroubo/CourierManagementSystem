using System.ComponentModel.DataAnnotations;


namespace CourierManagementSystem.Models.Entities
{
    public class Shipper
    {
        [Key]
        [Required]
        public int Id { get; set;}

        [Required]
        public string CompanyName { get; set; }

        [Required]
        public string ContactPersonName { get; set; }

        [Required]
        public string ContactPersonPhone { get; set; }


    }
}
