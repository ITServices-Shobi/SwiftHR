using SwiftHR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftHR.Utility
{
    public class EmployeeOnboardingDetails
    {
        public int EmployeeId { get; set; }
        public int? CompanyId { get; set; }
        public int? EmployeeNumber { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string ContactNumber { get; set; }
        public string EmployeeProfilePhoto { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public EmpOnboardingDetail empOnboardingDetails { get; set; }
        public List<EmpEducationDetail> empEducationDetail { get; set; }
        public EmpBankDetail empBankDetail { get; set; }
        public List<PrevEmploymentDetail> prevEmploymentDetail { get; set; }
        public List<EmpDocument> empDocument { get; set; }
    }
}
