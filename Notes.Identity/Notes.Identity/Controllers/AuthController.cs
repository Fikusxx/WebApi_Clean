using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Notes.Identity.Models;
using Notes.Identity.ViewModels;

namespace Notes.Identity.Controllers;

public class AuthController : Controller
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly SignInManager<ApplicationUser> signInManager;
    private readonly IIdentityServerInteractionService interactionService;

    public AuthController(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IIdentityServerInteractionService interactionService)
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
        this.interactionService = interactionService;
    }

    [HttpGet]
    public IActionResult Login(string returnUrl)
    {
        var loginVM = new LoginVM()
        {
            ReturnUrl = returnUrl,
        };

        return View(loginVM);
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginVM loginVM)
    {
        if (ModelState.IsValid == false)
            return View(loginVM);

        var user = await userManager.FindByNameAsync(loginVM.UserName);

         if (user == null)
        {
            ModelState.AddModelError("", $"User with {loginVM.UserName} doesnt exist.");
            return View(loginVM);
        }

        var result = await signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);

        if (result.Succeeded == false)
        {
            ModelState.AddModelError("", $"Login error");
            return View(loginVM);
        }

        return Redirect(loginVM.ReturnUrl);
    }


    [HttpGet]
    public IActionResult Register(string returnUrl)
    {
        var viewModel = new RegisterVM
        {
            ReturnUrl = returnUrl
        };

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterVM viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        var user = new ApplicationUser
        {
            UserName = viewModel.Username,
            FirstName = viewModel.Username,
            LastName = viewModel.Username
        };

        var result = await userManager.CreateAsync(user, viewModel.Password);

        if (result.Succeeded)
        {
            await signInManager.SignInAsync(user, false);
            return Redirect(viewModel.ReturnUrl);
        }

        ModelState.AddModelError(string.Empty, "Error occurred");

        return View(viewModel);
    }

    [HttpGet]
    public async Task<IActionResult> Logout(string logoutId)
    {
        await signInManager.SignOutAsync();
        var logoutRequest = await interactionService.GetLogoutContextAsync(logoutId);
        return Redirect(logoutRequest.PostLogoutRedirectUri);
    }
}
