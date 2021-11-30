import os


class TxtLoader:
  """Класс обработки файлов формата *.txt
  """
  def __init__(self) -> None:
    """Метод инициализации объекта класса
    """
    self.data = {}
  
  def __read_file__(self, filepath: str):
    """Метод инициализации объекта класса
    """
    # проверка наличия файла
    if os.path.exists(filepath):
      self.data["text"] = []
      with open(filepath, "r", encoding="utf-8") as filetxt:
        self.data["text"] = [line for line in filetxt]
  
