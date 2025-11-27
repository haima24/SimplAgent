using ModelContextProtocol.Server;
using SimplAgent.AITools.ReportingTool.Services;
using System.ComponentModel;
using System.Text.Json;

namespace SimplAgent.AITools.ReportingTool
{
    [McpServerToolType]
    public sealed class ReportingTool
    {
        private readonly IProductSaleService _productSaleService;
        public ReportingTool(IProductSaleService productSaleService)
        {
            _productSaleService = productSaleService;
        }

        [McpServerTool, Description("Get Company Sales")]
        public async Task<string> GetCompanySales()
        {
            var sales = await _productSaleService.GetAllSales();
            var serializedResult = JsonSerializer.Serialize(sales);
            return serializedResult;
        }
    }
}
