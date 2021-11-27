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

        private readonly PathConfig _pathConfig;
        protected readonly EmailService _emailService;
        private readonly ILogger<IndexModel> _logger;
        protected readonly UserManager _userManager;
        protected readonly IMapper _mapper;

        public ProjectController(IBaseEntityRepository<Applicant> applicantsRepository,
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
        /// Поиск объектов по ключевым словам
        /// </summary>
        /// <param name="inputText">Текст для поиска</param>
        /// <returns>Название файлов с совпадениями в порядке релевантности</returns>
        [HttpGet("guids/{inputText}")]
        [DisableRequestSizeLimit]
        [Produces("application/json")]
        [SwaggerResponse(200, "Название файлов с совпадениями в порядке релевантности", typeof(Dictionary<Guid, float>))]
        [ProducesResponseType(typeof(Exception), 400)]
        public IActionResult SearchGuidsByText(string inputText)
        {
            try
            {
                _logger.LogInformation($"Начало поиска объектов.");

                // Для замера времени
                DateTime timeStart = DateTime.UtcNow;

                // Инициализация
                _logger.LogInformation($"Инициализация.");

                var search = new WordSearch(_pathConfig.DocumentsIndexes);

                // Поиск
                _logger.LogInformation($"Поиск. {inputText}");

                var result = search.Search(inputText);

                // Результаты
                _logger.LogInformation($"Запись результатов. {inputText}");

                _logger.LogInformation($"Запрос: '{result.Query}' всего: {result.TotalHits}. {inputText}");

                Dictionary<Guid, float> results = new Dictionary<Guid, float>();

                foreach (var item in result.Hits)
                {
                    results.Add(item.Guid, item.Score);

                    _logger.LogInformation($"{item.Guid} --- Score: {item.Score}. {inputText}");
                }

                _logger.LogInformation($"Поиск завершен за: {(DateTime.UtcNow - timeStart).TotalSeconds} секунд. {inputText}");

                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Не удалось выполнить поиск по документам. " +
                    $"{inputText} Ошибка: {ex.Message}");

                return StatusCode(500, "Не удалось выполнить поиск по документам");
            }
        }

        /// <summary>
        /// Поиск объектов по ключевым словам
        /// </summary>
        /// <param name="inputText">Текст для поиска</param>
        /// <returns>Название файлов с совпадениями в порядке релевантности</returns>
        [HttpGet("objects/{inputText}")]
        [DisableRequestSizeLimit]
        [Produces("application/json")]
        [SwaggerResponse(200, "Объекты с совпадениями в порядке релевантности", typeof(List<Project>))]
        [ProducesResponseType(typeof(Exception), 400)]
        public IActionResult SearchObjectsByText(string inputText)
        {
            try
            {
                _logger.LogInformation($"Начало поиска объектов.");

                // Для замера времени
                DateTime timeStart = DateTime.UtcNow;

                // Инициализация
                _logger.LogInformation($"Инициализация.");

                var search = new WordSearch(_pathConfig.DocumentsIndexes);

                // Поиск
                _logger.LogInformation($"Поиск. {inputText}");

                var result = search.Search(inputText);

                // Результаты
                _logger.LogInformation($"Запись результатов. {inputText}");

                _logger.LogInformation($"Запрос: '{result.Query}' всего: {result.TotalHits}. {inputText}");

                var results = _fullProjectRepository.GetListQuery().Where(p => result.Hits.Select(t => t.Guid).Contains(p.Guid)).ToList();

                _logger.LogInformation($"Поиск завершен за: {(DateTime.UtcNow - timeStart).TotalSeconds} секунд. {inputText}");

                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Не удалось выполнить поиск по документам. " +
                    $"{inputText} Ошибка: {ex.Message}");

                return StatusCode(500, "Не удалось выполнить поиск по документам");
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

                var res =_fullProjectRepository.AddRange(projects);
                res.ToList().ForEach(p => p.AddSearchableObjectToIndex(p.Guid, search));
                search.CommitChanges();

                return Ok(res.Count(p=>p.Guid!=Guid.Empty));
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        /// <summary>
        /// Удаление всех индексов
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete-indexes")]
        [DisableRequestSizeLimit]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Exception), 400)]
        public IActionResult DeleteAllIndexes()
        {
            try
            {
                _logger.LogInformation($"Начало удаления всех индексов");

                // Для замера времени
                DateTime timeStart = DateTime.UtcNow;

                // Инициализация
                _logger.LogInformation($"Инициализация");

                var search = new WordSearch(_pathConfig.DocumentsIndexes);

                // Удаление
                _logger.LogInformation($"Удаление всех документов");

                search.DeleteAllIndexes();

                // Применить изменения
                search.CommitChanges();

                _logger.LogInformation($"Удаление завершено за: {(DateTime.UtcNow - timeStart).TotalSeconds} секунд");

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Не удалось удалить все индексы. " +
                    $"Ошибка: {ex.Message}");

                return StatusCode(500, "Не удалось удалить все индексы.");
            }
        }

        /// <summary>
        /// Обновление индексов
        /// </summary>
        /// <returns></returns>
        [HttpPut("update-indexes")]
        [DisableRequestSizeLimit]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Exception), 400)]
        public IActionResult UpdateDocumentForIndex()
        {
            try
            {
                _logger.LogInformation($"Начало индексирования записей.");

                // Для замера времени
                DateTime timeStart = DateTime.UtcNow;

                var projects = _fullProjectRepository.GetList();

                var search = new WordSearch(_pathConfig.DocumentsIndexes);

                search.DeleteAllIndexes();

                projects.ForEach(p => p.AddSearchableObjectToIndex(p.Guid, search));
                
                search.CommitChanges();

                _logger.LogInformation($"Индексирование завершено за: {(DateTime.UtcNow - timeStart).TotalSeconds} секунд.");

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Не удалось проиндексировать документ. " +
                    $"Ошибка: {ex.Message}");

                return StatusCode(500, "Не удалось проиндексировать документ.");
            }
        }
    }
}
