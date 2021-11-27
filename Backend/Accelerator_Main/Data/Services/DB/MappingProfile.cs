using AutoMapper;
using Data.Models.DB.Account;
using Data.Models.DB.Project;
using Data.Models.ModelViews.Account;
using Data.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Services.DB
{
    /// <summary>
    /// Мапирование 
    /// </summary>
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Мапирование 
        /// </summary>
        public MappingProfile()
        {
            CreateMap<UserRegister, User>();
            CreateMap<User, UserRegister>();

            CreateMap<UserCreation, User>();
            CreateMap<UserUpdateInfo, User>();

            #region Проекты
            CreateMap<ProjectData, FullProject>();

            CreateMap<ProjectData, Project>();
            CreateMap<ProjectData, Applicant>()
                .ForMember(p => p.Name, t => t.MapFrom(r => r.ApplicantName))
                .ForMember(p => p.LastName, t => t.MapFrom(r => r.ApplicantLastName))
                .ForMember(p => p.Role, t => t.MapFrom(r => r.ApplicantRole))
                .ForMember(p => p.Email, t => t.MapFrom(r => r.ApplicantEmail));

            CreateMap<ProjectData, Company>()
                .ForMember(p => p.Name, t => t.MapFrom(r => r.CompanyName))
                .ForMember(p => p.City, t => t.MapFrom(r => r.CompanyCity))
                .ForMember(p => p.Competence, t => t.MapFrom(r => r.CompanyCompetence))
                .ForMember(p => p.Country, t => t.MapFrom(r => r.CompanyCountry))
                .ForMember(p => p.Field, t => t.MapFrom(r => r.CompanyField))
                .ForMember(p => p.Inn, t => t.MapFrom(r => r.CompanyInn))
                .ForMember(p => p.People, t => t.MapFrom(r => r.CompanyPeople))
                .ForMember(p => p.Stage, t => t.MapFrom(r => r.CompanyStage))
                .ForMember(p => p.University, t => t.MapFrom(r => r.CompanyUniversity))
                .ForMember(p => p.Website, t => t.MapFrom(r => r.CompanyWebsite));
            #endregion
        }
    }
}