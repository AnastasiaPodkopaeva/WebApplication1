using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using WebApplication1.Models;
using WebApplication1.ViewModels;

namespace CustomIdentityApp.Controllers
{
    public class UsersController : Controller
    {
        UserManager<User> _userManager;
        SignInManager<User> _signInManager;

        public UsersController(UserManager<User> userManager,SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index() => View(_userManager.Users.ToList());

        public IActionResult Create() => View();

        /*
        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User { Email = model.Email, UserName = model.Email, uname = model.uname };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            EditUserViewModel model = new EditUserViewModel { Id = user.Id, Email = user.Email, Year = user.Year };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    user.Email = model.Email;
                    user.UserName = model.Email;
                    user.Year = model.Year;

                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }
            return View(model);
        }
        */
        
        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var user1 = await _userManager.GetUserAsync(HttpContext.User);
                IdentityResult result = await _userManager.DeleteAsync(user);
                if(user==user1)
                {
                    await _signInManager.SignOutAsync();
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index");
        }
        /*
        [HttpPost]
        public async Task<ActionResult> SelectAll()
        {
            foreach (var user in _userManager.Users.ToList())
            {
                user.isActive = true;
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> Delete()
        {
            var users = _userManager.Users.ToList();
            foreach (var user in users)
            {
                if (user.isActive)
                {
                    var user1 = await _userManager.GetUserAsync(HttpContext.User);
                    IdentityResult result = await _userManager.DeleteAsync(user);
                    if (user == user1)
                    {
                        await _signInManager.SignOutAsync();
                        return RedirectToAction("Index", "Home");
                    }

                }
            }
            return RedirectToAction("Index", "Home");
        }
        */
        [HttpPost]
        public async Task<ActionResult> Lock(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user != null&&!user.isBlock)
            {
                var user1 = await _userManager.GetUserAsync(HttpContext.User);
                IdentityResult result = await _userManager.SetLockoutEnabledAsync(user, true);
                user.isBlock = true;
                var res = await _userManager.UpdateAsync(user);
                if (user == user1)
                {
                    await _signInManager.SignOutAsync();
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> UnLock(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user != null&&user.isBlock)
            {
                var user1 = await _userManager.GetUserAsync(HttpContext.User);
                IdentityResult result = await _userManager.SetLockoutEnabledAsync(user, false);
                user.isBlock = false;
                var res = await _userManager.UpdateAsync(user);
            }
            return RedirectToAction("Index");
        }
    }
}