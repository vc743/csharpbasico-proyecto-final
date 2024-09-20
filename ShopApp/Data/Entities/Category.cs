using ShopApp.Data.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopApp.Data.Entities
{
    //[Table("Categories", Schema = "dbo" )]
    public sealed class Category : BaseEntity
    {
        //[Key]
        public int categoryid { get; set; }
        public string? categoryname { get; set; }
        public string? description { get; set; }
    }
}
