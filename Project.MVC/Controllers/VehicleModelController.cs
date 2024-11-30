using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project.MVC.Models.ViewModels;
using Project.Service;
using Project.Service.Models;

namespace Project.MVC.Controllers
{
    public class VehicleModelController : Controller
    {
        private readonly IVehicleService _vehicleService;
        private readonly IMapper _mapper;

        public VehicleModelController(IVehicleService vehicleService, IMapper mapper)
        {
            _vehicleService = vehicleService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string? searchQuery, string? sortBy, string? sortDirection, string? makeFilter, int pageSize = 5, int pageNumber = 1)
        {
            var totalRecords = await _vehicleService.GetVehicleModelCountAsync(searchQuery, makeFilter);
            var totalPages = Math.Ceiling((decimal)totalRecords / pageSize);

            if (pageNumber > totalPages)
            {
                pageNumber = (int)totalPages;
            }

            if (pageNumber < 1)
            {
                pageNumber = 1;
            }

            ViewBag.SearchQuery = searchQuery;
            ViewBag.MakeFilter = makeFilter;
            ViewBag.SortBy = sortBy;
            ViewBag.SortDirection = sortDirection;
            ViewBag.TotalPages = totalPages;
            ViewBag.PageSize = pageSize;
            ViewBag.PageNumber = pageNumber;
            ViewBag.HasPreviousPage = pageNumber > 1;
            ViewBag.HasNextPage = pageNumber < totalPages;
            var vehicleModels = await _vehicleService.GetAllVehicleModelsAsync(searchQuery, sortBy, sortDirection, makeFilter, pageSize, pageNumber);
            var viewModel = _mapper.Map<List<VehicleModelViewModel>>(vehicleModels);
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var makes = await _vehicleService.GetAllVehicleMakesForDropdownAsync();

            ViewBag.Makes = makes.Select(m => new SelectListItem
            {
                Value = m.Id.ToString(),
                Text = m.Name
            }).ToList();

            return View(new VehicleModelViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(VehicleModelViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var makes = await _vehicleService.GetAllVehicleMakesForDropdownAsync();
                ViewBag.Makes = makes.Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.Name
                }).ToList();
                TempData["ErrorMessage"] = "Invalid input. Please check your data.";
                return View(viewModel);
            }
            var model = _mapper.Map<VehicleModel>(viewModel);
            TempData["SuccessMessage"] = "Vehicle model added successfully!";
            await _vehicleService.AddVehicleModelAsync(model);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _vehicleService.GetVehicleModelByIdAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<VehicleModelViewModel>(model);

            var makes = await _vehicleService.GetAllVehicleMakesForDropdownAsync();
            ViewBag.Makes = makes.Select(m => new SelectListItem
            {
                Value = m.Id.ToString(),
                Text = m.Name
            }).ToList();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(VehicleModelViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var makes = await _vehicleService.GetAllVehicleMakesForDropdownAsync();
                ViewBag.Makes = makes.Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.Name
                }).ToList();

                TempData["ErrorMessage"] = "Invalid input. Please check your data.";
                return View(viewModel);
            }

            var model = _mapper.Map<VehicleModel>(viewModel);
            var result = await _vehicleService.UpdateVehicleModelAsync(model);

            if (result == null)
            {
                return NotFound(); 
            }
            TempData["SuccessMessage"] = "Vehicle model edited successfully!";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(VehicleModelViewModel viewModel)
        {
            var result = await _vehicleService.DeleteVehicleModelAsync(viewModel.Id);
            if (result == null)
            {
                return NotFound();
            }
            TempData["SuccessMessage"] = "Vehicle model deleted successfully!";
            return RedirectToAction(nameof(Index));
        }
    }
}
