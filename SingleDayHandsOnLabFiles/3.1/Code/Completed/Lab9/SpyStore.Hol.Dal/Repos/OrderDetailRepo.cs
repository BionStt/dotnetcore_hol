﻿// Copyright Information
// ==================================
// SpyStore.Hol - SpyStore.Hol.Dal - OrderDetailRepo.cs
// All samples copyright Philip Japikse
// http://www.skimedic.com 2020/03/07
// See License.txt for more information
// ==================================

using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SpyStore.Hol.Dal.EfStructures;
using SpyStore.Hol.Dal.Repos.Base;
using SpyStore.Hol.Dal.Repos.Interfaces;
using SpyStore.Hol.Models.Entities;
using SpyStore.Hol.Models.ViewModels;

namespace SpyStore.Hol.Dal.Repos
{
    public class OrderDetailRepo : RepoBase<OrderDetail>, IOrderDetailRepo
    {
        public OrderDetailRepo(StoreContext context) : base(context)
        {
        }

        internal OrderDetailRepo(DbContextOptions<StoreContext> options) : base(options)
        {
        }

        public IEnumerable<OrderDetailWithProductInfo> GetOrderDetailsWithProductInfoForOrder(int orderId)
            => Context
                .OrderDetailWithProductInfos
                .Where(x => x.OrderId == orderId)
                .OrderBy(x => x.ModelName);
    }
}