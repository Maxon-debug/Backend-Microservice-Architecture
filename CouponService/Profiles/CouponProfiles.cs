using CouponService.Model.Dtos;
using CouponService.Model;
using AutoMapper;

namespace CouponService.Profiles
{
    public class CouponProfiles:Profile
    {
        public CouponProfiles()
        {
            CreateMap<AddCouponDto, Coupon>().ReverseMap();
        }
    }
}
