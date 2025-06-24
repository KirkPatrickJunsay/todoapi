using MediatR;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Application.Common.Interfaces;
using TodoApp.Application.DTOs;
using TodoApp.Application.Features.TodoItems.Commands;
using TodoApp.Application.Features.TodoItems.Queries;

namespace TodoApp.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IJwtTokenGenerator _tokenGenerator;

    public AuthController(IJwtTokenGenerator tokenGenerator)
    {
        _tokenGenerator = tokenGenerator;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        if (request.Username == "admin" && request.Password == "password")
        {
            var token = _tokenGenerator.GenerateToken("1", request.Username);
            return Ok(new { Token = token });
        }

        return Unauthorized();
    }
}

public class LoginRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
}
