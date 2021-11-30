from config import *

import json
import os


def write_to_json_in_dir(filename, data, dir):
  # сформировать путь для сохранения файла
  path_to_save = os.path.join(PATH_TO_DATA, dir)
  # если папки нет, то создаем ее
  if not os.path.exists(path_to_save):
    # создаем папку и имя файла
    os.makedirs(path_to_save)
  # задать путь для сохранения файла
  filepath = os.path.join(path_to_save, filename)
  with open(filepath, "w", encoding="utf-8") as filejson:
    json.dump(data, filejson, ensure_ascii=False)

