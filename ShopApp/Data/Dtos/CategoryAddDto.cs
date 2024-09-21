namespace ShopApp.Data.Dtos
{
    public record CategoryAddDto
    {
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? Description { get; set; }
        public DateTime creation_date { get; set; }
        public int creation_user { get; set; }
    }
}
