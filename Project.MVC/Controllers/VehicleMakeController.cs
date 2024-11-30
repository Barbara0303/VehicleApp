using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.MVC.Models.ViewModels;
using Project.Service;
using Project.Service.Models;

namespace Project.MVC.Controllers
{
    public class VehicleMakeController : Controller
    {
        private readonly IVehicleService _vehicleService;
        private readonly IMapper _mapper;

        public VehicleMakeController(IVehicleService vehicleService, IMapper mapper)
        {
            _vehicleService = vehicleService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string? searchQuery, string? sortBy, string? sortDirection, int pageSize = 5, int pageNumber = 1)
        {
            var totalRecords = await _vehicleService.CountVehicleMakeAsync(searchQuery);
            var totalPages = Math.Ceiling((decimal)totalRecords / pageSize);

            if (pageNumber > totalPages)
            {
                pageNumber--;
            }

            if (pageNumber < 1)
            {
                pageNumber++;
            }

            ViewBag.SearchQuery = searchQuery;
            ViewBag.SortBy = sortBy;
            ViewBag.SortDirection = sortDirection;
            ViewBag.TotalPages = totalPages;
            ViewBag.PageSize = pageSize;
            ViewBag.PageNumber = pageNumber;
            ViewBag.HasPreviousPage = pageNumber > 1;
            ViewBag.HasNextPage = pageNumber < totalPages;
            var vehicleMakes = await _vehicleService.GetAllVehicleMakesAsync(searchQuery, sortBy, sortDirection, pageSize, pageNumber);
            var viewModel = _mapper.Map<List<VehicleMakeViewModel>>(vehicleMakes);
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(VehicleMakeViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Invalid input. Please check your data.";
                return View(viewModel);
            }
                var make = _mapper.Map<VehicleMake>(viewModel);
                await _vehicleService.CreateVehicleMakeAsync(make);
                TempData["SuccessMessage"] = "Vehicle make added successfully!";
                return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {

            var make = await _vehicleService.GetVehicleMakeByIdAsync(id);
            if (make != null)
            {
                var viewModel = _mapper.Map<VehicleMakeViewModel>(make);
                return View(viewModel);
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(VehicleMakeViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Invalid input. Please check your data.";
                return View(viewModel);
            }

            var make = _mapper.Map<VehicleMake>(viewModel);
            var updatedMake = await _vehicleService.UpdateVehicleMakeAsync(make);

            if (updatedMake == null)
            {
                return NotFound();
            }

            TempData["SuccessMessage"] = "Vehicle make updated successfully!";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(VehicleMakeViewModel viewModel)
        {

            try
            {
                var make = await _vehicleService.DeleteVehicleMakeAsync(viewModel.Id);

                if (make != null)
                {
                    TempData["SuccessMessage"] = "Vehicle make deleted successfully!";
                    return RedirectToAction(nameof(Index));
                }
                return NotFound();
            }
            catch (DbUpdateException)
            {
                TempData["ErrorMessage"] = "Unable to delete this vehicle make because it has associated vehicle models.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "An unexpected error occurred. Please try again.";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
