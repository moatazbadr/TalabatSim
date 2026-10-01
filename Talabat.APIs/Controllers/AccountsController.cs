using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.Domain.Entities.Identity;
using Talabat.Domain.Services;

namespace Talabat.APIs.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountsController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ITokenService _tokenService;
    private readonly ILogger<AccountsController> _Logger;
    private readonly IMapper _mapper;
    private readonly SignInManager<AppUser> _signInManager; //FOR LOGIN ==> the user tries to login with his email and password, we need to check if the password is correct or not, so we will use SignInManager 
    public AccountsController(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager, ILogger<AccountsController> logger, IMapper imapper)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _signInManager = signInManager;
        _Logger = logger;
        _mapper = imapper;
    }

    //1-Register 
    [HttpPost("Register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
        if (CheckEmailExists(registerDto.Email).Result.Value)
        {
            return BadRequest(new ApiResponse(400, "Email already exists"));
        }

        var user = new AppUser
        {
            UserName = registerDto.Email.Split("@")[0],
            Email = registerDto.Email,
            PhoneNumber = registerDto.PhoneNumber,
            DisplayName = registerDto.DisplayName
        };

        IdentityResult identityResult = await _userManager.CreateAsync(user, registerDto.Password);
        return identityResult.Succeeded ? new UserDto
        {
            DisplayName = user.DisplayName,
            Email = user.Email,
            token = await _tokenService.CreateToken(user, _userManager)  //implement token generation later
        } : BadRequest(new ApiResponse(400, "Problem in creating user"));

    }


    //2-Login
    [HttpPost("Login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        if (user == null) return Unauthorized(new ApiResponse(401, "Invalid Email or Password"));
        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
        if (!result.Succeeded) return Unauthorized(new ApiResponse(401, "Invalid Email or Password"));
        return new UserDto
        {
            DisplayName = user.DisplayName,
            Email = user.Email,
            token = await _tokenService.CreateToken(user, _userManager)
        };
    }



    //3-Get current User
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet("currentUser")]
    public async Task<ActionResult<UserDto>> GetCurrentUser()
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        if (email == null) return Unauthorized(new ApiResponse(401, "User not found"));
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null) return Unauthorized(new ApiResponse(401, "User not found"));
        return new UserDto
        {
            DisplayName = user.DisplayName,
            Email = user.Email,
            token = await _tokenService.CreateToken(user, _userManager)
        };
    }

    //4-get User Address
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet("GetUserAddress")]
    public async Task<ActionResult<UserAddressDto>> GetUserAddress()
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        if (email == null) return Unauthorized(new ApiResponse(401, "User not found"));
        var user = await _userManager.Users.Include(u => u.Address).FirstOrDefaultAsync(u => u.Email.Equals(email));
        if (user == null) return Unauthorized(new ApiResponse(401, "User not found"));
        if (user.Address == null) return NotFound(new ApiResponse(404, "Address not found"));
        return  _mapper.Map<UserAddressDto>(user.Address);
    }

    //5-UpdateAddress
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost("UpdateUserAddress")]
    public async Task<ActionResult<UserAddressDto>> UpdateUserAddress(UserAddressDto userAddressDto)
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        if (email == null) return Unauthorized(new ApiResponse(401, "User not found"));
        var user = await _userManager.Users.Include(u => u.Address).FirstOrDefaultAsync(u => u.Email.Equals(email));
        var MappedAddress = _mapper.Map<UserAddressDto ,UserAddress>(userAddressDto); 
        MappedAddress.Id =  user.Address.Id;
        user.Address = MappedAddress;
        var result =  await _userManager.UpdateAsync(user);
        if (!result.Succeeded) return BadRequest(new ApiResponse(400, "Problem in updating user address"));
        return Ok(userAddressDto);

    }


    [HttpGet("EmailExists")]
    public async Task<ActionResult<bool>> CheckEmailExists([FromQuery] string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        return user != null;
    }



}
