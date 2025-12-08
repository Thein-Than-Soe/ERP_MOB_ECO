using System;
namespace CS.ERP_MOB.Services.HCM
{
    public class Hcm_Name
    {
        public Hcm_Name()
        {
        }  
        
        #region "List"
        
        public static string wsgetJobVacancy = "getJobVacancy";
        public static string wsgetApplicantDtl = "getApplicantDtl";
        public static string wsgetApplicantVacancy = "getApplicantVacancy";
        public static string wsgetApplicantEvaluationDtl = "getApplicantEvaluationDtl";
        public static string wsgetEmployeeDtl = " getEmployeeRO";
        public static string wsgetEmployeeDailyLog = "getEmployeeDailyLog"; 
 
        public static string wsgetEmployeeLeaveTaken = "getEmployeeLeaveTaken";
      
        public static string wsgetEmployeeClaim = "getEmployeeClaim";
        public static string wsgetEmployeeClaimTemplate = "getEmployeeClaimTemplate";
        public static string wsgetEmployeeSurveyDtl = "getEmployeeSurveyDtl";
        public static string wsgetSurvey = "getSurveyDtl";
        public static string wsgetEmployeeReward = "getEmployeeReward";
        public static string wsgetEmployeePunishment = "getEmployeePunishment";               
        public static string wsgetKPIFactor = "getKPIFactor"; 
        public static string wsgetKPIForm = "getKPIFormDtl"; 
        public static string wsgetEmployeeKPI = "getEmployeeKPIFormDtl"; 
       
        public static string wsgetEmployeeAppraisal = "getEmployeeAppraisalDtl";
        public static string wsgetMeetingRoom = "getMeetingRoomDtl";
        public static string wsgetMeetingRoomBooking = "getMeetingRoomBooking";
       
        public static string wsgetDiscussionUserJun = "getDiscussionUserJun";
        public static string wsgetDiscussionUser = "getDiscussionUser";
        public static string wsgetDiscussionSurveyData = "getDiscussionSurveyData";
        public static string wsgetDiscussionDataBookmark = "getDiscussionDataBookmark";
        public static string wsgetDiscussionDataMention = "getDiscussionDataMention";
       

        #endregion
        #region "Save"
        public static string wssaveJobVacancy = "saveJobVacancy";
        public static string wssaveApplicantDtl = "saveApplicantDtl";        
        public static string wssaveApplicantEvaluation = "saveApplicantEvaluation";
        public static string wsupdateApplicantEvaluation = "updateApplicantEvaluation";        
        public static string wssaveEmployeeDtl = "saveEmployeeDtl";
        public static string wssaveEmployeeDailyLog = "saveEmployeeDailyLog";
        public static string wssaveLeave = "saveLeave";
        public static string wssaveLeaveTemplate = "saveLeaveTemplate";
        public static string wssaveEmployeeLeaveTaken = "saveEmployeeLeaveTaken";
        public static string wssaveEmployeeLeaveTemplate = "saveEmployeeLeaveTemplate";
        public static string wssaveEmployeeClaimTemplate = "saveEmployeeClaimTemplate";
        public static string wssaveEmployeeClaim = "saveEmployeeClaim";
        public static string wssaveSurvey = "saveSurvey";
        public static string wssaveEmployeeSurvey = "saveEmployeeSurvey";               
        public static string wssaveEmployeeReward = "saveEmployeeReward";
        public static string wssaveEmployeePunishment = "saveEmployeePunishment";           
        public static string wssaveKPIFactor = "saveKPIFactor";
        public static string wssaveKPIForm = "saveKPIForm";
        public static string wssaveEmployeeKPI = "saveEmployeeKPIForm";
        public static string wssaveAppraisalForm = "saveAppraisalForm";
        public static string wssaveAppraisalFactor = "saveAppraisalFactor";
        public static string wssaveEmployeeAppraisal = "saveEmployeeAppraisal";      
        public static string wssaveMeetingRoom = "saveMeetingRoom";
        public static string wssaveMeetingRoomBooking = "saveMeetingRoomBooking";       
        public static string wssaveMeetingRoomType = "saveMeetingRoomType";        
        public static string wssaveMeetingFacility = "saveMeetingFacility";
        public static string wssaveDiscussionUserJun = "saveDiscussionUserJun";
        public static string wssaveDiscussionSurveyData = "saveDiscussionSurveyData";
        public static string wssaveDiscussionDataBookMark = "saveDiscussionDataBookMark";
        public static string wssaveDiscussionDataMention = "saveDiscussionDataMention";

        #endregion
        #region "Load"
        public static string wsloadJobVacancy = "loadJobVacancy";
        public static string wsloadApplicant = "loadApplicant";
        public static string wsloadApplicantEvaluationDtl = "loadApplicantEvaluationDtl";
        public static string wsloadEmployee = "loadEmployee";
        public static string wsloadEmployeeDailyLog = "loadEmployeeDailyLog";
        public static string wsloadLeave = "loadLeave";
        public static string wsloadEmployeeLeaveTemplate = "loadEmployeeLeaveTemplateDtl";
        public static string wsloadEmployeeLeaveTaken = "loadEmployeeLeaveTaken";
        public static string wsloadEmployeeLeaveTemplateDtl = "loadEmployeeLeaveTemplateDtl";
        public static string wsloadEmployeeClaimTemplate = "loadEmployeeClaimTemplate";
        public static string wsloadEmployeeClaim = "loadEmployeeClaim";     
        public static string wsloadSurvey = "loadSurveyDtl";
        public static string wsloadEmployeeSurvey = "loadEmployeeSurveyDtl";              
        public static string wsloadEmployeeReward = "loadEmployeeReward";
        public static string wsloadEmployeePunishment = "loadEmployeePunishment";        
        public static string wsloadKPIForm = "loadKPIFormDtl";
        public static string wsloadEmployeeKPI = "loadEmployeeKPIDtl";
        public static string wsloadAppraisalForm = "loadAppraisalFormDtl";
        public static string wsloadEmployeeAppraisal = "loadEmployeeAppraisalDtl";       
        public static string wsloadMeetingRoom = "loadMeetingRoomDtl";
        public static string wsloadMeetingRoomBooking = "loadMeetingRoomBooking";
        
         #endregion
    }
}

