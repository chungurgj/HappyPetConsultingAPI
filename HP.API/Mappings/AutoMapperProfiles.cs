using AutoMapper;
using HP.API.Models.Domain;
using HP.API.Models.DTOs;

namespace HP.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Pet,PetDto>().ReverseMap();
            CreateMap<Pet, AddPetRequestDto>().ReverseMap();
            CreateMap<Product,AddProductRequestDto>().ReverseMap();
            CreateMap<Product,ProductDto>().ReverseMap();  
            CreateMap<VetVisit,VetVisitDto>().ReverseMap();
            CreateMap<Consultation,ConsultationDto>().ReverseMap();
        }
    }
}
