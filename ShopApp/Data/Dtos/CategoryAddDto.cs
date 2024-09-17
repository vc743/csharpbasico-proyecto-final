namespace ShopApp.Data.Dtos
{
    public record CategoryAddDto
    {
        public string? CategoryName { get; set; }
        public string? Description { get; set; }
        public DateTime CreationDate { get; set; }
        public int CreationUser { get; set; }
    }
}
