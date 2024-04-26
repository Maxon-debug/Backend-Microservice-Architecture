using AUTH_SERVICE.Models.DTOS;
using AUTH_SERVICE.SERVICE.ISERVICE;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AUTH_SERVICE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _userService;
        private readonly ResponseDto _response;

        public UserController(IUser user)
        {
            _userService = user;
            _response = new ResponseDto();
        }
        [HttpPost]
        public async Task<ActionResult<ResponseDto>> RegisterUser(RegisterUserDto registerUserDto)
        {
            var res = await _userService.RegisterUser(registerUserDto);

            if (string.IsNullOrWhiteSpace(res))
            {
                //this was success
                _response.Result = "User Rgistered Successfully";
                return Created("", _response);
            }

            _response.Errormessage = res;
            _response.IsSuccess = false;
            return BadRequest(_response);
        }
        [HttpPost("login")]
        public async Task<ActionResult<ResponseDto>> loginUser(LoginRequestDto loginRequestDto)
        {
            var res = await _userService.loginUser(loginRequestDto);

            if (res.User != null)
            {
                //this was success
                _response.Result = res;
                return Created("", _response);
            }

            _response.Errormessage = "Invalid Credentials";
            _response.IsSuccess = false;
            return BadRequest(_response);
        }
        [HttpPost("AssignRole")]
        public async Task<ActionResult<ResponseDto>> AssignRole(AssignRoleDto role)
        {
            var res = await _userService.AssignUserRoles(role.Email, role.Role);

            if (res)
            {
                //this was success
                _response.Result = res;
                return Ok(_response);
            }

            _response.Errormessage = "Error Occured ";
            _response.Result = res;
            _response.IsSuccess = false;
            return BadRequest(_response);
        }
    }
}
