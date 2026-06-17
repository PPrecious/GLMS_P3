using GLMS.Web.Models;
using GLMS.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace GLMS.Web.Controllers
{
    public class ClientController : Controller
    {
        private readonly ApiService _api;

        public ClientController(ApiService api)
        {
            _api = api;
        }

        public async Task<IActionResult> Index()
        {
            var client = _api.CreateClient();

            var clients = await client.GetFromJsonAsync<List<Client>>(
                "api/clients");

            return View(clients ?? new List<Client>());
        }

        public IActionResult Create()
        {
            ViewBag.Regions = Enum.GetValues(typeof(Region));
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Client model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Regions = Enum.GetValues(typeof(Region));
                return View(model);
            }

            var client = _api.CreateClient();

            var response = await client.PostAsJsonAsync(
                "api/clients",
                model);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(
                    "",
                    "Unable to create client.");

                ViewBag.Regions = Enum.GetValues(typeof(Region));
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var client = _api.CreateClient();

            var result = await client.GetFromJsonAsync<Client>(
                $"api/clients/{id}");

            if (result == null)
                return NotFound();

            return View(result);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = _api.CreateClient();

            await client.DeleteAsync(
                $"api/clients/{id}");

            return RedirectToAction(nameof(Index));
        }
    }
}