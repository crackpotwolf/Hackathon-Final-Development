from config import URL_DATABASE_SERVICE

import requests


def send_json_by_api(api: str, json_data: str):
  """Метод отправки данных формата JSON в БД системы

  Args:
      api (str): [название метода]
      json_data (str): [данные для отправки в формате JSON]
  """
  r = requests.post(url=URL_DATABASE_SERVICE + api, json=json_data)
  if r.status_code != 200:
    print("Posting of parsed data finished with error. Status code: %s"%str(r.status_code))
  else:
    print("Posting of parsed data successfully finished")