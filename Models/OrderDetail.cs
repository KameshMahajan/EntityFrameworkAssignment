using System.ComponentModel.DataAnnotations;

namespace EFAssignment.Models
{
    public class OrderDetail
    {
        [Key]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal AmountToPay { get; set; }
        public int CustomerId { get; set; }


        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
        public virtual Customer Customer { get; set; }
    }
    public class OrderViewModel
    {
        public int CompanyId { get; set; }

        public int CustomerId { get; set; }
        public List<OrderDetailViewModel> OrderDetails { get; set; }
    }

    // OrderDetailViewModel
    public class OrderDetailViewModel
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int CustomerId { get; set; }
    }
}
