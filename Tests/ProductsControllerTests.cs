using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TestPCS.Controllers;
using TestPCS.Models;

namespace Tests;

public class ProductsControllerTests
{
    private IEnumerable<Product> GetTestProducts()
    {
        return new List<Product>
            {
                new() { Id = "1", Name = "Product 1", Description = "Description 1" },
                new() { Id = "2", Name = "Product 2", Description = "Description 2" }
            };
    }

    [Fact]
    public async Task GetAllProducts_ReturnsAllProducts()
    {
        var mockService = new Mock<IProductService>();
        mockService.Setup(service => service.GetAllProductsAsync()).ReturnsAsync(GetTestProducts());

        var controller = new ProductsController(mockService.Object);

        var result = await controller.GetAllProducts();

        var actionResult = Assert.IsType<OkObjectResult>(result.Result);
        var model = Assert.IsAssignableFrom<IEnumerable<ProductReadResponse>>(
            actionResult.Value);

        Assert.Equal(2, model.Count());
    }

    [Fact]
    public async Task GetProductById_ReturnsProduct()
    {
        var mockService = new Mock<IProductService>();
        mockService.Setup(service => service.GetProductByIdAsync("1")).ReturnsAsync(GetTestProducts().First());

        var controller = new ProductsController(mockService.Object);

        var result = await controller.GetProductById("1");

        var actionResult = Assert.IsType<OkObjectResult>(result.Result);
        var model = Assert.IsType<ProductReadResponse>(actionResult.Value);

        Assert.Equal("Product 1", model.Name);
    }

    [Fact]
    public async Task CreateProduct_ReturnsProduct()
    {
        var mockService = new Mock<IProductService>();
        var testProduct = new Product { Name = "Product 3", Description = "Description 3" };

        mockService.Setup(service => service.CreateProductAsync(It.IsAny<Product>()))
                .Returns(Task.CompletedTask);

        var controller = new ProductsController(mockService.Object);

        var request = new ProductCreateRequest("Product 3", "Description 3");
        var result = await controller.CreateProduct(request);

        var actionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var model = Assert.IsType<ProductCreateResponse>(actionResult.Value);

        Assert.Equal("Product 3", model.Name);
    }


    [Fact]
    public async Task UpdateProduct_ReturnsNoContent()
    {
        var mockService = new Mock<IProductService>();
        mockService.Setup(service => service.UpdateProductAsync("1", It.IsAny<Product>())).Returns(Task.CompletedTask);

        var controller = new ProductsController(mockService.Object);

        var result = await controller.UpdateProduct("1", new ProductUpdateRequest("Product 1", "Description 1"));

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteProduct_ReturnsNoContent()
    {
        var mockService = new Mock<IProductService>();
        mockService.Setup(service => service.DeleteProductAsync("1")).Returns(Task.CompletedTask);

        var controller = new ProductsController(mockService.Object);

        var result = await controller.DeleteProduct("1");

        Assert.IsType<NoContentResult>(result);
    }
}
