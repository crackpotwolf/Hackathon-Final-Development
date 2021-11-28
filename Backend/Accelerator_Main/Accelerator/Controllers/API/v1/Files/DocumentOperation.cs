﻿using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Reflection;
using Data.Enum.Status;
using Data.Extensions.Files;
using Data.Interfaces;
using Data.Interfaces.Repositories;
using Data.Models.Configurations;
using Data.Models.DB.Files;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Accelerator.Controllers.API.v1.Files
{
    /// <summary>
    /// Операции с документами
    /// </summary>
    public class DocumentOperation : ControllerBase
    {
        private readonly PathConfig _pathConfig;
        private readonly ILogger<IndexModel> _logger;
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly IBaseEntityRepository<FileVersion> _fileVersion;
        private readonly IBaseEntityRepository<DocumentInfo> _documentInfo;

        /// <inheritdoc />
        public DocumentOperation(ILogger<IndexModel> logger,
            IBaseEntityRepository<DocumentInfo> documentInfo,
            IBaseEntityRepository<FileVersion> fileVersion,
            IWebHostEnvironment appEnvironment,
            IOptions<PathConfig> pathConfig)
        {
            _logger = logger;
            _fileVersion = fileVersion;
            _documentInfo = documentInfo;
            _pathConfig = pathConfig.Value;
            _appEnvironment = appEnvironment;
        }

        /// <summary>
        /// Работа с файлом: парсинг, индексация, обновление информации о версии файла
        /// </summary>
        /// <param name="documentInfoGuid">Тип документа</param>
        /// <param name="fileVersionGuid">Версия файла документа</param>
        /// <param name="indexOk">Флаг индекса</param>
        [NonAction]
        public void WorkingWithFile(Guid documentInfoGuid, Guid fileVersionGuid, bool indexOk)
        {
            // Для замера времени
            var timeStart = DateTime.UtcNow;

            // Получить информацию о документе
            var documentInfo = _documentInfo.FirstOrDefault(p => p.Guid == documentInfoGuid);

            // Если документ есть
            if (documentInfo != null)
            {
                // Получить информацию о версии файла 
                var fileVersion = _fileVersion.GetListQuery()
                    .Include(p => p.DocumentInfo)
                    .ThenInclude(p => p.Project)
                    .FirstOrDefault(p => p.Guid == fileVersionGuid);

                // Проверка версии файла 
                if (fileVersion == null)
                {
                    _logger.LogError($"Версия файла была удалена, дальнейшая работа невозможна.");
                    return;
                }

                try
                {
                    _logger.LogInformation($"Начало работы с документом НИОКР-а.");

                    #region Парсинг

                    // Если парсинг не завершен
                    if (fileVersion.ParceStatus != CompleteStatus.CompleteSuccess)
                    {
                        // Получить путь
                        var sourceDocPath = GetPathFileName(fileVersion.DocumentInfo.Project.PathName, documentInfo.PathName, fileVersion.PathName);

                        // Парсинг файла
                        var parce = ParsingDocument(sourceDocPath);

                        if (parce.OutputName.Length == 0)
                        {
                            // Error
                            fileVersion.ParceStatus = CompleteStatus.CompleteError;
                            fileVersion.IndexStatus = CompleteStatus.CompleteError;

                            _fileVersion.Update(fileVersion);

                            throw new Exception($"Не удалось распарсить файл.");
                        }

                        // Success
                        fileVersion.ParceStatus = CompleteStatus.CompleteSuccess;
                        fileVersion.PathNameParce = parce.OutputName;

                        _fileVersion.Update(fileVersion);

                        _logger.LogInformation($"Парсинг завершен.");
                    }

                    #endregion

                    //TO-DO Убрать
                    //Thread.Sleep(20000);

                    #region Индексирование

                    // Если индексирование не завершено
                    if (fileVersion.IndexStatus != CompleteStatus.CompleteSuccess)
                    {
                        // Индексирование документа
                        var index = true;

                        // Получить путь
                        var parceDocName = GetPathFileName(fileVersion.DocumentInfo.Project.PathName, documentInfo.PathName, fileVersion.PathName);

                        if (!indexOk)
                            index = AddDocumentForIndex(parceDocName, documentInfo.Guid);
                        else
                            index = UpdateDocumentForIndex(parceDocName, documentInfo.Guid);

                        if (!index)
                        {
                            // Error
                            fileVersion.IndexStatus = CompleteStatus.CompleteError;
                            _fileVersion.Update(fileVersion);

                            throw new Exception($"Невозможно проиндексировать документ.");
                        }

                        // Success
                        fileVersion.IndexStatus = CompleteStatus.CompleteSuccess;
                        _fileVersion.Update(fileVersion);

                        _logger.LogInformation($"Индексирование завершено.");

                        if (!indexOk)
                            _logger.LogInformation($"Документ добавлен за " +
                                $"{(DateTime.UtcNow - timeStart).TotalSeconds} секунд.");
                        else
                            _logger.LogInformation($"Документ обновлен за " +
                                $"{(DateTime.UtcNow - timeStart).TotalSeconds} секунд.");
                    }

                    #endregion
                }
                catch (Exception ex)
                {
                    // Error
                    if (fileVersion.ParceStatus == CompleteStatus.Incomplete)
                        fileVersion.ParceStatus = CompleteStatus.CompleteError;

                    if (fileVersion.IndexStatus == CompleteStatus.Incomplete)
                        fileVersion.IndexStatus = CompleteStatus.CompleteError;

                    _fileVersion.Update(fileVersion);

                    _logger.LogError($"Не удалось завершить работу с документом. " +
                        $"Ошибка: {ex}");

                    throw new Exception($"Не удалось завершить работу с документом.");
                }
            }
            else
            {
                _logger.LogError($"Документ был удален, дальнейшая работа невозможна.");
            }
        }

        /// <summary>
        /// Создать информацию о документе
        /// </summary>
        /// <param name="projectGuid">Guid заявки</param>
        /// <param name="fileName">Оригинальное название документа</param>
        /// <returns></returns>
        protected DocumentInfo CreateDocumentInfo(Guid projectGuid, string fileName)
        {
            try
            {
                _logger.LogInformation($"Начало создания информации о документе заявки.");

                // Информация о документе
                DocumentInfo documentInfo = new()
                {
                    Guid = Guid.NewGuid(),
                    ProjectGuid = projectGuid,
                    OriginalName = fileName,
                    PathName = Guid.NewGuid().ToString(),
                };

                _documentInfo.Add(documentInfo);

                _logger.LogInformation($"Информация о документе создана.");

                return documentInfo;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Не удалось создать информацию о документе. " +
                    $"Ошибка: {ex}");

                throw new Exception($"Не удалось добавить информацию о документе.");
            }
        }

        /// <summary>
        /// Создать информацию о версии файла
        /// </summary>
        /// <param name="projectGuid">Guid заявки</param>
        /// <param name="documentInfoGuid">Guid информации о документе</param>
        /// <param name="pathName">Имя документа</param>
        /// <param name="pathNameParce">Имя парсинга</param>
        /// <param name="originalName">Оригинальное название</param>
        /// <returns></returns>
        protected FileVersion CreateFileVersion(Guid projectGuid, Guid documentInfoGuid, string originalName = "",
            string pathName = "", string pathNameParce = "")
        {
            try
            {
                _logger.LogInformation($"Начало создания информации о версии файла заявки.");

                // Информация о версии файла 
                FileVersion fileVersion = new()
                {
                    Guid = Guid.NewGuid(),
                    DocumentInfoGuid = documentInfoGuid,
                    DateUpload = DateTime.UtcNow,
                    OriginalName = originalName,
                    PathName = pathName,
                    PathNameParce = pathNameParce,
                    ParceStatus = CompleteStatus.Incomplete,
                    IndexStatus = CompleteStatus.Incomplete
                };

                _fileVersion.Add(fileVersion);

                _logger.LogInformation($"Информации о версии файла создана.");

                return fileVersion;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Не удалось создать информацию о версии файла. " +
                    $"Ошибка: {ex}");

                throw new Exception($"Не удалось добавить информацию о версии файла.");
            }
        }

        /// <summary>
        /// Проверка файла
        /// </summary>
        /// <param name="file">Файл</param>
        /// <returns></returns>
        protected void DocumentCheck(IFormFile file)
        {
            // Если файл не прикреплен
            if (file == null)
            {
                _logger.LogInformation($"Файл не выбран");

                throw new Exception($"Файл не выбран");
            }

            // Если расширенеи файла не соответствует
            if (!DocumentManage.DocumentExtensionCheck(file.FileName))
            {
                _logger.LogInformation($"Не верный формат файла - {Path.GetExtension(file.FileName)}");

                throw new Exception($"Не верный формат файла - {Path.GetExtension(file.FileName)}");
            }
        }

        /// <summary>
        /// Сохраняет файл
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        /// <param name="file">Файл</param>
        /// <returns></returns>
        protected void SaveDocument(string path, IFormFile file)
        {
            try
            {
                _logger.LogInformation($"Начало сохранения файла.");

                // Получить информацию о файле
                FileInfo fileInfo = new(path);

                // Создать папку
                if (!fileInfo.Directory.Exists)
                    fileInfo.Directory.Create();

                // Записать файл
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + fileInfo.FullName, FileMode.Create))
                    file.CopyTo(fileStream);

                _logger.LogInformation($"Файл сохранен.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Не удалось сохранить файл. " +
                    $"Ошибка: {ex}");

                throw new Exception($"Не удалось сохранить файл для пути.");
            }
        }

        /// <summary>
        /// Удаляет файл
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        /// <returns></returns>
        protected void DeleteDocument(string path)
        {

            try
            {
                _logger.LogInformation($"Начало удаления файла.");

                // Получить информацию о файле
                FileInfo fileInfo = new(path);

                // Удалить файл
                if (fileInfo.Exists)
                    fileInfo.Delete();

                _logger.LogInformation($"Файл: {fileInfo.Name} удален.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Не удалось удалить файл. " +
                    $"Ошибка: {ex}");

                throw new Exception($"Не удалось удалить файл.");
            }
        }

        /// <summary>
        /// Удаляет папку
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        /// <returns></returns>
        protected void DeleteFolder(string path)
        {
            try
            {
                _logger.LogInformation($"Начало удаления папки.");

                var directoryInfo = new DirectoryInfo(path);

                if (directoryInfo.Exists)
                    directoryInfo.Delete(true);

                _logger.LogInformation($"Директория удалена.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Не удалось удалить директорию. " +
                    $"Ошибка: {ex}");

                throw new Exception($"Не удалось удалить директорию.");
            }
        }

        /// <summary>
        /// Получить путь к файлу
        /// </summary>
        /// <param name="projectPathName">Имя заявки</param>
        /// <param name="documentPathName">Имя документа</param>
        /// <param name="filePathName">Имя файла</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        protected string GetPathFileName(string projectPathName, string documentPathName, string filePathName)
        {
            try
            {
                // Путь к ниокру
                var niokrPath = Path.Combine(_pathConfig.Documentation, projectPathName);

                // Путь к документу
                var documentPath = Path.Combine(niokrPath, documentPathName);

                // Путь к файлу
                var filePath = Path.Combine(documentPath, filePathName);

                return filePath;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Не удалось получить путь. " +
                                 $"Ошибка: {ex}");

                throw new Exception($"Не удалось получить путь");
            }
        }

        /// <summary>
        /// Получить путь к папке с документами
        /// </summary>
        /// <returns></returns>
        protected string GetPathDocumentation()
        {
            try
            {
                return _pathConfig.Documentation;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Не удалось получить путь. " +
                                 $"Ошибка: {ex}");

                throw new Exception($"Не удалось получить путь");
            }
        }

        /// <summary>
        /// Получить путь к папке с фотками
        /// </summary>
        /// <returns></returns>
        protected string GetPathUserPhotos()
        {
            try
            {
                return _pathConfig.UserPhotos;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Не удалось получить путь. " +
                                 $"Ошибка: {ex}");

                throw new Exception($"Не удалось получить путь");
            }
        }
    }
}