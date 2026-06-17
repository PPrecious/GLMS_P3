using GLMS.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Json;

namespace GLMS.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AccountController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var client =
                _httpClientFactory.CreateClient("GLMSApi");

            var response =
                await client.PostAsJsonAsync(
                    "api/auth/login",
                    model);

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error =
                    "Invalid Username or Password";

                return View(model);
            }

            var result =
                await response.Content
                .ReadFromJsonAsync<LoginResponse>();

            if (result == null)
            {
                ViewBag.Error =
                    "Unable to authenticate.";

                return View(model);
            }

            HttpContext.Session.SetString(
                "JWT",
                result.Token);

            return RedirectToAction(
                "Index",
                "Dashboard");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction(
                "Login",
                "Account");
        }
    }
}