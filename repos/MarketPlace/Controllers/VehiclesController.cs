using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MarketPlace.Core.Interfaces;
using MarketPlace.Core.Models;
using MarketPlace.Persistence;
using MarketPlace.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarketPlace.Controllers
{
    [Route("/api/vehicles")]
    public class VehiclesController : Controller
    {
        private readonly IMapper mapper;
        private readonly IVehicleRepository vehicleRepository;
        private readonly IUnitofWork unitofWork;

        public VehiclesController(IMapper mapper, IUnitofWork unitofWork, IVehicleRepository vehicleRepository)
        {
            this.mapper = mapper;
            this.unitofWork = unitofWork;
            this.vehicleRepository = vehicleRepository;  
        }

        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> CreateVehicle([FromBody] SaveVehicleVM vehicleModelVM)
        {
           //throw new Exception();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vehicleModel = mapper.Map<SaveVehicleVM, VehicleModel>(vehicleModelVM);

            vehicleModel.LastUpdate = DateTime.Now;

            vehicleRepository.AddVehicle(vehicleModel);

            await unitofWork.CompleteAsync();

            vehicleModel = await vehicleRepository.GetVehicle(vehicleModel.Id);

            var result = mapper.Map<VehicleModel, VehicleModelVM>(vehicleModel);

            return Ok(result);
        }

        [HttpPut("{id}")]
       // [Authorize]
        public async Task<IActionResult> UpdateVehicle(int id, [FromBody] SaveVehicleVM vehicleModelVM)
        {
            
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vehicleModel = await vehicleRepository.GetVehicle(id);

            if (vehicleModel == null)
                return NotFound();

            mapper.Map<SaveVehicleVM, VehicleModel>(vehicleModelVM, vehicleModel);

            vehicleModel.LastUpdate = DateTime.Now;

            await unitofWork.CompleteAsync();

            vehicleModel = await vehicleRepository.GetVehicle(vehicleModel.Id);

            var result = mapper.Map<VehicleModel, VehicleModelVM>(vehicleModel);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        //[Authorize]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            // throw new Exception();
            var vehicleModel = await vehicleRepository.GetVehicle(id, includeRelated: false);

            if (vehicleModel == null)
                return NotFound();

            vehicleRepository.RemoveVehicle(vehicleModel);
            await unitofWork.CompleteAsync();

            return Ok(id);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicle(int id)
        {
            var vehicleModel = await vehicleRepository.GetVehicle(id);
            
            if (vehicleModel == null)
                return NotFound();

            var vehicleVM = mapper.Map<VehicleModel, VehicleModelVM>(vehicleModel);

            return Ok(vehicleVM);

        }

        [HttpGet]
        public async Task<QueryResultVM<VehicleModelVM>> GetVehicles(VehicleQueryVM vehicleQueryVM)
        {
            var filter = mapper.Map<VehicleQueryVM, VehicleQuery>(vehicleQueryVM);
            var queryResult = await vehicleRepository.GetVehicles(filter);

            return mapper.Map<QueryResult<VehicleModel>, QueryResultVM<VehicleModelVM>>(queryResult);
        }
    }
}