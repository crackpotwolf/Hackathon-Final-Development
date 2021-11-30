import pandas as pd
from pandas.io import excel


class XlsxCsvLoader:
  """Класс обработки файлов формата *.xlsx и *.csv
  """
  
  def __init__(self):
    """Метод инициализации объекта класса
    """
    self.data = {}
    
  def __read_file__(self, filepath: str, type: str):
    """Метод считывания данных из файла

    Args:
        filepath (str): [путь к файлу]
        type (str): [тип файла]
    """
    def get_data_from_sheet_xlsx(excel_file: pd.ExcelFile, sheet):
      """Метод считывания данных из листа файла формата *.xlsx

      Args:
          excel_file (pd.ExcelFile): [xlsx-файл]
          sheet ([type]): [текущий лист]
      """
      # with headers
      # without index
      # распарсить лист
      df_sheet = excel_file.parse(sheet)
      # получить названия столбцов
      columns_names = list(df_sheet.columns)
      # получить названия строк
      rows_values = list(df_sheet.values)
      # инициировать запись словаря с ключом - название листа
      self.data[sheet] = []
      # считывание строк и сохранение в словарь
      for row in rows_values:
        dict_cells = {}
        for num_column, column_name in enumerate(columns_names):
          dict_cells[column_name] = row[num_column]
        self.data[sheet].append(dict_cells)

    
    def get_data_from_xlsx(filepath: str):
      """Метод получения данных из листов файла формата *.xlsx

      Args:
          filepath (str): [путь к файлу]
      """
      # открыть файл
      excel_file = pd.ExcelFile(filepath)
      # получить названия файлов
      df_excel_sheets = excel_file.sheet_names
      # получить данные с каждого листа
      for sheet_name in df_excel_sheets:
        get_data_from_sheet_xlsx(excel_file=excel_file, sheet=sheet_name)
      
    
    def get_data_from_csv(filepath: str):
      """Метод считывания данных из файла типа *.csv

      Args:
          filepath (str): [путь к файлу]
      """
      # задать разделители
      separators = ['\t', ',', ';']
      # пройти по каждому разделителю
      for separator in separators:
        # открыть файл
        # with headers
        # without index
        df_csv = pd.read_csv(filepath, sep=separator)
        # получить названия столбцов
        columns_names = list(df_csv.columns)
        # если количество столбцов больше 1
        if len(columns_names) > 1:
          # получить строки
          rows_values = list(df_csv.values)
          # инициировать запись словаря с ключом - название листа
          self.data["csv_data"] = []
          # считывание строк и сохранение в словарь
          for row in rows_values:
            dict_cells = {}
            for num_column, column_name in enumerate(columns_names):
              dict_cells[column_name] = row[num_column]

            self.data["csv_data"].append(dict_cells)
          break
    
    # считываение данных из файла по его типу
    if type == "xlsx":
      get_data_from_xlsx(filepath=filepath)
    elif type == "csv":
      get_data_from_csv(filepath=filepath)
