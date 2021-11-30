from config import *
from data_load_application.FileLoader import FileLoader

import os


class FilesLoader:
  """Класс для считывания данных из файлов в папке входных данных
  """

  def __init__(self):
    """Метод инициализации объекта класса
    """
    self.files_text_data = {}
  
  def __load_files__(self):
    """Метод получения всех файлов и их типов в папке входных данных
    """
    # получить имена всех файлов
    filesnames = os.listdir(path=PATH_TO_INPUT_DATA)
    # сформировать лист путей найденных файлов
    filespathes = [os.path.join(PATH_TO_INPUT_DATA, filename) for filename in filesnames]
    # определене типа файлов и вызагрузка их данных
    for filepath in filespathes:
      # объект класса для загрузки файла
      file_loader = FileLoader()
      # определение типа файла 
      # чтение файла
      
      if filepath.endswith(".docx"):
        file_loader.__load_file__(filepath=filepath, filetype="docx")
      
      if filepath.endswith(".doc"):
        print("Can't read file %s: file format is *.doc" % filepath)
        file_loader.__load_file__(filepath=filepath, filetype="doc")
      
      elif filepath.endswith(".xlsx"):
        file_loader.__load_file__(filepath=filepath, filetype="xlsx")

      elif filepath.endswith(".xls"):
        print("Can't read file %s: file format is *.xls" % filepath)
        file_loader.__load_file__(filepath=filepath, filetype="xls")

      elif filepath.endswith(".csv"):
        file_loader.__load_file__(filepath=filepath, filetype="csv")
      
      elif filepath.endswith(".pptx"):
        file_loader.__load_file__(filepath=filepath, filetype="pptx")
      
      elif filepath.endswith(".ppt"):
        print("Can't read file %s: file format is *.ppt" % filepath)
        file_loader.__load_file__(filepath=filepath, filetype="ppt")
      
      # elif filepath.endswith(".txt"):
      #   file_loader.__load_file__(filepath=filepath, filetype="txt")
      
      # elif filepath.endswith(".json"):
      #   file_loader.__load_file__(filepath=filepath, filetype="json")
      
      # сохранить данные файла в словарь
      # ключ - путь к файлу
      # значениие - данные из файла
      self.files_text_data[filepath] = file_loader.file_data
    
