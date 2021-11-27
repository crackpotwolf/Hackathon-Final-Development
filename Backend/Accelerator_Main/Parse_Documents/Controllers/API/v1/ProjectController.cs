using AutoMapper;
using Data.Attributes;
using Data.Interfaces.Repositories;
using Data.Models.DB.Project;
using Data.Models.Services;
using Data.Services.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Swashbuckle.AspNetCore.Annotations;

namespace Parse_Documents.Controllers.API.v1
{
    /// <summary>
    /// Контроллер заявок
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [SetRoute]
    public class ProjectController : Controller
    {
        protected IBaseEntityRepository<Applicant> _applicantsRepository;
        protected IBaseEntityRepository<Company> _companiesRepository;
        protected IBaseEntityRepository<Project> _projectsRepository;
        protected IBaseEntityRepository<FullProject> _fullProjectRepository;

        protected readonly EmailService _emailService;
        private readonly ILogger<IndexModel> _logger;
        protected readonly UserManager _userManager;
        protected readonly IMapper _mapper;

        public ProjectController(IBaseEntityRepository<Applicant> applicantsRepository,
            IBaseEntityRepository<Company> companiesRepository,
            IBaseEntityRepository<Project> projectsRepository,
            IBaseEntityRepository<FullProject> fullProjectRepository,
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
        }

        [HttpGet("all")]
        [SwaggerResponse(200, "Токен", typeof(Project))]
        [SwaggerResponse(500, "Неизвестная ошибка")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _projectsRepository.GetListQuery()
                    .Include(p => p.Applicant)
                    .Include(p => p.Company)
                    .Include(p => p.Subfield).ThenInclude(p => p.Field)
                    .Include(p => p.Technologies).ToListAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        /// <summary>
        /// Для загрузки проектов в бд одним объектом
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost("fullproject-creation")]
        [SwaggerResponse(200)]
        [SwaggerResponse(500, "Неизвестная ошибка")]
        public IActionResult Creation(List<ProjectData> data)
        {
            try
            {
                var projects = _mapper.Map<List<FullProject>>(data);
                foreach(var item in projects)
                {
                    var da = item.GetSearchableString();
                }    
                var res=_fullProjectRepository.AddRange(projects);
                return Ok(res.Count(p=>p.Guid!=Guid.Empty));
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
