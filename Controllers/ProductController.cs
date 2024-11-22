using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using ProductWebAPI.Model;

namespace ProductWebAPI.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private static List<Product> products = new List<Product>
        {
            new Product { ProductID = 1, ProductName = "Apple Tab", Price = 100, Quantity = 10 },
            new Product { ProductID = 2, ProductName = "Apple iPhone 7s", Price = 75, Quantity = 5 },
            new Product { ProductID = 3, ProductName = "Nokia 10", Price = 45, Quantity = 4 },
            new Product { ProductID = 4, ProductName = "Window Surface Tab", Price = 125, Quantity = 15 }
        };

        // GET: api/v1/products/getall
        [HttpGet("getall")]
        public ActionResult<IEnumerable<Product>> GetAll()
        {
            return Ok(products);
        }

        // GET: api/v1/products/get/{id}
        [HttpGet("get/{id}")]
        public ActionResult<Product> Get(int id)
        {
            var prod = products.FirstOrDefault(x => x.ProductID == id);
            if (prod == null)
            {
                return NotFound();
            }
            return Ok(prod);
        }

        // POST: api/v1/products/add
        [HttpPost("add")]
        public ActionResult<Product> Add([FromBody] Product newProduct)
        {
            if (products.Any(x => x.ProductID == newProduct.ProductID))
            {
                return BadRequest("Product with the same ID already exists.");
            }
            products.Add(newProduct);
            return Ok(newProduct);
            //return Ok($"ProductID {newProduct.ProductID} added Successfully");
        }

        // PUT: api/v1/products/update/{id}
        [HttpPut("update/{id}")]
        public ActionResult Update(int id, [FromBody] Product updatedProduct)
        {
            var existingProduct = products.FirstOrDefault(x => x.ProductID == id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            existingProduct.ProductName = updatedProduct.ProductName;
            existingProduct.Price = updatedProduct.Price;
            existingProduct.Quantity = updatedProduct.Quantity;

            return Ok("Product Updated Successfully");
        }

        // DELETE: api/v1/products/delete/{id}
        [HttpDelete("delete/{id}")]
        public ActionResult Delete(int id)
        {
            var product = products.FirstOrDefault(x => x.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }
            products.Remove(product);
            return Ok("Product Deleted Successfully");
        }
    }
}
