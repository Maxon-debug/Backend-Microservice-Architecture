using CouponService.Data;
using CouponService.Model;
using CouponService.Model.Dtos;
using CouponService.Services.IService;
using static Azure.Core.HttpHeader;

namespace CouponService.Services
{
    public class CouponsService : ICoupon
    {
        private readonly ApplicationDbContext _context;
        public CouponsService(ApplicationDbContext context)
        {

            _context = context;

        }
        public string AddCoupon(Coupon coupon)
        {
            _context.Add(coupon);
            _context.SaveChanges();
            return "Coupon Added successifully";
        }

        public string DeleteCoupon(Coupon coupon)
        {
            _context.Remove(coupon);
            return "Coupon Deleted Successifully";
        }

        public List<Coupon> GetAllCoupons()
        {
            return _context.Coupons.ToList();
        }

        public Coupon GetCoupon(Guid Id)
        {
            return _context.Coupons.Where(x=>x.CouponId == Id).FirstOrDefault();
        }

        public string UpdateCoupon()
        {
            _context.SaveChanges();
            return "Coupon Updated Successfully";
        }
    }
}
