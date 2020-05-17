using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MarketPlace.Core.Interfaces;
using MarketPlace.Core.Models;
using MarketPlace.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace MarketPlace.Controllers
{
    [Route("/api/vehicles/{vehicleId}/photos")]
    public class PhotosController : Controller
    {
        private readonly IHostingEnvironment host; //get root directories info

        private readonly IMapper mapper;

        private readonly PhotoSettings photoSettings;

        private readonly IVehicleRepository vehicleRepository;

        private readonly IPhotoService photoService;

        private readonly IPhotoRepository photoRepository;

        public PhotosController(IHostingEnvironment host, IMapper mapper,
                               IOptionsSnapshot<PhotoSettings> options, 
                               IVehicleRepository vehicleRepository,
                               IPhotoRepository photoRepository,
                               IPhotoService photoService
            )
        {
            this.host = host;
            this.mapper = mapper;
            this.photoSettings = options.Value;
            this.photoService = photoService;
            this.photoRepository = photoRepository;
            this.vehicleRepository = vehicleRepository;
        }
        
        [HttpGet]
        public async Task<IEnumerable<PhotoVM>> GetPhotos(int vehicleId)
        {
            var photos = await photoRepository.GetPhotos(vehicleId);
            
            return mapper.Map<IEnumerable<Photo>, IEnumerable<PhotoVM>>(photos);
        }

        [HttpPost]
        public async Task<IActionResult> Upload(int vehicleId, IFormFile file)
        {
            var vehicleModel = await vehicleRepository.GetVehicle(vehicleId, includeRelated: false);
            if (vehicleModel == null)
                return NotFound();

            if (file == null) return BadRequest("Null file");
            if (file.Length == 0) return BadRequest("Empty file");
            if (file.Length > photoSettings.MaxBytes) return BadRequest("Max file size exceeded.");
            if (!photoSettings.IsSupported(file.FileName)) return BadRequest("Invalid file type.");

            var uploadsFolderPath = Path.Combine(host.WebRootPath, "uploads");
            var photo = await photoService.UploadPhoto(vehicleModel, file, uploadsFolderPath);

            return Ok(mapper.Map<Photo, PhotoVM>(photo));

        }
    }
}