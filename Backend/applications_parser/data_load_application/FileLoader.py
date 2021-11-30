from config import *
from data_load_application.DocxLoader import DocxLoader
from data_load_application.PptxLoader import PptxLoader
from data_load_application.XlsxCsvLoader import XlsxCsvLoader



class FileLoader:
  """Класс определения метода для считывания данных из файла по его типу
  """
  def __init__(self):
    """Метод инициализации объекта класса
    """
    self.file_data = None
  
  
  def __load_file__(self, filepath: str, filetype: str):
    """Метод определения способа считывания данных из файла по его типу

    Args:
        filepath (str): [путь к файлу]
        filetype (str): [тип файла]
    """
    
    def load_docx(filepath: str):
      """Считывание данных из файла типа *.docx

      Args:
          filepath (str): [путь к файлу]
      """
      docxloader_obj = DocxLoader()
      docxloader_obj.__read_file__(filepath=filepath)
      self.file_data = docxloader_obj.data
    
    def load_doc(filepath):
      """Считывание данных из файла типа *.doc

      Args:
          filepath (str): [путь к файлу]
      """
      self.file_data = None
    
    def load_xlsx(filepath):
      """Считывание данных из файла типа *.xlsx

      Args:
          filepath (str): [путь к файлу]
      """
      xlsxcsvloader_obj = XlsxCsvLoader()
      xlsxcsvloader_obj.__read_file__(filepath=filepath, type="xlsx")
      self.file_data = xlsxcsvloader_obj.data
    
    def load_xls(filepath):
      """Считывание данных из файла типа *.xls

      Args:
          filepath (str): [путь к файлу]
      """
      self.file_data = None
    
    def load_csv(filepath):
      """Считывание данных из файла типа *.csv

      Args:
          filepath (str): [путь к файлу]
      """
      xlsxcsvloader_obj = XlsxCsvLoader()
      xlsxcsvloader_obj.__read_file__(filepath=filepath, type="csv")
      self.file_data = xlsxcsvloader_obj.data
    
    def load_pptx(filepath):
      """Считывание данных из файла типа *.pptx

      Args:
          filepath (str): [путь к файлу]
      """
      pptxloader_obj = PptxLoader()
      pptxloader_obj.__read_file__(filepath=filepath)
      self.file_data = pptxloader_obj.data

    def load_ppt(filepath):
      """Считывание данных из файла типа *.ppt

      Args:
          filepath (str): [путь к файлу]
      """
      self.file_data = None
    
    def load_txt(filepath):
      """Считывание данных из файла типа *.txt

      Args:
          filepath (str): [путь к файлу]
      """
      pass
    
    def load_json(filepath):
      """Считывание данных из файла типа *.json

      Args:
          filepath (str): [путь к файлу]
      """
      pass
    
    # определение метода по типу файла
    if filetype == "doc":
      load_doc(filepath=filepath)
    
    elif filetype == "docx":
      load_docx(filepath=filepath)
    
    elif filetype == "xlsx":
      load_xlsx(filepath=filepath)

    elif filetype == "xls":
      load_xls(filepath=filepath)

    elif filetype == "csv":
      load_csv(filepath=filepath)
    
    elif filetype == "pptx":
      load_pptx(filepath=filepath)
    
    elif filetype == "ppt":
      load_ppt(filepath=filepath)
    
    elif filetype == "txt":
      load_txt(filepath=filepath)
    
    elif filetype == "json":
      load_json(filepath=filepath)
    
