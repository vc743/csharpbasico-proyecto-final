﻿using ShopApp.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace ShopApp.Data.Entities
{
    public sealed class Customer : BaseEntity
    {
        [Key]
        public int custid { get; set; }
        public string? companyname { get; set; }
        public string? contactname { get; set; }
        public string? contacttitle { get; set; }
        public string? address { get; set; }
        public string? email { get; set; }
        public string? city { get; set; }
        public string? region { get; set; }
        public string? postalcode { get; set; }
        public string? country { get; set; }
        public string? phone { get; set; }
        public string? fax { get; set; }
    }
}
