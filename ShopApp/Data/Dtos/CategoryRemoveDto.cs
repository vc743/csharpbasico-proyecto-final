namespace ShopApp.Data.Dtos
{
    public class CategoryRemoveDto
    {
        public int categoryid { get; set; }
        public bool Deleted { get; set; }
        public int DeletedUser { get; set; }
        public DateTime DeletedDate { get; set; }
    }
}
