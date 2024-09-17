using AutoMapper;
using GitLabApiClient.Models.Issues.Responses;
using GitLabApiClient.Models.Projects.Responses;
using GitLabApiClient.Models.Users.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStack.Domain.Entities;

namespace TechStack.Domain.Shared
{
    public class EntityProfile : Profile
    {
        public EntityProfile()
        {
            //.ForMember(destination => destination.Email, options => options.MapFrom(source => source.Email2))
            //.ForMember(destination => destination.ResourceName, options => options.MapFrom(source => (string.IsNullOrEmpty(source.LastName) ? source.FirstName : source.FirstName + " " + source.LastName)));
            // Use CreateMap... Etc.. here (Profile methods are the same as configuration methods)
        }
    }
}
