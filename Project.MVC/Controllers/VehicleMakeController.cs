using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.MVC.Models.ViewModels;
using Project.Service.Parameters;
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
        public async Task<IActionResult> Index(
            [FromQuery] FilteringParameters filteringParams,
            [FromQuery] SortingParameters sortingParams,
            [FromQuery] PagingParameters pagingParams)
        {
            var totalRecords = await _vehicleService.CountVehicleMakeAsync(filteringParams);
            var totalPages = Math.Ceiling((decimal)totalRecords / pagingParams.PageSize);

            if (pagingParams.PageNumber > totalPages)
            {
                pagingParams.PageNumber = (int)totalPages;
            }

            if (pagingParams.PageNumber < 1)
            {
                pagingParams.PageNumber = 1;
            }

            var vehicleMakes = await _vehicleService.GetAllVehicleMakesAsync(
                filteringParams,
                sortingParams,
                pagingParams);

            var viewModel = _mapper.Map<List<VehicleMakeViewModel>>(vehicleMakes);

            ViewBag.Filtering = filteringParams;
            ViewBag.Sorting = sortingParams;
            ViewBag.Paging = pagingParams;
            ViewBag.TotalPages = totalPages;
            ViewBag.HasPreviousPage = pagingParams.PageNumber > 1;
            ViewBag.HasNextPage = pagingParams.PageNumber < totalPages;

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
            var result = await _vehicleService.UpdateVehicleMakeAsync(make);

            if (!result)
            {
                return NotFound();
            }

            TempData["SuccessMessage"] = "Vehicle make updated successfully!";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {

            try
            {
                var result = await _vehicleService.DeleteVehicleMakeAsync(id);

                if (result)
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
