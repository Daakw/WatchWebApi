using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class WatchModel
    {
        public int Id { get; set; }
        public string ModelName { get; set; }
        public string ReferenceNumber { get; set; }
        public int WatchBrandId { get; set; }

        [ForeignKey("WatchBrandId")]
        public WatchBrand WatchBrand { get; set; }
    }
}
