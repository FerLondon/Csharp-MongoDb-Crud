using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMongoI.Models;

namespace WebMongoI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly MongoDbService _mongoDbService;

        public SalesController(MongoDbService mongoDbService)
        {
            _mongoDbService = mongoDbService;
        }


        // Get all the products
        [HttpGet("get-all-products")]
        public IActionResult GetAllProducts()
        {
            try
            {
                var salesCollection = _mongoDbService.GetCollection<Sale>("sales");
                var products = salesCollection.Find(Builders<Sale>.Filter.Empty).ToList();
                
                if (products.Count == 0)
                {
                    return NotFound("Nenhum produto encontrado.");
                }
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        // Get product
        [HttpGet("get-product-by-id")]
        public IActionResult GetProductById(string id)
        {
            try
            {
                var salesCollection = _mongoDbService.GetCollection<Sale>("sales");

                // Create a filter to find the item by its ID
                var filter = Builders<Sale>.Filter.Eq(x => x.Id, id);

                // Find the first document that matches the filter
                var product = salesCollection.Find(filter).FirstOrDefault();

                if (product == null)
                {
                    return NotFound($"Produto com o ID '{id}' não encontrado.");
                }

                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        // Insert new item
        [HttpPost("insert-sale")]
        public IActionResult InsertSale()
        {
            var salesCollection = _mongoDbService.GetCollection<Sale>("sales");

            var newSale = new Sale
            {
                Item = "Asus Laptop I9",
                Quantity = 2,
                Price = 699,
                Date = DateTime.UtcNow
            };

            salesCollection.InsertOne(newSale);

            return Ok("Sale inserted successfully!");
        }


        // Delete item 
        [HttpDelete("{id}")]
        public IActionResult DeleteProductById(string id)
        {
            // Validar o formato do Id
            if (!MongoDB.Bson.ObjectId.TryParse(id, out _))
            {
                return BadRequest("Id is invalid.");
            }

            // Access to the collection
            var salesCollection = _mongoDbService.GetCollection<Sale>("sales");

            // Create filter
            var filter = Builders<Sale>.Filter.Eq(s => s.Id, id);

            // Delete
            var result = salesCollection.DeleteOne(filter);

            if (result.DeletedCount > 0)
            {
                return Ok("Product has been deleted!");
            }

            return NotFound("Product not found.");
        }


        // Update item
        [HttpPut("{id}", Name ="update-product")]
        public IActionResult UpdateProduct(string id, [FromBody] Sale updatedProduct)
        {
            // Try to validate the Id
            if (!MongoDB.Bson.ObjectId.TryParse(id, out _))
            {
                return BadRequest("Id is not valid.");
            }

            // Get the items
            var salesCollection = _mongoDbService.GetCollection<Sale>("sales");

            // Create filter for the Id
            var filter = Builders<Sale>.Filter.Eq(p => p.Id, id);

            // Update
            var updateResult = salesCollection.ReplaceOne(filter, updatedProduct);

            if (updateResult.MatchedCount == 0)
            {
                return NotFound("Produto not found");
            }

            return Ok("Product has been updated successfully!");
        }


    }
}
