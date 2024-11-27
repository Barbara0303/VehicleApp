using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Index(string? searchQuery, string? sortBy, string? sortDirection, int pageSize = 3, int pageNumber = 1)
        {
            var totalRecords = await _vehicleService.CountAsync();
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
            var vehicleMakes = await _vehicleService.GetAllMakesAsync(searchQuery, sortBy, sortDirection, pageSize, pageNumber);
            var viewModel = _mapper.Map<List<VehicleMakeViewModel>>(vehicleMakes);
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(VehicleMakeViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var make = _mapper.Map<VehicleMake>(viewModel);
            await _vehicleService.CreateMakeAsync(make);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {

            var make = await _vehicleService.GetMakeByIdAsync(id);
            if (make != null)
            {
                var viewModel = _mapper.Map<VehicleMakeViewModel>(make);

                return View(viewModel);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(VehicleMakeViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var make = _mapper.Map<VehicleMake>(viewModel);
            var updatedMake = await _vehicleService.UpdateMakeAsync(make);

            if (updatedMake == null)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(VehicleMakeViewModel viewModel)
        {

            var make = await _vehicleService.DeleteMakeAsync(viewModel.Id);

            if (make != null)
            {
                return RedirectToAction(nameof(Index));
            }

            return NotFound();
        }
    }
}
