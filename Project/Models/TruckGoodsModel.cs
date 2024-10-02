using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class TruckGoodsModel
    {
        [Key]
        public int SerialNo { get; set; }

        [Required(ErrorMessage = "Vehicle Number is required.")]
        [DisplayName("Vehicle Number")]
        public string? VehicleNo { get; set; }

        [Required(ErrorMessage = "Please Select the unit ")]
        public string Unit { get; set; }

        [Required(ErrorMessage = "Please Select the Quantity ")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Please Select the inward Type ")]
        [DisplayName("Inward Number")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Add Description of the Material ")]
        public string? Description { get; set; }
      
        public DateTime ArrivalTime { get; set; }

        //[Required]
        //public bool IsCustomsApproved { get; set; }


        [Required]

        public ApprovalStatus ApprovalStatus { get; set; }

        [Required(ErrorMessage = "Please provide the Supplier Name ")]
        [DisplayName("Supplier Name")]
        public string? SupplierName { get; set; }


        [Required(ErrorMessage = "Please provide the Supplier Location ")]
        [DisplayName("Supplier Location")]
        public string? SupplierLocation { get; set; }

        public string? Remarks { get; set; }



        [Required(ErrorMessage = "Please enter the Amount ")]
        public int Amount { get; set; }


        [Required(ErrorMessage = "Please enter the Invoice Number ")]
        [DisplayName("Invoice Number")]
        public string InvoiceNo { get; set; }

        public ICollection<Document> Documents { get; set; } = new List<Document>();
    }

    public enum ApprovalStatus
    {
        Saved,
        Approved,
        Pending_Approval,
        Rejected
    }


}
