using IPTSE_portal.DAL;
using System;
using System.Linq;

namespace IPTSE_portal.BLL
{
    public class FinalResultBLL
    {
        FinalResult objFinalResult = new FinalResult();
        public FinalResult GetFinalResult(int userId)
        {
            Final_Result objfinal_Result = new Final_Result();
            try
            {
                IPTSEDBEntities entity = new IPTSEDBEntities();
                objfinal_Result = entity.Final_Result.Where(t => t.CandidateId == userId).Select(t1 => t1).FirstOrDefault();
                objFinalResult.Id = objfinal_Result.Id;
                objFinalResult.CandidateId = objfinal_Result.CandidateId;
                objFinalResult.MarksObtained = objfinal_Result.MarksObtained;
                objFinalResult.MarksPercentage = objfinal_Result.MarksPercentage;
                objFinalResult.TotalQuestion = objfinal_Result.TotalQuestion;
                return objFinalResult;
            }
            catch (Exception ex)
            {
            }
            return objFinalResult;
        }
    }
}
