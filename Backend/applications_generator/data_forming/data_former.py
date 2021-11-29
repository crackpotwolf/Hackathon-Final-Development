from data_generating.ApplicantGenerator import ApplicantGenerator
from data_generating.ApplicationGenerator import ApplicationGenerator
from data_generating.CompanyGenerator import CompanyGenerator
from data_generating.ProjectGenerator import ProjectGenerator
from data_loading.DataLoader import DataLoader

from json_processing.json_processor import write_to_json_in_dir


def form_unloading_with_entities_by_classes():
  #
  data_loader_obj = DataLoader()
  data_loader_obj.get_names_and_lastnames()
  data_loader_obj.get_nouns()
  data_loader_obj.get_adjectives()
  data_loader_obj.get_company_competence()
  data_loader_obj.get_company_fields()
  data_loader_obj.get_company_stages()
  data_loader_obj.get_fields_and_subfields()
  data_loader_obj.get_project_sale_stages()
  data_loader_obj.get_project_stages()
  data_loader_obj.get_roles()
  data_loader_obj.get_technologies()
  
  #
  records_count = 50
  records = []
  
  #
  applicant_obj = ApplicantGenerator()
  company_obj = CompanyGenerator()
  project_obj = ProjectGenerator()
  
  #
  for i in range(0, records_count):
    
    #
    applicant_obj._generate_(
      names=data_loader_obj.names,
      lastnames=data_loader_obj.lastnames,
      roles=data_loader_obj.roles
    )
    
    #
    company_obj._generate_(
      adjectives=data_loader_obj.adjectives,
      nouns=data_loader_obj.nouns,
      company_competence=data_loader_obj.company_competence,
      company_fields=data_loader_obj.company_fields,
      company_stages=data_loader_obj.company_stages)
    
    #
    project_obj._generate_(
      adjectives=data_loader_obj.adjectives, 
      nouns=data_loader_obj.nouns,
      project_sale_stages=data_loader_obj.project_sale_stages,
      project_stages=data_loader_obj.project_stages,
      fields_subfields=data_loader_obj.fields_and_subfields,
      technologies=data_loader_obj.technologies
    )
    
    #
    application_obj = ApplicationGenerator(
      applicant_obj=applicant_obj,
      company_obj=company_obj,
      project_obj=project_obj  
    )
    application_obj.form()
    
    records.append(application_obj.application_json)
  
  return records

def form_unloading_with_fullprojects():
  
  #
  return form_unloading_with_entities_by_classes()
