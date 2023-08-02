using KMU.EduActivity.ApplicationLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KMUH.iKASAWebApi.UI.MVC.Models
{
    public class iKASAModel
    {
        public class LoginUser
        {
            public string UserName { get; set; }
            public string Password { get; set; }
            public bool RememberMeSet { get; set; }
        }

        public class IndividualStudy
        {
            //public string EduTermID { get; set; }
            public string TitleList { get; set; }
            //public string ContentForm { get; set; }
            public List<EduTermFormReqDto> TermReqs
            {
                get;
                set;
            } 
        }

        public class MemberID // 查詢職編流水編號
        {
            public string memberid {get;set;}
            public string MemberType { get; set; }
        }

        public class InstanceID //查詢會議記錄流水編號
        {
            public string instanceid { get; set; }
        }

        public class TemplateID //查詢會議記錄樣板編號
        {
            public int Templateid { get; set; }
        }

        public class EduPassport //首頁-學習護照狀況
        {
            public string Instanceid { get; set; }
            public string TemplateName { get; set; } //護照名稱
            public string ThreeStepFinishRateStr { get; set; } //完成狀況
            public string Completed { get; set; } //已完成
            public string CheckReview { get; set; } //審核中
            public string Undone { get; set; } //未完成
            public string Total { get; set; } //總數
            public string NecessaryFinishRate { get; set; } //必修完成率
            public string FinishRate { get; set; } //總完成率
            public string StudentFinishRate { get; set; } //學生完成率
        }

        public class IPDChat //首頁-教學評量狀況(雷達圖)
        {
            public string Status { get; set; }
            public double Rate { get; set; }
        }

        public class InternScore //首頁-實習各階段評量成績分數
        {
            public string MemberType { get; set; }
            public string ClassName { get; set; }
            public string MiniCEXscore { get; set; }
            public string PrimaryCareScore { get; set; }
            public string CbDScore { get; set; }
            public string DOPsScore { get; set; }
            public string SemesterGrades { get; set; }
        }

        public class HandsOn //首頁-臨床照顧能力
        {
            public string ClassName { get; set;}
            public string PrimaryCare { get;set; }
            public string StudyNight { get; set; }
            public string StudyOverNight { get; set; }
        }

        public class EPAsBarChart //臨床照護分級(EPAs)狀況長條圖
        {
            public string Finished { get; set; } //已完成數量
            public string TotalFinished { get; set; } //總完成數量
            public string Rate { get; set; } //相差百分比
        }

        public class test 
        {
            public string dept { get; set; }
        }

        public class InternStudentFrom //實習醫學生學習紀錄及回饋表 & 實習醫學生夜間學習紀錄
        {
            public int Instance_ID { get; set; }
            public string Lesson { get; set; } //課程名稱
            public string List { get; set; } //表單名稱
            public DateTime? CreateTime { get; set; } //建立時間
        }

        public class PrimaryCare //實習醫生Primary Care心得報告
        {
            public int Instance_ID { get; set; }
            public string INHOSPID { get; set; }
            public string Lesson { get; set; }
            public string List { get; set; }
            public DateTime? CreateTime { get; set; }
        }

        public class InternEduScore //實習各階段評量
        { 
            public string DeptLevl {get;set;}
            public string InternClassName { get; set; }
            public decimal TotalScore { get; set; }
        }

        #region 首頁表單_實習各階段評量成績分數

        public class MiniCEXScore  //首頁表單_迷你評量表
        {
            public int Instance_ID { get; set; } //表單編號
            public string INHOSPID { get; set; } //院內流水編號
            public string Lesson { get; set; } //課程名稱
            public string List { get; set; } //表單名稱
            public DateTime? CreateTime { get; set; } //建立時間
            public int TempID { get; set; }
        }

        public class InternScoreList //實習各階段評量標題名稱
        {
            public List<MiniCEX_InternFormScore> MiniCEX { get; set; }
            public List<PrimaryCare_InternFormScore> PrimaryCare { get; set; }
        }

        public class MiniCEX_InternFormScore //首頁表單_Mini-CEX實習各階段評量成績分數
        {
            public int InsID { get; set; }
            public string DeptName { get; set; }
            public string TotalScore { get; set; }
            public int TempId { get; set; }
        }

        public class PrimaryCare_InternFormScore //首頁表單_ParimaryCare實習各階段評量成績分數
        {
            public int InsID { get; set; }
            public string DeptName { get; set; }
            public string TotalScore { get; set; }
            public int TempId { get; set; }
        }

        public class SemesterGradesScoreList //首頁表單_學期成績分數標題
        {
            public List<SemesterGrades_InternFormScore> SemesterGrades { get; set; }
        }

        public class SemesterGrades_InternFormScore //首頁表單_學期成績分數
        {
            public string MemberType { get; set; }
            public string ClassName { get; set; }
            public string SemesterGrades { get; set; }
        }

        #endregion

        public class StudyPassPort //學習護照
        {
            public string Instanceid { get; set; }
            public string EmpCode { get; set; }
            public string EmpName { get; set; }
            public string TemplateName { get; set; }
            public string FinishStatus { get; set; }
            public string Completed { get; set; } //已完成
            public string CheckReview { get; set; } //審核中
            public string Undone { get; set; } //未完成
            public string Total { get; set; } //總數
            public string NecessaryFinishRate { get; set; }
            public string TotalFinish { get; set; }
            public string StudentFinishRate { get; set; }
        }

        public class StudyPassPortStatus //學習護照狀況長條圖
        {
            public string Finished { get; set; } //已完成數量
            public string TotalFinished { get; set; } //總完成數量
            public string Rate { get; set; } //相差百分比
        }

        public class MiniCEX //迷你評量表
        {
            public int Instance_ID { get; set; } //表單編號
            public string INHOSPID { get; set; } //院內流水編號
            public string Lesson { get; set; } //課程名稱
            public string List { get; set; } //表單名稱
            public DateTime? CreateTime { get; set; } //建立時間
        }

        public class DOPS //DOPS表(臨床技術直擊評量表)
        {
            public int Instance_ID { get; set; } //表單編號
            public string Lesson { get; set; } //課程名稱
            public string List { get; set; } //表單名稱
            public DateTime? CreateTime { get; set; } //建立時間
        }

        public class CBD //案例病歷討論評量表
        {
            public int Instance_ID { get; set; } //表單編號
            public string Lesson { get; set; } //課程名稱
            public string List { get; set; } //表單名稱
            public DateTime? CreateTime { get; set; } //建立時間
        }

        public class Satisfaction //滿意度
        {
            public int Instance_ID { get; set; } //表單編號
            public string Lesson { get; set; } //課程名稱
            public string List { get; set; } //表單名稱
            public DateTime? CreateTime { get; set; } //建立時間
        }

        #region 學習護照

        public class PassPortTitle //學習護照清單標題
        {
            public string PassPortName { get; set; }
        }

        public class PassPortList //學習護照清單
        {
            public string Title { get; set; }
            public List<FormItem> Items { get; set; }           
        }

        public class FormItem
        {
            public string ItemName { get; set; }
            public DateTime? ModifyDate { get; set; }
            public string AuditTeacher { get; set; }
            public string IitemID { get; set; }
        }

        public class PassPortListTitle //學習護照表單內容標題
        {
            public string PassPort { get; set; }
            public string ItemName { get; set; }
            public string TeacherName { get; set; }
            public string CurrentStatus { get; set; }
            public DateTime? ModifyDate { get; set; }
            public DateTime? SubmitTime { get; set; }
        }

        public class PassPortForm //學習護照表單內容
        {
            public string FieldItem { get; set; }
            public string FieldValue { get; set; }
        }

        #endregion

        #region 跨團隊溝通

        public class CATeamTitle // 標題
        {
            public string InstanceID { get; set; }
            public string DropdownTitle { get; set; }
            public string Discussion { get; set; }
            public string DeptName { get; set; }
            public DateTime? Sdate { get; set; }
            public DateTime? Edate { get; set; }
            public string ConferenceTime { get; set; }
        }

        public class CATeamSign //簽到名單
        {
            public string EmpName { get; set; }
            public DateTime? SignTime { get; set; }
            public string RoleName { get; set; }
        }

        public class CATeamApproval //簽核
        {
            public int Order { get; set; } //順序
            public string ApprovalName { get; set; } //簽核人
            public string ApprovalStatus { get; set; } //簽核狀態
            public DateTime? ApprovalTime { get; set; } //簽核時間
            public string Memo { get; set; } //備註
        }
        #endregion

        #region 教學門住診紀錄表

        public class ClinicRecord //教學門住診紀錄表-紀錄清單
        {
            public string RecordTitle { get; set; } //下拉選單-記錄清單
            public string RecordTheme { get; set; } //會議主題
            public string ConferenceSection { get; set; } //會議科別
            public string ConferenceTime { get; set; } //會議時間
            public DateTime? Sdate { get; set; }
            public DateTime? Edate { get; set; }
            public string InstanceID { get; set; } //表單紀錄流水編號
        }

        public class ClinicSignList //教學門住診紀錄表-簽到名單
        {
            public string EmpName { get; set; } //姓名
            public string RoleName { get; set; } //身分
            public DateTime? SignTime { get; set; } //簽到時間
        }

        public class ClinicSign //教學門住診紀錄表-簽核
        {
            public int Order { get; set; } //順序
            public string ApprovalName { get; set; } //簽核人
            public string ApprovalStatus { get; set; } //簽核狀態
            public DateTime? ApprovalTime { get; set; } //簽核時間
            public string Memo { get; set; } //備註
        }

        #endregion

        #region EPAs
        public class EAPsEduTeamCode //職員職別職稱
        {
            public string EmpName { get; set; }
            public string Jobcode { get; set; }
            public string Teamcode { get; set; }
        }

        public class EAPsCheckItem //技術項目 & 非技術項目
        {
            public string ItemName { get; set; }
        }

        #endregion
        
        #region OSCE成績

        public class OSCEscore //OSCE成績
        {
            public string HospName { get; set; } //醫院名稱
            public string ExamName { get; set; } //考場
            public DateTime ExamDate { get; set; } //考試日期
            public string QEmpCode { get; set; } //識別證號 & 准考證編號
            public string QEmpName { get; set; } //考生姓名
            public decimal TotalScore { get; set; } //總分
            public decimal ScoreRate { get; set; } //得分率
            public int SuccessStations { get; set; } //通過站數
            public string DropPoint { get; set; } //落點
            public string SatgeNo { get; set; } //站別
            public string SatgeName { get; set; } //教案名稱
            public decimal PassScore { get; set; } //及格總分
            public int PassStage { get; set; } //及格站數
            public decimal ExamScore { get; set; } //考生分數
            public List<IKASA_OSCEExamScoreDto> IKASA_OSCEExamScores { get; set; }
            public List<IKASA_OSCEExamStageDto> IKASA_OSCEExamStages { get; set; }
        }

        public partial class IKASA_OSCEExamScoreDto
        {
            public string StageName { get; set; }
            public decimal PassScore { get; set; }
            public string ScoreLevel { get; set; }
            public string TotalScoreLevel { get; set; }
            public int Rank { get; set; }
            public int TotalRank { get; set; }
            public string IsPassStr
            {
                get
                {
                    if (this.IsPass)
                    {
                        return "通過";
                    }
                    else
                    {
                        return "未通過";
                    }
                }

            }
            public bool IsPass { get; set; }
        }

        public partial class IKASA_OSCEExamStageDto
        {
            public virtual string DtoKey { get; set; }
            public virtual string ExamID { get; set; }
            public virtual int StageNo { get; set; }
            public virtual string StageName { get; set; }
            public virtual decimal? PassScore { get; set; }
            public virtual IKASA_OSCEExamDto IKASA_OSCEExam { get; set; }
        }

        public partial class IKASA_OSCEExamDto
        {
            public string HospName { get; set; }
            public string QEmpCode { get; set; }
            public string QEmpName { get; set; }
            public string QExamIdno { get; set; }
            public decimal QTotalScore { get; set; }
            public decimal QScoreRate { get; set; }
            public int QPassStageCount { get; set; }
            public string QScoreLevel { get; set; }
        }

        #endregion

        #region 學習階段(下拉選單)
        public class LearningPhase
        {
            public string StudentNo { get; set; } //學號
            public string JobTitle { get; set; } //職稱
            public string TeamName { get; set; } //組別
            public string InternshipTime { get; set; } //實習期間
        }

        #region 個人資訊

        public class Person
        {
            public string EmpCode { get; set; } //職編
            public string EmpName { get; set; } //姓名
            public List<string> TrainingDate { get; set; } //實習期間
        }
        #endregion

        #endregion

        #region 個人學習歷程

        public class PersonalLearningHistoryFormList //主標題
        {
            public string Dept { get; set; } //部門名稱
            //public string TermRange { get; set; } //實習區間
            public List<PersonalLearningHistory> Items { get; set; }
        }

        public class PersonalLearningHistory //次標題
        {
            public string EduTermId { get; set; } //編號
            
            public string CourseTitle { get; set; } //名稱
        }

        public class PersonalLearningList //內容清單
        {
            public string EduTermId { get; set; } //編號
            public string InsID { get; set; } //表單編號
            public string CourseTitle { get; set; } //名稱
            public string TermName { get; set; } //組別名稱
            public string TermRange { get; set; } //實習區間
            public string LearnignContent { get; set; } //表單內容標題
            public List<int> OtherForms { get; set; }
        }

        public class PersonalLearningContent
        {
            public int INSTANCE_ID { get; set; }
            public string TermName { get; set; }
            public string TermContent { get; set; }
        }

        public class PersonalLearningForm
        {
            public int Instance_ID { get; set; } //表單編號
            public string Instance_NAME { get; set; } //
            public bool? IsPass { get; set; } //
            public DateTime? CreateTime { get; set; } //建立時間
        }
        public virtual DateTime INSTANCE_CREATE_DATETIME { get; set; }  

        public class LearningHistoryContent
        {
            public string TermName { get; set; }
            public DateTime Sdate { get; set; }
            public DateTime Edate { get; set; }
            public string TermRange { get; set; }
        }

        public class PersonalTitle 
        {
            public string Title { get; set; }
        }
        #endregion
    }
}