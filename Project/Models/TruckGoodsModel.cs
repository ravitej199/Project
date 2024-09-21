using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class TruckGoodsModel
    {
        [Key]
        public int SerialNo { get; set; }

        [Required]
        public string? VehicleNo { get; set; }

        [Required]
        public string Unit { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public string Type { get; set; }
        [Required]
        public string? Description { get; set; }
      
        public DateTime ArrivalTime { get; set; }

        [Required]
        public bool IsCustomsApproved { get; set; }

        [Required]
        public string? SupplierName { get; set; }

        [Required]
        public string? SupplierLocation { get; set; }

        public string? Remarks { get; set; }


        [Required]
        public int Amount { get; set; }

        [Required]
        public string InvoiceNo { get; set; }

        public List<Document> Documents { get; set; } = new List<Document>();
    }


}
