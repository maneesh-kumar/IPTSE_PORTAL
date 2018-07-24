using IPTSE_portal.BLL.Models;
using IPTSE_portal.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPTSE_portal.BLL
{
    public class SchoolRegistrationBLL
    {
        public void InsertSchool(SchoolRegistrationModel schoolRegistrationModel)
        {
            IPTSE_School_Reg_table _schoolRegistration = new IPTSE_School_Reg_table();
            login_table _objLoginTable = new login_table();
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
                        _schoolRegistration.Updated_Date = DateTime.Now;
                        objContext.IPTSE_School_Reg_table.Add(_schoolRegistration);
                        objContext.SaveChanges();
                        decimal newIdentityValue = objContext.IPTSE_School_Reg_table.DefaultIfEmpty().Max(r => r == null ? 0 : r.Id);
                        _objLoginTable.Id = newIdentityValue;
                        _objLoginTable.password = schoolRegistrationModel.Password;
                        _objLoginTable.Email = schoolRegistrationModel.institution_email;
                        objContext.login_table.Add(_objLoginTable);
                        objContext.SaveChanges();
                        dbcxtransaction.Commit();
                    }
                    catch
                    {
                        dbcxtransaction.Rollback();
                    }
                }
            }
        }
    }
}
