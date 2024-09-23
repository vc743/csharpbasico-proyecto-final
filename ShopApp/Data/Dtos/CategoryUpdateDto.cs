namespace ShopApp.Data.Dtos
{
    public record CategoryUpdateDto
    {
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? Description { get; set; }
        public DateTime modify_date { get; set; }
        public int modify_user { get; set; }
    }
}
