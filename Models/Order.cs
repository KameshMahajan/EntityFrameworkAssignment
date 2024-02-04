using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFAssignment.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string OrderNumber { get; set; }
        public int CustomerId { get; set; }
        public int CompanyId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalBillAmount { get; set; }
        public decimal DiscountedAmount { get; set; }
        public decimal BillToPayAmount { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Company Company { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
