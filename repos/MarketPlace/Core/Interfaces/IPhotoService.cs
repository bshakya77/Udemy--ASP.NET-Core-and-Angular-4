using MarketPlace.Core.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketPlace.Core.Interfaces
{
    public interface IPhotoService
    {
        Task<Photo> UploadPhoto(VehicleModel vehicleModel, IFormFile file, string uploadsFolderPath);
    }
}
