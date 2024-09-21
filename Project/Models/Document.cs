using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    public class Document
    {
        public int DocumentId { get; set; } 

        public string? DocType { get; set; }
        public string? DocNum { get; set; }
        [NotMapped]
        public IFormFile? DocumentFile { get; set; }
        public string? DocumentPath { get; set; }
        public int TruckGoodsModelId { get; set; }

        public TruckGoodsModel? TruckGoods { get; set; }
    }
}
