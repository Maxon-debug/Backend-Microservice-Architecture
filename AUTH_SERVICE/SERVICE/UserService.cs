using AUTH_SERVICE.DATA;
using AUTH_SERVICE.Models;
using AUTH_SERVICE.Models.DTOS;
using AUTH_SERVICE.SERVICE.ISERVICE;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AUTH_SERVICE.SERVICE
{
    public class UserService : IUser
    {
        private readonly SystemDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<SystemUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwt _JwtServices;
        public UserService(SystemDbContext applicationDbContext, IMapper mapper, UserManager<SystemUser> userManager,
            RoleManager<IdentityRole> roleManager, IJwt jwtService)
        {
            _context = applicationDbContext;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            _JwtServices = jwtService;
        }
        public async Task<bool> AssignUserRoles(string Email, string RoleName)
        {
            var user = await _context.ApplicationUsers.Where(x => x.Email.ToLower() == Email.ToLower()).FirstOrDefaultAsync();
            //does  the user exist?
            if (user == null)
            {
                return false;
            }
            else
            {
                //does the role exist 
                if (!_roleManager.RoleExistsAsync(RoleName).GetAwaiter().GetResult())
                {
                    //create the role 
                    await _roleManager.CreateAsync(new IdentityRole(RoleName));
                    
                }

                //assign the user the role
                await _userManager.AddToRoleAsync(user, RoleName);
                
                return true;
            }
        }

        public async Task<LoginResponseDto> loginUser(LoginRequestDto loginRequestDto)
        {
            var user = await _context.ApplicationUsers.Where(x => x.UserName.ToLower() == loginRequestDto.UserName.ToLower()).FirstOrDefaultAsync();
            //compare hashed password with plain text password 
            var isValid = _userManager.CheckPasswordAsync(user, loginRequestDto.Password).GetAwaiter().GetResult();

            if (!isValid || user == null)
            {
                //if username or password or the two are wrong
                return new LoginResponseDto();
            }
            var loggeduser = _mapper.Map<UserDto>(user);

            // var roles = user.Role;
            var roles = await _userManager.GetRolesAsync(user);

            var token = _JwtServices.GenerateToken(user, roles);

            var response = new LoginResponseDto()
            {
                User = loggeduser,
                Token = token
            };

            return response;

        }

        public async Task<string> RegisterUser(RegisterUserDto registerUserDto)
        {
            try
            {
                var user = _mapper.Map<SystemUser>(registerUserDto);

                //create user
                var result = await _userManager.CreateAsync(user, registerUserDto.Password);

                //if this succeeded
                if (result.Succeeded)
                {


                    return "User Added Successifuly";
                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }    
        
}
