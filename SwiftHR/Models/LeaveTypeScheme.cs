using System;
using System.Collections.Generic;

#nullable disable

namespace SwiftHR.Models
{
    public partial class LeaveTypeScheme
    {
        public int LeaveTypeSchemeId { get; set; }
        public string LeaveTypeSchemeName { get; set; }
        public string LeaveTypeSchemeDescription { get; set; }
        public string SchemeAppliedFrom { get; set; }
        public string SchemeAppliedTill { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
    }
}
