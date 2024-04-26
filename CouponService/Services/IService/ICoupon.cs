using CouponService.Model;
using CouponService.Model.Dtos;

namespace CouponService.Services.IService
{
    public interface ICoupon
    {

        List<Coupon> GetAllCoupons();

        Coupon GetCoupon(Guid Id);

        
        string AddCoupon(Coupon coupon);


        string UpdateCoupon();

        string DeleteCoupon(Coupon coupon);
    }
}
