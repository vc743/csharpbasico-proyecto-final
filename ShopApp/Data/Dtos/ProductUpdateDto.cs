namespace ShopApp.Data.Dtos
{
    public record ProductUpdateDto
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public int SupplierId { get; set; }
        public int CategoryId { get; set; }
        public decimal UnitPrice { get; set; }
        public bool Discontinued { get; set; }
        public DateTime modify_date { get; set; }
        public int modify_user { get; set; }
    }
}
