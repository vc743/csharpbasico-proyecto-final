namespace ShopApp.Data.Dtos
{
    public record CustomerRemoveDto
    {
        public int custid { get; set; }
        public bool deleted { get; set; }
        public int delete_user { get; set; }
        public DateTime delete_date { get; set; }
    }
}
