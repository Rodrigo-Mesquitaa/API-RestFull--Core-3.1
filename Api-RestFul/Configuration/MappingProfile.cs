using Api_RestFul.Models;
using Api_RestFul.ViweModels;
using AutoMapper;

namespace Api_RestFul.Configuration
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<Applicant, ApplicantViewModel>();
        }
    }
}
