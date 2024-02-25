using AutoMapper;
using Bookify.Core.Models;

namespace Bookify.Core.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Category
            CreateMap<Category,CategoryViewModel>();

            CreateMap<CategoryFormViewModel,Category>().ReverseMap();

        }
    }
}
