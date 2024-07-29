using System.Security.Claims;
using Humanizer;
using IdentityModel;
using IdentityService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace IdentityService.Pages.Register
{
    [SecurityHeaders]
    [AllowAnonymous]
    public class Index : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public Index(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        [BindProperty]
        public RegisterViewModel Input { get; set;}

        [BindProperty]
        public bool RegisterSuccess { get; set;}


        public IActionResult OnGet(string returnUrl)
        {
            Input = new RegisterViewModel
            {
                ReturnUrl = returnUrl,
            };
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            Console.WriteLine("yooooooooooooooooooooooooooooo");
            Console.WriteLine(Input.Password);
            Console.WriteLine(Input.Username);
            Console.WriteLine(Input.Email);
            Console.WriteLine(Input.Fullname);
            Console.WriteLine(ModelState.IsValid);
                if (ModelState.IsValid)
                {
                
                Console.WriteLine("step 1");
                var user = new ApplicationUser
                {
                    UserName = Input.Username,
                    Email = Input.Email,
                    EmailConfirmed = true
                };
                
                Console.WriteLine(user.Email);
                Console.WriteLine(user.UserName);
                var result = await _userManager.CreateAsync(user , Input.Password);
                Console.WriteLine(result.Succeeded);
                if (result.Succeeded)
                {
                    await _userManager.AddClaimsAsync(user , new Claim[]
                    {
                        new Claim(JwtClaimTypes.Name , Input.Fullname)
                    });
                    RegisterSuccess = true;
                };
                }
            
            return Page();
        }
    }
}