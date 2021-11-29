from config import *
from datetime import datetime
from io import BytesIO
from json_processing.json_processor import *
from typing import Dict, List, Tuple, Union
from urllib.request import urlopen
from zipfile import ZipFile

import requests


def get_file_by_url(url):
  #
  r = requests.get(url=url)


class DataLoader:
  """Класс для загрузки данных из интернета
  """
  def __init__(self) -> None:
    self.names = []
    self.lastnames = []
    self.nouns = []
    self.adjectives = []
    
    self.company_competence = []
    self.company_fields = []
    self.company_stages = []
    self.fields_and_subfields = []
    self.roles = []
    self.project_sale_stages = []
    self.project_stages = []
    self.technologies = []
  
  def __get_data_by_url__(self, main_url: str, data_type: str, result_type: str) -> List:
    '''
    Метод выгрузки данных из источника в интернете
    '''
    start_time = datetime.now()
    print("Start loading {0}: {1}".format(data_type, str(start_time)))

    r = requests.get(url=main_url)

    loading_status = r.status_code
    if result_type == "json":
      data = r.json() if loading_status == 200 else []
    elif result_type == "csv":
      if loading_status == 200:
        data = r.text
        # приведение к JSON-формату
        lines = data.split('\n')
        data = []
        headers = []

        for num_line, line in enumerate(lines):
          if num_line == 0:
            # определяем основные ключи словаря
            headers = line.split('\t')
          else:
            words = line.split('\t')
            if len(words) == len(headers):
              # добавляем преобразованный элемент русских слов
              item = {}
              for num_element, header in enumerate(headers):
                item[header] = words[num_element]
            data.append(item)
      else:
        data = []
    elif result_type == "txt":
      if loading_status == 200:
        data = r.text
        # приведение к JSON-формату
        data = data.split('\n')
      else:
        data = []

    end_time = datetime.now()
    print("End loading {0} from DB: {1}".format(data_type, str(end_time)))
    process_time = end_time - start_time
    print("Loading time: {0}. Loading status {1}".format(
        str(process_time), str(r.status_code)))
    return data, loading_status
  
  
  def get_names_and_lastnames(self):
    self.names = read_from_json_in_dir(
      filename="names.json",
      dir="input_data"
    )
    self.lastnames = read_from_json_in_dir(
      filename="lastnames.json",
      dir="input_data"
    )
    
    if len(self.names) == 0 or len(self.lastnames) == 0:

      self.names = {}
      self.lastnames = {}
      
      self.names["m"], m_names_loading_status = \
        self.__get_data_by_url__(
            main_url="https://raw.githubusercontent.com/linuxforse/random_russian_and_ukraine_name_surname/master/imena_m_ru.txt",
            data_type="men names",
            result_type="txt"
        )
      
      self.lastnames["m"], m_lastnames_loading_status = \
        self.__get_data_by_url__(
            main_url="https://raw.githubusercontent.com/linuxforse/random_russian_and_ukraine_name_surname/master/family_m_ru.txt",
            data_type="men lastnames",
            result_type="txt"
        )
      
      self.names["w"], w_names_loading_status = \
        self.__get_data_by_url__(
            main_url="https://raw.githubusercontent.com/linuxforse/random_russian_and_ukraine_name_surname/master/imena_f_ru.txt",
            data_type="women names",
            result_type="txt"
        )
      
      self.lastnames["w"], w_lastnames_loading_status = \
        self.__get_data_by_url__(
            main_url="https://raw.githubusercontent.com/linuxforse/random_russian_and_ukraine_name_surname/master/family_f_ru.txt",
            data_type="women lastnames",
            result_type="txt"
        )
      
      write_to_json_in_dir(
          filename="names.json",
          data=self.names,
          dir="input_data"
      )
      
      write_to_json_in_dir(
          filename="lastnames.json",
          data=self.lastnames,
          dir="input_data"
      )

    # return loading_status
  
  def get_nouns(self):
    self.nouns = read_from_json_in_dir(
        filename="nouns.json",
        dir="input_data"
    )
    if len(self.nouns) == 0:

      nouns, loading_status = \
        self.__get_data_by_url__(
            main_url="https://raw.githubusercontent.com/Badestrand/russian-dictionary/master/nouns.csv",
            filename="nouns.json",
            data_type="russian nouns",
            result_type="csv"
        )

      self.nouns = [word["bare"] for word in nouns]
      
      write_to_json_in_dir(
        filename="nouns.json",
        data=self.nouns,
        dir="input_data"
      )
    # return loading_status
  
  def get_adjectives(self):
    self.adjectives = read_from_json_in_dir(
        filename="adjectives.json",
        dir="input_data"
    )
    if len(self.adjectives) == 0:

      adjectives, loading_status = \
          self.__get_data_by_url__(
              main_url="https://raw.githubusercontent.com/Badestrand/russian-dictionary/master/adjectives.csv",
              filename="adjectives.json",
              data_type="russian adjectives",
              result_type="csv"
          )
      
      self.adjectives = [word["bare"] for word in adjectives]

      write_to_json_in_dir(
        filename="adjectives.json",
        data=self.adjectives,
        dir="input_data"
      )

    # return loading_status

  def get_roles(self):
    self.roles = read_from_json_in_dir(
      filename="applicant_roles.json",
      dir="input_data"
    )
    if len(self.roles) == []:
      print("There is no data about {0}", "roles")
  
  def get_company_competence(self):
    self.company_competence = read_from_json_in_dir(
        filename="company_competence.json",
        dir="input_data"
    )
    if len(self.company_competence) == []:
      print("There is no data about {0}", "company competence")
  
  def get_company_fields(self):
    self.company_fields = read_from_json_in_dir(
      filename="company_fields.json",
      dir="input_data"
    )
    if len(self.company_fields) == []:
      print("There is no data about {0}", "company fields")
  
  def get_company_stages(self):
    self.company_stages = read_from_json_in_dir(
      filename="company_stages.json",
      dir="input_data"
    )
    if len(self.company_stages) == []:
      print("There is no data about {0}", "company stages")

  def get_fields_and_subfields(self):
    self.fields_and_subfields = read_from_json_in_dir(
      filename="fields_subfields.json",
      dir="input_data"
    )
    if len(self.fields_and_subfields) == []:
      print("There is no data about {0}", "fields and subfields")
    
  def get_project_sale_stages(self):
    self.project_sale_stages = read_from_json_in_dir(
      filename="project_sale_stages.json",
      dir="input_data"
    )
    if len(self.project_sale_stages) == []:
      print("There is no data about {0}", "project sale stages")
    
  def get_project_stages(self):
    self.project_stages = read_from_json_in_dir(
      filename="project_stages.json",
      dir="input_data"
    )
    if len(self.project_stages) == []:
      print("There is no data about {0}", "project stages")
    
  def get_technologies(self):
    self.technologies = read_from_json_in_dir(
        filename="technologies.json",
        dir="input_data"
    )
    if len(self.technologies) == []:
      print("There is no data about {0}", "technologies")

