﻿// Copyright Information
// ==================================
// SpyStore.Hol - SpyStore.Hol.Models - OrderDetailBase.cs
// All samples copyright Philip Japikse
// http://www.skimedic.com 2020/03/07
// See License.txt for more information
// ==================================

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpyStore.Hol.Models.Entities.Base
{
    public class OrderDetailBase : EntityBase
    {
        [Required] public int OrderId { get; set; }

        [Required] public int ProductId { get; set; }

        [Required] public int Quantity { get; set; }

        [Required, DataType(DataType.Currency), Display(Name = "Unit Cost")]
        public decimal UnitCost { get; set; }
    }
}