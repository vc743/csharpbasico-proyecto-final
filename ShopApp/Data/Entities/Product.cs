using ShopApp.Data.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopApp.Data.Entities
{
    public sealed class Product : BaseEntity
    {
        [Key]
        public int productid { get; set; }
        public string? productname { get; set; }
        public int supplierid {  get; set; }
        public int categoryid { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal unitprice { get; set; }
        public bool discontinued { get; set; }

    }
}
