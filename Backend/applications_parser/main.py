from data_load_application.FilesLoader import FilesLoader
from data_generate_application.DataGenerator import DataGenerator
from data_process_application.json_data_save import write_to_json_in_dir


if __name__ == "__main__":
 # 
 files_loader = FilesLoader()
 files_loader.__load_files__()
 #
 parsed_data = files_loader.files_text_data
 #
 write_to_json_in_dir(
  filename="parsed_data_about_application.json",
  data=parsed_data,
  dir="result_data"
 )
 #
 