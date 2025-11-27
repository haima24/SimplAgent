namespace SimplAgent.AITools.ReportingTool.Dto
{
    // DTO representing a sold product
    public class ProductSaleDto
    {
        /// <summary>
        /// Unique product identifier
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Name of the product
        /// </summary>
        public string ProductName { get; set; } = string.Empty;

        /// <summary>
        /// Number of units sold
        /// </summary>
        public int UnitsSold { get; set; }

        /// <summary>
        /// Price per unit
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Total sales amount = UnitsSold * UnitPrice
        /// </summary>
        public decimal TotalAmount => UnitsSold * UnitPrice;

        /// <summary>
        /// Date of the sale
        /// </summary>
        public DateTime SaleDate { get; set; }

        /// <summary>
        /// Product category (optional)
        /// </summary>
        public string Category { get; set; } = string.Empty;
    }
}
