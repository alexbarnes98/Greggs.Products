using System.Collections.Generic;
using System.Linq;
using Greggs.Products.Api.Controllers;
using Greggs.Products.Api.DataAccess;
using Greggs.Products.Api.Models;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Greggs.Products.UnitTests;

public class ProductControllerTests
{
    private readonly ProductController _controller;
    private readonly Mock<IDataAccess<Product>> _mockDataAccess;
    private readonly Mock<ILogger<ProductController>> _mockLogger;

    public ProductControllerTests()
    {
        _mockDataAccess = new Mock<IDataAccess<Product>>();
        _mockLogger = new Mock<ILogger<ProductController>>();
        _controller = new ProductController(_mockLogger.Object, _mockDataAccess.Object);
    }

    [Fact]
    public void Get_ReturnsProductsInPounds()
    {
        // Arrange
        List<Product> products = new List<Product>
        {
            new() { Name = "Sausage Roll", Price = 1m },
            new() { Name = "Vegan Sausage Roll", Price = 1.1m }
        };
        _mockDataAccess.Setup(m => m.List(It.IsAny<int?>(), It.IsAny<int?>(), false)).Returns(products);

        // Act
        List<ProductDto> result = _controller.Get().ToList();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal("Sausage Roll", result[0].Name);
        Assert.Equal("£1.00", result[0].Price);
        Assert.Equal("Vegan Sausage Roll", result[1].Name);
        Assert.Equal("£1.10", result[1].Price);
    }

    [Fact]
    public void Get_ReturnsProductsInEuros()
    {
        // Arrange
        List<Product> products = new List<Product>
        {
            new() { Name = "Sausage Roll", Price = 1m },
            new() { Name = "Vegan Sausage Roll", Price = 1.1m }
        };
        _mockDataAccess.Setup(m => m.List(It.IsAny<int?>(), It.IsAny<int?>(), true)).Returns(products);

        // Act
        List<ProductDto> result = _controller.Get(0, 5, true).ToList();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal("Sausage Roll", result[0].Name);
        Assert.Equal("€1.00", result[0].Price);
        Assert.Equal("Vegan Sausage Roll", result[1].Name);
        Assert.Equal("€1.10", result[1].Price);
    }
}