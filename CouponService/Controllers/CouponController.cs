using AutoMapper;
using Azure;
using CouponService.Model.Dtos;
using CouponService.Services;
using CouponService.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace CouponService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly ICoupon _couponService;
        private readonly IMapper _mapper;
        private readonly ResponseDto _response;
        public CouponController(ICoupon coupon, IMapper mapper)
        {
            _couponService = coupon;
            _mapper = mapper;
            _response = new ResponseDto();
        }
        [HttpGet]

        public ActionResult<ResponseDto> GetAllCoupons()
        {
            var coupons =  _couponService.GetAllCoupons();
            _response.Result = coupons;
            return Ok(_response);
        }
        [HttpGet("single/{Id}")]

        public ActionResult<ResponseDto> GetCoupon(Guid Id)
        {
            var coupon =  _couponService.GetCoupon(Id);
            if (coupon == null)
            {
                _response.Errormessage = "Coupon Not found";
                return NotFound(_response);
            }
            _response.Result = coupon;
            return Ok(_response);
        }
        

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult<ResponseDto> AddCoupon(AddCouponDto newCoupon)
        {
            var coupon = _mapper.Map<Model.Coupon>(newCoupon);
            var response =  _couponService.AddCoupon(coupon);
            _response.Result = response;

            //add coupon to stripe

            var options = new CouponCreateOptions()
            {
                AmountOff = (long)newCoupon.CouponAmount * 100,
                Currency = "kes",
                Id = newCoupon.CouponCode,
                Name = newCoupon.CouponCode
            };

            var service = new Stripe.CouponService();
            service.Create(options);

            return Created("", _response);
        }
        [HttpPut("{Id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult<ResponseDto> updateCoupon(Guid Id, AddCouponDto UCoupon)
        {
            var coupon =  _couponService.GetCoupon(Id);
            if (coupon == null)
            {
                _response.Result = "Coupon Not Found";
                _response.IsSuccess = false;
                return NotFound(_response);
            }
            _mapper.Map(UCoupon, coupon);
            var res =  _couponService.UpdateCoupon();
            var service = new Stripe.CouponService();
            service.Delete(coupon.CouponCode);

            var options = new CouponCreateOptions()
            {
                AmountOff = (long)UCoupon.CouponAmount * 100,
                Currency = "kes",
                Id = UCoupon.CouponCode,
                Name = UCoupon.CouponCode
            };

            service.Create(options);


            _response.Result = res;
            return Ok(_response);
        }

        [HttpDelete("{Id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult<ResponseDto> deleteCoupon(Guid Id)
        {
            var coupon =  _couponService.GetCoupon(Id);
            if (coupon == null)
            {
                _response.Result = "Coupon Not Found";
                _response.IsSuccess = false;
                return NotFound(_response);
            }
            var res = _couponService.DeleteCoupon(coupon);

            var service = new Stripe.CouponService();
            service.Delete(coupon.CouponCode);

            _response.Result = res;
            return Ok(_response);
        }
    }
}
