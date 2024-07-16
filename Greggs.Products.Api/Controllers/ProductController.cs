using System.Collections.Generic;
using System.Linq;
using Greggs.Products.Api.DataAccess;
using Greggs.Products.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Greggs.Products.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly IDataAccess<Product> _dataAccess;
    private readonly ILogger<ProductController> _logger;

    public ProductController(ILogger<ProductController> logger, IDataAccess<Product> dataAccess)
    {
        _logger = logger;
        _dataAccess = dataAccess;
    }

    [HttpGet]
    public IEnumerable<ProductDto> Get(int pageStart = 0, int pageSize = 5, bool inEuros = false)
    {
        List<Product> products = _dataAccess.List(pageStart, pageSize, inEuros).ToList();

        return products
            .Where(product => product != null) // Filter out null products
            .Select(product => new ProductDto
            {
                Name = product.Name,
                Price = inEuros ? $"€{product.Price:F2}" : $"£{product.Price:F2}"
            });
    }
}