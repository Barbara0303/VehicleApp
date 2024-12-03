using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project.MVC.Models.ViewModels;
using Project.Service;
using Project.Service.Parameters;
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
        public async Task<IActionResult> Index(
            [FromQuery] FilteringParameters filteringParams,
            [FromQuery] SortingParameters sortingParams,
            [FromQuery] PagingParameters pagingParams)
        {
            var totalRecords = await _vehicleService.GetVehicleModelCountAsync(filteringParams);
            var totalPages = Math.Ceiling((decimal)totalRecords / pagingParams.PageSize);

            if (pagingParams.PageNumber > totalPages)
            {
                pagingParams.PageNumber = (int)totalPages;
            }

            if (pagingParams.PageNumber < 1)
            {
                pagingParams.PageNumber = 1;
            }

            var vehicleModels = await _vehicleService.GetAllVehicleModelsAsync(filteringParams, sortingParams, pagingParams);
            var viewModel = _mapper.Map<List<VehicleModelViewModel>>(vehicleModels);
            ViewBag.Filtering = filteringParams;
            ViewBag.Sorting = sortingParams;
            ViewBag.Paging = pagingParams;
            ViewBag.TotalPages = totalPages;
            ViewBag.HasPreviousPage = pagingParams.PageNumber > 1;
            ViewBag.HasNextPage = pagingParams.PageNumber < totalPages;

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

            if (!result)
            {
                return NotFound(); 
            }
            TempData["SuccessMessage"] = "Vehicle model edited successfully!";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _vehicleService.DeleteVehicleModelAsync(id);
            if (!result)
            {
                return NotFound();
            }
            TempData["SuccessMessage"] = "Vehicle model deleted successfully!";
            return RedirectToAction(nameof(Index));
        }
    }
}
