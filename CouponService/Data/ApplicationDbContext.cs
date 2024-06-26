﻿using CouponService.Model;
using Microsoft.EntityFrameworkCore;

namespace CouponService.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Coupon> Coupons { get; set; }
    }
}
