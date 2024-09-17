using ShopApp.Data.Base;

namespace ShopApp.Data.Entities
{
    public sealed class Category : BaseEntity
    {
        public int categoryid { get; set; }
        public string? categoryname { get; set; }
        public string? description { get; set; }
    }
}
