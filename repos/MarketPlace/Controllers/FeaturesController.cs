using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MarketPlace.Core.Models;
using MarketPlace.Persistence;
using MarketPlace.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarketPlace.Controllers
{
    public class FeaturesController : Controller
    {
        private readonly MarketPlaceDbContext context;
        private readonly IMapper mapper;

        public FeaturesController(MarketPlaceDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("/api/features")]
        public async Task<IEnumerable<KeyValuePairVM>> Index()
        {
           var features = await context.Features.ToListAsync();

           return mapper.Map<List<Feature>, List<KeyValuePairVM>>(features);
        }
    }
}