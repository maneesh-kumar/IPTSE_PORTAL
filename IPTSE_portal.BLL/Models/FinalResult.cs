using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPTSE_portal.BLL
{
    public class FinalResult
    {
        public int Id { get; set; }
        public int CandidateId { get; set; }
        public decimal MarksObtained { get; set; }
        public int TotalQuestion { get; set; }
        public decimal MarksPercentage { get; set; }
        public int TotalMarks { get; set; }
    }
}
