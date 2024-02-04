using System.ComponentModel.DataAnnotations;

namespace EFAssignment.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime? DateOfAnniversary { get; set; }
        public string EmailId { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
    }
}
