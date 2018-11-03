using IPTSE_portal.BLL.Models;
using IPTSE_portal.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPTSE_portal.BLL
{
    public class SchoolRegistrationBLL
    {
        public decimal InsertSchool(SchoolRegistrationModel schoolRegistrationModel)
        {
            IPTSE_School_Reg_table _schoolRegistration = new IPTSE_School_Reg_table();
            login_table _objLoginTable = new login_table();
            decimal newIdentityValue = -1;
            using (var objContext = new IPTSEDBEntities())
            {
                using (var dbcxtransaction = objContext.Database.BeginTransaction())
                {
                    try
                    {
                        _schoolRegistration.institution_name = schoolRegistrationModel.institution_name;
                        _schoolRegistration.principal_director_name = schoolRegistrationModel.principal_director_name;
                        _schoolRegistration.branch_name = schoolRegistrationModel.branch_name;
                        _schoolRegistration.institution_type = schoolRegistrationModel.institution_type;
                        _schoolRegistration.addr1 = schoolRegistrationModel.addr1;
                        _schoolRegistration.addr2 = schoolRegistrationModel.addr2;
                        _schoolRegistration.city = schoolRegistrationModel.city;
                        _schoolRegistration.state = schoolRegistrationModel.state;
                        _schoolRegistration.state = schoolRegistrationModel.state;
                        _schoolRegistration.zipcode = schoolRegistrationModel.zipcode;
                        _schoolRegistration.country = schoolRegistrationModel.country;
                        _schoolRegistration.institution_contact = schoolRegistrationModel.institution_contact;
                        _schoolRegistration.institution_email = schoolRegistrationModel.institution_email;
                        _schoolRegistration.contact_person_name = schoolRegistrationModel.contact_person_name;
                        _schoolRegistration.contact_person_contact = schoolRegistrationModel.contact_person_contact;
                        _schoolRegistration.contact_person_email = schoolRegistrationModel.contact_person_email;
                        _schoolRegistration.information_source = schoolRegistrationModel.information_source;
                        _schoolRegistration.Created_Date = DateTime.Now;
                        //_schoolRegistration.Updated_Date = DateTime.Now;
                        objContext.IPTSE_School_Reg_table.Add(_schoolRegistration);
                        objContext.SaveChanges();
                        newIdentityValue = objContext.IPTSE_School_Reg_table.DefaultIfEmpty().Max(r => r == null ? 0 : r.Id);
                        _objLoginTable.Id = newIdentityValue;
                        byte[] encode = new byte[schoolRegistrationModel.password.Length];
                        encode = System.Text.Encoding.UTF8.GetBytes(schoolRegistrationModel.password);
                        _objLoginTable.password = Convert.ToBase64String(encode);
                        _objLoginTable.email = schoolRegistrationModel.institution_email;
                        _objLoginTable.Login_type = "Institution";
                        objContext.login_table.Add(_objLoginTable);
                        objContext.SaveChanges();
                        dbcxtransaction.Commit();
                        return newIdentityValue;
                    }
                    catch (Exception ex)
                    {
                        dbcxtransaction.Rollback();
                        throw ex;
                    }
                    
                }
            }
        }

        public SchoolRegistrationModel GetSchoolRegistration(int id)
        {
            IPTSEDBEntities entity = new IPTSEDBEntities();
            var resultModel = entity.IPTSE_School_Reg_table.Where(a => a.Id.Equals(id)).FirstOrDefault();

            SchoolRegistrationModel registrationModel = new SchoolRegistrationModel();
            if (resultModel != null)
            {
                registrationModel.addr1 = resultModel.addr1;
                registrationModel.addr2 = resultModel.addr2;
                registrationModel.branch_name = resultModel.branch_name;
                registrationModel.city = resultModel.city;
                registrationModel.contact_person_contact = resultModel.contact_person_contact;
                registrationModel.contact_person_email = resultModel.contact_person_email;
                registrationModel.contact_person_name = resultModel.contact_person_name;
                registrationModel.country = resultModel.country;
                registrationModel.Created_Date = resultModel.Created_Date;
                registrationModel.Id = resultModel.Id;
                registrationModel.information_source = resultModel.information_source;
                registrationModel.institution_contact = resultModel.institution_contact;
                registrationModel.institution_email = resultModel.institution_email;
                registrationModel.institution_name = resultModel.institution_name;
                registrationModel.principal_director_name = resultModel.principal_director_name;
                registrationModel.state = resultModel.state;
                registrationModel.zipcode = resultModel.zipcode;
                
            }

            return registrationModel;
        }

        public List<SchoolRegistrationModel> GetSchoolRegistrations()
        {
            IPTSEDBEntities entity = new IPTSEDBEntities();
            var resultModels = entity.IPTSE_School_Reg_table.Where(s => s.IsActive == true || s.IsActive == null);

            List<SchoolRegistrationModel> registrationModels = new List<SchoolRegistrationModel>();
            foreach(var resultModel in resultModels)
            {
                SchoolRegistrationModel registrationModel = new SchoolRegistrationModel();
                if (resultModel != null)
                {
                    registrationModel.addr1 = resultModel.addr1;
                    registrationModel.addr2 = resultModel.addr2;
                    registrationModel.branch_name = resultModel.branch_name;
                    registrationModel.city = resultModel.city;
                    registrationModel.contact_person_contact = resultModel.contact_person_contact;
                    registrationModel.contact_person_email = resultModel.contact_person_email;
                    registrationModel.contact_person_name = resultModel.contact_person_name;
                    registrationModel.country = resultModel.country;
                    registrationModel.Created_Date = resultModel.Created_Date;
                    registrationModel.Id = resultModel.Id;
                    registrationModel.information_source = resultModel.information_source;
                    registrationModel.institution_contact = resultModel.institution_contact;
                    registrationModel.institution_email = resultModel.institution_email;
                    registrationModel.institution_name = resultModel.institution_name;
                    registrationModel.principal_director_name = resultModel.principal_director_name;
                    registrationModel.state = resultModel.state;
                    registrationModel.zipcode = resultModel.zipcode;

                    registrationModels.Add(registrationModel);
                }
            }
            

            return registrationModels;
        }
    }
}
