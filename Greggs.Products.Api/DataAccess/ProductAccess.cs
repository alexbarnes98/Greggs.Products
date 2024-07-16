using System;
using System.Collections.Generic;
using System.Linq;
using Greggs.Products.Api.Models;

namespace Greggs.Products.Api.DataAccess
{
    /// <summary>
    ///     DISCLAIMER: This is only here to help enable the purpose of this exercise, this doesn't reflect the way we work!
    ///     Hope not ;) - Alex
    /// </summary>
    public class ProductAccess : IDataAccess<Product>
    {
        private const decimal ExchangeRate = 1.11m;

        private static readonly IEnumerable<Product> ProductDatabase = new List<Product>
        {
            new() { Name = "Sausage Roll", Price = 1m },
            new() { Name = "Vegan Sausage Roll", Price = 1.1m },
            new() { Name = "Steak Bake", Price = 1.2m },
            new() { Name = "Yum Yum", Price = 0.7m },
            new() { Name = "Pink Jammie", Price = 0.5m },
            new() { Name = "Mexican Baguette", Price = 2.1m },
            new() { Name = "Bacon Sandwich", Price = 1.95m },
            new() { Name = "Coca Cola", Price = 1.2m }
        };

        public IEnumerable<Product> List(int? pageStart, int? pageSize, bool inEuros = false)
        {
            return ProductDatabase
                .Skip(pageStart ?? 0)
                .Take(pageSize ?? ProductDatabase.Count())
                .Select(product => new Product
                {
                    Name = product.Name,
                    Price = inEuros ? Math.Round(product.Price * ExchangeRate, 2) : product.Price
                })
                .AsEnumerable();

        }
    }
}