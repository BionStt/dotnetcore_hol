﻿// Copyright Information
// ==================================
// SpyStore.Hol - SpyStore.Hol.Models - Product.cs
// All samples copyright Philip Japikse
// http://www.skimedic.com 2020/03/07
// See License.txt for more information
// ==================================

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using SpyStore.Hol.Models.Entities.Base;

namespace SpyStore.Hol.Models.Entities
{
    [Table("Products", Schema = "Store")]
    public class Product : EntityBase
    {
        public ProductDetails Details { get; set; } = new ProductDetails();
        public bool IsFeatured { get; set; }
        [DataType(DataType.Currency)] public decimal UnitCost { get; set; }
        [DataType(DataType.Currency)] public decimal CurrentPrice { get; set; }
        public int UnitsInStock { get; set; }

        [Required] public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))] public Category CategoryNavigation { get; set; }

        [JsonIgnore, InverseProperty(nameof(ShoppingCartRecord.ProductNavigation))]
        public List<ShoppingCartRecord> ShoppingCartRecords { get; set; }
            = new List<ShoppingCartRecord>();

        [JsonIgnore, InverseProperty(nameof(OrderDetail.ProductNavigation))]
        public List<OrderDetail> OrderDetails { get; set; }
            = new List<OrderDetail>();

        [NotMapped] public string CategoryName => CategoryNavigation?.CategoryName;
    }
}