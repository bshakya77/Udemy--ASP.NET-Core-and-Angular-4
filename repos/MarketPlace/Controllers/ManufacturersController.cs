using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketPlace.Core.Models;
using MarketPlace.Persistence;
using MarketPlace.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace MarketPlace.Controllers
{
    public class ManufacturersController : Controller
    {
        private readonly MarketPlaceDbContext context;
        private readonly IMapper mapper;

        public ManufacturersController(MarketPlaceDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("/api/manufacturers")]
        public async Task<IEnumerable<ManufacturerVM>> Index()
        {
            var manufacturers = await context.Manufacturers.Include(m => m.Vehicles).ToListAsync();

            return mapper.Map<List<Manufacturer>, List<ManufacturerVM>>(manufacturers);
        }
    }
}