using System;
using System.Collections.Generic;
using CS.ERP_MOB.Views.HCM;
using CS.ERP_MOB.Views.Frame;

namespace CS.ERP_MOB.Route
{
    public class Hcm_Route
    {
        public static Dictionary<string, Type> DicRouteList { get; private set; }
        static Hcm_Route()
        {
            DicRouteList = new Dictionary<string, Type>();

            DicRouteList.Add("home", typeof(HomePage));

            DicRouteList.Add("hcm-vacancy-lst", typeof(FrmHcmVacancyLst));
            DicRouteList.Add("hcm-applicants-lst", typeof(FrmHcmApplicantLst));
            DicRouteList.Add("hcm-applicants-set", typeof(FrmHcmApplicantSet));

            DicRouteList.Add("hcm-applicant-evaluation-lst", typeof(FrmHcmApplicantEvaluationLst));
            DicRouteList.Add("hcm-employee-lst", typeof(FrmHcmEmployeeLst));

            DicRouteList.Add("hcm-daily-log-lst", typeof(FrmHcmDailyLogLst));
            DicRouteList.Add("hcm-daily-log-set", typeof(FrmHcmDailyLogSet));
            DicRouteList.Add("hcm-leave-lst", typeof(FrmHcmLeaveLst));

            DicRouteList.Add("hcm-claim-lst", typeof(FrmHcmClaimLst));
            DicRouteList.Add("hcm-claim-set", typeof(FrmHcmClaimSet));
            DicRouteList.Add("hcm-survey-lst", typeof(FrmHcmSurveyLst));

            DicRouteList.Add("hcm-reward-lst", typeof(FrmHcmRewardLst));
            DicRouteList.Add("hcm-punsihment-lst", typeof(FrmHcmPunishmentLst));

            DicRouteList.Add("hcm-kpi-lst", typeof(FrmHcmKPILst));
            DicRouteList.Add("hcm-appraisal-lst", typeof(FrmHcmAppraisalLst));
            DicRouteList.Add("hcm-meeting-reservation-lst", typeof(FrmHcmMeetingReservationLst));
            DicRouteList.Add("hcm-meeting-reservation-set", typeof(FrmHcmMeetingReservationSet));
            DicRouteList.Add("hcm-discussion-lst", typeof(FrmHcmDiscussionLst));



            DicRouteList.Add("hcm-vacancy-mm-lst", typeof(FrmHcmVacancyLst));
            DicRouteList.Add("hcm-applicants-mm-lst", typeof(FrmHcmApplicantLst));

            DicRouteList.Add("hcm-applicant-evaluation-mm-lst", typeof(FrmHcmApplicantEvaluationLst));
            DicRouteList.Add("hcm-employee-mm-lst", typeof(FrmHcmEmployeeLst));

            DicRouteList.Add("hcm-daily-log-mm-lst", typeof(FrmHcmDailyLogLst));
            DicRouteList.Add("hcm-leave-mm-lst", typeof(FrmHcmLeaveLst));

            DicRouteList.Add("hcm-claim-mm-lst", typeof(FrmHcmClaimLst));
            DicRouteList.Add("hcm-survey-mm-lst", typeof(FrmHcmSurveyLst));

            DicRouteList.Add("hcm-reward-mm-lst", typeof(FrmHcmRewardLst));
            DicRouteList.Add("hcm-punsihment-mm-lst", typeof(FrmHcmPunishmentLst));

            DicRouteList.Add("hcm-kpi-mm-lst", typeof(FrmHcmKPILst));
            DicRouteList.Add("hcm-appraisal-mm-lst", typeof(FrmHcmAppraisalLst));
            DicRouteList.Add("hcm-meeting-reservation-mm-lst", typeof(FrmHcmMeetingReservationLst));
            DicRouteList.Add("hcm-discussion-mm-lst", typeof(FrmHcmDiscussionLst));


        }
    }
}
