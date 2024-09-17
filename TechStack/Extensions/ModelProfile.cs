using AutoMapper;

namespace TechStack.Extensions
{
    public class ModelProfile : Profile
    {
        public ModelProfile()
        {
            //CreateMap<GitProject, ProjectViewModel>().ReverseMap();
            //CreateMap<TRMProject, TRMProjectViewModel>()
            //    .ForMember(destination => destination.ProjectId, options => options.MapFrom(source => source.Id))
            //    .ForMember(destination => destination.ProjectManagerId, options => options.MapFrom(source => source.ProjectManager))
            //    .ForMember(destination => destination.ProjectName, options => options.MapFrom(source => source.Name));
        }
    }
}
