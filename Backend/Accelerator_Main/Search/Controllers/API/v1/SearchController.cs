using AutoMapper;
using Data.Attributes;
using Data.Extensions;
using Data.Interfaces.Repositories;
using Data.Models.DB.Project;
using Data.Models.Services;
using Data.Services.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Microsoft.Extensions.Options;
using Search_Data.Models;
using Search_Data.Search;
using Swashbuckle.AspNetCore.Annotations;

namespace Search.Controllers.API.v1
{
    /// <summary>
    /// Контроллер заявок
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [SetRoute]
    public class SearchController : Controller
    {
        protected IBaseEntityRepository<Applicant> _applicantsRepository;
        protected IBaseEntityRepository<Company> _companiesRepository;
        protected IBaseEntityRepository<Project> _projectsRepository;
        protected IBaseEntityRepository<FullProject> _fullProjectRepository;

        private readonly PathConfig _pathConfig;
        protected readonly EmailService _emailService;
        private readonly ILogger<IndexModel> _logger;
        protected readonly UserManager _userManager;
        protected readonly IMapper _mapper;

        public SearchController(IBaseEntityRepository<Applicant> applicantsRepository,
            IBaseEntityRepository<Company> companiesRepository,
            IBaseEntityRepository<Project> projectsRepository,
            IBaseEntityRepository<FullProject> fullProjectRepository,
            IOptions<PathConfig> pathConfig,
            EmailService emailService,
            ILogger<IndexModel> logger,
            UserManager userManager,
            IMapper mapper)
        {
            _applicantsRepository = applicantsRepository;
            _companiesRepository = companiesRepository;
            _projectsRepository = projectsRepository;
            _fullProjectRepository = fullProjectRepository;
            _emailService = emailService;
            _userManager = userManager;
            _mapper = mapper;
            _logger = logger;
            _pathConfig = pathConfig.Value;
        }


    }
}
