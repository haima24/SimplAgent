using SimplAgent.AITools.ReportingTool.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplAgent.AITools.ReportingTool.Services
{
    // Interface for DI
    public interface IProductSaleService
    {
        Task<IEnumerable<ProductSaleDto>> GetAllSales();
    }

    // Implementation
    public class ProductSaleService : IProductSaleService
    {
        public static List<ProductSaleDto> GetSampleSales()
        {
            return new List<ProductSaleDto>
    {
        new ProductSaleDto
        {
            ProductId = 1,
            ProductName = "Wireless Mouse",
            UnitsSold = 120,
            UnitPrice = 25.50m,
            SaleDate = DateTime.Today.AddDays(-5),
            Category = "Electronics"
        },
        new ProductSaleDto
        {
            ProductId = 2,
            ProductName = "Mechanical Keyboard",
            UnitsSold = 75,
            UnitPrice = 85.99m,
            SaleDate = DateTime.Today.AddDays(-3),
            Category = "Electronics"
        },
        new ProductSaleDto
        {
            ProductId = 3,
            ProductName = "Coffee Mug",
            UnitsSold = 200,
            UnitPrice = 9.99m,
            SaleDate = DateTime.Today.AddDays(-1),
            Category = "Home & Kitchen"
        },
        new ProductSaleDto
        {
            ProductId = 4,
            ProductName = "Ceramic Plate Set",
            UnitsSold = 50,
            UnitPrice = 29.99m,
            SaleDate = DateTime.Today.AddDays(-2),
            Category = "Home & Kitchen"
        },
        new ProductSaleDto
        {
            ProductId = 5,
            ProductName = "Kitchen Knife Set",
            UnitsSold = 35,
            UnitPrice = 49.50m,
            SaleDate = DateTime.Today.AddDays(-4),
            Category = "Home & Kitchen"
        }
    };
        }

        private readonly List<ProductSaleDto> _sales;

        public ProductSaleService()
        {
            // Load sample data
            _sales = GetSampleSales();
        }

        // Get all sales
        public async Task<IEnumerable<ProductSaleDto>> GetAllSales()
        {
            await Task.CompletedTask;
            return _sales;
        }
    }
}
