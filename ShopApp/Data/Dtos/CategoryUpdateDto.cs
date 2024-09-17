namespace ShopApp.Data.Dtos
{
    public record CategoryUpdateDto
    {
        public int categoryid { get; set; }
        public string? CategoryName { get; set; }
        public string? Description { get; set; }
        public DateTime ModifyDate { get; set; }
        public DateTime ModifyUser { get; set; }
    }
}
