from data_generating.ApplicantGenerator import ApplicantGenerator
from data_generating.CompanyGenerator import CompanyGenerator
from data_generating.ProjectGenerator import ProjectGenerator
from transliterate import translit
from typing import Dict, List

import random


class ApplicationGenerator:
  """Класс генерации Заявки
  """

  def __init__(self, applicant_obj: ApplicantGenerator, company_obj: CompanyGenerator, project_obj: ProjectGenerator) -> None:
    self.applicant = applicant_obj
    self.company = company_obj
    self.project = project_obj
    self.application_json = {}

  def form(self) -> None:
    #
    self.application_json["ApplicantName"] = self.applicant.name
    self.application_json["ApplicantLastName"] = self.applicant.lastname
    self.application_json["ApplicantEmail"] = self.applicant.email
    self.application_json["ApplicantPhone"] = self.applicant.phone
    self.application_json["ApplicantRole"] = self.applicant.role
    #
    self.application_json["CompanyName"] = self.company.name
    self.application_json["CompanyField"] = self.company.field
    self.application_json["CompanyStage"] = self.company.stage
    self.application_json["CompanyPeople"] = self.company.people
    self.application_json["CompanyCompetence"] = self.company.competence
    self.application_json["CompanyUniversity"] = self.company.university
    self.application_json["CompanyInn"] = self.company.inn
    self.application_json["CompanyCountry"] = self.company.country
    self.application_json["CompanyCity"] = self.company.city
    self.application_json["CompanyWebsite"] = self.company.website
    
    #
    self.application_json["Name"] = self.project.name
    self.application_json["SaleStage"] = self.project.sale_stage
    self.application_json["Stage"] = self.project.stage
    self.application_json["ImplementPilotsCount"] = self.project.implement_pilots_count
    self.application_json["ShortDescription"] = self.project.short_description
    self.application_json["Description"] = self.project.description
    self.application_json["ValueProposition"] = self.project.value_proposition
    self.application_json["PilotCharacteristic"] = self.project.pilot_characteristics
    self.application_json["Budget"] = self.project.budget
    self.application_json["Competitors"] = self.project.competitors
    self.application_json["Comments"] = self.project.comments
    self.application_json["Investment"] = self.project.investment
    self.application_json["Expertise"] = self.project.expertise
    self.application_json["ExpertiseReport"] = self.project.expertise_report
    self.application_json["Field"] = self.project.field
    self.application_json["SubField"] = self.project.subfield
    self.application_json["Technology"] = self.project.technologies
