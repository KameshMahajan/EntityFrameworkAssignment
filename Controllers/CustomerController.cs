
using EFAssignment.Data;
using EFAssignment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFAssignment.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ApplicationContext context;

        public CustomerController(ApplicationContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            return View(context.Customers.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("FirstName,LastName,Gender,DateOfBirth,DateOfAnniversary,EmailId,Address,City")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                context.Add(customer);
                context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }
        public  IActionResult  EditCustomer(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer =   context.Customers.SingleOrDefault(x => x.Id == id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditCustomer(int id, [Bind("Id,FirstName,LastName,Gender,DateOfBirth,DateOfAnniversary,EmailId,Address,City")] Customer customer)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(customer);
                     context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.Id))
                    {
                        return NotFound();
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }
        private bool CustomerExists(int id)
        {
            return context.Customers.Any(e => e.Id == id);
        }


        
        public RedirectToActionResult DeleteCustomer(int id)
        {
            var customer = context.Customers.SingleOrDefault(e => e.Id == id);
            context.Customers.Remove(customer);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
