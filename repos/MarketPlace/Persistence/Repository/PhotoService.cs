using MarketPlace.Core.Interfaces;
using MarketPlace.Core.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketPlace.Persistence.Repository
{
    public class PhotoService : IPhotoService
    {
        private readonly IUnitofWork unitofWork;
        private readonly IPhotoStorage photoStorage;

        public PhotoService(IUnitofWork unitofWork, IPhotoStorage photoStorage)
        {
            this.unitofWork = unitofWork;
            this.photoStorage = photoStorage;
        }
        
        public async Task<Photo> UploadPhoto(VehicleModel vehicleModel, IFormFile file, string uploadsFolderPath)
        {
            var fileName = await photoStorage.StorePhoto(uploadsFolderPath, file);

            var photo = new Photo { FileName = fileName };
            vehicleModel.Photos.Add(photo);
            await unitofWork.CompleteAsync();

            return photo;
        }
    }
}
