from pymorphy2 import MorphAnalyzer
from transliterate import translit
from typing import Dict, List

import random

class CompanyGenerator:
  """[summary]
  """

  def __init__(self) -> None:
    self.name = ""
    self.field = ""
    self.stage = ""
    self.people = ""
    self.competence = ""
    self.university = None
    self.inn = None
    self.country = ""
    self.city = ""
    self.website = ""

  def _generate_(self, adjectives, nouns, company_competence, company_fields, company_stages):
    
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

    def random_field(company_fields: List, current_random: random.SystemRandom):
      return current_random.choice(company_fields)
    
    def random_stage(company_stages: List, current_random: random.SystemRandom):
      return current_random.choice(company_stages)
    
    def random_people(current_random: random.SystemRandom):
      return current_random.randint(4, 1000)
    
    def random_competence(company_competence: List, current_random: random.SystemRandom):
      return current_random.choice(company_competence)
    
    def generate_university(city: str, current_random: random.SystemRandom):
      fl_university = current_random.choice([True, False])
      if fl_university:
        universities = {
          "Москва": [
              "Московский государственный технический университет им. Н.Э. Баумана",
              "Московский политехнический университет",
              "Московский государственный университет имени М.В. Ломоносова",
              "Национальный исследовательский университет «Высшая школа экономики»",
              "Московский физико-технический институт (Государственный университет)"
            ],
          "Санкт-Петербург": [
              "Санкт-Петербургский политехнический университет Петра Великого",
              "Санкт-Петербургский национальный исследовательский университет информационных технологий, механики и оптики"
            ],
          "Казань": ["Казанский инновационный университет имени В.Г. Тимирясова (ИЭУП)"],
          "Волгоград": [
              "Волгоградский государственный технический университет",
              "Волгоградский государственный университет",
            ],
          "Ростов": [
              "Южно-Российский государственный политехнический университет им. М.И. Платова",
              "Южный федеральный университет"
            ],
          "Тюмень": ["Тюменский индустриальный университет"],
        }
        
        return current_random.choice(universities[city])
      else:
        return None
    
    def generate_inn(current_random: random.SystemRandom):
      part_1 = current_random.randint(10, 99)
      part_2 = current_random.randint(10, 99)
      part_3 = current_random.randint(10, 99)
      part_4 = current_random.randint(10, 99)
      part_5 = current_random.randint(10, 99)
      return "%s%s%s%s%s" % (str(part_1), str(part_2), str(part_3), str(part_4), str(part_5))

    def random_country():
      return "Россия"

    def random_city(current_random: random.SystemRandom):
      cities = ["Москва", "Санкт-Петербург", "Казань", "Волгоград", "Ростов", "Тюмень"]
      return current_random.choice(cities)
    
    def generate_website(name: str, current_random: random.SystemRandom):
      #
      website_name = translit(name, language_code="ru", reversed=True).replace(" ", "").lower()
      #
      domen = current_random.choice(["com", "io", "ru"])
      return "%s.%s" % (website_name, domen)
    
    current_random = random.SystemRandom()
    
    #
    self.name = generate_name(adjectives=adjectives, nouns=nouns, current_random=current_random)
    self.competence = random_competence(company_competence=company_competence, current_random=current_random)
    self.field = random_field(company_fields=company_fields, current_random=current_random)
    self.stage = random_stage(company_stages=company_stages, current_random=current_random)
    self.country = random_country()
    self.city = random_city(current_random=current_random)
    self.website = generate_website(name=self.name, current_random=current_random)
    self.people = random_people(current_random=current_random)
    
    if self.people < 15:
      self.university = generate_university(city=self.city, current_random=current_random)
    
    if self.university is None:
      self.inn = generate_inn(current_random=current_random)
