using JobBoard.Application.DTOs;
using JobBoard.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using LoginRequest = JobBoard.Application.DTOs.LoginRequest;

namespace JobBoard.API.Controllers;

[ApiController]
[Route("api/v1/auths")]
public class AuthController : ControllerBase
{

    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    
    [HttpPost("set-password")]
    public async Task<ActionResult<TokenResponse>> SetPassword([FromBody] LoginRequest request)
    {
        await _authService.SetPassword(request);
        return Ok();
    }
    
    [HttpPost("login")]
    public async Task<ActionResult<TokenResponse>> Login([FromBody] LoginRequest request)
    {
        return Ok(await _authService.GenerateJwtTokenAsync(request));
    }
    
}