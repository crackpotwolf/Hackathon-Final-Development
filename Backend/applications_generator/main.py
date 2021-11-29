from data_forming.data_former import *
from json_processing.json_processor import *


if __name__ == "__main__":

  #
  result_common = form_unloading_with_fullprojects()
  
  # сохранить результат
  write_to_json_in_dir(
    filename="result_common.json",
    data=result_common,
    dir="result_data"
  )
