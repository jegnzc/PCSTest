using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using TestPCS.Models;

namespace TestPCS.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController(IProductService productService) : ControllerBase
{
    private readonly IProductService _productService = productService;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductReadResponse>>> GetAllProducts()
    {
        var products = await _productService.GetAllProductsAsync();

        return Ok(products.Select(p => new ProductReadResponse(p.Id, p.Name, p.Description)));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductReadResponse>> GetProductById(string id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        if (product == null)
            return NotFound();

        return Ok(new ProductReadResponse(product.Id, product.Name, product.Description));
    }

    [HttpPost]
    public async Task<ActionResult<ProductCreateResponse>> CreateProduct([FromBody] ProductCreateRequest request)
    {
        var product = new Product { Name = request.Name, Description = request.Description };

        await _productService.CreateProductAsync(product);

        return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, new ProductCreateResponse(product.Id, product.Name, product.Description));
    }


    [HttpPut("{id}")]
    public async Task<ActionResult<ProductUpdateResponse>> UpdateProduct(string id, [FromBody] ProductUpdateRequest request)
    {
        var product = new Product { Name = request.Name, Description = request.Description };

        await _productService.UpdateProductAsync(id, product);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProduct(string id)
    {
        await _productService.DeleteProductAsync(id);
        return NoContent();
    }
}
