using Data.Interfaces.Repositories;
using Data.Models.Configurations;
using Data.Models.DB.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Microsoft.Extensions.Options;
using Search_Data.Models;
using Search_Data.Search;
using System.ComponentModel;

namespace Search.Controllers.API.v1
{
    /// <summary>
    /// Индексирование
    /// </summary>
    [ApiController]
    [DisplayName("indices")]
    [Route("api/indices/")]
    public class IndicesController : ControllerBase
    {
        protected IBaseEntityRepository<FullProject> _fullProjectRepository;

        private readonly ILogger<IndexModel> _logger;
        private readonly PathConfig _pathConfig;

        /// <inheritdoc />
        public IndicesController(ILogger<IndexModel> logger,
            IBaseEntityRepository<FullProject> fullProjectRepository,
            IOptions<PathConfig> pathConfig)
        {
            _fullProjectRepository = fullProjectRepository;
            _pathConfig = pathConfig.Value;
            _logger = logger;
        }

        /// <summary>
        /// Индексирование документа
        /// </summary>
        /// <param name="filePath">Путь к файлу</param>
        /// <param name="guidFile">Id файла</param>
        /// <returns></returns>
        [HttpPost("file/{guidFile:guid}")]
        [DisableRequestSizeLimit]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Exception), 400)]
        public IActionResult AddDocumentForIndex(string filePath, Guid guidFile)
        {
            try
            {
                _logger.LogInformation($"Начало индексирования документа.");

                // Для замера времени
                var timeStart = DateTime.UtcNow;

                var document = new TextDocument()
                {
                    Guid = guidFile,
                    Text = System.IO.File.ReadAllText(filePath)
                };

                _logger.LogInformation($"Документ считан.");

                // Инициализация
                _logger.LogInformation($"Инициализация документа.");

                var search = new WordSearch(_pathConfig.DocumentsIndexes);

                // Индексирование
                _logger.LogInformation($"Индексирование документа.");

                search.AddIndex(document);

                // Применить изменения
                search.CommitChanges();

                _logger.LogInformation($"Индексирование завершено за: {(DateTime.UtcNow - timeStart).TotalSeconds} секунд.");

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Не удалось проиндексировать документ. " +
                    $"Ошибка: {ex}");

                return StatusCode(500, "Не удалось проиндексировать документ.");
            }
        }

        /// <summary>
        /// Удаление документа
        /// </summary>
        /// <param name="guidFile">Id файла</param>
        /// <returns></returns>
        [HttpDelete("file/{guidFile:guid}")]
        [DisableRequestSizeLimit]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Exception), 400)]
        public IActionResult DeleteDocumentForIndex(Guid guidFile)
        {
            try
            {
                _logger.LogInformation($"Начало удаления документа.");

                // Для замера времени
                var timeStart = DateTime.UtcNow;

                // Инициализация
                _logger.LogInformation($"Инициализация для документа.");

                var search = new WordSearch(_pathConfig.DocumentsIndexes);

                // Удаление
                _logger.LogInformation($"Удаление документа.");

                search.DeleteIndex(guidFile);

                // Применить изменения
                search.CommitChanges();

                _logger.LogInformation($"Удаление завершено за: {(DateTime.UtcNow - timeStart).TotalSeconds} секунд.");

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Не удалось удалить документ. " +
                    $"Ошибка: {ex}");

                return StatusCode(500, "Не удалось удалить документ.");
            }
        }

        /// <summary>
        /// Удаление всех документов
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [DisableRequestSizeLimit]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Exception), 400)]
        public IActionResult DeleteAllDocuments()
        {
            try
            {
                _logger.LogInformation($"Начало удаления всех документов");

                // Для замера времени
                var timeStart = DateTime.UtcNow;

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
                _logger.LogError($"Не удалось удалить все документы. " +
                    $"Ошибка: {ex}");

                return StatusCode(500, "Не удалось удалить все документы.");
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

        /// <summary>
        /// Обновление документа
        /// </summary>
        /// <param name="filePath">Путь к файлу</param>
        /// <param name="guidFile">Id файла</param>
        /// <returns></returns>
        [HttpPut("file/{guidFile:guid}")]
        [DisableRequestSizeLimit]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Exception), 400)]
        public IActionResult UpdateDocumentForIndex(string filePath, Guid guidFile)
        {
            try
            {
                _logger.LogInformation($"Начало индексирования документа.");

                // Для замера времени
                var timeStart = DateTime.UtcNow;

                var document = new TextDocument()
                {
                    Guid = guidFile,
                    Text = System.IO.File.ReadAllText(filePath)
                };

                _logger.LogInformation($"Документ считан.");

                // Инициализация
                _logger.LogInformation($"Инициализация документа.");

                var search = new WordSearch(_pathConfig.DocumentsIndexes);

                // Индексирование
                _logger.LogInformation($"Индексирование документа.");

                search.UpdateIndex(document);

                // Применить изменения
                search.CommitChanges();

                _logger.LogInformation($"Индексирование завершено за: {(DateTime.UtcNow - timeStart).TotalSeconds} секунд.");

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Не удалось проиндексировать документ. " +
                    $"Ошибка: {ex}");

                return StatusCode(500, "Не удалось проиндексировать документ.");
            }
        }
    }
}