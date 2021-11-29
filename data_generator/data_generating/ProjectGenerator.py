from pymorphy2 import MorphAnalyzer
from transliterate import translit
from typing import Dict, List

import random
import requests


def get_random_text_request(sentences_count: int, paragraph_count: int):
  url = "https://fish-text.ru/get?"
  
  if sentences_count > 0:
    r = requests.get(url=url+ "format=json&type=sentence&number=%s"%(str(sentences_count)))
  elif paragraph_count > 0:
    r = requests.get(url=url+ "format=json&type=paragraph&number=%s"%(str(paragraph_count)))
  
  r.encoding = "utf-8"
  text = r.json()["text"]
  return text

class ProjectGenerator:
  """Класс генерации заявки
  """
  
  def __init__(self):
    self.name = ""
    self.stage = ""
    self.sale_stage = ""
    self.implement_pilots_count = 0
    self.short_description = ""
    self.description = ""
    self.value_proposition = ""
    self.pilot_characteristics = ""
    self.budget = ""
    self.competitors = ""
    self.comments = ""
    self.investment = False
    self.expertise = False
    self.expertise_report = ""
    self.field = ""
    self.subfield = ""
    self.technologies = []
  
  def _generate_(self, adjectives: List, nouns: List, project_sale_stages: List, project_stages: List, fields_subfields: Dict, technologies: List):
    
    def generate_name(adjectives, nouns, current_random: random.SystemRandom):
      fl_found_noun = False
      morph = MorphAnalyzer(lang="ru")

      while not fl_found_noun:
        noun = current_random.choice(nouns)

        noun_info_list = morph.parse(noun)
        for i in range(0, len(noun_info_list)):
          if "NOUN" in noun_info_list[i].tag:
            if "masc" in noun_info_list[i].tag:
              fl_found_noun = True
              break

      adjective = current_random.choice(adjectives)

      return "%s %s" % (adjective.upper(), noun.upper())
    
    def random_sale_stage(project_sale_stages: List, current_random: random.SystemRandom):
      return current_random.choice(project_sale_stages)

    def random_stage(project_stages: List, current_random: random.SystemRandom):
      return current_random.choice(project_stages)
    
    def random_implement_pilots_count(current_random: random.SystemRandom):
      return current_random.randint(0, 10)
    
    def generate_short_description():
      return get_random_text_request(sentences_count=5, paragraph_count=0)

    def generate_description():
      return get_random_text_request(sentences_count=0, paragraph_count=5)
    
    def generate_value_proposition():
      return get_random_text_request(sentences_count=0, paragraph_count=3)
    
    def generate_pilot_characteristics():
      return get_random_text_request(sentences_count=0, paragraph_count=3)
    
    def random_budget(current_random: random.SystemRandom):
      order = current_random.choice([5, 6])
      cash_value = current_random.randint(1, 99)
      return "%s" % (str(cash_value * pow(10, order)))
    
    def generate_competitors(adjectives, nouns, current_random: random.SystemRandom):
      competitors_count = current_random.randint(1, 10)
      competitors = ""
      
      for i in range(0, competitors_count):
        competitors += generate_name(adjectives=adjectives, nouns=nouns, current_random=current_random)
        competitors += "; " if i < competitors_count - 1 else ""
      
      return ""
    
    def generate_comments():
      return get_random_text_request(sentences_count=3, paragraph_count=0)
    
    def generate_expertise_report():
      return get_random_text_request(sentences_count=0, paragraph_count=3)
    
    def random_technologies(technologies: List, current_random: random.SystemRandom):
      technologies_list = []
      technologies_count = current_random.randint(1, 3)
      
      for i in range(0, technologies_count):
        current_technology = current_random.choice(technologies)
        while not current_technology in technologies_list:
          current_technology = current_random.choice(technologies)
          break
        technologies_list.append(current_technology)
      
      return technologies_list
    
    current_random = random.SystemRandom()
    
    #
    self.name = generate_name(adjectives=adjectives, nouns=nouns, current_random=current_random)
    self.sale_stage = random_sale_stage(project_sale_stages=project_sale_stages, current_random=current_random)
    self.stage = random_stage(project_stages=project_stages, current_random=current_random)
    self.implement_pilots_count = random_implement_pilots_count(current_random=current_random)
    self.short_description = generate_short_description()
    self.description = generate_description()
    self.value_proposition = generate_value_proposition()
    self.pilot_characteristics = generate_pilot_characteristics()
    self.budget = random_budget(current_random=current_random)
    self.competitors = generate_competitors(adjectives=adjectives, nouns=nouns, current_random=current_random)
    self.comments = generate_comments()
    
    self.investment = current_random.choice([True, False])
    
    self.expertise = current_random.choice([True, False])
    self.expertise_report = generate_expertise_report() if self.expertise else ""
    
    self.field = current_random.choice(list(fields_subfields.keys()))
    self.subfield = current_random.choice(fields_subfields[self.field])
    
    self.technologies = random_technologies(technologies=technologies, current_random=current_random)
