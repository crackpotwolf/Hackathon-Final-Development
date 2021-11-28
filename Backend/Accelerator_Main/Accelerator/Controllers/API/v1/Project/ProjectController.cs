using AutoMapper;
using Data.Attributes;
using Data.Extensions;
using Data.Interfaces.Repositories;
using Data.Models.Configurations;
using Data.Models.DB.Project;
using Data.Models.Services;
using Data.Services.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Microsoft.Extensions.Options;
using Search_Data.Search;
using Swashbuckle.AspNetCore.Annotations;

namespace Accelerator.Controllers.API.v1.Projects
{
    /// <summary>
    /// Контроллер заявок
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [SetRoute]
    public class ProjectController : ControllerBase
    {
        protected IBaseEntityRepository<FullProject> _fullProjectRepository;
        protected IBaseEntityRepository<Applicant> _applicantsRepository;
        protected IBaseEntityRepository<Company> _companiesRepository;
        protected IBaseEntityRepository<Project> _projectsRepository;

        protected readonly EmailService _emailService;
        private readonly ILogger<IndexModel> _logger;
        protected readonly UserManager _userManager;
        private readonly PathConfig _pathConfig;
        protected readonly IMapper _mapper;

        public ProjectController(IBaseEntityRepository<Applicant> applicantsRepository,
            IBaseEntityRepository<FullProject> fullProjectRepository,
            IBaseEntityRepository<Company> companiesRepository,
            IBaseEntityRepository<Project> projectsRepository,
            IOptions<PathConfig> pathConfig,
            ILogger<IndexModel> logger,
            EmailService emailService,
            UserManager userManager,
            IMapper mapper)
        {
            _fullProjectRepository = fullProjectRepository;
            _applicantsRepository = applicantsRepository;
            _companiesRepository = companiesRepository;
            _projectsRepository = projectsRepository;
            _pathConfig = pathConfig.Value;
            _emailService = emailService;
            _userManager = userManager;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("all")]
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

        [HttpGet("full-all")]
        [SwaggerResponse(500, "Неизвестная ошибка")]
        public async Task<IActionResult> GetFullAll()
        {
            try
            {
                return Ok(await _fullProjectRepository.GetListQuery()
                    .ToListAsync());
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
                var search = new WordSearch(_pathConfig.DocumentsIndexes);

                var res = _fullProjectRepository.AddRange(projects);
                res.ToList().ForEach(p => p.AddSearchableObjectToIndex(p.Guid, search));
                search.CommitChanges();

                return Ok(res.Count(p => p.Guid != Guid.Empty));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}