import json
import os


class JsonLoader:
  """Класс считывания JSON-файла
  """

  def __init__(self):
    """Метод инициализации объекта класса
    """
    self.data = {}
    
  def __read_file(self, filepath: str):
    """Метод считывания данных из файла

    Args:
        filepath (str): [путь к файлу]
    """
    if os.path.exists(filepath):
      with open(filepath, "r", encoding="utf-8") as filejson:
        self.data = json.load(filejson)
