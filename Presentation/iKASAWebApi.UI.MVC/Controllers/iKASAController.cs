using KMU.EduActivity.ApplicationLayer.Assemblers;
using KMU.EduActivity.ApplicationLayer.DTO;
using KMU.EduActivity.ApplicationLayer.Services;
using KMU.EduActivity.DomainModel.Entities;
using KMUH.iKASAWebApi.UI.MVC.Models;
using KMUH.iKASAWebApi.UI.MVC.SecurityReadWcf;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.UI;
using System.Reflection;
using System.Drawing;
using System.Web.UI.WebControls;
using System.Threading.Tasks;
using System.Web.Security;
using System.Web.Hosting;
using System.Text;
using HtmlAgilityPack;


namespace KMUH.iKASAWebApi.UI.MVC.Controllers
{
    public class iKASAController : Controller
    {
        #region 第一週 Api項目

        #region 學生教學儀錶板首頁_學習護照狀況_Api OK
        public JsonResult EduPassport(string eduyear, string jobcode, string deptcode, string empcode, string templateid, DateTime? sdate, DateTime? edate, string itemid, DataTable itemdata)
        {
            using (EduActivityContextService service = new EduActivityContextService())
            {
                List<iKASAModel.EduPassport> result = new List<iKASAModel.EduPassport>();

                var empdata = (from mem in service.UnitOfWork.Members
                               join run in service.UnitOfWork.EduTeamMemberRundowns on mem.MemberID equals run.MemberID
                               join term in service.UnitOfWork.EduTerms on run.EduTermID equals term.EduTermID
                               where (eduyear == null || eduyear == "" || term.EduYear == eduyear) && (jobcode == null || jobcode == "" || term.JobCode == jobcode)
                               && (deptcode == null || deptcode == "" || term.DepCode == deptcode || term.Department == deptcode)
                               && (empcode == null || empcode == "" || mem.IsHospMember == empcode || mem.Name == empcode)
                               && (sdate == null || mem.DateFrom.Date >= sdate) && (edate == null || mem.DateFrom.Date <= edate)
                               select mem.IsHospMember).Distinct().ToList();

                var getedList = (from instance in service.UnitOfWork.EduPassportInstances
                                 join template in service.UnitOfWork.EduPassportTemplates on instance.TemplateID equals template.TemplateID
                                 join emp in service.UnitOfWork.V_KmuEmps on instance.EmpCode equals emp.Empcode
                                 where (templateid == null || templateid == "" || instance.TemplateID == templateid)
                                 && (empdata.Contains(instance.EmpCode) || (instance.EmpCode == empcode)) && instance.Status == "V"
                                 select new { instance, template, emp }).ToList();

                EduPassportInstanceAssembler asm = new EduPassportInstanceAssembler();

                foreach (var ins in getedList)
                {
                    EduPassportInstanceDto dto = asm.Assemble(ins.instance);
                    iKASAModel.EduPassport dtos = new iKASAModel.EduPassport();

                    dto.IsGet = true;
                    dto.ItemCount = ins.instance.EduPassportInsItems.Where(c => c.ItemID != "").Count();
                    dto.FinishCount = ins.instance.EduPassportInsItems.Count(c => c.Status == "V");
                    dto.WaitingCount = ins.instance.EduPassportInsItems.Count(c => c.Status == "1");
                    dto.NotFinishCount = ins.instance.EduPassportInsItems.Count(c => c.Status == "0");
                    dto.StudentFinishCount = ins.instance.EduPassportInsItems.Count(c => (c.Status == "1" || c.Status == "V"));
                    dto.NecessaryCount = ins.instance.EduPassportInsItems.Count(c => c.EduPassportItem.IsNecessary);
                    dto.NecessaryNotFinishCount = ins.instance.EduPassportInsItems.Count(c => c.EduPassportItem.IsNecessary && c.Status == "0");
                    dto.NecessaryWaitingCount = ins.instance.EduPassportInsItems.Count(c => c.EduPassportItem.IsNecessary && c.Status == "1");
                    dto.NecessaryFinishCount = ins.instance.EduPassportInsItems.Count(c => c.EduPassportItem.IsNecessary && c.Status == "V");

                    dtos.TemplateName = dto.TemplateName; //護照名稱
                    dtos.ThreeStepFinishRateStr = dto.ThreeStepFinishRateStr; //完成狀況
                    dtos.Completed = dto.ThreeStepFinishRateStr.Split('/')[0]; //已完成
                    dtos.CheckReview = dto.ThreeStepFinishRateStr.Split('/')[1]; //審核中
                    dtos.Undone = dto.ThreeStepFinishRateStr.Split('/')[2]; //未完成
                    dtos.Total = dto.ThreeStepFinishRateStr.Split('/')[3]; //總數
                    dtos.NecessaryFinishRate = dto.NecessaryFinishRate; //必修完成率
                    dtos.FinishRate = dto.FinishRate; //總完成率
                    dtos.StudentFinishRate = dto.StudentFinishRate; //學生完成率
                    result.Add(dtos);
                }

                string json = JsonConvert.SerializeObject(result, new JsonSerializerSettings //日期格式化
                {
                    DateFormatString = "yyyy/MM/dd"
                });
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public DataTable QueryEduPassportInsItemData(string itemid, List<EduPassportInstanceDto> inslist)
        {
            using (EduActivityContextService service = new EduActivityContextService())
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("學生職編");
                dt.Columns.Add("學生姓名");
                dt.Columns.Add("項目名稱");
                dt.Columns.Add("項目狀態");
                dt.Columns.Add("審核老師職編");
                dt.Columns.Add("審核老師姓名");

                List<EduPassportItemDet> itemdets = service.UnitOfWork.EduPassportItemDets.Where(c => c.ItemID == itemid).ToList();

                Dictionary<int, string> detidmap = new Dictionary<int, string>();
                foreach (EduPassportItemDet det in itemdets)
                {
                    string columnname = det.FieldDesc;
                    int count = 0;

                    while (dt.Columns.Contains(columnname))
                    {
                        count++;
                        columnname = columnname + "_" + count.ToString();
                    }
                    detidmap.Add(det.ID, columnname);
                    dt.Columns.Add(columnname);
                }

                var datas = from iitems in service.UnitOfWork.EduPassportInsItems
                            join ins in inslist on iitems.InstanceID equals ins.InstanceID
                            join teacher in service.UnitOfWork.V_KmuEmps on iitems.TTarget equals teacher.Empcode
                            into tempteacher
                            from teacher in tempteacher.DefaultIfEmpty()
                            where iitems.ItemID == itemid
                            select new { iitems, ins, teacher };

                foreach (var data in datas)
                {
                    DataRow dr = dt.NewRow();
                    dr["學生職編"] = data.ins.EmpCode;
                    dr["學生姓名"] = data.ins.EmpName;
                    dr["項目名稱"] = data.iitems.ItemName;
                    switch (data.iitems.Status)
                    {
                        case "0":
                            dr["項目狀態"] = "未完成";
                            break;
                        case "1":
                            dr["項目狀態"] = "待審核";
                            break;
                        case "V":
                            dr["項目狀態"] = "已完成";
                            break;
                    }

                    if (data.teacher != null)
                    {
                        dr["審核老師職編"] = data.teacher.Empcode;
                        dr["審核老師姓名"] = data.teacher.Empname;
                    }

                    foreach (var det in data.iitems.EduPassportInsItemDets)
                    {
                        dr[detidmap[det.DetID.Value]] = det.FieldValue;
                    }

                    dt.Rows.Add(dr);
                }
                return dt;
            }
        }
        #endregion

        #region 學生教學儀錶板首頁_臨床照顧能力_Api OK

        public JsonResult IPDData(string empcode)
        {
            using (EduActivityContextService service = new EduActivityContextService())
            {
                List<IKASA_IPDDataCountDto> data = service.ReadIKASA_IPDDataCounts(c => c.EmpCode == empcode).OrderBy(c => c.SDATE).ToList();
                DataTable result = new DataTable();
                result.Columns.Add("月份");

                List<DateTime> datelist = data.Select(c => c.SDATE).Distinct().OrderByDescending(c => c).ToList();

                foreach (DateTime date in datelist)
                {
                    result.Columns.Add(date.ToString("yyyyMM"));
                }

                DataRow drPrimaryCare = result.NewRow();
                drPrimaryCare["月份"] = "PrimaryCare人數";
                DataRow drIPDNote = result.NewRow();
                drIPDNote["月份"] = "病歷篇數";
                DataRow drWorkHr = result.NewRow();
                drWorkHr["月份"] = "排班時數";

                foreach (var d in data)
                {
                    drPrimaryCare[d.SDATE.ToString("yyyyMM")] = d.PrimaryCareCount == null ? 0 : d.PrimaryCareCount.Value;
                    drIPDNote[d.SDATE.ToString("yyyyMM")] = d.IPDNoteCount == null ? 0 : d.IPDNoteCount.Value;
                    drWorkHr[d.SDATE.ToString("yyyyMM")] = d.WorkHour == null ? 0 : d.WorkHour.Value;
                }
                result.Rows.Add(drPrimaryCare);
                result.Rows.Add(drIPDNote);
                result.Rows.Add(drWorkHr);

                List<Dictionary<string, object>> serializedResult = new List<Dictionary<string, object>>();
                foreach (DataRow row in result.Rows)
                {
                    var dict = new Dictionary<string, object>();
                    foreach (DataColumn col in result.Columns)
                    {
                        dict[col.ColumnName] = row[col];
                    }
                    serializedResult.Add(dict);
                }

                return Json(serializedResult, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region 學生教學儀錶板首頁_教學評量狀況_Api(雷達圖) OK
        public JsonResult IPDChart(string empcode, string memberid)
        {
            using (EduActivityContextService service = new EduActivityContextService())
            {
                List<iKASAModel.IPDChat> result = new List<iKASAModel.IPDChat>();
                string sql = @"select a.INSTANCE_ID,a.expireDate,c.Status from FORM_INSTANCES a inner join FORM_INSTANCE_TARGETS c on   
                            a.INSTANCE_ID = c.INSTANCE_ID where (a.PARENT_INSTANCE_ID is null or (a.PARENT_INSTANCE_ID is not null and
                            not exists (select 1 from FORM_INSTANCES b where b.PARENT_INSTANCE_ID = a.parent_instance_id and b.TEMPLATE_ID < 
                            a.template_id and b.status ='0'))) and (a.TargetID = @empcode)";

                var data = (service.UnitOfWork as EduActivityContext).ExecuteQuery<iKasaFormFinishObject>(sql, new SqlParameter("@memberid", GetDBObject(memberid)), new SqlParameter("@empcode", GetDBObject(empcode)));

                int totalcount = 0;
                int finishcount = 0;
                int notfinishcount = 0;
                int expirenotfinishcount = 0;

                foreach (var d in data)
                {
                    totalcount++;
                    if (d.status == "1")
                    {
                        finishcount++;
                    }
                    else
                    {
                        if (d.expiredate >= DateTime.Now)
                        {
                            notfinishcount++;
                        }
                        else
                        {
                            expirenotfinishcount++;
                        }
                    }
                }

                if (totalcount == 0)
                {
                }
                else
                {
                    double r1 = (Convert.ToDouble(finishcount) / Convert.ToDouble(totalcount)) * Convert.ToDouble(100);
                    double r2 = (Convert.ToDouble(notfinishcount) / Convert.ToDouble(totalcount)) * Convert.ToDouble(100);
                    double r3 = Convert.ToDouble(100) - r1 - r2;
                    result.Add(new iKASAModel.IPDChat { Status = "已完成", Rate = r1 });
                    result.Add(new iKASAModel.IPDChat { Status = "未完成", Rate = r2 });
                    result.Add(new iKASAModel.IPDChat { Status = "逾期未完成", Rate = r2 });
                }
                return Json(result, "application/json", JsonRequestBehavior.AllowGet);
            }
        }

        private object GetDBObject(object input)
        {
            if (input == null)
            {
                return DBNull.Value;
            }
            else
            {
                return input;
            }
        }
        #endregion

        #region 學生教學儀錶板首頁_學習護照狀況長條圖 OK

        public ActionResult StudyPassPortBarChart(string eduyear, string jobcode, string deptcode, string empcode, string templateid, DateTime? sdate, DateTime? edate, string itemid, DataTable itemdata)
        {
            try
            {
                using (EduActivityContextService Service = new EduActivityContextService())
                {
                    List<iKASAModel.StudyPassPortStatus> result = new List<iKASAModel.StudyPassPortStatus>();

                    var empdata = (from mem in Service.UnitOfWork.Members
                                   join run in Service.UnitOfWork.EduTeamMemberRundowns on mem.MemberID equals run.MemberID
                                   join term in Service.UnitOfWork.EduTerms on run.EduTermID equals term.EduTermID
                                   where (eduyear == null || eduyear == "" || term.EduYear == eduyear)
                                   && (jobcode == null || jobcode == "" || term.JobCode == jobcode)
                                   && (deptcode == null || deptcode == "" || term.DepCode == deptcode || term.Department == deptcode)
                                   && (empcode == null || empcode == "" || mem.IsHospMember == empcode || mem.Name == empcode)
                                   && (sdate == null || mem.DateFrom.Date >= sdate) && (edate == null || mem.DateFrom.Date <= edate)
                                   select mem.IsHospMember).Distinct().ToList();

                    var getedList = (from instance in Service.UnitOfWork.EduPassportInstances
                                     join template in Service.UnitOfWork.EduPassportTemplates
                                     on instance.TemplateID equals template.TemplateID
                                     join emp in Service.UnitOfWork.V_KmuEmps
                                     on instance.EmpCode equals emp.Empcode
                                     where (templateid == null || templateid == "" || instance.TemplateID == templateid)
                                     && (empdata.Contains(instance.EmpCode) || (instance.EmpCode == empcode)) && instance.Status == "V"
                                     select new { instance, template, emp }).ToList();

                    EduPassportInstanceAssembler asm = new EduPassportInstanceAssembler();

                    foreach (var ins in getedList)
                    {
                        EduPassportInstanceDto dto = asm.Assemble(ins.instance);
                        iKASAModel.StudyPassPortStatus dtos = new iKASAModel.StudyPassPortStatus();

                        dto.IsGet = true;
                        dto.ItemCount = ins.instance.EduPassportInsItems.Where(c => c.ItemID != "").Count();
                        dto.FinishCount = ins.instance.EduPassportInsItems.Count(c => c.Status == "V");
                        dto.WaitingCount = ins.instance.EduPassportInsItems.Count(c => c.Status == "1");
                        dto.NotFinishCount = ins.instance.EduPassportInsItems.Count(c => c.Status == "0");
                        dto.StudentFinishCount = ins.instance.EduPassportInsItems.Count(c => (c.Status == "1" || c.Status == "V"));
                        dto.NecessaryCount = ins.instance.EduPassportInsItems.Count(c => c.EduPassportItem.IsNecessary);
                        dto.NecessaryNotFinishCount = ins.instance.EduPassportInsItems.Count(c => c.EduPassportItem.IsNecessary && c.Status == "0");
                        dto.NecessaryWaitingCount = ins.instance.EduPassportInsItems.Count(c => c.EduPassportItem.IsNecessary && c.Status == "1");
                        dto.NecessaryFinishCount = ins.instance.EduPassportInsItems.Count(c => c.EduPassportItem.IsNecessary && c.Status == "V");

                        dtos.Finished = dto.FinishRate;

                        result.Add(dtos);
                    }

                    int totalCount = getedList.Count; //獲得總紀錄數量
                    int notFinishCount = result.Count(dtos => dtos.Finished != "100%"); // 計算未達100%的記錄數量
                    int totalFinishCount = totalCount - notFinishCount;

                    double RateCount = Math.Round(((double)totalFinishCount / totalCount) * 100, 2); //計算百分比誤差值，並四捨五入

                    iKASAModel.StudyPassPortStatus status = new iKASAModel.StudyPassPortStatus();
                    status.Finished = totalFinishCount.ToString(); // 計算總數跟已完成的數量
                    status.TotalFinished = totalCount.ToString();

                    status.Rate = RateCount.ToString() + "%";

                    result.Clear();
                    result.Add(status);

                    string json = JsonConvert.SerializeObject(result, new JsonSerializerSettings
                    {
                        DateFormatString = "yyyy/MM/dd HH:mm"
                    });

                    return Content(json, "application/json");
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 學生教學儀錶板首頁_實習各階段評量成績分數-不使用

        public string EduTermFullName(string edutermid)
        {
            using (EduActivityContextService service = new EduActivityContextService())
            {
                EduTerm term = service.UnitOfWork.EduTerms.Where(c => c.EduTermID == edutermid).FirstOrDefault();
                return EduTermFullName(term);
            }
        }

        public string EduTermFullName(EduTerm term)
        {
            EduTerm tempterm = term;
            string name = "";

            name = tempterm.Name;

            while (tempterm.EduTerm1 != null)
            {
                name = tempterm.EduTerm1.Name;
                tempterm = tempterm.EduTerm1;
            }

            int lastSpaceIndex = name.LastIndexOf(' ');
            if (lastSpaceIndex >= 0 && lastSpaceIndex < name.Length - 1)
            {
                name = name.Substring(lastSpaceIndex + 1);
            }

            return name;
        }

        public JsonResult InternScore(string empcode)
        {
            try
            {
                using (EduActivityContextService Service = new EduActivityContextService())
                {
                    var empCode = empcode.ToUpper(); //強制轉大寫字母

                    List<iKASAModel.InternScore> result = new List<iKASAModel.InternScore>();

                    var members = from mem in Service.UnitOfWork.Members
                                  join rundown in Service.UnitOfWork.EduTeamMemberRundowns
                                  on mem.MemberID equals rundown.MemberID
                                  join term in Service.UnitOfWork.EduTerms
                                  on rundown.EduTermID equals term.EduTermID
                                  where mem.MemberID == empcode
                                  orderby term.DateFrom
                                  select new { mem, rundown, term };

                    //List<int> existsforms = new List<int>();

                    var datas = (from Scores in Service.UnitOfWork.EduScores
                                 join member in Service.UnitOfWork.Members on Scores.Empcode equals member.IsHospMember
                                 where Scores.Empcode == empcode
                                 select new iKASAModel.InternScore
                             {
                                 MemberType = GetMappedMemberType(member.MemberType),
                                 ClassName = Scores.Classname,
                                 SemesterGrades = Scores.Score.ToString()

                             }).ToList();

                    foreach (var data in datas)
                    {
                        if (data.MiniCEXscore == null) //Mini-CEX
                            data.MiniCEXscore = "-";

                        if (data.PrimaryCareScore == null) //PrimaryCare報告
                            data.PrimaryCareScore = "-";

                        if (data.CbDScore == null) //CbD
                            data.CbDScore = "-";

                        if (data.DOPsScore == null) //DOPs
                            data.DOPsScore = "-";

                        if (data.SemesterGrades == null) //學期成績
                            data.SemesterGrades = "-";
                    }

                    return Json(datas, "application/json", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 個人學習歷程_Api

        private List<iKASAModel.IndividualStudy> GetParent(EduTerm term)
        {
            using (EduActivityContextService service = new EduActivityContextService())
            {
                List<iKASAModel.IndividualStudy> result = new List<iKASAModel.IndividualStudy>();
                if (term.EduTerm1 != null)
                {
                    EduTerm tempterm = term.EduTerm1;
                    do
                    {
                        iKASAModel.IndividualStudy dto = new iKASAModel.IndividualStudy();

                        //dto.EduTermID = tempterm.EduTermID;

                        result.Add(dto);

                        tempterm = tempterm.EduTerm1;
                    }

                    while (tempterm != null);
                }
                return result;
            }
        }

        public ActionResult Termquery(string empcode, bool withpass, string edutermid, string memberID) //個人學習歷程標題_OK
        {
            try
            {
                using (EduActivityContextService Service = new EduActivityContextService())
                {
                    List<EduMemberTermPassDto> parentdtos = new List<EduMemberTermPassDto>();
                    List<EduMemberTermPassDto> result = new List<EduMemberTermPassDto>();
                    List<iKASAModel.PersonalLearningHistory> res = new List<iKASAModel.PersonalLearningHistory>();

                    string memberid = SearchMemberID(empcode, memberID); //查詢memberID

                    //人員查詢
                    var members = (from mem in Service.UnitOfWork.Members
                                   join rundown in Service.UnitOfWork.EduTeamMemberRundowns on mem.MemberID equals rundown.MemberID
                                   join emp in Service.UnitOfWork.V_KmuEmps on mem.IsHospMember equals emp.Empcode
                                   join term in Service.UnitOfWork.EduTerms on rundown.EduTermID equals term.EduTermID
                                   join dept in Service.UnitOfWork.V_departments
                                   on new { deptcode = term.DepCode, hospcode = term.Hospital } equals new { deptcode = dept.Deptcode, hospcode = dept.Shorthospcode }
                                   join jobdata in Service.UnitOfWork.V_CodeRefs on new { codetype = "EduAct_JobSerial", code = term.JobCode } equals new
                                   {
                                       codetype = jobdata.CodeType,
                                       code = jobdata.Code
                                   }
                                   where emp.Empcode == empcode && (Service.UnitOfWork.EduTermFormReqs.Count(c => c.EduTermID ==
                                   term.EduTermID) > 0 || Service.UnitOfWork.EduFormTemplateLists.Count(c => c.EduTermID == term.EduTermID) > 0
                                   || !withpass) && (edutermid == null || term.EduTermID == edutermid) && mem.MemberID == memberID
                                   orderby emp.Idno, term.DateFrom
                                   select new { mem, rundown, emp, term, dept, jobdata }).ToList();

                    foreach (var mem in members)
                    {
                        EduMemberTermPassDto dto = new EduMemberTermPassDto();
                        dto.EduTermID = mem.term.EduTermID;
                        dto.ParentEduTermID = mem.term.ParentEduTermID;
                        dto.TermName = mem.term.Name;
                        dto.Sdate = mem.term.DateFrom;
                        dto.Edate = mem.term.DateTo;
                        dto.JobName = mem.jobdata.Name;
                        dto.DepName = mem.dept.Deptname;



                        EduActivityAppService aservice = new EduActivityAppService();
                        dto.TermName = aservice.GetEduTermFullName(dto.EduTermID); //表單設定的course才列出來
                        result.Add(dto);
                    }

                    Dictionary<string, int> itemCount = new Dictionary<string, int>();

                    //var Modified = result.Concat(parentdtos
                    //    .Where(d => result.Count(m => m.EduTermID == d.EduTermID) == 0))
                    //    .GroupBy(dtos => dtos.TermName)
                    //    .Select(g => new iKASAModel.PersonalLearningHistory
                    //    {
                    //        EduTermId = g.FirstOrDefault() != null ? g.FirstOrDefault().EduTermID : null,
                    //        CourseTitle = g.Key + "(" + g.Count().ToString() + ")" ,
                    //        Dept = g.FirstOrDefault().DepName, //部門名稱
                    //        TermRange = "(" + g.FirstOrDefault().TermRange + ")" //實習期間
                    //    });

                    var Modified = result.GroupBy(dtos => dtos.DepName + "(" + dtos.TermRange + ")")
                                    .Select(g => new iKASAModel.PersonalLearningHistoryFormList
                                    {
                                        Dept = g.Key,
                                        Items = g.Select(item => new iKASAModel.PersonalLearningHistory
                                    {
                                        EduTermId = item.EduTermID,
                                        CourseTitle = item.TermName + "(" + item.TermRange + ")",
                                    }).ToList()}).ToList();

                    string json = JsonConvert.SerializeObject(Modified, new JsonSerializerSettings
                    {
                        DateFormatString = "yyyy/MM/dd"
                    });
                    return Content(json, "application/json");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult TermContent(string empcode, bool withpass, string edutermid) //個人學習歷程清單內容
        {
            try
            {
                using (EduActivityContextService Service = new EduActivityContextService())
                {
                    List<EduMemberTermPassDto> parentdtos = new List<EduMemberTermPassDto>();
                    List<EduMemberTermPassDto> result = new List<EduMemberTermPassDto>();

                    //人員查詢
                    var members = (from mem in Service.UnitOfWork.Members
                                   join rundown in Service.UnitOfWork.EduTeamMemberRundowns on mem.MemberID equals rundown.MemberID
                                   join emp in Service.UnitOfWork.V_KmuEmps on mem.IsHospMember equals emp.Empcode //職員職編
                                   join term in Service.UnitOfWork.EduTerms on rundown.EduTermID equals term.EduTermID //表單內容
                                   join dept in Service.UnitOfWork.V_departments //部門
                                   on new { deptcode = term.DepCode, hospcode = term.Hospital } equals new { deptcode = dept.Deptcode, hospcode = dept.Shorthospcode }
                                   join jobdata in Service.UnitOfWork.V_CodeRefs on new { codetype = "EduAct_JobSerial", code = term.JobCode } equals new
                                   {
                                       codetype = jobdata.CodeType,
                                       code = jobdata.Code
                                   }
                                   where emp.Empcode == empcode && (Service.UnitOfWork.EduTermFormReqs.Count(c => c.EduTermID ==
                                   term.EduTermID) > 0 || Service.UnitOfWork.EduFormTemplateLists.Count(c => c.EduTermID == term.EduTermID) > 0
                                   || !withpass) && (edutermid == null || term.EduTermID == edutermid) //不需要或許會快一點?
                                   orderby emp.Idno, term.DateFrom
                                   select new { mem, rundown, emp, term, dept, jobdata }).ToList();

                    foreach (var mem in members)
                    {
                        EduMemberTermPassDto dto = new EduMemberTermPassDto();
                        dto.EduTermID = mem.term.EduTermID;
                        dto.ParentEduTermID = mem.term.ParentEduTermID;
                        dto.TermName = mem.term.Name;
                        dto.Sdate = mem.term.DateFrom;
                        dto.Edate = mem.term.DateTo;
                        //dto.JobName = mem.jobdata.Name;

                        parentdtos.AddRange(GetParentEduMemberTerms(mem.term));

                        List<EduTermFormReqDto> reqs = GetEduTermFromReq(mem.term.EduTermID);

                        for (int i = 0; i < reqs.Count; i++)
                        {
                            EduTermFormReqDto req = reqs[i];
                            CheckFormReq(empcode, ref req);
                            reqs[i] = req;
                        }

                        dto.TermReqs = reqs;

                        List<int> existsforms = new List<int>();

                        foreach (var i in reqs)
                        {
                            foreach (var form in i.MemberPassForms)
                            {
                                if (!existsforms.Contains(form.INSTANCE_ID))
                                {
                                    existsforms.Add(form.INSTANCE_ID);
                                }
                            }
                        }

                        List<FORM_INSTANCEDto> otherforms = (from ins in Service.UnitOfWork.FORM_INSTANCEs
                                                             where ins.INHOSPID == mem.term.EduTermID
                                                             && (ins.TargetID == mem.mem.IsHospMember || ins.EvalTargetID == mem.mem.MemberID)
                                                             && !existsforms.Contains(ins.INSTANCE_ID) && ins.Status != '0' && ins.PARENT_INSTANCE_ID == null
                                                             select new FORM_INSTANCEDto
                                                             {
                                                                 INSTANCE_ID = ins.INSTANCE_ID,
                                                                 INSTANCE_NAME = ins.INSTANCE_NAME,
                                                                 IsPass = ins.IsPass,
                                                                 INSTANCE_ALTER_DATETIME = ins.INSTANCE_ALTER_DATETIME,
                                                                 INSTANCE_CREATE_DATETIME = ins.INSTANCE_CREATE_DATETIME,
                                                                 DISPLAY_TO_EVALTARGET = ins.FORM_TEMPLATE.DISPLAY_TO_EVALTARGET
                                                             }).ToList();

                        existsforms.AddRange(otherforms.Select(c => c.INSTANCE_ID));

                        List<int> pidlist = (from ins in Service.UnitOfWork.FORM_INSTANCEs
                                             where ins.INHOSPID == mem.term.EduTermID
                                             && (ins.TargetID == mem.mem.IsHospMember || ins.EvalTargetID == mem.mem.MemberID)
                                             && !existsforms.Contains(ins.INSTANCE_ID) && ins.Status != '0' && ins.PARENT_INSTANCE_ID != null
                                             && Service.UnitOfWork.FORM_INSTANCEs.Count(c => c.PARENT_INSTANCE_ID == ins.PARENT_INSTANCE_ID && c.Status == '0') == 0
                                             select ins.PARENT_INSTANCE_ID.Value).Distinct().ToList();

                        foreach (int pid in pidlist)
                        {
                            FORM_INSTANCEDto pdto = (from ins in Service.UnitOfWork.FORM_INSTANCEs
                                                     join pins in Service.UnitOfWork.FORM_INSTANCEs
                                                     on ins.PARENT_INSTANCE_ID equals pins.INSTANCE_ID
                                                     where ins.PARENT_INSTANCE_ID == pid
                                                     && !existsforms.Contains(ins.INSTANCE_ID)
                                                     orderby ins.INSTANCE_ID descending
                                                     select new FORM_INSTANCEDto
                                                     {
                                                         INSTANCE_ID = ins.INSTANCE_ID,
                                                         INSTANCE_NAME = pins.INSTANCE_NAME,
                                                         IsPass = ins.IsPass,
                                                         INSTANCE_ALTER_DATETIME = ins.INSTANCE_ALTER_DATETIME,
                                                         INSTANCE_CREATE_DATETIME = ins.INSTANCE_CREATE_DATETIME,
                                                         DISPLAY_TO_EVALTARGET = pins.FORM_TEMPLATE.DISPLAY_TO_EVALTARGET
                                                     }).FirstOrDefault();
                            otherforms.Add(pdto);

                            existsforms.Add(pdto.INSTANCE_ID);
                        }

                        dto.OtherForms = otherforms;

                        if (dto.OtherForms.Count > 0 || dto.TermReqs.Count > 0) //若缺少dto.OtherForms.Count，會沒有第二層內容
                        {
                            result.Add(dto);
                        }
                    }

                    result.AddRange(parentdtos.Where(c => result.Count(d => d.EduTermID == c.EduTermID) == 0));

                    //var abc = from ins in Service.UnitOfWork.FORM_INSTANCEs where ins.INSTANCE_ID select
                    var response = result.Select(dtos => new
                    {
                        ContentForm = new iKASAModel.PersonalLearningList
                        {
                            EduTermId = dtos.ParentEduTermID, //文件編號
                            InsID = dtos.OtherForms != null && dtos.OtherForms.Count > 0 ? dtos.OtherForms[0].INSTANCE_ID.ToString() : dtos.EduTermID, //表單編號
                            CourseTitle = dtos.TermName,
                            TermName = dtos.TermName, //組別名稱
                            TermRange = dtos.TermRange, //實習期間
                            LearnignContent = RemoveHtmlTags(dtos.DetStr),
                        },
                    });

                    var rs = response.Select(rr => new
                        {
                            ContentForm = new
                            {
                                rr.ContentForm.EduTermId,
                                rr.ContentForm.InsID,
                                rr.ContentForm.CourseTitle,
                                rr.ContentForm.TermName,
                                rr.ContentForm.TermRange,
                                rr.ContentForm.LearnignContent
                            }
                        });

                    var Modified = members.GroupBy(m => m.term.Name).Select(g => new iKASAModel.PersonalLearningList
                        {
                            CourseTitle = g.Key + "(" + g.Count().ToString() + ")"

                        });

                    string json = JsonConvert.SerializeObject(rs, new JsonSerializerSettings
                    {
                        DateFormatString = "yyyy/MM/dd"
                    });
                    return Content(json, "application/json");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ContentResult TermForm(string InsId, string Empcode) //個人學習歷程表單_OK
        {
            try
            {
                var InsID = InsId;

                if (InsID != null && InsID != "")
                {
                    Session["CurrentInstanceID"] = InsID;
                    int instanceid = Convert.ToInt32(InsID);
                    HtmlFormUtility.Components.HtmlForm htmlform = new HtmlFormUtility.Components.HtmlForm();
                    HtmlFormUtility.Components.ViewComponent vc = new HtmlFormUtility.Components.ViewComponent();
                    HtmlFormUtility.FORM_INSTANCES currentinstance = vc.SelectFormInstance(instanceid);
                    htmlform.ReadOnly = true;

                    //外帶instanceid
                    htmlform.ParameterCollection.Add("instance_id", instanceid.ToString());

                    string edutermid = currentinstance.INHOSPID;

                    List<HtmlFormUtility.Components.HtmlForm> list = htmlform.Query(instanceid, true, true, false);

                    string sessionKey = InsID + "htmlform";

                    Session[sessionKey] = htmlform;

                    var formContent = SetHtmlContent();

                    //ReadData(Empcode);

                    if (currentinstance.FORM_TEMPLATES.ALLOW_ATTACHMENT != null)
                    {
                        string attachmentUrl = "InstanceAttachment.aspx?id=" + htmlform.BeforeInstances.Where(c => c.FORM_TEMPLATES.ALLOW_ATTACHMENT == "U").FirstOrDefault().INSTANCE_ID.ToString() + "&auth=" + currentinstance.FORM_TEMPLATES.ALLOW_ATTACHMENT + "', '附件管理', config='height=300,width=500');return false;";
                    }

                    return Content(formContent, "text/html");
                }
                return Content("", "text/html");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string SetHtmlContent() //表單內容設定
        {
            string sessionKey = Session["CurrentInstanceID"] + "htmlform";

            HtmlFormUtility.Components.HtmlForm htmlform = Session[sessionKey] as HtmlFormUtility.Components.HtmlForm;

            List<HtmlFormUtility.Components.HtmlForm> list = htmlform.BeforeForms;

            StringBuilder sb = new StringBuilder();

            foreach (HtmlFormUtility.Components.HtmlForm form in list)
            {
                if (form.ReadOnly)
                {
                    var elements = form.InstanceDocument.DocumentNode.SelectNodes("//*");

                    if (elements != null)
                    {
                        foreach (var element in elements)
                        {
                            element.Attributes.Remove("name");
                        }
                    }
                }

                sb.Append(form.InstanceDocument.DocumentNode.InnerHtml);
            }

            sb.Append(htmlform.InstanceDocument.DocumentNode.InnerHtml);
            return sb.ToString();
        }

        //private void ReadData(string empcode) //讀取個人歷程相關表單
        //{
        //    using (EduActivityAppService Service = new EduActivityAppService())
        //    {
        //        var data = Service.QueryEduMemberTermWithPass(empcode, false, null);

        //        foreach (var d in data)
        //        {
        //            d.UDLinkTemplate = "<a onclick=\"ReadIns('{0}'); return false;\" href='InstanceDetial.aspx?edutermid={1}&mode=v'  >{2}</a>";
        //        }

        //        Session["currentdata"] = data;
        //    }
        //}

        private string RemoveHtmlTags(string input) //處理字串
        {
            string withoutTags = Regex.Replace(input, "<.*?>", string.Empty);
            string withoutNewlines = withoutTags.Replace("\r\n", string.Empty);
            //string replacedText = withoutNewlines.Replace("已完成表單無", "已完成表單-無");
            string replacedText = Regex.Replace(withoutNewlines, "(.+)無", "$1-無"); //用正則表達式 "(.+)無" 來匹配一個或多個字符後面跟著的 "無"，並使用 "$1-無" 進行替換。這裡的 "$1" 表示捕獲組，它將保留在替換字串中，並在其後添加橫線
            return replacedText;
        }

        public JsonResult Term(string empcode, bool withpass, string edutermid)
        {
            using (EduActivityContextService service = new EduActivityContextService())
            {
                List<EduMemberTermPassDto> parentdtos = new List<EduMemberTermPassDto>();
                List<EduMemberTermPassDto> result = new List<EduMemberTermPassDto>();

                var members = (from mem in service.UnitOfWork.Members
                               join rundown in service.UnitOfWork.EduTeamMemberRundowns on mem.MemberID equals rundown.MemberID
                               join emp in service.UnitOfWork.V_KmuEmps on mem.IsHospMember equals emp.Empcode
                               join term in service.UnitOfWork.EduTerms on rundown.EduTermID equals term.EduTermID
                               join dept in service.UnitOfWork.V_departments
                               on new { deptcode = term.DepCode, hospcode = term.Hospital } equals new { deptcode = dept.Deptcode, hospcode = dept.Shorthospcode }
                               join jobdata in service.UnitOfWork.V_CodeRefs on new { codetype = "EduAct_JobSerial", code = term.JobCode } equals new
                               {
                                   codetype = jobdata.CodeType,
                                   code = jobdata.Code
                               }
                               where emp.Empcode == empcode && (service.UnitOfWork.EduTermFormReqs.Count(c => c.EduTermID ==
                                   term.EduTermID) > 0 || service.UnitOfWork.EduFormTemplateLists.Count(c => c.EduTermID == term.EduTermID) > 0
                                   || !withpass) && (edutermid == null || term.EduTermID == edutermid)
                               orderby emp.Idno, term.DateFrom
                               select new
                               {
                                   mem,
                                   rundown,
                                   emp,
                                   term,
                                   dept,
                                   jobdata
                               }).ToList();

                foreach (var mem in members)
                {
                    EduMemberTermPassDto dto = new EduMemberTermPassDto();
                    dto.EduTermID = mem.term.EduTermID;
                    dto.ParentEduTermID = mem.term.ParentEduTermID;
                    dto.TermName = mem.term.Name;
                    dto.Sdate = mem.term.DateFrom;
                    dto.Edate = mem.term.DateTo;
                    dto.JobName = mem.jobdata.Name;

                    parentdtos.AddRange(GetParentEduMemberTerms(mem.term));

                    List<EduTermFormReqDto> reqs = GetEduTermFromReq(mem.term.EduTermID);

                    for (int i = 0; i < reqs.Count; i++)
                    {
                        EduTermFormReqDto req = reqs[i];
                        CheckFormReq(empcode, ref req);
                        reqs[i] = req;
                    }

                    dto.TermReqs = reqs;
                    List<int> existsforms = new List<int>();
                    foreach (var i in reqs)
                    {
                        foreach (var form in i.MemberPassForms)
                        {
                            if (!existsforms.Contains(form.INSTANCE_ID))
                            {
                                existsforms.Add(form.INSTANCE_ID);
                            }
                        }
                    }

                    //第二層表單標題
                    List<FORM_INSTANCEDto> otherforms = (from ins in service.UnitOfWork.FORM_INSTANCEs
                                                         where ins.INHOSPID == mem.term.EduTermID
                                                             && (ins.TargetID == mem.mem.IsHospMember || ins.EvalTargetID == mem.mem.MemberID)
                                                             && !existsforms.Contains(ins.INSTANCE_ID) && ins.Status != '0' && ins.PARENT_INSTANCE_ID == null
                                                         select new FORM_INSTANCEDto
                                                         {
                                                             INSTANCE_ID = ins.INSTANCE_ID,
                                                             INSTANCE_NAME = ins.INSTANCE_NAME,
                                                             IsPass = ins.IsPass,
                                                             INSTANCE_ALTER_DATETIME = ins.INSTANCE_ALTER_DATETIME,
                                                             INSTANCE_CREATE_DATETIME = ins.INSTANCE_CREATE_DATETIME,
                                                             DISPLAY_TO_EVALTARGET = ins.FORM_TEMPLATE.DISPLAY_TO_EVALTARGET
                                                         }).ToList();

                    existsforms.AddRange(otherforms.Select(c => c.INSTANCE_ID));

                    List<int> pidlist = (from ins in service.UnitOfWork.FORM_INSTANCEs
                                         where ins.INHOSPID == mem.term.EduTermID
                                             && (ins.TargetID == mem.mem.IsHospMember || ins.EvalTargetID == mem.mem.MemberID)
                                             && !existsforms.Contains(ins.INSTANCE_ID) && ins.Status != '0' && ins.PARENT_INSTANCE_ID != null
                                             && service.UnitOfWork.FORM_INSTANCEs.Count(c => c.PARENT_INSTANCE_ID == ins.PARENT_INSTANCE_ID && c.Status == '0') == 0
                                         select ins.PARENT_INSTANCE_ID.Value).Distinct().ToList();

                    foreach (int pid in pidlist)
                    {
                        FORM_INSTANCEDto pdto = (from ins in service.UnitOfWork.FORM_INSTANCEs
                                                 join pins in service.UnitOfWork.FORM_INSTANCEs
                                                     on ins.PARENT_INSTANCE_ID equals pins.INSTANCE_ID
                                                 where ins.PARENT_INSTANCE_ID == pid
                                                     && !existsforms.Contains(ins.INSTANCE_ID)
                                                 orderby ins.INSTANCE_ID descending
                                                 select new FORM_INSTANCEDto
                                                 {
                                                     INSTANCE_ID = ins.INSTANCE_ID,
                                                     INSTANCE_NAME = pins.INSTANCE_NAME,
                                                     IsPass = ins.IsPass,
                                                     INSTANCE_ALTER_DATETIME = ins.INSTANCE_ALTER_DATETIME,
                                                     INSTANCE_CREATE_DATETIME = ins.INSTANCE_CREATE_DATETIME,
                                                     DISPLAY_TO_EVALTARGET = pins.FORM_TEMPLATE.DISPLAY_TO_EVALTARGET
                                                 }).FirstOrDefault();
                        otherforms.Add(pdto);
                        existsforms.Add(pdto.INSTANCE_ID);
                    }

                    dto.OtherForms = otherforms;

                    if (dto.OtherForms.Count > 0 || dto.TermReqs.Count > 0) //若缺少dto.OtherForms.Count，會沒有第二層內容
                    {
                        result.Add(dto);
                    }
                }
                result.AddRange(parentdtos.Where(c => result.Count(d => d.EduTermID == c.EduTermID) == 0));

                string json = JsonConvert.SerializeObject(result, new JsonSerializerSettings
                {
                    DateFormatString = "yyyy/MM/dd"
                });
                return Json(json, "application/json", JsonRequestBehavior.AllowGet);
            }
        }

        private List<EduMemberTermPassDto> GetParentEduMemberTerms(EduTerm term)
        {
            using (EduActivityContextService service = new EduActivityContextService())
            {
                List<EduMemberTermPassDto> result = new List<EduMemberTermPassDto>();
                if (term.EduTerm1 != null)
                {
                    EduTerm tempterm = term.EduTerm1;
                    do
                    {
                        EduMemberTermPassDto dto = new EduMemberTermPassDto();
                        dto.EduTermID = tempterm.EduTermID;
                        dto.ParentEduTermID = tempterm.ParentEduTermID;
                        dto.TermName = tempterm.Name;
                        dto.Sdate = tempterm.DateFrom;
                        dto.Edate = tempterm.DateTo;
                        result.Add(dto);
                        tempterm = tempterm.EduTerm1;
                    } while (tempterm != null);
                }
                return result;
            }
        }

        public List<EduTermFormReqDto> GetEduTermFromReq(string edutermid)
        {
            using (EduActivityContextService service = new EduActivityContextService())
            {
                List<EduTermFormReqDto> result = new List<EduTermFormReqDto>();
                var reqs = from q in service.UnitOfWork.EduTermFormReqs join qt in service.UnitOfWork.V_CodeRefs on new { ctype = "EduAct_FormReqType", qtype = q.ReqType } equals new { ctype = qt.CodeType, qtype = qt.Code } where q.EduTermID == edutermid select new { q, qt };

                EduTermFormReqAssembler asm = new EduTermFormReqAssembler();

                foreach (var r in reqs)
                {
                    EduTermFormReqDto dto = asm.Assemble(r.q);
                    dto.ReqTypeName = r.qt.Name;
                    dto.ReqName = GetEduTermFormReqName(r.q.ReqType, r.q.ReqID);
                    result.Add(dto);
                }
                return result;
            }
        }

        private string GetEduTermFormReqName(string reqtype, int reqid)
        {
            using (EduActivityContextService service = new EduActivityContextService())
            {
                string reqname = null;
                switch (reqtype)
                {
                    case "Form":
                        reqname = service.UnitOfWork.FORM_TEMPLATEs.Where(c => c.TEMPLATE_ID == reqid).Select(c => c.TEMPLATE_NAME).FirstOrDefault();
                        break;
                    case "Category":
                        reqname = service.UnitOfWork.FormCategories.Where(c => c.ID == reqid).Select(c => c.CategoryName).FirstOrDefault();
                        break;
                }
                return reqname;
            }
        }

        private void CheckFormReq(string empcode, ref EduTermFormReqDto req)
        {
            using (EduActivityContextService service = new EduActivityContextService())
            {
                int reqid = req.ReqID;
                bool needpass = req.NeedPass;
                string edutermid = req.EduTermID;
                var currentmem = (from mem in service.UnitOfWork.Members
                                  join rd in service.UnitOfWork.EduTeamMemberRundowns on mem.MemberID equals rd.MemberID
                                  where rd.EduTermID == edutermid && mem.IsHospMember == empcode
                                  select mem).FirstOrDefault();

                string memberid = null;

                if (currentmem != null)
                {
                    memberid = currentmem.MemberID;
                }
                switch (req.ReqType)
                {
                    case "Form":
                        var forms = (from ins in service.UnitOfWork.FORM_INSTANCEs
                                     where ins.TEMPLATE_ID == reqid
                                         && (ins.TargetID == empcode || ins.EvalTargetID == memberid)
                                         && (!needpass || (needpass && ins.IsPass == true))
                                         && ins.INHOSPID == edutermid
                                     select new FORM_INSTANCEDto { INSTANCE_ID = ins.INSTANCE_ID, INSTANCE_NAME = ins.INSTANCE_NAME, INSTANCE_ALTER_DATETIME = ins.INSTANCE_CREATE_DATETIME, INSTANCE_CREATE_DATETIME = ins.INSTANCE_CREATE_DATETIME, DISPLAY_TO_EVALTARGET = ins.FORM_TEMPLATE.DISPLAY_TO_EVALTARGET }).ToList();

                        req.MemberPassForms = forms;
                        req.MemberPassCount = forms.Count;
                        break;

                    case "Category":
                        var cates = service.ReadFormCategoryRefs(c => c.CategoryID == reqid).Select(c => c.TEMPLATE_ID).ToList();
                        var catforms = (from ins in service.UnitOfWork.FORM_INSTANCEs
                                        where (cates.Contains(ins.TEMPLATE_ID) || (ins.FORM_TEMPLATE.PARENT_TEMPLATE_ID.HasValue && cates.Contains(ins.FORM_TEMPLATE.PARENT_TEMPLATE_ID.Value)))
                                            && (ins.TargetID == empcode || ins.EvalTargetID == memberid)
                                            && (!needpass || (needpass && ins.IsPass == true))
                                            && ins.INHOSPID == edutermid
                                        select new FORM_INSTANCEDto { INSTANCE_ID = ins.INSTANCE_ID, INSTANCE_NAME = ins.INSTANCE_NAME, DISPLAY_TO_EVALTARGET = ins.FORM_TEMPLATE.DISPLAY_TO_EVALTARGET }).ToList();

                        req.MemberPassForms = catforms;
                        req.MemberPassCount = catforms.Count;
                        break;
                }
            }
        }

        #endregion

        #region 跨團隊溝通_Api OK
        public ActionResult CommAcrossTeams(string empcode, List<string> groupids, string instanceid)
        {
            try
            {
                if (groupids == null) // 檢查 groupids 是否包含 "groupid"
                {
                    throw new ArgumentException("參數錯誤: 請使用正確的參數名稱 'groupids'。");
                }

                using (EduActivityContextService Service = new EduActivityContextService())
                {
                    var empCode = empcode.ToUpper(); //強制轉大寫字母

                    List<RecordInstanceDto> rs = new List<RecordInstanceDto>();
                    RecordInstanceDto result = new RecordInstanceDto();
                    RecordInstanceAssembler asm = new RecordInstanceAssembler();
                    RecordInsDetAssembler detasm = new RecordInsDetAssembler();
                    RecordTemplateAssembler tasm = new RecordTemplateAssembler();
                    RecordInsReaderAssembler rasm = new RecordInsReaderAssembler();

                    List<iKASAModel.CATeamTitle> res = new List<iKASAModel.CATeamTitle>(); //根據下拉清單所選擇項目會只帶出指定紀錄表內容
                    var recs = (from rec in Service.UnitOfWork.RecordInstances
                                join dep in Service.UnitOfWork.V_departments on
                                new { dept = rec.DeptCode, hosp = rec.HospCode } equals new { dept = dep.Deptcode, hosp = dep.Hospcode }
                                where rec.Status == "V" && (rec.RecordInsReaders.Count(c => c.Reader == empcode) > 0
                                || rec.RecordInsSignIns.Count(c => c.EmpCode == empcode) > 0 || rec.RecordInsViewers.Count(c => c.Viewer == empcode) > 0
                                || rec.Recoder == empcode) && groupids.Contains(rec.TemplateID.ToString())
                                select new iKASAModel.CATeamTitle
                                {
                                    Sdate = rec.Sdate, //起始日
                                    Edate = rec.Edate, //結束日
                                    InstanceID = rec.InstanceID, //會議記錄編號
                                    DropdownTitle = rec.Title, //下拉選單
                                    Discussion = rec.Title + "", //會議主題
                                    DeptName = dep.Deptname, //會議科別
                                    ConferenceTime = rec.Sdate.Value.ToString("yyyy/MM/dd HH:mm") + "~" + rec.Edate.Value.ToString("yyyy/MM/dd HH:mm") //會議時間
                                }).ToList();

                    res = recs.ToList();

                    //if (!string.IsNullOrEmpty(instanceid)) 
                    //{
                    //    recs = recs.Where(r => r.InstanceID == instanceid).ToList();
                    //}

                    foreach (var r in recs) //組合會議主題&時間
                    {
                        r.DropdownTitle = r.DeptName + " " + r.DropdownTitle;
                        r.DropdownTitle += "(" + r.Sdate.Value.ToString("yyyy/MM/dd") + ")";
                    }

                    //表單資料內容
                    var insdata = (from ins in Service.UnitOfWork.RecordInstances
                                   join emp in Service.UnitOfWork.V_KmuEmps
                                   on ins.Creater equals emp.Empcode
                                   join dep in Service.UnitOfWork.V_departments
                                   on new { hosp = ins.HospCode, depcode = ins.DeptCode } equals new { hosp = dep.Hospcode, depcode = dep.Deptcode }
                                   into tempdep
                                   from dep in tempdep.DefaultIfEmpty()
                                   where ins.InstanceID == instanceid
                                   select new { ins, emp, dep }).FirstOrDefault();

                    result = asm.Assemble(insdata.ins);

                    if (insdata.dep != null)
                    {
                        result.DeptName = insdata.dep.Deptname;
                    }

                    result.CreaterName = insdata.emp.Empname;
                    result.RecordInsDets = detasm.Assemble(insdata.ins.RecordInsDets).ToList(); //表單內容
                    result.RecordTemplate = tasm.Assemble(insdata.ins.RecordTemplate);
                    result.RecordInsViewers = GetRecordInsViewer(insdata.ins.InstanceID); //簽核
                    result.RecordInsReaders = GetRecordInsReader(insdata.ins.InstanceID);

                    List<iKASAModel.CATeamApproval> SignViewer = new List<iKASAModel.CATeamApproval>(); //簽核
                    if (insdata != null)
                    {
                        var recordInsViewers = GetRecordInsViewer(insdata.ins.InstanceID);

                        foreach (var apr in result.RecordInsViewers)
                        {
                            var ApprName = recordInsViewers.FirstOrDefault(n => n.Viewer == apr.Viewer);

                            iKASAModel.CATeamApproval dtos = new iKASAModel.CATeamApproval()
                            {
                                Order = apr.ViewOrder, //順序
                                ApprovalName = ApprName.EmpName, //簽核人
                                ApprovalTime = apr.ViewTime, //簽核時間
                                ApprovalStatus = apr.ViewStatus, //簽核狀態
                                Memo = apr.ViewMemo.Replace("\r\n", ",") //備註
                            };

                            SignViewer.Add(dtos);
                        }
                    }

                    //簽到名單
                    var signIns = from si in Service.UnitOfWork.RecordInsSignIns
                                  join emp in Service.UnitOfWork.V_KmuEmps
                                  on si.EmpCode equals emp.Empcode
                                  join rtype in Service.UnitOfWork.V_CodeRefs
                                  on new { t = si.RoleType, ct = "EduAct_ActRoleType" } equals new { t = rtype.Code, ct = rtype.CodeType }
                                  into temprtype
                                  from rtype in temprtype.DefaultIfEmpty()
                                  where si.InstanceID == instanceid
                                  orderby si.SignTime
                                  select new { si, emp, rtype };

                    List<iKASAModel.CATeamSign> SignList = new List<iKASAModel.CATeamSign>();
                    foreach (var d in signIns)
                    {

                        iKASAModel.CATeamSign dtos = new iKASAModel.CATeamSign
                        {
                            EmpName = d.emp.Empname, //姓名
                            SignTime = d.si.SignTime, //簽到時間
                            RoleName = d.rtype == null ? "一般出席者" : d.rtype.Name //身份                   
                        };
                        SignList.Add(dtos);
                    }

                    var response = new
                    {
                        RecordTitle = recs, //標題
                        RecordSignIn = SignList, //簽到名單
                        Approval = SignViewer //簽核
                    };

                    string json = JsonConvert.SerializeObject(response, new JsonSerializerSettings
                    {
                        DateFormatString = "yyyy/MM/dd HH:mm"
                    });
                    return Content(json, "application/json");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RecordInsViewerDto> GetRecordInsViewer(string instanceid)
        {
            using (EduActivityContextService Service = new EduActivityContextService())
            {
                var list = Service.ReadRecordInsViewers(c => c.InstanceID == instanceid).OrderBy(c => c.ViewOrder).ToList();

                foreach (var i in list)
                {
                    V_KmuEmpDto emp = Service.ReadV_KmuEmps(c => c.Empcode == i.Viewer).FirstOrDefault();
                    if (emp != null)
                    {
                        i.EmpName = emp.Empname;
                    }
                }
                return list;
            }
        }

        public List<RecordInsReaderDto> GetRecordInsReader(string instanceid)
        {
            using (EduActivityContextService service = new EduActivityContextService())
            {
                var list = service.ReadRecordInsReaders(c => c.InstanceID == instanceid).OrderBy(c => c.Reader).ToList();

                foreach (var i in list)
                {
                    V_KmuEmpDto emp = service.ReadV_KmuEmps(c => c.Empcode == i.Reader).FirstOrDefault();
                    if (emp != null)
                    {
                        i.EmpName = emp.Empname;
                    }
                }
                return list;
            }
        }

        //protected Dictionary<string, string> FuncParams
        //{
        //    get
        //    {
        //        return Session["FuncParams"] as Dictionary<string, string>;
        //    }
        //    set
        //    {
        //        Session["FuncParams"] = value;
        //    }
        //}

        //private List<RecordInstanceDto> ReadFormList()
        //{
        //    string groupid = this.FuncParams["groupid"];
        //    List<string> gids = groupid.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).ToList();

        //    using (EduActivityAppService Service = new EduActivityAppService())
        //    {
        //        List<RecordInstanceDto> list = Service.GetiKasaMemberRecordInstance(this.CurrentPersonInfo.empcode, gids);

        //        return list;
        //    }
        //}

        #endregion

        #region 實習醫學生學習紀錄及回饋表_Api && 實習醫學生夜間學習紀錄_Api OK

        public ActionResult InternFormList(string memberid, string groupid)
        {
            try
            {
                // 檢查參數名稱是否正確
                if (groupid == null)
                {
                    throw new ArgumentException("參數錯誤: 請使用正確的參數名稱 'groupid'。");
                }

                using (EduActivityContextService Service = new EduActivityContextService())
                {
                    int catid = Convert.ToInt32(groupid);

                    var members = from mem in Service.UnitOfWork.Members
                                  join rundown in Service.UnitOfWork.EduTeamMemberRundowns on mem.MemberID equals rundown.MemberID
                                  join term in Service.UnitOfWork.EduTerms on rundown.EduTermID equals term.EduTermID
                                  where mem.MemberID == memberid
                                  orderby term.DateFrom
                                  select new { mem, rundown, term };

                    List<iKASAModel.InternStudentFrom> result = new List<iKASAModel.InternStudentFrom>(); //自訂Model類別名稱
                    List<int> existsforms = new List<int>();

                    foreach (var mem in members)
                    {
                        List<iKASAModel.InternStudentFrom> otherforms = (from ins in Service.UnitOfWork.FORM_INSTANCEs
                                                                         where ins.INHOSPID == mem.term.EduTermID && (ins.TargetID == mem.mem.IsHospMember || ins.EvalTargetID ==
                                                                         mem.mem.MemberID) && Service.UnitOfWork.FormCategoryRefs.Count(c => c.CategoryID == catid && c.TEMPLATE_ID ==
                                                                         ins.TEMPLATE_ID) > 0 && !existsforms.Contains(ins.INSTANCE_ID) && ins.Status != '0' && ins.PARENT_INSTANCE_ID ==
                                                                         null
                                                                         select new iKASAModel.InternStudentFrom { Instance_ID = ins.INSTANCE_ID, Lesson = ins.INHOSPID, List = ins.INSTANCE_NAME, CreateTime = ins.INSTANCE_ALTER_DATETIME, }).ToList();

                        existsforms.AddRange(otherforms.Select(c => c.Instance_ID));

                        List<int> pidlist = (from ins in Service.UnitOfWork.FORM_INSTANCEs
                                             where ins.INHOSPID == mem.term.EduTermID
                                             && (ins.TargetID == mem.mem.IsHospMember || ins.EvalTargetID == mem.mem.MemberID) && !existsforms.Contains(ins.INSTANCE_ID)
                                             && ins.Status != '0' && ins.PARENT_INSTANCE_ID != null
                                             && Service.UnitOfWork.FORM_INSTANCEs.Count(c => c.PARENT_INSTANCE_ID == ins.PARENT_INSTANCE_ID && c.Status == '0') == 0
                                           && Service.UnitOfWork.FormCategoryRefs.Count(c => c.CategoryID == catid && c.TEMPLATE_ID == ins.FORM_TEMPLATE.PARENT_TEMPLATE_ID) > 0
                                             select ins.PARENT_INSTANCE_ID.Value).Distinct().ToList();

                        foreach (int pid in pidlist)
                        {
                            iKASAModel.InternStudentFrom pdto = (from ins in Service.UnitOfWork.FORM_INSTANCEs
                                                                 join pins in Service.UnitOfWork.FORM_INSTANCEs
                                                                 on ins.PARENT_INSTANCE_ID equals pins.INSTANCE_ID
                                                                 where ins.PARENT_INSTANCE_ID == pid && !existsforms.Contains(ins.INSTANCE_ID)
                                                                 orderby ins.INSTANCE_ID descending
                                                                 select new iKASAModel.InternStudentFrom
                                                                 {
                                                                     Instance_ID = ins.INSTANCE_ID,
                                                                     Lesson = pins.INHOSPID,
                                                                     CreateTime = ins.INSTANCE_CREATE_DATETIME,
                                                                     List = pins.INSTANCE_NAME
                                                                 }).FirstOrDefault();
                            otherforms.Add(pdto);

                            existsforms.Add(pdto.Instance_ID);
                        }

                        result.AddRange(otherforms);
                    }

                    foreach (var f in result)
                    {
                        try
                        {
                            if (f.Lesson != null)
                            {
                                f.Lesson = GetEduTermFullName(f.Lesson);
                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }

                    var filteredResult = result.Select(f => new
                    {
                        f.Instance_ID,
                        f.Lesson,
                        f.List,
                        f.CreateTime
                    }).ToList();

                    string json = JsonConvert.SerializeObject(filteredResult, new JsonSerializerSettings
                    {
                        DateFormatString = "yyyy/MM/dd HH:mm"
                    });
                    return Content(json, "application/json");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetEduTermFullName(string edutermid)
        {
            using (EduActivityContextService service = new EduActivityContextService())
            {
                EduTerm term = service.UnitOfWork.EduTerms.Where(c => c.EduTermID == edutermid).FirstOrDefault();
                return GetEduTermFullName(term);
            }
        }

        public string GetEduTermFullName(EduTerm term)
        {
            EduTerm tempterm = term;
            string name = "";

            name = tempterm.Name;

            while (tempterm.EduTerm1 != null)
            {
                name = tempterm.EduTerm1.Name + "->" + name;
                tempterm = tempterm.EduTerm1;
            }
            return name;
        }

        #endregion

        #endregion

        #region 第二週 Api項目

        #region 教學門/住診紀錄表_Api OK

        public ActionResult ClinicRecord(string empcode, List<string> groupids, string instanceid)
        {
            try
            {
                if (groupids == null) // 檢查 groupids 是否包含 "groupid"
                {
                    throw new ArgumentException("參數錯誤: 請使用正確的參數名稱 'groupids'。");
                }

                using (EduActivityContextService Service = new EduActivityContextService())
                {
                    List<RecordInstanceDto> rs = new List<RecordInstanceDto>();
                    RecordInstanceDto result = new RecordInstanceDto();
                    RecordInstanceAssembler asm = new RecordInstanceAssembler();
                    RecordInsDetAssembler detasm = new RecordInsDetAssembler();
                    RecordTemplateAssembler tasm = new RecordTemplateAssembler();
                    //RecordInsViewerAssembler vasm = new RecordInsViewerAssembler();
                    RecordInsReaderAssembler rasm = new RecordInsReaderAssembler();
                    //RecordInsSignInAssembler siasm = new RecordInsSignInAssembler();

                    //紀錄清單&標題

                    List<iKASAModel.ClinicRecord> res = new List<iKASAModel.ClinicRecord>(); //根據下拉清單所選擇項目會只帶出指定紀錄表內容

                    var recs = (from rec in Service.UnitOfWork.RecordInstances
                                join dep in Service.UnitOfWork.V_departments on
                                new { dept = rec.DeptCode, hosp = rec.HospCode } equals new { dept = dep.Deptcode, hosp = dep.Hospcode }
                                where rec.Status == "V" && (rec.RecordInsReaders.Count(c => c.Reader == empcode) > 0 || rec.RecordInsSignIns.Count(c => c.EmpCode == empcode) > 0 || rec.RecordInsViewers.Count(c => c.Viewer == empcode) > 0 || rec.Recoder == empcode) && groupids.Contains(rec.TemplateID.ToString())
                                select new iKASAModel.ClinicRecord
                                {
                                    Sdate = rec.Sdate,
                                    Edate = rec.Edate,
                                    InstanceID = rec.InstanceID,
                                    RecordTitle = rec.Title,
                                    RecordTheme = rec.Title + "",
                                    ConferenceSection = dep.Deptname,
                                    ConferenceTime = rec.Sdate.Value.ToString("yyyy/MM/dd HH:mm") + "~" + rec.Edate.Value.ToString("yyyy/MM/dd HH:mm")
                                });

                    res = recs.ToList();

                    //if (!string.IsNullOrEmpty(instanceid))
                    //{
                    //    recs = recs.Where(r => r.InstanceID == instanceid).ToList();
                    //}

                    //組合會議主題&時間
                    foreach (var r in recs)
                    {
                        r.RecordTitle = r.ConferenceSection + " " + r.RecordTitle;
                        r.RecordTitle += "(" + r.Sdate.Value.ToString("yyyy/MM/dd") + ")";
                    }

                    //表單資料內容
                    var insdata = (from ins in Service.UnitOfWork.RecordInstances
                                   join emp in Service.UnitOfWork.V_KmuEmps on ins.Creater equals emp.Empcode
                                   join dep in Service.UnitOfWork.V_departments
                                   on new { hosp = ins.HospCode, depcode = ins.DeptCode } equals new { hosp = dep.Hospcode, depcode = dep.Deptcode }
                                   into tempdep
                                   from dep in tempdep.DefaultIfEmpty()
                                   where ins.InstanceID == instanceid
                                   select new { ins, emp, dep }).FirstOrDefault();

                    result = asm.Assemble(insdata.ins);

                    if (insdata.dep != null)
                    {
                        result.DeptName = insdata.dep.Deptname;
                    }

                    result.CreaterName = insdata.emp.Empname;
                    result.RecordInsDets = detasm.Assemble(insdata.ins.RecordInsDets).ToList(); //表單內容
                    result.RecordTemplate = tasm.Assemble(insdata.ins.RecordTemplate);
                    result.RecordInsViewers = GetRecordInsViewer(insdata.ins.InstanceID); //簽核單
                    result.RecordInsReaders = GetRecordInsReader(insdata.ins.InstanceID);

                    //List<iKASAModel.ClinicPatient> PatientData = new List<iKASAModel.ClinicPatient>(); //病患基本資料

                    //if (insdata != null)
                    //{
                    //    var RecordInsDets = detasm.Assemble(insdata.ins.RecordInsDets).ToList();

                    //    foreach (var pat in result.RecordInsDets)
                    //    {
                    //        var Pat = RecordInsDets.FirstOrDefault(t => t.ControlValue == pat.ControlValue);

                    //        iKASAModel.ClinicPatient dtos = new iKASAModel.ClinicPatient
                    //        {
                    //            Patient = pat.ControlValue,
                    //            Age = pat.ControlValue
                    //        };
                    //        PatientData.Add(dtos);
                    //    }
                    //}

                    List<iKASAModel.ClinicSign> Approval = new List<iKASAModel.ClinicSign>(); //簽核
                    if (insdata != null)
                    {
                        var recordInsViewers = GetRecordInsViewer(insdata.ins.InstanceID);

                        foreach (var apr in result.RecordInsViewers)
                        {
                            var ApprName = recordInsViewers.FirstOrDefault(v => v.Viewer == apr.Viewer);

                            iKASAModel.ClinicSign dtos = new iKASAModel.ClinicSign
                            {
                                Order = apr.ViewOrder, //順序
                                ApprovalName = ApprName.EmpName, //簽核人
                                ApprovalTime = apr.ViewTime, //簽核時間
                                ApprovalStatus = apr.ViewStatus, //簽核狀態
                                Memo = apr.ViewMemo //備註
                            };
                            Approval.Add(dtos);
                        }
                    }

                    //簽到名單
                    var signIns = from si in Service.UnitOfWork.RecordInsSignIns
                                  join emp in Service.UnitOfWork.V_KmuEmps
                                  on si.EmpCode equals emp.Empcode
                                  join rtype in Service.UnitOfWork.V_CodeRefs
                                  on new { t = si.RoleType, ct = "EduAct_ActRoleType" } equals new { t = rtype.Code, ct = rtype.CodeType }
                                  into temprtype
                                  from rtype in temprtype.DefaultIfEmpty()
                                  where si.InstanceID == instanceid
                                  orderby si.SignTime
                                  select new { si, emp, rtype };

                    List<iKASAModel.ClinicSignList> SignInsList = new List<iKASAModel.ClinicSignList>();
                    foreach (var d in signIns)
                    {
                        iKASAModel.ClinicSignList dtos = new iKASAModel.ClinicSignList
                        {
                            EmpName = d.emp.Empname, //姓名
                            SignTime = d.si.SignTime, //簽到時間
                            RoleName = d.rtype.Name //身分
                        };

                        dtos.EmpName = d.emp.Empname;
                        dtos.RoleName = d.rtype == null ? "一般出席者" : d.rtype.Name;

                        SignInsList.Add(dtos);
                    }

                    var response = new
                    {
                        RecordIInsTitle = recs, //表單標題
                        RecordInsSignIns = SignInsList, //簽到名單
                        RecordApproval = Approval //簽核
                    };

                    string json = JsonConvert.SerializeObject(response, new JsonSerializerSettings
                    {
                        DateFormatString = "yyyy/MM/dd HH:mm"
                    });
                    //return Json(json, "application/json", JsonRequestBehavior.AllowGet);
                    return Content(json, "application/json");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string iKASAIDNo
        {
            get
            {
                if (Session["iKasaIdNO"] == null)
                {
                    return null;
                }
                else
                {
                    return Session["iKasaIdNO"].ToString();
                }
            }
            set
            {
                Session["iKasaIdNO"] = value;
            }
        }

        private iKASAPersonInfo iKASAPersonInfo
        {
            get
            {
                return Session["iKasaCurrentPersonInfo"] as iKASAPersonInfo;
            }
            set
            {
                Session["iKasaCurrentPersonInfo"] = value;
            }
        }

        private void RecordClin(string iKasaIdNo)
        {
            using (EduActivityAppService Service = new EduActivityAppService())
            {
                if (iKasaIdNo != null)
                {
                    CurrentIDNo = iKasaIdNo;
                    CurrentMemberIDs = null;
                    CurrentPersonInfo = null;
                }
                else
                {
                    if (CurrentIDNo == null)
                    {
                        if (Session["EduAct_EmpCode"] != null)
                        {
                            var emp = Service.GetVKmuEmpByEmpCode(Session["EduAct_EmpCode"].ToString());
                            if (emp != null)
                            {
                                CurrentIDNo = emp.Idno; //身分證字號
                                CurrentMemberIDs = null;
                                CurrentPersonInfo = null;
                            }
                        }
                    }
                }

                if (CurrentIDNo == null)
                {
                    Response.Redirect("SessionTimeout.aspx");
                    return;
                }

                var userinfo = Service.GetiKASALoginInfo(CurrentIDNo);
                CurrentRoleList = userinfo;

                //if (CurrentMemberID == null) //下拉選單-學習階段
                //{
                //    if (userinfo.Count > 0)
                //    {
                //        rcbRole.SelectedValue = userinfo.FirstOrDefault().memberid;
                //        rcbRole.Text = userinfo.FirstOrDefault().membertypename + " - " + userinfo.FirstOrDefault().teamname;
                //    }
                //}
                //else
                //{
                //    rcbRole.SelectedValue = CurrentMemberID;
                //    rcbRole.Text = userinfo.Where(c => c.memberid == CurrentMemberID).FirstOrDefault().membertypename + " - " + userinfo.Where(c => c.memberid == CurrentMemberID).FirstOrDefault().teamname;
                //}

                //if (rcbRole.SelectedValue != "")
                //{
                //    CurrentPersonInfo = CurrentRoleList.Where(c => c.memberid == rcbRole.SelectedValue).FirstOrDefault();
                //    LoadUserData();
                //    LoadMenu();
                //    ReloadIframe();
                //}
            }
        }

        private string CurrentMemberIDs
        {
            get
            {
                if (Session["iKasaCurrentMemberID"] == null)
                {
                    return null;
                }
                else
                {
                    return Session["iKasaCurrentMemberID"].ToString();
                }
            }
            set
            {
                Session["iKasaCurrentMemberID"] = value;
            }
        }

        private List<iKASAPersonInfo> CurrentRoleList
        {
            get
            {
                return Session["CurrentRoleList"] as List<iKASAPersonInfo>;
            }
            set
            {
                Session["CurrentRoleList"] = value;
            }

        }

        private string CurrentMemberID
        {
            get
            {
                if (Session["iKasaCurrentMemberID"] == null)
                {
                    return null;
                }
                else
                {
                    return Session["iKasaCurrentMemberID"].ToString();
                }
            }
            set
            {
                Session["iKasaCurrentMemberID"] = value;
            }
        }
        #endregion

        #region 實習醫生Primary Care心得報告_Api OK

        public ActionResult PrimaryCare(string memberid, string groupid)
        {
            try
            {
                // 檢查參數名稱是否正確
                if (groupid == null)
                {
                    throw new ArgumentException("參數錯誤: 請使用正確的參數名稱 'groupid'。");
                }

                using (EduActivityContextService service = new EduActivityContextService())
                {
                    int catid = Convert.ToInt32(groupid);

                    //人員
                    var members = from mem in service.UnitOfWork.Members
                                  join rundown in service.UnitOfWork.EduTeamMemberRundowns on mem.MemberID equals rundown.MemberID
                                  join term in service.UnitOfWork.EduTerms on rundown.EduTermID equals term.EduTermID
                                  where mem.MemberID == memberid
                                  orderby term.DateFrom
                                  select new { mem, rundown, term };

                    List<iKASAModel.PrimaryCare> result = new List<iKASAModel.PrimaryCare>();
                    List<int> existsforms = new List<int>();

                    foreach (var mem in members)
                    {
                        List<iKASAModel.PrimaryCare> otherforms = (from ins in service.UnitOfWork.FORM_INSTANCEs
                                                                   where ins.INHOSPID == mem.term.EduTermID && (ins.TargetID ==
                                                                   mem.mem.IsHospMember || ins.EvalTargetID ==
                                                                   mem.mem.MemberID) && service.UnitOfWork.FormCategoryRefs.Count(c =>
                                                                   c.CategoryID == catid && c.TEMPLATE_ID ==
                                                                   ins.TEMPLATE_ID) > 0 && !existsforms.Contains(ins.INSTANCE_ID) && ins.Status !=
                                                                   '0' && ins.PARENT_INSTANCE_ID == null
                                                                   select new iKASAModel.PrimaryCare
                                                                   {
                                                                       Instance_ID = ins.INSTANCE_ID,
                                                                       Lesson = ins.INHOSPID,
                                                                       List = ins.INSTANCE_NAME,
                                                                       CreateTime = ins.INSTANCE_ALTER_DATETIME
                                                                   }).ToList();

                        existsforms.AddRange(otherforms.Select(c => c.Instance_ID));

                        List<int> pidlist = (from ins in service.UnitOfWork.FORM_INSTANCEs
                                             where ins.INHOSPID == mem.term.EduTermID
                                             && (ins.TargetID == mem.mem.IsHospMember || ins.EvalTargetID == mem.mem.MemberID) && !
                                             existsforms.Contains(ins.INSTANCE_ID)
                                             && ins.Status != '0' && ins.PARENT_INSTANCE_ID != null
                                             && service.UnitOfWork.FORM_INSTANCEs.Count(c => c.PARENT_INSTANCE_ID ==
                                             ins.PARENT_INSTANCE_ID && c.Status == '0') == 0
                                             && service.UnitOfWork.FormCategoryRefs.Count(c => c.CategoryID == catid && c.TEMPLATE_ID
                                             == ins.FORM_TEMPLATE.PARENT_TEMPLATE_ID) > 0
                                             select ins.PARENT_INSTANCE_ID.Value).Distinct().ToList();

                        foreach (int pid in pidlist)
                        {
                            iKASAModel.PrimaryCare pdto = (from ins in service.UnitOfWork.FORM_INSTANCEs
                                                           join pins in service.UnitOfWork.FORM_INSTANCEs on ins.PARENT_INSTANCE_ID
                                                           equals pins.INSTANCE_ID
                                                           where ins.PARENT_INSTANCE_ID ==
                                                           pid && !existsforms.Contains(ins.INSTANCE_ID)
                                                           orderby ins.INSTANCE_ID descending
                                                           select new iKASAModel.PrimaryCare
                                                           {
                                                               Instance_ID = ins.INSTANCE_ID,
                                                               Lesson = pins.INHOSPID,
                                                               List = pins.INSTANCE_NAME,
                                                               CreateTime = ins.INSTANCE_CREATE_DATETIME
                                                           }).FirstOrDefault();
                            otherforms.Add(pdto);

                            existsforms.Add(pdto.Instance_ID);
                        }
                        result.AddRange(otherforms);
                    }

                    foreach (var f in result)
                    {
                        try
                        {
                            if (f.Lesson != null)
                            {
                                f.Lesson = GetEduTermFullName(f.Lesson);
                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                    var filteredResult = result.Select(f => new
                    {
                        f.Instance_ID, //表單編號
                        f.Lesson, //課程
                        f.List, //表單
                        f.CreateTime //時間             
                    }).ToList();

                    string json = JsonConvert.SerializeObject(filteredResult, new JsonSerializerSettings
                    {
                        DateFormatString = "yyyy/MM/dd HH:mm:ss"
                    });
                    return Content(json, "application/json");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 學習護照_Api 區分實習階段-OK

        public JsonResult StudyPassPort(string empcode, string job)
        {
            using (EduActivityContextService service = new EduActivityContextService())
            {
                List<iKASAModel.EduPassport> result = new List<iKASAModel.EduPassport>();

                var empdata = (from mem in service.UnitOfWork.Members
                               join run in service.UnitOfWork.EduTeamMemberRundowns on mem.MemberID equals run.MemberID
                               join term in service.UnitOfWork.EduTerms on run.EduTermID equals term.EduTermID
                               where (empcode == null || empcode == "" || mem.IsHospMember == empcode || mem.Name == empcode)
                               select mem.IsHospMember).Distinct().ToList();

                var getedList = (from instance in service.UnitOfWork.EduPassportInstances
                                 join template in service.UnitOfWork.EduPassportTemplates on instance.TemplateID equals template.TemplateID
                                 join emp in service.UnitOfWork.V_KmuEmps on instance.EmpCode equals emp.Empcode
                                 where (empdata.Contains(instance.EmpCode) || (instance.EmpCode == empcode)) && instance.Status == "V"
                                 && instance.JobSerial == job.ToUpper()
                                 select new { instance, template, emp }).ToList();

                EduPassportInstanceAssembler asm = new EduPassportInstanceAssembler();

                foreach (var ins in getedList)
                {
                    EduPassportInstanceDto dto = asm.Assemble(ins.instance);
                    iKASAModel.EduPassport dtos = new iKASAModel.EduPassport();

                    dto.IsGet = true;
                    dto.ItemCount = ins.instance.EduPassportInsItems.Where(c => c.ItemID != "").Count();
                    dto.FinishCount = ins.instance.EduPassportInsItems.Count(c => c.Status == "V");
                    dto.WaitingCount = ins.instance.EduPassportInsItems.Count(c => c.Status == "1");
                    dto.NotFinishCount = ins.instance.EduPassportInsItems.Count(c => c.Status == "0");
                    dto.StudentFinishCount = ins.instance.EduPassportInsItems.Count(c => (c.Status == "1" || c.Status == "V"));
                    dto.NecessaryCount = ins.instance.EduPassportInsItems.Count(c => c.EduPassportItem.IsNecessary);
                    dto.NecessaryNotFinishCount = ins.instance.EduPassportInsItems.Count(c => c.EduPassportItem.IsNecessary && c.Status == "0");
                    dto.NecessaryWaitingCount = ins.instance.EduPassportInsItems.Count(c => c.EduPassportItem.IsNecessary && c.Status == "1");
                    dto.NecessaryFinishCount = ins.instance.EduPassportInsItems.Count(c => c.EduPassportItem.IsNecessary && c.Status == "V");

                    dtos.Instanceid = dto.InstanceID;
                    dtos.TemplateName = dto.TemplateName; //護照名稱
                    dtos.ThreeStepFinishRateStr = dto.ThreeStepFinishRateStr; //完成狀況
                    dtos.Completed = dto.ThreeStepFinishRateStr.Split('/')[0]; //已完成
                    dtos.CheckReview = dto.ThreeStepFinishRateStr.Split('/')[1]; //審核中
                    dtos.Undone = dto.ThreeStepFinishRateStr.Split('/')[2]; //未完成
                    dtos.Total = dto.ThreeStepFinishRateStr.Split('/')[3]; //總數
                    dtos.NecessaryFinishRate = dto.NecessaryFinishRate; //必修完成率
                    dtos.FinishRate = dto.FinishRate; //總完成率
                    dtos.StudentFinishRate = dto.StudentFinishRate; //學生完成率
                    result.Add(dtos);
                }

                string json = JsonConvert.SerializeObject(result, new JsonSerializerSettings //日期格式化
                {
                    DateFormatString = "yyyy/MM/dd"
                });
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
        //public ActionResult StudyPassPort(string eduyear, string jobcode, string deptcode, string empcodename, string templateid, DateTime? sdate, DateTime? edate, string itemid, DataTable itemdata)
        //{
        //    try
        //    {
        //        using (EduActivityContextService service = new EduActivityContextService())
        //        {
        //            List<iKASAModel.StudyPassPort> result = new List<iKASAModel.StudyPassPort>();

        //            var empdata = (from mem in service.UnitOfWork.Members
        //                           join run in service.UnitOfWork.EduTeamMemberRundowns on mem.MemberID equals run.MemberID
        //                           join term in service.UnitOfWork.EduTerms on run.EduTermID equals term.EduTermID
        //                           where (eduyear == null || eduyear == "" || term.EduYear == eduyear)
        //                           && (jobcode == null || jobcode == "" || term.JobCode == jobcode)
        //                           && (deptcode == null || deptcode == "" || term.DepCode == deptcode || term.Department == deptcode)
        //                           && (empcodename == null || empcodename == "" || mem.IsHospMember == empcodename || mem.Name == empcodename)
        //                           && (sdate == null || mem.DateFrom.Date >= sdate) && (edate == null || mem.DateFrom.Date <= edate)
        //                           select mem.IsHospMember).Distinct().ToList();

        //            var getedList = (from instance in service.UnitOfWork.EduPassportInstances
        //                             join template in service.UnitOfWork.EduPassportTemplates
        //                             on instance.TemplateID equals template.TemplateID
        //                             join emp in service.UnitOfWork.V_KmuEmps
        //                             on instance.EmpCode equals emp.Empcode
        //                             where (templateid == null || templateid == "" || instance.TemplateID == templateid)
        //                             && (empdata.Contains(instance.EmpCode) || (instance.EmpCode == empcodename)) && instance.Status == "V"
        //                             select new { instance, template, emp }).ToList();

        //            EduPassportInstanceAssembler asm = new EduPassportInstanceAssembler();

        //            foreach (var ins in getedList)
        //            {
        //                EduPassportInstanceDto dto = asm.Assemble(ins.instance);
        //                iKASAModel.StudyPassPort dtos = new iKASAModel.StudyPassPort();
        //                dto.IsGet = true;
        //                dto.ItemCount = ins.instance.EduPassportInsItems.Where(c => c.ItemID != "").Count();
        //                dto.FinishCount = ins.instance.EduPassportInsItems.Count(c => c.Status == "V");
        //                dto.WaitingCount = ins.instance.EduPassportInsItems.Count(c => c.Status == "1");
        //                dto.NotFinishCount = ins.instance.EduPassportInsItems.Count(c => c.Status == "0");
        //                dto.StudentFinishCount = ins.instance.EduPassportInsItems.Count(c => (c.Status == "1" || c.Status == "V"));
        //                dto.NecessaryCount = ins.instance.EduPassportInsItems.Count(c => c.EduPassportItem.IsNecessary);
        //                dto.NecessaryNotFinishCount = ins.instance.EduPassportInsItems.Count(c => c.EduPassportItem.IsNecessary && c.Status == "0");
        //                dto.NecessaryWaitingCount = ins.instance.EduPassportInsItems.Count(c => c.EduPassportItem.IsNecessary && c.Status == "1");
        //                dto.NecessaryFinishCount = ins.instance.EduPassportInsItems.Count(c => c.EduPassportItem.IsNecessary && c.Status == "V");

        //                dtos.Instanceid = dto.InstanceID;
        //                dtos.EmpCode = ins.emp.Empcode;
        //                dtos.EmpName = ins.emp.Empname;
        //                dtos.TemplateName = ins.template.TemplateName;
        //                dtos.EmpCode = ins.emp.Empcode;
        //                dtos.EmpName = ins.emp.Empname;
        //                dtos.FinishStatus = dto.ThreeStepFinishRateStr;
        //                dtos.NecessaryFinishRate = dto.NecessaryFinishRate;
        //                dtos.Completed = dto.ThreeStepFinishRateStr.Split('/')[0]; //已完成
        //                dtos.CheckReview = dto.ThreeStepFinishRateStr.Split('/')[1]; //審核中
        //                dtos.Undone = dto.ThreeStepFinishRateStr.Split('/')[2]; //未完成
        //                dtos.Total = dto.ThreeStepFinishRateStr.Split('/')[3]; //總數
        //                dtos.TotalFinish = dto.FinishRate;
        //                dtos.StudentFinishRate = dto.StudentFinishRate;
        //                result.Add(dtos);
        //            }
        //            string json = JsonConvert.SerializeObject(result, new JsonSerializerSettings
        //            {
        //                DateFormatString = "yyyy/MM/dd HH:mm"
        //            });

        //            return Content(json, "application/json");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}

        #endregion

        #region 學習護照清單_Api OK

        public ActionResult PassPortList(string instanceid)
        {
            try
            {
                using (EduActivityContextService Service = new EduActivityContextService())
                {
                    CurrentInstanceID = instanceid;
                    var PassportName = ReadInstanceData();

                    List<iKASAModel.PassPortList> result = new List<iKASAModel.PassPortList>();

                    var query = from item in Service.UnitOfWork.EduPassportInsItems
                                join emp in Service.UnitOfWork.V_KmuEmps on item.TTarget equals emp.Empcode
                                where item.InstanceID == instanceid
                                select new { item, emp };

                    //Dictionary<string, int> itemcount = new Dictionary<string, int>();
                    //使用字典，其中每個標題（Title）都有自己的itemcount字典來記錄ItemName出現的次數
                    Dictionary<string, Dictionary<string, int>> itemcount = new Dictionary<string, Dictionary<string, int>>();

                    iKASAModel.PassPortTitle dto = new iKASAModel.PassPortTitle();

                    dto.PassPortName = PassportName.PassPortName;

                    foreach (var it in query)
                    {
                        //if (!itemcount.ContainsKey(it.item.Title))
                        //{
                        //    itemcount.Add(it.item.Title, 0);
                        //}

                        var title = it.item.Title;

                        if (!itemcount.ContainsKey(title))
                        {
                            itemcount.Add(title, new Dictionary<string, int>());
                        }

                        var itemname = it.item.ItemName;
                        
                        if (!itemcount[title].ContainsKey(itemname))
                        {
                            itemcount[title].Add(itemname, 0);
                        }

                        var dtos = result.FirstOrDefault(p => p.Title == it.item.Title);

                        if (dtos == null)
                        {
                            dtos = new iKASAModel.PassPortList
                            {
                                Title = it.item.Title,
                                Items = new List<iKASAModel.FormItem>()
                            };

                            result.Add(dtos);

                            // 現在在這裡執行累加動作
                            //itemcount[it.item.Title]++;
                        }

                        itemcount[title][itemname]++; //執行累加動作
                        int itemNum = itemcount[title][itemname];

                        iKASAModel.FormItem formItem = new iKASAModel.FormItem
                        {
                            //ItemName = it.item.ItemName + "(" + itemcount[it.item.Title].ToString() + ")",
                            ItemName = itemname + "(" + itemNum + ")",
                            ModifyDate = it.item.ModifyDate,
                            AuditTeacher = it.emp.Empname,
                            IitemID = it.item.IItemID
                        };
                        dtos.Items.Add(formItem);
                    }

                    var response = new
                    {
                        TopTitle = dto,
                        Form = result,
                    };
                    string json = JsonConvert.SerializeObject(response, new JsonSerializerSettings
                    {
                        DateFormatString = "yyyy/MM/dd HH:mm"
                    });
                    return Content(json, "application/json");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 學習護照表單內容_Api OK

        public ActionResult PassPortContent(string IitemID)
        {
            try
            {
                using (EduActivityContextService Service = new EduActivityContextService())
                {
                    EduPassportInstanceAssembler iasm = new EduPassportInstanceAssembler();
                    EduPassportInsItemAssembler itasm = new EduPassportInsItemAssembler();
                    EduPassportInsItemDetAssembler itdasm = new EduPassportInsItemDetAssembler();
                    EduPassportInsItem iitem = (from i in Service.UnitOfWork.EduPassportInsItems
                                                where i.IItemID == IitemID
                                                select i).FirstOrDefault();

                    EduPassportInsItemDto result = itasm.Assemble(iitem);

                    EduPassportTemplateItem ti = Service.UnitOfWork.EduPassportTemplateItems.Where(c => c.TemplateID == iitem.EduPassportInstance.TemplateID && c.ItemID == iitem.ItemID).FirstOrDefault();
                    if (ti.Title != "")
                    {
                        result.ItemName = ti.Title + "-" + result.ItemName;
                    }

                    result.EduPassportInstance = iasm.Assemble(iitem.EduPassportInstance);

                    result.EduPassportInsItemDets = itdasm.Assemble(iitem.EduPassportInsItemDets.ToList()).ToList();

                    if (result.TTarget != null)
                    {
                        V_KmuEmpDto emp = Service.ReadV_KmuEmps(c => c.Empcode == result.TTarget).FirstOrDefault();
                        if (emp != null)
                        {
                            result.TeacherName = emp.Empname;
                        }
                    }

                    List<iKASAModel.PassPortForm> res = new List<iKASAModel.PassPortForm>();

                    EduPassportInstanceDto dos = new EduPassportInstanceDto();

                    var tepName = (from insItem in Service.UnitOfWork.EduPassportInsItems
                                   join ins in Service.UnitOfWork.EduPassportInstances
                                   on insItem.InstanceID equals ins.InstanceID
                                   where insItem.IItemID == IitemID
                                   select ins.TemplateName).FirstOrDefault();

                    iKASAModel.PassPortListTitle output = new iKASAModel.PassPortListTitle
                    {
                        PassPort = tepName, //護照名稱
                        ItemName = result.ItemName, //項目
                        SubmitTime = result.SubmitDate, //送審時間
                        TeacherName = result.TeacherName, //審核老師
                        CurrentStatus = result.StatusName, //目前狀態
                        ModifyDate = result.ModifyDate, //審核日期

                    };

                    var query = from item in Service.UnitOfWork.EduPassportInsItemDets where item.IItemID == IitemID select new { item };

                    foreach (var im in query)
                    {
                        if (im.item != null)
                        {
                            iKASAModel.PassPortForm dto = new iKASAModel.PassPortForm();
                            dto.FieldItem = im.item.FieldDesc; //項目名稱
                            dto.FieldValue = im.item.FieldValue; //項目內容
                            res.Add(dto);
                        }
                    }

                    var response = new
                    {
                        Title = output,
                        Form = res,
                    };

                    string json = JsonConvert.SerializeObject(response, new JsonSerializerSettings
                    {
                        DateFormatString = "yyyy/MM/dd HH:mm"
                    });

                    return Content(json, "application/json");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public ActionResult PassPortContent(string IitemID)
        //{
        //    try
        //    {
        //        using (EduActivityContextService Service = new EduActivityContextService())
        //        {
        //            var detData = ReadDetData();

        //            List<iKASAModel.PassPortForm> result = new List<iKASAModel.PassPortForm>();

        //            var query = from item in Service.UnitOfWork.EduPassportInsItemDets where item.IItemID == IitemID select new { item };

        //            iKASAModel.PassPortListTitle dtos = new iKASAModel.PassPortListTitle();

        //            dtos.PassPort =  detData.PassPort; //護照名稱
        //            dtos.ItemName = detData.ItemName; //項目
        //            dtos.SubmitTime = detData.SubmitTime; //送審時間
        //            dtos.TeacherName = detData.TeacherName; //審核老師
        //            dtos.CurrentStatus = detData.CurrentStatus; //目前狀態
        //            dtos.ModifyDate = detData.ModifyDate;//審核日期

        //            foreach (var im in query)
        //            {
        //                if (im.item != null)
        //                {
        //                    iKASAModel.PassPortForm dto = new iKASAModel.PassPortForm();
        //                    dto.FieldItem = im.item.FieldDesc; //項目名稱
        //                    dto.FieldValue = im.item.FieldValue; //項目內容
        //                    result.Add(dto);
        //                }
        //            }

        //            var response = new
        //            {
        //                Title = dtos,
        //                Form = result,
        //            };
        //            string json = JsonConvert.SerializeObject(response, new JsonSerializerSettings
        //            {
        //                DateFormatString = "yyyy/MM/dd HH:mm"
        //            });
        //            return Content(json, "application/json");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        private iKASAModel.PassPortListTitle ReadDetData()
        {
            if (CurrentIItemID == null)
            {
                TempData["ErrorMessage"] = "參數傳遞錯誤";
                return null;
            }

            using (EduActivityAppService Service = new EduActivityAppService())
            {
                EduPassportInsItemDto iitem = Service.GetEduPassportInsItemByID(CurrentIItemID);

                CurrentInstanceItem = iitem;

                if (CurrentInstanceItem.SubmitDate.HasValue)
                {
                    var SubmitTime = CurrentInstanceItem.SubmitDate.Value.ToString("yyyy/MM/dd HH:mm");
                }
                else
                {
                    var SubmitTime = "";
                }

                var PassportName = CurrentInstance.TemplateName; //護照名稱
                var ItemName = CurrentInstanceItem.ItemName; //項目
                var TeacherName = CurrentInstanceItem.TeacherName; //審核老師
                var CurrentStatus = CurrentInstanceItem.StatusName; //目前狀態


                if (CurrentInstanceItem.ModifyDate.HasValue)
                {
                    var ModifyDate = CurrentInstanceItem.ModifyDate.Value.ToString("yyyy/MM/dd HH:mm");
                }
                else
                {
                    var ModifyDate = "";
                }
                iKASAModel.PassPortListTitle detData = new iKASAModel.PassPortListTitle
                {
                    PassPort = CurrentInstance.TemplateName,
                    ItemName = CurrentInstanceItem.ItemName,
                    TeacherName = CurrentInstanceItem.TeacherName,
                    CurrentStatus = CurrentInstanceItem.StatusName,
                    ModifyDate = CurrentInstanceItem.ModifyDate,
                    SubmitTime = CurrentInstanceItem.SubmitDate
                };
                EduPassportItemDto item = Service.GetEduPassportItemWithDet(iitem.ItemID);

                if (item != null)
                {
                    var Desc = item.ItemDesc;
                }
                return detData;
            }
        }

        private List<EduPassportInsItemDto> CurrentInstanceItems
        {
            get
            {
                return CurrentInstance.EduPassportInsItems.ToList();
            }
        }

        private EduPassportInstanceDto CurrentInstances
        {
            get
            {
                return Session["CurrentInstances"] as EduPassportInstanceDto;
            }
            set
            {
                Session["CurrentInstances"] = value;
            }
        }

        private List<EduPassportInsItemDetDto> CurrentTInsItemDets
        {
            get
            {
                return CurrentInstanceItem.EduPassportInsItemDets.Where(c => c.FieldTarget == "T").ToList();
            }
        }

        private List<EduPassportInsItemDetDto> CurrentSInsItemDets
        {
            get
            {
                return CurrentInstanceItem.EduPassportInsItemDets.Where(c => c.FieldTarget == "S").ToList();
            }
        }

        private string CurrentIItemID
        {
            get
            {
                return Session["CurrentIItemID"] as string;
            }
            set
            {
                Session["CurrentIItemID"] = value;
            }
        }

        private EduPassportInsItemDto CurrentInstanceItem
        {
            get
            {
                return Session["CurrentInstanceItem"] as EduPassportInsItemDto;
            }
            set
            {
                Session["CurrentInstanceItem"] = value;
            }
        }

        #endregion

        #region 實習各階段評量_Api OK

        public ActionResult InternEduScore(string empcodename, string creater, DateTime? createdates, DateTime? createdatee)
        {
            using (EduActivityContextService Service = new EduActivityContextService())
            {
                List<string> empcodenames = empcodename.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).ToList();

                try
                {
                    var datas = (from item in Service.UnitOfWork.EduScores
                                 join emp in Service.UnitOfWork.V_KmuEmps on item.Empcode equals emp.Empcode
                                 join cemp in Service.UnitOfWork.V_KmuEmps on item.Uploader equals cemp.Empcode
                                 where (empcodenames.Count() == 0 || empcodenames.Contains(emp.Empcode) || empcodenames.Contains
                                 (emp.Empname)) && (creater == null || creater == "" || cemp.Empcode == creater || cemp.Empname == creater)
                                 && (createdates == null || (createdates != null && item.Uploadtime >= createdates)) && (createdatee == null || (createdatee != null &&
                                 item.Uploadtime <= createdatee)) && item.Status != "X"
                                 orderby item.Id
                                 select new iKASAModel.InternEduScore
                                 {
                                     DeptLevl = item.Deplevel,
                                     InternClassName = item.Classname,
                                     TotalScore = item.Score
                                 }).ToList();

                    string json = JsonConvert.SerializeObject(datas, new JsonSerializerSettings
                    {
                        DateFormatString = "yyyy/MM/dd HH:mm"
                    });
                    return Content(json, "application/json");
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        private int ExtractTotalScoreFromHtml(string htmlContent) //取得html表單分數
        {
            int totalScore = 0;
            string searchString = "id=\"tbScore\""; //搜尋分數欄位
            int index = htmlContent.IndexOf(searchString);

            if (index != -1)
            {
                int startIndex = htmlContent.IndexOf("value=\"", index) + "value=\"".Length;
                int endIndex = htmlContent.IndexOf("\"", startIndex);
                string valueStr = htmlContent.Substring(startIndex, endIndex - startIndex);
                int.TryParse(valueStr, out totalScore);
            }
            return totalScore;
        }

        private string ExtractDeptNameFromHtml(string htmlContent) //取得html表單科別名稱
        {
            string deptName = "";
            string searchString = "id=\"text_2\""; //搜尋DeptName欄位
            int index = htmlContent.IndexOf(searchString);

            if (index != -1)
            {
                int startIndex = htmlContent.IndexOf("value=\"", index) + "value=\"".Length;
                int endIndex = htmlContent.IndexOf("\"", startIndex);
                deptName = htmlContent.Substring(startIndex, endIndex - startIndex);
            }
            return deptName;
        }
        #endregion

        #region 臨床照護授權分級(EPAs) & 長條圖 OK

        public ActionResult EPAs(string EmpCode) //async 關鍵字和 Task<T>傳回類型的目的是支援非同步操作
        {
            try
            {
                string url = "https://www.kmuh.org.tw/Web/EduActivity/PassPortForNurseEmpty.aspx?empcode=" + EmpCode;

                return Content(url, "text/plain");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult EPAsBarChart(string EmpCode)
        {
            try
            {
                // 取得 jobcode 和 topteamcode 參數
                List<V_KmuEmpDto> serachEmp = SerachEmp(EmpCode);
                string jobcode = serachEmp.FirstOrDefault().Jobcode;
                string topteamcode = GetCurrentEduTeamCode(EmpCode);

                // 驗證 jobcode 和 topteamcode 是否為有效值
                if (string.IsNullOrEmpty(jobcode) || string.IsNullOrEmpty(topteamcode))
                {
                    return Json(new { error = "無效參數" }, JsonRequestBehavior.AllowGet);
                }

                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["www_EduActivityConnectionString"].ConnectionString))
                {
                    var query = @"SELECT  PassPortCheckItem.itemCode ,PassPortCheckItem.itemName,
                                case when PassPortJobTitleItem.authenticateFrequency = 1 and appItem1.checkResult = 1 then PassPortJobTitleItem.authorizeLevel
                                when PassPortJobTitleItem.authenticateFrequency = 2 and appItem2.checkResult = 1 then PassPortJobTitleItem.authorizeLevel
                                when PassPortJobTitleItem.authenticateFrequency = 3 and appItem3.checkResult = 1 then PassPortJobTitleItem.authorizeLevel
                                when isnull(speciallist.DefaultLevel,PassPortJobTitleItem.exauthorizeLevel) = 'S0' 
                                and (appItem1.checkResult = 0 or appItem1.checkResult is null) then '--'
                                when isnull(speciallist.DefaultLevel,PassPortJobTitleItem.exauthorizeLevel) = 'S0' and appItem2.checkResult = 1 then 'S2'
                                when isnull(speciallist.DefaultLevel,PassPortJobTitleItem.exauthorizeLevel) = 'S0' and appItem1.checkResult = 1 then 'S1'
                                else isnull(speciallist.DefaultLevel,PassPortJobTitleItem.exauthorizeLevel) end
                                as authorizeLevel,PassPortJobTitleItem.authorizeLevel as TopauthorizeLevel,
                                case when PassPortJobTitleItem.authenticateFrequency = 1 and appItem1.checkResult = 1 then '通過'
                                when PassPortJobTitleItem.authenticateFrequency = 2 and appItem2.checkResult = 1 then '通過'
                                when PassPortJobTitleItem.authenticateFrequency = 3 and appItem3.checkResult = 1 then '通過'
                                when PassPortJobTitleItem.authorizeLevel = 'I' and isnull(speciallist.DefaultLevel,PassPortJobTitleItem.exauthorizeLevel) = 'I' then '通過'
                                when appitem1.itemcode is null then '未評核' else '評核中' end as PassStatus,
                                case when PassPortJobTitleItem.authenticateFrequency in (1) and not (PassPortJobTitleItem.authorizeLevel = 'I' and isnull
                                (speciallist.DefaultLevel,PassPortJobTitleItem.exauthorizeLevel) = 'I') then 'slash' end as PassDiv2,
                                case when PassPortJobTitleItem.authenticateFrequency in (1,2) and not (PassPortJobTitleItem.authorizeLevel = 'I' and isnull
                                (speciallist.DefaultLevel,PassPortJobTitleItem.exauthorizeLevel) = 'I') then 'slash' end as PassDiv3,
                                appItem1.checkDate,
                                case when appItem1.checkStatus is null and PassPortJobTitleItem.authorizeLevel = 'I' and isnull
                                (speciallist.DefaultLevel,PassPortJobTitleItem.exauthorizeLevel) = 'I' then '已達最高等級'
                                else appItem1.checkStatus end as checkStatus ,isnull(emp1.empname,case when appItem1.checkStatus ='2' then '臨教部審核通過' else ''
                                end) as empname,appItem1.checkResult,appItem1.teacherremark,isnull(speciallist.DefaultLevel,PassPortJobTitleItem.exauthorizeLevel)+' 第'+cast
                                (appItem1.checkOrder as varchar)+'次' as checkOrder1 , 
                                appItem2.checkDate,
                                case when PassPortJobTitleItem.authenticateFrequency in (1) then 'N/A'
                                when appItem2.checkStatus is null and PassPortJobTitleItem.authorizeLevel = 'I' and isnull
                                (speciallist.DefaultLevel,PassPortJobTitleItem.exauthorizeLevel) = 'I' then '已達最高等級'
                                else appItem2.checkStatus end as checkStatus ,isnull(emp2.empname,
                                case when appItem2.checkStatus ='2' then '臨教部審核通過'
                                else '' end) as empname,appItem2.checkResult,appItem2.teacherremark,isnull(speciallist.DefaultLevel,PassPortJobTitleItem.exauthorizeLevel)+' 
                                第'+cast(appItem2.checkOrder as varchar)+'次' as checkOrder2,
                                appItem3.checkDate,
                                case when PassPortJobTitleItem.authenticateFrequency in (1,2) then 'N/A'
                                when appItem3.checkStatus is null and PassPortJobTitleItem.authorizeLevel = 'I' and isnull
                                (speciallist.DefaultLevel,PassPortJobTitleItem.exauthorizeLevel) = 'I' then '已達最高等級'
                                else appItem3.checkStatus end as checkStatus ,isnull(emp3.empname,case when appItem3.checkStatus ='2' then '臨教部審核通過'
                                else '' end) as empname,appItem3.checkResult,appItem3.teacherremark,isnull(speciallist.DefaultLevel,PassPortJobTitleItem.exauthorizeLevel)+' 
                                第'+cast(appItem3.checkOrder as varchar)+'次' as checkOrder3,row_number() over(order by PassPortCheckItem.itemCode) as rownum, 
                                case when PassPortJobTitleItem.authorizeLevel = isnull(speciallist.DefaultLevel,PassPortJobTitleItem.exauthorizeLevel) and 
                                PassPortJobTitleItem.authorizeLevel='I' then 0 when PassPortJobTitleItem.authenticateFrequency = 1 and appItem1.applicationID is null then 1 
                                else 0 end as allowsend1,
                                case when PassPortJobTitleItem.authorizeLevel = isnull(speciallist.DefaultLevel,PassPortJobTitleItem.exauthorizeLevel) and 
                                PassPortJobTitleItem.authorizeLevel='I' then 0
                                when PassPortJobTitleItem.authenticateFrequency =2 and appItem1.checkstatus ='2' then 1 
                                else 0 end as allowsend2,
                                case when PassPortJobTitleItem.authorizeLevel = isnull(speciallist.DefaultLevel,PassPortJobTitleItem.exauthorizeLevel) and 
                                PassPortJobTitleItem.authorizeLevel='I' then 0 when PassPortJobTitleItem.authenticateFrequency =3 and appItem2.checkstatus ='2' then 1 
                                else 0 end as allowsend3,
                                case when PassPortJobTitleItem.authorizeLevel = isnull(speciallist.DefaultLevel,PassPortJobTitleItem.exauthorizeLevel) and 
                                PassPortJobTitleItem.authorizeLevel in ('I') then 0 else 1 end as isdisplay,appItem1.applicationid as applicationid1,appItem2.applicationid 
                                as applicationid2,appItem3.applicationid as applicationid3 FROM [PassPortCheckItem] 
                                INNER JOIN [PassPortJobTitleItem] ON PassPortCheckItem.itemCode=PassPortJobTitleItem.itemCode
                                left JOIN (select *,ROW_NUMBER() OVER (PARTITION BY itemcode, ApplicationMemberNumber ORDER BY checkresult desc, checkDate ) 
                                AS realCheckOrder from (select *,ROW_NUMBER() OVER (PARTITION BY itemcode, ApplicationMemberNumber, checkOrder ORDER BY checkresult desc, applicationid) AS rn from PassPortStudentApplicationItem) aa where rn = 1) as appItem1 ON PassPortCheckItem.itemCode=appItem1.itemCode and appItem1.realcheckorder = 1 and appItem1.ApplicationMemberNumber = @empcode and appItem1.rn = 1 left join V_KmuEmp as emp1
                                on appItem1.designationTeacherNumber = emp1.empcode left JOIN (select *,ROW_NUMBER() OVER (PARTITION BY itemcode, ApplicationMemberNumber ORDER BY checkresult desc, checkDate ) AS realCheckOrder from (select *,ROW_NUMBER() OVER (PARTITION BY itemcode, ApplicationMemberNumber, checkOrder ORDER BY checkresult desc, applicationid) AS rn from PassPortStudentApplicationItem) aa where rn = 1) as appItem2 
                                ON PassPortCheckItem.itemCode=appItem2.itemCode and appItem2.realcheckorder = 2 and appItem2.ApplicationMemberNumber = @empcode 
                                and appItem1.rn = 1 left join V_KmuEmp as emp2 on appItem2.designationTeacherNumber = emp2.empcode
                                left JOIN  (select *,ROW_NUMBER() OVER (PARTITION BY itemcode, ApplicationMemberNumber ORDER BY checkresult desc, checkDate ) 
                                AS realCheckOrder from (select *,ROW_NUMBER() OVER (PARTITION BY itemcode, ApplicationMemberNumber, checkOrder ORDER BY checkresult desc, applicationid) AS rn from PassPortStudentApplicationItem) aa where rn = 1) as appItem3 
                                ON PassPortCheckItem.itemCode=appItem3.itemCode and appItem3.realcheckorder = 3 and appItem3.ApplicationMemberNumber = @empcode and appItem1.rn = 1 left join V_KmuEmp as emp3 on appItem3.designationTeacherNumber = emp3.empcode
                                left join PassPortSpecialList as speciallist on speciallist.itemcode = PassPortCheckItem.itemcode and speciallist.empcode = @empcode
                                where [PassPortJobTitleItem].jobtitlecode =@jobcode and PassPortCheckItem.topTeamCode = dbo.fn_GetTopTeamCode(@topteamcode)  and (PassPortCheckItem.ItemGroup = 1 OR PassPortCheckItem.ItemGroup = 2) order by PassPortCheckItem.itemCode";

                    List<iKASAModel.EPAsBarChart> result;

                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("empcode", EmpCode); //職編
                        command.Parameters.AddWithValue("jobcode", jobcode); //職別
                        command.Parameters.AddWithValue("topteamcode", topteamcode); //實習科別種類

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            result = new List<iKASAModel.EPAsBarChart>();

                            //    int passStatusCount = 0; // 計算不屬於 "通過" 的筆數

                            //    while (reader.Read())
                            //    {
                            //        string passStatus = reader["PassStatus"].ToString();

                            //        if (passStatus != "通過")
                            //        {
                            //            passStatusCount++;
                            //        }

                            //        iKASAModel.EPAsBarChart dto = new iKASAModel.EPAsBarChart
                            //        {
                            //            Finished = "", // 尚未計算
                            //            TotalFinished = "21",
                            //        };

                            //        result.Add(dto);
                            //    }

                            //    int finished = 21 - passStatusCount; // 計算 Finished 的值
                            //    double rate = Calculate(finished, 21); // 計算百分比誤差

                            //    if (result.Count > 0)
                            //    {
                            //        iKASAModel.EPAsBarChart dto = result[0];
                            //        dto.Finished = finished.ToString(); // 將計算結果設定給 Finished 屬性
                            //        dto.Rate = rate.ToString() + "%"; // 將計算結果設定給 Rate 屬性

                            //        // 移除多餘的結果
                            //        result.RemoveRange(1, result.Count - 1);
                            //    }
                            if (reader.Read())
                            {
                                int passStatusCount = 0; // 計算不屬於 "通過" 的筆數

                                do
                                {
                                    string passStatus = reader["PassStatus"].ToString();

                                    if (passStatus != "通過")
                                    {
                                        passStatusCount++;
                                    }
                                } while (reader.Read());

                                int finished = 21 - passStatusCount; // 計算 Finished 的值
                                double rate = Calculate(finished, 21); // 計算百分比誤差

                                iKASAModel.EPAsBarChart dto = new iKASAModel.EPAsBarChart
                                {
                                    Finished = finished.ToString(), // 將計算結果設定給 Finished 屬性
                                    TotalFinished = "21",
                                    Rate = rate.ToString() + "%" // 將計算結果設定給 Rate 屬性
                                };

                                result.Add(dto);
                            }
                        }

                    }
                    return Json(result, "application/json", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //計算百分比誤差
        private double Calculate(int finished, int totalFinished)
        {
            double rate = ((double)finished / totalFinished) * 100;

            return Math.Round(rate, MidpointRounding.AwayFromZero); //四捨五入
        }

        public List<V_KmuEmpDto> SerachEmp(string keyword) //查詢jobcode參數
        {
            using (EduActivityContextService service = new EduActivityContextService())
            {
                List<V_KmuEmpDto> list = service.ReadV_KmuEmps(c =>
                    c.Empcode == keyword
                   ||
                    c.Empname == keyword
                    ).ToList();

                return list;
            }
        }

        public string GetCurrentEduTeamCode(string empcode) //查詢topteamcode參數
        {
            using (EduActivityContextService service = new EduActivityContextService())
            {
                var datas = from mem in service.UnitOfWork.Members
                            join teammem in service.UnitOfWork.EduTeamMembers
                            on mem.MemberID equals teammem.MemberID
                            where mem.IsHospMember == empcode
                            orderby mem.DateTo descending
                            select new { mem, teammem };
                var data = datas.FirstOrDefault();

                if (data == null)
                {
                    return null;
                }
                string result = data.teammem.EduTeamCode;

                return result;
            }
        }

        #endregion

        #endregion

        #region 第三週 Api項目

        #region Mini-CEX(迷你評量表)_Api OK

        public ActionResult MiniCEX(string memberid, string groupid)
        {
            try
            {
                // 檢查參數名稱是否正確
                if (groupid == null)
                {
                    throw new ArgumentException("參數錯誤: 請使用正確的參數名稱 'groupid'。");
                }
                using (EduActivityContextService Service = new EduActivityContextService())
                {
                    int catid = Convert.ToInt32(groupid); //iKASA_MenuParam代碼

                    var members = from mem in Service.UnitOfWork.Members
                                  join rundown in Service.UnitOfWork.EduTeamMemberRundowns on mem.MemberID equals rundown.MemberID
                                  join term in Service.UnitOfWork.EduTerms on rundown.EduTermID equals term.EduTermID
                                  where mem.MemberID == memberid
                                  orderby term.DateFrom
                                  select new { mem, rundown, term };

                    List<iKASAModel.MiniCEX> result = new List<iKASAModel.MiniCEX>(); //自訂Model名稱類型
                    List<int> existsforms = new List<int>();

                    foreach (var mem in members)
                    {
                        List<iKASAModel.MiniCEX> otherforms = (from ins in Service.UnitOfWork.FORM_INSTANCEs
                                                               where ins.INHOSPID == mem.term.EduTermID && (ins.TargetID == mem.mem.IsHospMember || ins.EvalTargetID ==
                                                               mem.mem.MemberID) && Service.UnitOfWork.FormCategoryRefs.Count(c => c.CategoryID == catid && c.TEMPLATE_ID ==
                                                               ins.TEMPLATE_ID) > 0 && !existsforms.Contains(ins.INSTANCE_ID) && ins.Status != '0' && ins.PARENT_INSTANCE_ID ==
                                                               null
                                                               select new iKASAModel.MiniCEX
                                                               {
                                                                   Instance_ID = ins.INSTANCE_ID,
                                                                   INHOSPID = ins.INHOSPID,
                                                                   Lesson = ins.INHOSPID,
                                                                   List = ins.INSTANCE_NAME,
                                                                   CreateTime = ins.INSTANCE_CREATE_DATETIME
                                                               }).ToList();

                        existsforms.AddRange(otherforms.Select(c => c.Instance_ID));

                        List<int> pidlist = (from ins in Service.UnitOfWork.FORM_INSTANCEs
                                             where ins.INHOSPID == mem.term.EduTermID
                                             && (ins.TargetID == mem.mem.IsHospMember || ins.EvalTargetID == mem.mem.MemberID) && !existsforms.Contains(ins.INSTANCE_ID)
                                             && ins.Status != '0' && ins.PARENT_INSTANCE_ID != null
                                             && Service.UnitOfWork.FORM_INSTANCEs.Count(c => c.PARENT_INSTANCE_ID == ins.PARENT_INSTANCE_ID && c.Status == '0') == 0
                                             && Service.UnitOfWork.FormCategoryRefs.Count(c => c.CategoryID == catid && c.TEMPLATE_ID ==
                                             ins.FORM_TEMPLATE.PARENT_TEMPLATE_ID) > 0
                                             select ins.PARENT_INSTANCE_ID.Value).Distinct().ToList();

                        foreach (int pid in pidlist)
                        {
                            iKASAModel.MiniCEX pdto = (from ins in Service.UnitOfWork.FORM_INSTANCEs
                                                       join pins in Service.UnitOfWork.FORM_INSTANCEs on ins.PARENT_INSTANCE_ID
                                                       equals pins.INSTANCE_ID
                                                       where ins.PARENT_INSTANCE_ID == pid && !existsforms.Contains(ins.INSTANCE_ID)
                                                       orderby ins.INSTANCE_ID descending
                                                       select new iKASAModel.MiniCEX
                                                      {
                                                          Instance_ID = ins.INSTANCE_ID,
                                                          INHOSPID = pins.INHOSPID,
                                                          Lesson = pins.INHOSPID,
                                                          List = pins.INSTANCE_NAME,
                                                          CreateTime = ins.INSTANCE_CREATE_DATETIME
                                                      }).FirstOrDefault();
                            otherforms.Add(pdto);

                            existsforms.Add(pdto.Instance_ID);
                        }
                        result.AddRange(otherforms);
                    }

                    foreach (var f in result)
                    {
                        try
                        {
                            if (f.Lesson != null)
                            {
                                f.Lesson = GetEduTermFullName(f.Lesson);
                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                    var dtos = result.Select(f => new
                    {
                        f.Instance_ID,
                        f.INHOSPID,
                        f.Lesson,
                        f.List,
                        f.CreateTime
                    }).ToList();

                    string json = JsonConvert.SerializeObject(dtos, new JsonSerializerSettings //時間格式化
                    {
                        DateFormatString = "yyyy/MM/dd HH:mm"
                    });

                    return Content(json, "application/json");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region DOPS_Api OK

        public ActionResult DOPS(string memberid, string groupid)
        {
            try
            {
                // 檢查參數名稱是否正確
                if (groupid == null)
                {
                    throw new ArgumentException("參數錯誤: 請使用正確的參數名稱 'groupid'。");
                }

                using (EduActivityContextService Service = new EduActivityContextService())
                {
                    int catid = Convert.ToInt32(groupid); //iKASA_MenuParam代碼

                    var members = from mem in Service.UnitOfWork.Members
                                  join rundown in Service.UnitOfWork.EduTeamMemberRundowns on mem.MemberID equals rundown.MemberID
                                  join term in Service.UnitOfWork.EduTerms on rundown.EduTermID equals term.EduTermID
                                  where mem.MemberID == memberid
                                  orderby term.DateFrom
                                  select new { mem, rundown, term };

                    List<iKASAModel.DOPS> result = new List<iKASAModel.DOPS>(); //自訂Model名稱類型
                    List<int> existsforms = new List<int>();

                    foreach (var mem in members)
                    {
                        List<iKASAModel.DOPS> otherforms = (from ins in Service.UnitOfWork.FORM_INSTANCEs
                                                            where ins.INHOSPID == mem.term.EduTermID && (ins.TargetID == mem.mem.IsHospMember || ins.EvalTargetID ==
                                                            mem.mem.MemberID) && Service.UnitOfWork.FormCategoryRefs.Count(c => c.CategoryID == catid && c.TEMPLATE_ID ==
                                                            ins.TEMPLATE_ID) > 0 && !existsforms.Contains(ins.INSTANCE_ID) && ins.Status != '0' && ins.PARENT_INSTANCE_ID == null
                                                            select new iKASAModel.DOPS
                                                            {
                                                                Instance_ID = ins.INSTANCE_ID,
                                                                Lesson = ins.INHOSPID,
                                                                List = ins.INSTANCE_NAME,
                                                                CreateTime = ins.INSTANCE_CREATE_DATETIME
                                                            }).ToList();

                        existsforms.AddRange(otherforms.Select(c => c.Instance_ID));

                        List<int> pidlist = (from ins in Service.UnitOfWork.FORM_INSTANCEs
                                             where ins.INHOSPID == mem.term.EduTermID
                                             && (ins.TargetID == mem.mem.IsHospMember || ins.EvalTargetID == mem.mem.MemberID) && !existsforms.Contains(ins.INSTANCE_ID)
                                             && ins.Status != '0' && ins.PARENT_INSTANCE_ID != null
                                             && Service.UnitOfWork.FORM_INSTANCEs.Count(c => c.PARENT_INSTANCE_ID == ins.PARENT_INSTANCE_ID && c.Status == '0') == 0
                                             && Service.UnitOfWork.FormCategoryRefs.Count(c => c.CategoryID == catid && c.TEMPLATE_ID ==
                                             ins.FORM_TEMPLATE.PARENT_TEMPLATE_ID) > 0
                                             select ins.PARENT_INSTANCE_ID.Value).Distinct().ToList();

                        foreach (int pid in pidlist)
                        {
                            iKASAModel.DOPS pdto = (from ins in Service.UnitOfWork.FORM_INSTANCEs
                                                    join pins in Service.UnitOfWork.FORM_INSTANCEs on ins.PARENT_INSTANCE_ID
                                                    equals pins.INSTANCE_ID
                                                    where ins.PARENT_INSTANCE_ID == pid && !existsforms.Contains(ins.INSTANCE_ID)
                                                    orderby ins.INSTANCE_ID descending
                                                    select new iKASAModel.DOPS
                                                     {
                                                         Instance_ID = ins.INSTANCE_ID,
                                                         Lesson = pins.INHOSPID,
                                                         List = pins.INSTANCE_NAME,
                                                         CreateTime = ins.INSTANCE_CREATE_DATETIME
                                                     }).FirstOrDefault();
                            otherforms.Add(pdto);

                            existsforms.Add(pdto.Instance_ID);
                        }

                        result.AddRange(otherforms);
                    }

                    foreach (var f in result)
                    {
                        try
                        {
                            if (f.Lesson != null)
                            {
                                f.Lesson = GetEduTermFullName(f.Lesson);
                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                    var filteredResult = result.Select(f => new
                    {
                        f.Instance_ID,
                        f.Lesson,
                        f.List,
                        f.CreateTime
                    }).ToList();

                    string json = JsonConvert.SerializeObject(filteredResult, new JsonSerializerSettings //時間格式化
                    {
                        DateFormatString = "yyyy/MM/dd HH:mm"
                    });

                    return Content(json, "application/json");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region CbD(案例病歷討論評量表)_Api OK

        public ActionResult CBD(string memberid, string groupid)
        {
            try
            {
                // 檢查參數名稱是否正確
                if (groupid == null)
                {
                    throw new ArgumentException("參數錯誤: 請使用正確的參數名稱 'groupid'。");
                }

                using (EduActivityContextService Service = new EduActivityContextService())
                {
                    int catid = Convert.ToInt32(groupid);

                    var members = from mem in Service.UnitOfWork.Members
                                  join rundown in Service.UnitOfWork.EduTeamMemberRundowns on mem.MemberID equals rundown.MemberID
                                  join term in Service.UnitOfWork.EduTerms on rundown.EduTermID equals term.EduTermID
                                  where mem.MemberID == memberid
                                  orderby term.DateFrom
                                  select new { mem, rundown, term };

                    List<iKASAModel.CBD> result = new List<iKASAModel.CBD>();
                    List<int> existsforms = new List<int>();

                    foreach (var mem in members)
                    {
                        List<iKASAModel.CBD> otherforms = (from ins in Service.UnitOfWork.FORM_INSTANCEs
                                                           where ins.INHOSPID == mem.term.EduTermID && (ins.TargetID == mem.mem.IsHospMember || ins.EvalTargetID ==
                                                           mem.mem.MemberID) && Service.UnitOfWork.FormCategoryRefs.Count(c => c.CategoryID == catid && c.TEMPLATE_ID ==
                                                           ins.TEMPLATE_ID) > 0 && !existsforms.Contains(ins.INSTANCE_ID) && ins.Status != '0' && ins.PARENT_INSTANCE_ID == null
                                                           select new iKASAModel.CBD
                                                           {
                                                               Instance_ID = ins.INSTANCE_ID,
                                                               Lesson = ins.INHOSPID,
                                                               List = ins.INSTANCE_NAME,
                                                               CreateTime = ins.INSTANCE_CREATE_DATETIME
                                                           }).ToList();

                        existsforms.AddRange(otherforms.Select(c => c.Instance_ID));

                        List<int> pidlist = (from ins in Service.UnitOfWork.FORM_INSTANCEs
                                             where ins.INHOSPID == mem.term.EduTermID
                                             && (ins.TargetID == mem.mem.IsHospMember || ins.EvalTargetID == mem.mem.MemberID) && !existsforms.Contains(ins.INSTANCE_ID)
                                             && ins.Status != '0' && ins.PARENT_INSTANCE_ID != null
                                             && Service.UnitOfWork.FORM_INSTANCEs.Count(c => c.PARENT_INSTANCE_ID == ins.PARENT_INSTANCE_ID && c.Status == '0') == 0
                                             && Service.UnitOfWork.FormCategoryRefs.Count(c => c.CategoryID == catid && c.TEMPLATE_ID == ins.FORM_TEMPLATE.PARENT_TEMPLATE_ID) > 0
                                             select ins.PARENT_INSTANCE_ID.Value).Distinct().ToList();

                        foreach (int pid in pidlist)
                        {
                            iKASAModel.CBD pdto = (from ins in Service.UnitOfWork.FORM_INSTANCEs
                                                   join pins in Service.UnitOfWork.FORM_INSTANCEs on ins.PARENT_INSTANCE_ID equals pins.INSTANCE_ID
                                                   where ins.PARENT_INSTANCE_ID == pid && !existsforms.Contains(ins.INSTANCE_ID)
                                                   orderby ins.INSTANCE_ID descending
                                                   select new iKASAModel.CBD
                                                   {
                                                       Instance_ID = ins.INSTANCE_ID,
                                                       Lesson = pins.INHOSPID,
                                                       List = pins.INSTANCE_NAME,
                                                       CreateTime = ins.INSTANCE_CREATE_DATETIME
                                                   }).FirstOrDefault();
                            otherforms.Add(pdto);

                            existsforms.Add(pdto.Instance_ID);
                        }

                        result.AddRange(otherforms);
                    }

                    foreach (var f in result)
                    {
                        try
                        {
                            if (f.Lesson != null)
                            {
                                f.Lesson = GetEduTermFullName(f.Lesson);
                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                    var filteredResult = result.Select(f => new
                    {
                        f.Instance_ID,
                        f.Lesson,
                        f.List,
                        f.CreateTime
                    }).ToList();

                    string json = JsonConvert.SerializeObject(filteredResult, new JsonSerializerSettings
                    {
                        DateFormatString = "yyyy/MM/dd HH:mm:ss"
                    });

                    return Content(json, "application/json");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 滿意度_Api 不做了

        public JsonResult Satisfaction(string memberid, string groupid)
        {
            using (EduActivityContextService Service = new EduActivityContextService())
            {
                int catid = Convert.ToInt32(groupid);

                var members = from mem in Service.UnitOfWork.Members
                              join rundown in Service.UnitOfWork.EduTeamMemberRundowns on mem.MemberID equals rundown.MemberID
                              join term in Service.UnitOfWork.EduTerms on rundown.EduTermID equals term.EduTermID
                              where mem.MemberID == memberid
                              orderby term.DateFrom
                              select new { mem, rundown, term };

                List<iKASAModel.Satisfaction> result = new List<iKASAModel.Satisfaction>();
                List<int> existsforms = new List<int>();

                foreach (var mem in members)
                {
                    List<iKASAModel.Satisfaction> otherforms = (from ins in Service.UnitOfWork.FORM_INSTANCEs
                                                                where ins.INHOSPID == mem.term.EduTermID && (ins.TargetID == mem.mem.IsHospMember || ins.EvalTargetID ==
                                                                mem.mem.MemberID) && Service.UnitOfWork.FormCategoryRefs.Count(c => c.CategoryID == catid && c.TEMPLATE_ID ==
                                                                ins.TEMPLATE_ID) > 0 && !existsforms.Contains(ins.INSTANCE_ID) && ins.Status != '0' && ins.PARENT_INSTANCE_ID ==
                                                                null
                                                                select new iKASAModel.Satisfaction
                                                                {
                                                                    Instance_ID = ins.INSTANCE_ID,
                                                                    Lesson = ins.INHOSPID,
                                                                    List = ins.INSTANCE_NAME,
                                                                    CreateTime = ins.INSTANCE_CREATE_DATETIME
                                                                }).ToList();

                    existsforms.AddRange(otherforms.Select(c => c.Instance_ID));

                    List<int> pidlist = (from ins in Service.UnitOfWork.FORM_INSTANCEs
                                         where ins.INHOSPID == mem.term.EduTermID
                                         && (ins.TargetID == mem.mem.IsHospMember || ins.EvalTargetID == mem.mem.MemberID) && !existsforms.Contains(ins.INSTANCE_ID)
                                         && ins.Status != '0' && ins.PARENT_INSTANCE_ID != null
                                         && Service.UnitOfWork.FORM_INSTANCEs.Count(c => c.PARENT_INSTANCE_ID == ins.PARENT_INSTANCE_ID && c.Status == '0') == 0
                                         && Service.UnitOfWork.FormCategoryRefs.Count(c => c.CategoryID == catid && c.TEMPLATE_ID == ins.FORM_TEMPLATE.PARENT_TEMPLATE_ID) > 0
                                         select ins.PARENT_INSTANCE_ID.Value).Distinct().ToList();

                    foreach (int pid in pidlist)
                    {
                        iKASAModel.Satisfaction pdto = (from ins in Service.UnitOfWork.FORM_INSTANCEs
                                                        join pins in Service.UnitOfWork.FORM_INSTANCEs on ins.PARENT_INSTANCE_ID
                                                        equals pins.INSTANCE_ID
                                                        where ins.PARENT_INSTANCE_ID == pid && !existsforms.Contains(ins.INSTANCE_ID)
                                                        orderby ins.INSTANCE_ID descending
                                                        select new iKASAModel.Satisfaction
                                                        {
                                                            Instance_ID = ins.INSTANCE_ID,
                                                            Lesson = pins.INHOSPID,
                                                            List = pins.INSTANCE_NAME,
                                                            CreateTime = ins.INSTANCE_CREATE_DATETIME
                                                        }).FirstOrDefault();
                        otherforms.Add(pdto);

                        existsforms.Add(pdto.Instance_ID);
                    }

                    result.AddRange(otherforms);
                }

                foreach (var f in result)
                {
                    try
                    {
                        if (f.Lesson != null)
                        {
                            f.Lesson = GetEduTermFullName(f.Lesson);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                var filteredResult = result.Select(f => new
                {
                    f.Instance_ID,
                    f.Lesson,
                    f.List,
                    f.CreateTime
                }).ToList();

                string json = JsonConvert.SerializeObject(filteredResult, new JsonSerializerSettings
                {
                    DateFormatString = "yyyy/MM/dd HH:mm"
                });

                return Json(json, "application/json", JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region OSCE成績 OK

        public ActionResult OSCE_Score(string empcode) //主程式
        {
            try
            {
                List<IKASA_OSCEExamDto> result = GetEmpiKasaOSCEExamData(empcode.ToUpper());

                string json = JsonConvert.SerializeObject(result, new JsonSerializerSettings
                {
                    DateFormatString = "yyyy/MM/dd HH:mm"
                });

                return Content(json, "application/json");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<IKASA_OSCEExamDto> GetEmpiKasaOSCEExamData(string empcode)
        {
            using (EduActivityContextService service = new EduActivityContextService())
            {
                List<IKASA_OSCEExamDto> result = new List<IKASA_OSCEExamDto>();

                V_KmuEmpDto emp = service.ReadV_KmuEmps(c => c.Empcode == empcode).FirstOrDefault();
                if (emp == null)
                {
                    return result;
                }
                IKASA_OSCEExamAssembler asm = new IKASA_OSCEExamAssembler();
                IKASA_OSCEExamScoreAssembler scasm = new IKASA_OSCEExamScoreAssembler();
                IKASA_OSCEExamStageAssembler stasm = new IKASA_OSCEExamStageAssembler();

                var datas = (from exam in service.UnitOfWork.IKASA_OSCEExams
                             join hosp in service.UnitOfWork.V_hosps on exam.HospCode equals hosp.Unitcode
                             where exam.IKASA_OSCEExamScores.Count(c => c.EmpCode == empcode) > 0 && exam.DeleteFlag == null
                             orderby exam.ExamDate
                             select new { exam, hosp }).ToList();

                foreach (var data in datas)
                {
                    IKASA_OSCEExamDto dto = asm.Assemble(data.exam);
                    dto.HospName = data.hosp.Unitname_all;
                    dto.QEmpCode = empcode;
                    dto.QEmpName = emp.Empname;
                    dto.IKASA_OSCEExamStages = stasm.Assemble(data.exam.IKASA_OSCEExamStages.OrderBy(c => c.StageNo)).ToList();
                    dto.IKASA_OSCEExamScores = scasm.Assemble(data.exam.IKASA_OSCEExamScores.Where(c => c.IKASA_OSCEExam.IKASA_OSCEExamStages.Count(d => d.StageNo == c.StageNo) > 0)).ToList();

                    var dt = data.exam.IKASA_OSCEExamStages.Sum(d => d.PassScore).Value.ToString("0.00");
                    dto.PassScore = Convert.ToDecimal(dt); //及格總分

                    dto.PassStage = data.exam.PassStage; // 須通過及格站數

                    GetiKASA_OSCE_ExamRankOrder(ref dto);

                    Dictionary<string, int> scorediv = GetScoreLevelDiv(dto.IKASA_OSCEExamScores.Select(c => c.EmpCode).Distinct().Count(), new List<string>() { "A", "B", "<font color='Red'>C</font>", "<font color='Red'>D</font>" });

                    foreach (var sc in dto.IKASA_OSCEExamScores)
                    {
                        foreach (string key in scorediv.Keys)
                        {
                            if (sc.Rank <= scorediv[key])
                            {
                                sc.ScoreLevel = key;
                                break;
                            }
                        }

                        foreach (string key in scorediv.Keys)
                        {
                            if (sc.TotalRank <= scorediv[key])
                            {
                                sc.TotalScoreLevel = key;
                                break;
                            }
                        }
                    }

                    dto.IKASA_OSCEExamScores = dto.IKASA_OSCEExamScores.Where(c => c.EmpCode == empcode).ToList();

                    if (dto.IKASA_OSCEExamScores.Count > 0)
                    {
                        dto.QExamIdno = dto.IKASA_OSCEExamScores.FirstOrDefault().ExamIDNo;

                        dto.QScoreLevel = dto.IKASA_OSCEExamScores.FirstOrDefault().TotalScoreLevel;
                    }

                    dto.QPassStageCount = 0;

                    foreach (var stage in dto.IKASA_OSCEExamStages)
                    {
                        var sc = dto.IKASA_OSCEExamScores.Where(c => c.StageNo == stage.StageNo).FirstOrDefault();
                        if (sc != null)
                        {
                            sc.StageName = stage.StageName;
                            sc.PassScore = stage.PassScore.Value;
                            sc.IsPass = sc.Score >= sc.PassScore;
                            if (sc.IsPass)
                            {
                                dto.QPassStageCount++;
                            }
                        }
                    }
                    if (dto.IKASA_OSCEExamScores.Count > 0)
                    {
                        dto.QTotalScore = dto.IKASA_OSCEExamScores.Sum(c => c.Score);
                        dto.QScoreRate = dto.IKASA_OSCEExamScores.Average(c => c.Score);
                    }
                    result.Add(dto);
                }

                return result;
            }
        }

        private void GetiKASA_OSCE_ExamRankOrder(ref IKASA_OSCEExamDto exam)
        {
            foreach (var stage in exam.IKASA_OSCEExamStages)
            {
                int count = 0;
                decimal exscore = 0;
                int buffcount = 0;
                foreach (var sc in exam.IKASA_OSCEExamScores.Where(c => c.StageNo == stage.StageNo).OrderByDescending(c => c.Score))
                {
                    if (sc.Score != exscore)
                    {
                        count += buffcount + 1;
                        buffcount = 0;
                    }
                    else
                    {
                        buffcount++;
                    }

                    sc.Rank = count;
                    exscore = sc.Score;
                }
            }

            var total = exam.IKASA_OSCEExamScores.GroupBy(c => c.EmpCode).Select(c => new { empcode = c.Key, score = c.Sum(b => b.Score) }).OrderByDescending(c => c.score).ToList();

            int tcount = 0;
            decimal texscore = 0;
            int tbuffcount = 0;

            foreach (var t in total)
            {
                if (t.score != texscore)
                {
                    tcount += tbuffcount + 1;
                    tbuffcount = 0;
                }
                else
                {
                    tbuffcount++;
                }

                foreach (var ss in exam.IKASA_OSCEExamScores.Where(c => c.EmpCode == t.empcode))
                {
                    ss.TotalRank = tcount;
                }

                texscore = t.score;
            }
        }

        private Dictionary<string, int> GetScoreLevelDiv(int datacount, List<string> levels)
        {
            Dictionary<string, int> result = new Dictionary<string, int>();
            int levelcount = levels.Count;
            double levelrate = 1.0 / Convert.ToDouble(levelcount);
            for (int i = 1; i <= levelcount; i++)
            {
                int maxrank = Convert.ToInt32(Math.Floor(levelrate * Convert.ToDouble(i) * Convert.ToDouble(datacount)));

                if (maxrank == 0)
                {
                    maxrank = 1;
                }

                if (i - 1 > 0)
                {
                    if (maxrank == result[levels[i - 2]])
                    {
                        maxrank++;
                    }
                }

                result.Add(levels[i - 1], maxrank);
            }
            return result;
        }

        #endregion

        #endregion

        #region 各個表單內容_Api(Mini-CEX、DOPS、CbD、個人學習歷程檔、實習醫學生學習紀錄及回饋表、實習醫學生夜間學習紀錄、實習醫師PrimaryCare心得報告、滿意度) OK

        public ContentResult Forms(string fileId)
        {
            int instanceId = 0;

            if (!string.IsNullOrEmpty(fileId))
            {
                instanceId = Convert.ToInt32(fileId);
            }

            HtmlFormUtility.Components.HtmlForm htmlForm = new HtmlFormUtility.Components.HtmlForm();
            HtmlFormUtility.Components.ViewComponent viewComponent = new HtmlFormUtility.Components.ViewComponent();
            HtmlFormUtility.FORM_INSTANCES currentInstance = viewComponent.SelectFormInstance(instanceId);

            htmlForm.ReadOnly = true;
            htmlForm.ParameterCollection.Add("instance_id", instanceId.ToString());
            string edutermid = currentInstance.INHOSPID;
            List<HtmlFormUtility.Components.HtmlForm> list = htmlForm.Query(instanceId, true, true, false);
            HttpContext.Session[instanceId + "htmlform"] = htmlForm;

            string html = "";

            foreach (HtmlFormUtility.Components.HtmlForm form in list)
            {
                if (form.ReadOnly)
                {
                    var elements = form.InstanceDocument.DocumentNode.SelectNodes("//*");

                    if (elements != null)
                    {
                        foreach (var element in elements)
                        {
                            element.Attributes.Remove("name");
                        }
                    }
                }
                html += "<form>" + form.InstanceDocument.DocumentNode.InnerHtml + "</form>";
            }
            html += "<form>" + htmlForm.InstanceDocument.DocumentNode.InnerHtml + "</form>";
            return Content(html, "text/html");
        }

        public ActionResult FromsList(string fileId) //不使用
        {
            try
            {
                int instanceId = 0;

                if (!string.IsNullOrEmpty(fileId))
                {
                    instanceId = Convert.ToInt32(fileId);
                }

                HtmlFormUtility.Components.HtmlForm htmlForm = new HtmlFormUtility.Components.HtmlForm();
                HtmlFormUtility.Components.ViewComponent viewComponent = new HtmlFormUtility.Components.ViewComponent();
                HtmlFormUtility.FORM_INSTANCES currentInstance = viewComponent.SelectFormInstance(instanceId);

                htmlForm.ReadOnly = true;
                htmlForm.ParameterCollection.Add("instance_id", instanceId.ToString());
                string edutermid = currentInstance.INHOSPID;
                List<HtmlFormUtility.Components.HtmlForm> list = htmlForm.Query(instanceId, true, true, false);
                HttpContext.Session[instanceId + "htmlform"] = htmlForm;

                string html = "";

                foreach (HtmlFormUtility.Components.HtmlForm form in list)
                {
                    if (form.ReadOnly)
                    {
                        var elements = form.InstanceDocument.DocumentNode.SelectNodes("//*");

                        if (elements != null)
                        {
                            foreach (var element in elements)
                            {
                                element.Attributes.Remove("name");
                            }
                        }
                    }
                    html += "<form>" + form.InstanceDocument.DocumentNode.InnerHtml + "</form>";
                }
                html += "<form>" + htmlForm.InstanceDocument.DocumentNode.InnerHtml + "</form>";
                string url = "data:text/html;charset=utf-8," + HttpUtility.UrlEncode(html);
                return Content(url);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 個人資訊 & 學習階段(下拉選單) OK

        private string CurrentIDNo
        {
            get
            {
                if (Session["iKasaIdNO"] == null)
                {
                    return null;
                }
                else
                {
                    return Session["iKasaIdNO"].ToString();
                }
            }
            set
            {
                Session["iKasaIdNO"] = value;
            }
        }

        private iKASAPersonInfo CurrentPersonInfo
        {
            get
            {
                return Session["iKasaCurrentPersonInfo"] as iKASAPersonInfo;
            }
            set
            {
                Session["iKasaCurrentPersonInfo"] = value;
            }
        }

        public ActionResult PersonInfo(string Empcode)
        {
            try
            {
                using (EduActivityAppService Service = new EduActivityAppService())
                {
                    var empCode = Empcode.ToUpper(); //強制轉大寫字母

                    Session["EduAct_EmpCode"] = empCode;

                    iKASAModel.Person person = new iKASAModel.Person(); //個人資訊Model
                    var Learning = new List<iKASAModel.LearningPhase>(); //學習階段Model

                    string imageUrl = "https://www.kmuh.org.tw/Web/EduActivity/Avatar/" + empCode + ".jpg"; //個人照片

                    string NoEmpImage = HttpContext.Server.MapPath("~/Images/no_avatars.jpg"); //無個人照片的替代照片

                    var emp = Service.GetVKmuEmpByEmpCode(empCode); //搜尋登入者資訊

                    if (System.IO.File.Exists(Server.MapPath("~/Images/" + empCode + "s.jpg"))) //讀取登入者資訊，"~/Images/" + empCode + "s.jpg"
                    {
                        using (var httpClient = new HttpClient())
                        {
                            var pic = httpClient.GetByteArrayAsync(imageUrl).Result; // 使用 HttpClient 下載圖片的位元組資料
                            string picImage = Convert.ToBase64String(pic); //轉成base64格式

                            if (emp != null)
                            {
                                CurrentIDNo = emp.Idno; //身分證字號

                                var EmpList = Service.GetiKASALoginInfo(CurrentIDNo).Where(c => c.empcode == empCode).ToList(); //查詢登入身分資訊

                                foreach (var Emp in EmpList)
                                {
                                    var LearningPhase = new iKASAModel.LearningPhase
                                    {
                                        StudentNo = Emp.membercode, //學號
                                        JobTitle = Emp.membertypename, //職稱
                                        TeamName = Emp.teamname, //組別
                                        InternshipTime = Emp.daterange //實習期間for下拉選單
                                    };

                                    Learning.Add(LearningPhase);
                                }

                                person = new iKASAModel.Person //個人資訊
                                {
                                    EmpCode = emp.Empcode, //職編
                                    EmpName = emp.Empname, //姓名
                                    TrainingDate = EmpList.Select(e => e.daterange).ToList()
                                };

                                var result = new
                                {
                                    Image = picImage, //圖片base64字串
                                    LearningList = Learning, //學習階段(下拉選單)
                                };

                                string json = JsonConvert.SerializeObject(result, new JsonSerializerSettings
                                {
                                    DateFormatString = "yyyy/MM/dd"
                                });

                                return Json(json, JsonRequestBehavior.AllowGet);
                            }
                            return null;
                        }
                    }
                    else
                    {
                        using (var httpClient = new HttpClient())
                        {
                            byte[] pic;
                            string picImage;
                            var request = WebRequest.Create(imageUrl);  //使用WebRequest判斷imageUrl是否存在照片
                            request.Method = "HEAD"; //是設定 WebRequest 的請求方法為 "HEAD"，與 GET 方法類似，但不返回實際的內容主體，只返回頭部訊息

                            try
                            {
                                using (var response = request.GetResponse())
                                {
                                    pic = httpClient.GetByteArrayAsync(imageUrl).Result; //個人照片存在
                                    picImage = Convert.ToBase64String(pic);
                                }
                            }
                            catch (WebException)
                            {
                                picImage = Convert.ToBase64String(System.IO.File.ReadAllBytes(NoEmpImage)); //照片不存在，使用替代照片
                            }

                            if (emp != null)
                            {
                                CurrentIDNo = emp.Idno; //身分證字號

                                var EmpList = Service.GetiKASALoginInfo(CurrentIDNo).Where(c => c.empcode == empCode).ToList(); //查詢登入身分資訊

                                foreach (var Emp in EmpList)
                                {
                                    var LearningPhase = new iKASAModel.LearningPhase
                                    {
                                        StudentNo = Emp.membercode, //學號
                                        JobTitle = Emp.membertypename, //職稱
                                        TeamName = Emp.teamname, //組別
                                        InternshipTime = Emp.daterange //實習期間for下拉選單
                                    };

                                    Learning.Add(LearningPhase);
                                }

                                person = new iKASAModel.Person //個人資訊
                                {
                                    EmpCode = emp.Empcode, //職編
                                    EmpName = emp.Empname, //姓名
                                    TrainingDate = EmpList.Select(e => e.daterange).ToList()
                                };

                                var personInfoList = new List<iKASAModel.Person>(); // 個人資訊列表

                                foreach (var emps in EmpList)
                                {
                                    var personInfo = new iKASAModel.Person
                                    {
                                        EmpCode = emp.Empcode,
                                        EmpName = emp.Empname,
                                        TrainingDate = new List<string> { emps.daterange } // 單筆實習期間
                                    };

                                    personInfoList.Add(personInfo);
                                }

                                var result = new
                                {
                                    Image = picImage, //圖片base64字串
                                    LearningList = Learning, //學習階段(下拉選單)
                                    PersonInfo = personInfoList //個人資訊
                                };

                                return Json(result, JsonRequestBehavior.AllowGet);
                            }

                            return null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region 測試輸出圖片API

        public FileResult GetImage(string Empcode) //版本1
        {
            try
            {

                if (Empcode != null)  //System.IO.File.Exists(Server.MapPath("~/Images/" + Empcode + "s.jpg"))
                {
                    using (HttpClient client = new HttpClient())
                    {
                        string imageUrl = "https://www.kmuh.org.tw/Web/EduActivity/Avatar/" + Empcode + ".jpg";
                        byte[] imageBytes = client.GetByteArrayAsync(imageUrl).Result;
                        string base64Image = Convert.ToBase64String(imageBytes);

                        //var result = new
                        //{
                        //    Image = base64Image
                        //};
                        return File(imageBytes, "image/jpeg");
                    }
                }
                else
                {
                    string NoEmpImage = HttpContext.Server.MapPath("~/Images/no_avatars.jpg");

                    return File(NoEmpImage, "image/jpeg");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JsonResult Images() //版本2
        {
            try
            {
                string imagePath = HttpContext.Server.MapPath("~/Images/cat.jpg");

                if (System.IO.File.Exists(imagePath))
                {
                    byte[] imageBytes = System.IO.File.ReadAllBytes(imagePath);
                    string base64Image = Convert.ToBase64String(imageBytes);

                    var result = new { imageData = base64Image };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }

                return Json(null, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #endregion

        #region 查詢memberid參數 OK

        public JsonResult SearchMemberId(string empcode)
        {
            try
            {
                iKASAModel.MemberID memid = new iKASAModel.MemberID();

                List<iKASAModel.MemberID> memberIDs;
                using (EduActivityContextService Service = new EduActivityContextService())
                {
                    memberIDs = (from mem in Service.UnitOfWork.Members
                                 where mem.IsHospMember == empcode
                                 orderby mem.DateTo descending
                                 select new iKASAModel.MemberID
                                     {
                                         memberid = mem.MemberID, //職編流水號
                                         MemberType = mem.MemberType //職稱
                                     }).ToList();
                }

                return Json(memberIDs, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region 查詢instanceid參數 OK

        public JsonResult SearchInstanceId(string empcode, List<string> groupids)
        {
            using (EduActivityContextService service = new EduActivityContextService())
            {
                List<iKASAModel.InstanceID> result = new List<iKASAModel.InstanceID>();

                var recs = from rec in service.UnitOfWork.RecordInstances
                           join dep in service.UnitOfWork.V_departments
                           on
                           new { dept = rec.DeptCode, hosp = rec.HospCode } equals new { dept = dep.Deptcode, hosp = dep.Hospcode }
                           where rec.Status == "V"
                           && (
                           rec.RecordInsReaders.Count(c => c.Reader == empcode) > 0
                           ||
                           rec.RecordInsSignIns.Count(c => c.EmpCode == empcode) > 0
                           ||
                           rec.RecordInsViewers.Count(c => c.Viewer == empcode) > 0
                           ||
                           rec.Recoder == empcode
                           )
                           && groupids.Contains(rec.TemplateID.ToString())
                           select new iKASAModel.InstanceID { instanceid = rec.InstanceID };
                result = recs.ToList();

                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region 查詢TemplateID參數 OK

        public JsonResult SearchTemplateId(string InstanceId)
        {
            try
            {
                iKASAModel.TemplateID TempId = new iKASAModel.TemplateID();

                List<iKASAModel.TemplateID> TempIDs;

                using (EduActivityContextService Service = new EduActivityContextService())
                {
                    TempIDs = (from Temp in Service.UnitOfWork.RecordInstances
                               where Temp.InstanceID == InstanceId
                               select new iKASAModel.TemplateID
                               {
                                   Templateid = Temp.TemplateID
                               }).ToList();
                }

                return Json(TempIDs, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region 教學門住診紀錄表_表單 OK

        const string TemplatePath = "~/UserControls/{0}.ascx";

        public ContentResult ClinicForm(string groupid, string empcode, string instanceid)
        {
            try
            {
                // 檢查參數名稱是否正確
                if (groupid == null)
                {
                    throw new ArgumentException("參數錯誤: 請使用正確的參數名稱 'groupid'。");
                }

                List<string> gids = groupid.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).ToList();

                using (EduActivityAppService Service = new EduActivityAppService())
                {
                    List<RecordInstanceDto> list = Service.GetiKasaMemberRecordInstance(empcode, gids);

                    if (list.Count > 0)
                    {
                        CurrentInstanceID = instanceid;
                        RecordInstanceDto ins = Service.GetRecordInstance(CurrentInstanceID);
                        CurrentIns = ins;

                        string userControlPath = VirtualPathUtility.ToAbsolute(string.Format(TemplatePath, CurrentIns.RecordTemplate.ClassName + "_p"));

                        Page pageHolder = new Page();

                        Control ctr = pageHolder.LoadControl(userControlPath);
                        (ctr as IRecordBase).CurrentRecordIns = ins; //紀錄者
                        (ctr as IRecordBase).SetControlValues(ins.DetNameValueData); //表單內容
                        List<AccountRoleDto> roles = Service.GetAuthRole(CurrentEmpCode);
                        (ctr as IRecordBase).SetReadOnly(true);

                        return Content(RenderUserControl(ctr), "text/html");
                    }
                    else
                    {
                        return Content("No data");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string RenderUserControl(Control control)
        {
            Page pageHolder = new Page();
            pageHolder.Controls.Add(control);
            StringWriter stringWriter = new StringWriter();
            HttpContext.Server.Execute(pageHolder, stringWriter, false);
            return stringWriter.ToString();
        }

        private RecordInstanceDto CurrentIns
        {
            get
            {
                return Session["CurrentIns"] as RecordInstanceDto;
            }
            set
            {
                Session["CurrentIns"] = value;
            }
        }

        private string CurrentEmpCode
        {
            get
            {
                return Request.QueryString["EmpCode"];
            }
        }

        #endregion

        #region 跨團隊溝通表單 OK

        const string TemPath = "~/UserControls/{0}.ascx";

        public ContentResult CATeamsForm(string groupid, string empcode, string instanceid)
        {
            try
            {
                // 檢查參數名稱是否正確
                if (groupid == null)
                {
                    throw new ArgumentException("參數錯誤: 請使用正確的參數名稱 'groupid'。");
                }

                List<string> gids = groupid.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).ToList();

                using (EduActivityAppService Service = new EduActivityAppService())
                {
                    List<RecordInstanceDto> list = Service.GetiKasaMemberRecordInstance(empcode, gids);

                    if (list.Count > 0)
                    {
                        CurrentInstanceID = instanceid;
                        RecordInstanceDto ins = Service.GetRecordInstance(CurrentInstanceID);
                        CurrentIns = ins;

                        string userControlPath = VirtualPathUtility.ToAbsolute(string.Format(TemplatePath, CurrentIns.RecordTemplate.ClassName + "_p"));

                        Page pageHolder = new Page();

                        Control ctr = pageHolder.LoadControl(userControlPath);
                        (ctr as IRecordBase).CurrentRecordIns = ins; //紀錄者
                        (ctr as IRecordBase).SetControlValues(ins.DetNameValueData); //表單內容
                        List<AccountRoleDto> roles = Service.GetAuthRole(CurrentEmpCode);
                        (ctr as IRecordBase).SetReadOnly(true);

                        return Content(RenderUserControl(ctr), "text/html");
                    }
                    else
                    {
                        return Content("No data");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private iKASAModel.PassPortTitle ReadInstanceData()
        {
            if (CurrentInstanceID == null)
            {
                TempData["ErrorMessage"] = "參數傳遞錯誤";
                return null;
            }
            using (EduActivityAppService Service = new EduActivityAppService())
            {
                EduPassportInstanceDto ins = Service.GetEduPassportInstanceByID(CurrentInstanceID);

                CurrentInstance = ins;
                var PassportName = ins.TemplateName; //護照名稱

                iKASAModel.PassPortTitle Title = new iKASAModel.PassPortTitle
                {
                    PassPortName = ins.TemplateName,
                };
                return Title;
            }
        }

        private string CurrentInstanceID
        {
            get
            {
                return Session["CurrentInstanceID"] as string;
            }
            set
            {
                Session["CurrentInstanceID"] = value;
            }
        }

        private EduPassportInstanceDto CurrentInstance
        {
            get
            {
                return Session["CurrentInstance"] as EduPassportInstanceDto;
            }
            set
            {
                Session["CurrentInstance"] = value;
            }
        }

        #endregion

        #region 新功能API

        #region 首頁-實習各階段評量分數成績 OK

        //主程式-實習各階段評量實習各階段評量_分數成績
        public JsonResult InternshipGrades(string empcode, string memberid)
        {
            try
            {
                List<iKASAModel.MiniCEXScore> combinedResult = ClinicalGrades(empcode, memberid); //取得MiniCEX和PrimaryCare的InstanceID

                List<string> empcodenames = empcode.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).ToList();

                using (EduActivityContextService Service = new EduActivityContextService())
                {
                    int InstanceID = GetInstanceID(combinedResult);

                    List<int> instanceIds = combinedResult.Select(c => c.Instance_ID).ToList(); //獲取所有的 Instance_ID

                    var res = (from el in Service.UnitOfWork.FORM_INSTANCE_ELEMENTs

                               join fi in Service.UnitOfWork.FORM_INSTANCEs on el.INSTANCE_ID equals fi.INSTANCE_ID

                               join et in Service.UnitOfWork.EduTerms on fi.INHOSPID equals et.EduTermID

                               join emp in Service.UnitOfWork.V_departments on et.DepCode equals emp.Deptcode

                               join temp in Service.UnitOfWork.FORM_INSTANCEs on fi.TEMPLATE_ID equals temp.TEMPLATE_ID

                               where instanceIds.Contains(el.INSTANCE_ID) &&
                              ((el.NAME == "tbScore" && temp.TEMPLATE_ID == 33) || (el.NAME == "tbDoctor" && temp.TEMPLATE_ID == 3585))
                               //33-MiniCEX、3585-PrimaryCare

                               select new
                               {
                                   InsID = el.INSTANCE_ID, //InstanceID編號
                                   DeptName = emp.Deptname, //科別名稱
                                   TotalScore = el.ELEMENT_VALUE, //表單分數
                                   TemplateID = temp.TEMPLATE_ID //表單類別編號

                               }).ToList();

                    var dto = res.Where(item => instanceIds.Contains(item.InsID) && item.TemplateID == 33) //Mini-CEX
                                .Select(item => new iKASAModel.MiniCEX_InternFormScore
                                {
                                    InsID = item.InsID,
                                    DeptName = item.DeptName,
                                    TotalScore = item.TotalScore,
                                    TempId = item.TemplateID

                                }).ToList();

                    var dtos = res.Where(item => instanceIds.Contains(item.InsID) && item.TemplateID == 3585) //PrimartCare
                                .Select(item => new iKASAModel.PrimaryCare_InternFormScore
                                {
                                    InsID = item.InsID,
                                    DeptName = item.DeptName,
                                    TotalScore = item.TotalScore,
                                    TempId = item.TemplateID
                                }).ToList();

                    var list = new iKASAModel.InternScoreList
                    {
                        MiniCEX = dto.GroupBy(item => item.InsID).Select(group => group.First()).ToList(),
                        PrimaryCare = dtos.GroupBy(item => item.InsID).Select(group => group.First()).ToList(),
                    };

                    return Json(list, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //將InstanceID轉為int並判斷Null
        private int GetInstanceID(List<iKASAModel.MiniCEXScore> combinedResult)
        {
            if (combinedResult != null && combinedResult.Count > 0)
            {
                return combinedResult[0].Instance_ID;
            }
            else
            {
                return 0;
            }
        }

        //查詢所有的Mini-CEX和PrimaryCare的InstanceID-第一步驟
        private List<iKASAModel.MiniCEXScore> ClinicalGrades(string empcode, string memberid)
        {
            try
            {
                using (EduActivityContextService Service = new EduActivityContextService())
                {
                    string groupidMiniCEX = "16";
                    string groupidPrimaryCare = "24";

                    // 呼叫SearchInsID方法兩次，分別取得兩個 groupid 的結果
                    List<iKASAModel.MiniCEXScore> resMini = SearchInsID(empcode, groupidMiniCEX, memberid);
                    List<iKASAModel.MiniCEXScore> resPrimary = SearchInsID(empcode, groupidPrimaryCare, memberid);

                    List<iKASAModel.MiniCEXScore> combinedResult = new List<iKASAModel.MiniCEXScore>();
                    combinedResult.AddRange(resMini);
                    combinedResult.AddRange(resPrimary);

                    int InstanceID = combinedResult.FirstOrDefault().Instance_ID;

                    var filteredResult = combinedResult.Select(f => new
                    {
                        f.Instance_ID,
                        f.INHOSPID,
                        f.Lesson,
                        f.List,
                        f.TempID
                    }).ToList();

                    return combinedResult;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //查詢InsatnceID-第二步驟
        private List<iKASAModel.MiniCEXScore> SearchInsID(string empcode, string groupid, string memberID)
        {
            try
            {
                if (groupid == null)
                {
                    throw new ArgumentException("參數錯誤: 請使用正確的參數名稱 'groupid'。");
                }

                using (EduActivityContextService Service = new EduActivityContextService())
                {
                    int catid = Convert.ToInt32(groupid); //iKASA_MenuParam代碼

                    string memberid = SearchMemberID(empcode, memberID); //查詢memberID

                    var members = from mem in Service.UnitOfWork.Members
                                  join rundown in Service.UnitOfWork.EduTeamMemberRundowns on mem.MemberID equals rundown.MemberID
                                  join term in Service.UnitOfWork.EduTerms on rundown.EduTermID equals term.EduTermID
                                  where mem.MemberID == memberid
                                  orderby term.DateFrom
                                  select new { mem, rundown, term };

                    List<iKASAModel.MiniCEXScore> result = new List<iKASAModel.MiniCEXScore>(); //自訂Model名稱類型
                    List<int> existsforms = new List<int>();

                    foreach (var mem in members)
                    {
                        List<iKASAModel.MiniCEXScore> otherforms = (from ins in Service.UnitOfWork.FORM_INSTANCEs
                                                                    where ins.INHOSPID == mem.term.EduTermID && (ins.TargetID == mem.mem.IsHospMember || ins.EvalTargetID ==
                                                                    mem.mem.MemberID) && Service.UnitOfWork.FormCategoryRefs.Count(c => c.CategoryID == catid && c.TEMPLATE_ID ==
                                                                    ins.TEMPLATE_ID) > 0 && !existsforms.Contains(ins.INSTANCE_ID) && ins.Status != '0'
                                                                    && ins.PARENT_INSTANCE_ID == null
                                                                    select new iKASAModel.MiniCEXScore
                                                            {
                                                                Instance_ID = ins.INSTANCE_ID,
                                                                INHOSPID = ins.INHOSPID,
                                                                Lesson = ins.INHOSPID,
                                                                List = ins.INSTANCE_NAME,
                                                                CreateTime = ins.INSTANCE_CREATE_DATETIME,
                                                                TempID = ins.TEMPLATE_ID
                                                            }).ToList();

                        existsforms.AddRange(otherforms.Select(c => c.Instance_ID));

                        List<int> pidlist = (from ins in Service.UnitOfWork.FORM_INSTANCEs
                                             where ins.INHOSPID == mem.term.EduTermID
                                             && (ins.TargetID == mem.mem.IsHospMember || ins.EvalTargetID == mem.mem.MemberID) && !existsforms.Contains(ins.INSTANCE_ID)
                                             && ins.Status != '0' && ins.PARENT_INSTANCE_ID != null
                                             && Service.UnitOfWork.FORM_INSTANCEs.Count(c => c.PARENT_INSTANCE_ID == ins.PARENT_INSTANCE_ID && c.Status == '0') == 0
                                             && Service.UnitOfWork.FormCategoryRefs.Count(c => c.CategoryID == catid && c.TEMPLATE_ID ==
                                             ins.FORM_TEMPLATE.PARENT_TEMPLATE_ID) > 0
                                             select ins.PARENT_INSTANCE_ID.Value).Distinct().ToList();

                        foreach (int pid in pidlist)
                        {
                            iKASAModel.MiniCEXScore pdto = (from ins in Service.UnitOfWork.FORM_INSTANCEs
                                                            join pins in Service.UnitOfWork.FORM_INSTANCEs on ins.PARENT_INSTANCE_ID equals pins.INSTANCE_ID
                                                            where ins.PARENT_INSTANCE_ID == pid && !existsforms.Contains(ins.INSTANCE_ID)
                                                            orderby ins.INSTANCE_ID descending
                                                            select new iKASAModel.MiniCEXScore
                                                            {
                                                                Instance_ID = ins.INSTANCE_ID,
                                                                INHOSPID = pins.INHOSPID,
                                                                Lesson = pins.INHOSPID,
                                                                List = pins.INSTANCE_NAME,
                                                                CreateTime = ins.INSTANCE_CREATE_DATETIME,
                                                                TempID = ins.TEMPLATE_ID
                                                            }).FirstOrDefault();
                            otherforms.Add(pdto);

                            existsforms.Add(pdto.Instance_ID);
                        }
                        result.AddRange(otherforms);
                    }

                    foreach (var f in result)
                    {
                        try
                        {
                            if (f.INHOSPID != null)
                            {
                                f.INHOSPID = GetEduTermFullName(f.INHOSPID);
                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }

                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //查詢memberID職編流水號-第三步驟
        private string SearchMemberID(string empcode, string memberID)
        {
            try
            {
                string memberid;

                using (EduActivityContextService Service = new EduActivityContextService())
                {
                    var memberIdQuery = (from mem in Service.UnitOfWork.Members
                                         where mem.IsHospMember == empcode && mem.MemberID == memberID
                                         orderby mem.DateTo descending
                                         select mem.MemberID).FirstOrDefault();

                    if (memberIdQuery != null)
                    {
                        memberid = memberIdQuery;
                    }
                    else
                    {
                        memberid = "未找到對應的 memberid";
                    }
                }
                return memberid;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 學科分數成績 ok

        //主程式-首頁_學期成績分數
        public JsonResult SemesterGrades(string empcode)
        {
            try
            {
                List<string> empcodenames = empcode.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).ToList();

                using (EduActivityContextService Service = new EduActivityContextService())
                {
                    var dto = (from Scores in Service.UnitOfWork.EduScores //學期成績
                               join emp in Service.UnitOfWork.V_KmuEmps on Scores.Empcode equals emp.Empcode
                               join cemp in Service.UnitOfWork.V_KmuEmps on Scores.Uploader equals cemp.Empcode
                               where (empcodenames.Count() == 0 || empcodenames.Contains(emp.Empcode) || empcodenames.Contains
                               (emp.Empname)) && Scores.Status != "X"
                               orderby Scores.Id
                               select new iKASAModel.SemesterGrades_InternFormScore
                               {
                                   MemberType = GetMappedMemberType(Scores.Deplevel),
                                   ClassName = Scores.Classname,
                                   SemesterGrades = Scores.Score.ToString()
                               }).ToList();

                    var list = new iKASAModel.SemesterGradesScoreList
                    {
                        SemesterGrades = dto,
                    };

                    return Json(list, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //轉換文字-學期成績分數
        private string GetMappedMemberType(string memberType)
        {
            if (memberType == "醫學系5" || memberType == "後醫系3")
                return "Clerk1";
            else if (memberType == "醫學系6" || memberType == "後醫系4")
                return "Clerk2";
            else
                return memberType;
        }

        #endregion


        #region 新版-臨床照護能力
        //public JsonResult ad(string empcode)
        //{
        //    try
        //    {

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        #endregion

        #region 傑出表現

        //public JsonResult aj(string empcode)
        //{
        //    try
        //    {

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}


        #endregion

        #region 六大核心能力

        #endregion

        #endregion

    }
}