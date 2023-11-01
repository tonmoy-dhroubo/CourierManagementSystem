using System.ComponentModel.DataAnnotations;


namespace CourierManagementSystem.Models.Entities
{

    public class Consignment
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string TrackingNumber { get; set; }

        [Required]
        public string SenderId { get; set; }

        [Required]
        public string SenderName { get; set; }

        [Required]
        [EmailAddress]
        public string SenderEmail { get; set; }

        [Required]
        [Phone]
        public string SenderPhoneNumber { get; set; }

        [Required]
        public string RecipientName { get; set; }

        [EmailAddress]
        public string RecipientEmail { get; set; }

        [Required]
        [Phone]
        public string RecipientPhoneNumber { get; set; }


        [Required]
        public string SenderAddress { get; set; }

        [Required]
        public string DeliveryAddress { get; set; }

        [Required]
        public decimal ParcelWeight { get; set; }

        [Required]
        public string ParcelContent { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public DateTime EstimatedDeliveryDate { get; set; }

        [Required]
        public decimal ShippingCost { get; set; }

        [Required]
        public bool PaymentCompletion { get; set; }

        [Required]
        public ICollection<TrackingLog> TrackingLogs { get; set; }
    }

}
