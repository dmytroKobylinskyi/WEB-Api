using Microsoft.AspNetCore.Mvc;
using Lab4.Models;
using Microsoft.AspNetCore.Cors;
using System.Collections.Generic;
using System.Linq;

namespace lab4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyCorsImplementationPolicy")]
    public class CustomerController : ControllerBase
    {
        public List<Customer> customers = new List<Customer>();

        public CustomerController()
        {
            using (PlumbingShopContext db = new PlumbingShopContext())
            {
                foreach (Customer g in db.Customers)
                {
                    customers.Add(g);
                }
            }

        }
        
        [HttpGet]
        public IEnumerable<Customer> Get() => customers;

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var customer = customers.SingleOrDefault(g => g.id_customer == id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var customer = customers.SingleOrDefault(g => g.id_customer == id);
            if (customer != null)
            {
                customers.Remove(customer);
                using (PlumbingShopContext db = new PlumbingShopContext())
                {
                    customer = db.Customers.SingleOrDefault(g => g.id_customer == id);
                    db.Customers.Remove(customer);
                    db.SaveChanges();
                }
            }
            return Ok();
        }

        private int NextCustomerId =>
            (int)(customers.Count == 0 ? 1 : customers.Max(x => x.id_customer) + 1);

        [HttpGet("GetNextCustomerId")]
        public int GetNextCustomerId()
        {
            return NextCustomerId;
        }

        [HttpPost]
        public IActionResult Post(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            customer.id_customer = NextCustomerId;
            customers.Add(customer);
            using (PlumbingShopContext db = new PlumbingShopContext())
            {
                db.Customers.Add(customer);
                db.SaveChanges();
            }
            return CreatedAtAction(nameof(Get), new { id = customer.id_customer }, customer);
        }

        [HttpPost("AddCustomer")]
        public IActionResult PostBody([FromBody] Customer customer) =>
                Post(customer);

        [HttpPut]
        public IActionResult Put(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var storedCustomer = customers.SingleOrDefault(g => g.id_customer == customer.id_customer);
            if (storedCustomer == null)
                return NotFound();
            storedCustomer.customer_phone = customer.customer_phone;
            storedCustomer.customer_surname = customer.customer_surname;
            storedCustomer.customer_name = customer.customer_name;
            storedCustomer.customer_city = customer.customer_city;

            using (PlumbingShopContext db = new PlumbingShopContext())
            {
                var customerDel = db.Customers.SingleOrDefault(g => g.id_customer == customer.id_customer);
                db.Customers.Remove(customerDel);
                db.Customers.Add(storedCustomer);
                db.SaveChanges();
            }
            return Ok(storedCustomer);
        }

        [HttpPut("UpdateCustomer")]
        public IActionResult PutBody([FromBody] Customer customer) =>
                Put(customer);
        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
