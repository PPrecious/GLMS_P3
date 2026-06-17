using GLMS.Web.Models;
using GLMS.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace GLMS.Web.Controllers
{
    public class ContractController : SecureController
    {
        private readonly ApiService _api;

        public ContractController(ApiService api)
        {
            _api = api;
        }

        public async Task<IActionResult> Index(
            DateTime? startDate,
            DateTime? endDate,
            string? status)
        {
            var client = _api.CreateClient();

            string url =
                $"api/contracts?startDate={startDate}&endDate={endDate}&status={status}";

            var contracts =
                await client.GetFromJsonAsync<List<Contract>>(url);

            return View(contracts ?? new List<Contract>());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Contract contract)
        {
            if (!ModelState.IsValid)
                return View(contract);

            var client = _api.CreateClient();

            var response =
                await client.PostAsJsonAsync(
                    "api/contracts",
                    contract);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(
                    "",
                    "Unable to create contract.");

                return View(contract);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var client = _api.CreateClient();

            var contract =
                await client.GetFromJsonAsync<Contract>(
                    $"api/contracts/{id}");

            if (contract == null)
                return NotFound();

            return View(contract);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var client = _api.CreateClient();

            var contract =
                await client.GetFromJsonAsync<Contract>(
                    $"api/contracts/{id}");

            if (contract == null)
                return NotFound();

            return View(contract);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = _api.CreateClient();

            await client.DeleteAsync(
                $"api/contracts/{id}");

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> UpdateStatus(
            int id,
            string status)
        {
            var client = _api.CreateClient();

            var response =
                await client.PatchAsJsonAsync(
                    $"api/contracts/{id}/status",
                    status);

            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] =
                    "Unable to update contract status.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}