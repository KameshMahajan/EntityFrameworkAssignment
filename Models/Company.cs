using System.ComponentModel.DataAnnotations;

namespace EFAssignment.Models
{
    public class Company
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
