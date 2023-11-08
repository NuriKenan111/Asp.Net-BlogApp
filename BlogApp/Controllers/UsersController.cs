using System.Security.Claims;
using BlogApp.Data.Abstract;
using BlogApp.Entity;
using BlogApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Controllers;

public class UsersController : Controller
{
    private readonly IUserRepository _userRepository;
    public UsersController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public IActionResult Login()
    {
        if(User.Identity!.IsAuthenticated)
            return RedirectToAction("Index","Posts");

        return View();
    }
   
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login");
    }
    [HttpPost]
    public async Task< IActionResult> Login(LoginViewModel loginViewModel)
    {
        if (ModelState.IsValid)
        {
            var isUser = await _userRepository.Users.FirstOrDefaultAsync(u => u.Email == loginViewModel.Email && u.Password == loginViewModel.Password);
            if(isUser != null)
            {
                var userClaims = new List<Claim>();
                userClaims.Add(new Claim(ClaimTypes.NameIdentifier, isUser.Id.ToString()));
                userClaims.Add(new Claim(ClaimTypes.Name, isUser.UserName ?? ""));
                userClaims.Add(new Claim(ClaimTypes.GivenName, isUser.Name ?? ""));
                userClaims.Add(new Claim(ClaimTypes.UserData, isUser.Image ?? ""));

                if(isUser.Email == "kenan@gmail.com"){
                    userClaims.Add(new Claim(ClaimTypes.Role, "admin"));
                }
                var identity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);
                
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true
                };

                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,new ClaimsPrincipal(identity),authProperties
                );
                return RedirectToAction("Index","Posts");
            }
            else{
                ModelState.AddModelError("","Email or Password is incorrect");
            }
        }
        
        return View(loginViewModel);
    }
    
    public IActionResult Register()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if(ModelState.IsValid){
            var user = await _userRepository.Users.FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);
            if(user == null){
                _userRepository.CreateUser(new User{
                    UserName = model.UserName,
                    Name = model.Name,
                    Email = model.Email,
                    Password = model.Password,
                    Image = "avatar.jpg"
                });
                return RedirectToAction("Login");
            }
            else{
                ModelState.AddModelError("","User already exists");
            }
        }
        return View(model);
    }
    public IActionResult Profile(string username){
        if(string.IsNullOrEmpty(username))
            return NotFound();
        
        var user = _userRepository
        .Users
        .Include(user => user.Posts)
        .Include(user => user.Comments)
        .ThenInclude(post => post.Post)
        .FirstOrDefault(user => user.UserName == username);
        if(user == null)
            return NotFound();

        return View(user);
    }
}