//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IPTSE_portal.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Final_Result
    {
        public int Id { get; set; }
        public int CandidateId { get; set; }
        public decimal MarksObtained { get; set; }
        public int TotalQuestion { get; set; }
        public decimal MarksPercentage { get; set; }
        public int TotalMarks { get; set; }
    }
}
