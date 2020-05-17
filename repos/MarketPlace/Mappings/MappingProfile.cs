using AutoMapper;
using MarketPlace.Core.Models;
using MarketPlace.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketPlace.Mappings
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            //Domain to API ViewModel
            CreateMap<Photo, PhotoVM>();
            CreateMap(typeof(QueryResult<>), typeof(QueryResultVM<>));
            CreateMap<Manufacturer, ManufacturerVM>();
            CreateMap<Manufacturer, KeyValuePairVM>();
            CreateMap<Vehicle, KeyValuePairVM>();
            CreateMap<Feature, KeyValuePairVM>();
            CreateMap<Vehicle, SaveVehicleVM>();

            CreateMap<VehicleModel, SaveVehicleVM>()
                .ForMember(vr => vr.Contact, opt => opt.MapFrom(v => new ContanctVM { Name = v.ContactName, Email = v.ContactEmail, Phone = v.ContactPhone}))
                .ForMember(vr => vr.Features, opt => opt.MapFrom(v => v.Features.Select(vf => vf.FeatureId)));

            CreateMap<VehicleModel, VehicleModelVM>()
                .ForMember(vm => vm.Manufacturer, opt => opt.MapFrom(v => v.Vehicle.Manufacturer))
                .ForMember(vm => vm.Contact, opt => opt.MapFrom(v => new ContanctVM { Name = v.ContactName, Email = v.ContactEmail, Phone = v.ContactPhone }))
                .ForMember(vm => vm.Features, opt => opt.MapFrom(v => v.Features.Select(vf => new KeyValuePairVM { Id = vf.Feature.Id, Name = vf.Feature.Name })));


            //API ViewModel to Domain
            CreateMap<VehicleQueryVM, VehicleQuery>();
            CreateMap<SaveVehicleVM, VehicleModel>()
                .ForMember(v => v.Id, opt => opt.Ignore())
                .ForMember(v => v.Features, opt => opt.Ignore()).AfterMap((vr, v) => {

                    //Remove unselected features

                    var removedFeatures = v.Features.Where(f => !vr.Features.Contains(f.FeatureId)).ToList();

                    foreach (var f in removedFeatures)
                        v.Features.Remove(f);

                    var addedFeatures = vr.Features.Where(id => !v.Features.Any(f => f.FeatureId == id)).Select(id => new VehicleFeature { FeatureId = id });

                    // Add new Features
                    foreach (var id in addedFeatures)
                        v.Features.Add(id);
                })
                .ForMember(v => v.ContactName, opt => opt.MapFrom(vm => vm.Contact.Name))
                .ForMember(v => v.ContactEmail, opt => opt.MapFrom(vm => vm.Contact.Email))
                .ForMember(v => v.ContactPhone, opt => opt.MapFrom(vm => vm.Contact.Phone))
                .ForMember(v => v.Features, opt => opt.MapFrom(vm => vm.Features.Select(id => new VehicleFeature { FeatureId = id })));
               
        }
    }
}
