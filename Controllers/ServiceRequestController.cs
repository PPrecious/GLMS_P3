using GLMS.Web.Models;
using GLMS.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace GLMS.Web.Controllers
{
    public class ServiceRequestController : SecureController
    {
        private readonly ApiService _api;

        public ServiceRequestController(ApiService api)
        {
            _api = api;
        }

        public async Task<IActionResult> Index()
        {
            var client = _api.CreateClient();

            var requests =
                await client.GetFromJsonAsync<List<ServiceRequest>>(
                    "api/servicerequests");

            return View(requests ?? new List<ServiceRequest>());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ServiceRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var client = _api.CreateClient();

            var response =
                await client.PostAsJsonAsync(
                    "api/servicerequests",
                    request);

            if (!response.IsSuccessStatusCode)
            {
                var error =
                    await response.Content.ReadAsStringAsync();

                ModelState.AddModelError(
                    "",
                    error);

                return View(request);
            }

            TempData["Success"] =
                "Service Request created successfully.";

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var client = _api.CreateClient();

            var requests =
                await client.GetFromJsonAsync<List<ServiceRequest>>(
                    "api/servicerequests");

            var request =
                requests?.FirstOrDefault(r => r.Id == id);

            if (request == null)
                return NotFound();

            return View(request);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var client = _api.CreateClient();

            var requests =
                await client.GetFromJsonAsync<List<ServiceRequest>>(
                    "api/servicerequests");

            var request =
                requests?.FirstOrDefault(r => r.Id == id);

            if (request == null)
                return NotFound();

            return View(request);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = _api.CreateClient();

            await client.DeleteAsync(
                $"api/servicerequests/{id}");

            return RedirectToAction(nameof(Index));
        }
    }
}