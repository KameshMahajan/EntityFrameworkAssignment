using EFAssignment.Data;
using EFAssignment.Models;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EFAssignment.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationContext context;

        public OrderController(ApplicationContext context)
        {
            this.context = context;
        }
        public async Task<IActionResult> Index()
        {
            //var orders = await context.Orders.Include(o => o.Customer).ToListAsync();
            return View(context.Orders.ToList());
        }

        public IActionResult Create()
        {
            // Populate ViewData or ViewBag with necessary data (e.g., customer list, product list)
             
            ViewData["Products"] = context.Products.ToList();
            ViewBag.CompanyId = new SelectList(context.Companies, "Id", "Name");
            ViewBag.ProductId=new SelectList(context.Products, "Id", "Name");
            ViewBag.Customer = new SelectList(context.Customers, "Id", "FirstName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(OrderViewModel orderViewModel)
        {
            if (ModelState.IsValid)
            {
                var order = new Order
                {
                    CompanyId = orderViewModel.CompanyId,
                    OrderDate = DateTime.Now,
                    CustomerId= orderViewModel.OrderDetails[0].CustomerId,
                    OrderDetails = new List<OrderDetail>()
                };
                foreach (var odViewModel in orderViewModel.OrderDetails)
                {
                    // Fetch the product from the database to get its price
                    var product = context.Products.Find(odViewModel.ProductId);

                    // Calculate amounts based on product price and quantity
                    var orderDetail = new OrderDetail
                    {
                        ProductId = odViewModel.ProductId,
                        Quantity = odViewModel.Quantity,
                        CustomerId = odViewModel.CustomerId,
                        TotalAmount = product.Price * odViewModel.Quantity,
                        DiscountAmount = ( product.CompanyId == 1) ? product.Price * 0.15m : (product.IsMobile = true && product.CompanyId == 2)? product.Price * 0.2m : 0,
                        AmountToPay= (product.Price * odViewModel.Quantity) - ((product.CompanyId == 1) ? product.Price * 0.15m : (product.IsMobile = true && product.CompanyId == 2) ? product.Price * 0.2m : 0),
                    };
                    order.OrderDetails.Add(orderDetail);
                }

                // Calculate total amounts for the order
                order.TotalBillAmount = order.OrderDetails.Sum(od => od.TotalAmount);
                order.DiscountedAmount = order.OrderDetails.Sum(od => od.DiscountAmount);
                order.BillToPayAmount = order.OrderDetails.Sum(od => od.AmountToPay);
                


                context.Orders.Add(order);
                context.SaveChanges();

                return RedirectToAction(nameof(Index)); 
            }


            return View(orderViewModel);
        }

        public IActionResult CustomerWise(int CustomerId)
        {
            var customerOrders = context.Orders
                .Where(o => o.CustomerId == CustomerId)  
                .ToList();

            return View("Index", customerOrders); 
        }
        public IActionResult OrderNumberWise(string orderNumber)
        {
            var orderWithDetails = context.Orders
                .Where(o => o.OrderNumber == orderNumber)
                .ToList();

            return View("Index", orderWithDetails); 
        }
        public IActionResult DateRange(DateTime startDate, DateTime endDate)
    {
        var ordersInDateRange = context.Orders
            .Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate)
            .ToList();

        return View("Index", ordersInDateRange);  
    }
    }
}

