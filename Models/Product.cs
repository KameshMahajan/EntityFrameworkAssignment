using System.ComponentModel.DataAnnotations;

namespace EFAssignment.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int CompanyId { get; set; }
        public int Price { get; set; }
        public Boolean IsMobile { get; set; }
    }
}
