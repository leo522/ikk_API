using KMU.EduActivity.ApplicationLayer.DTO;
using KMU.EduActivity.ApplicationLayer.Services;
using KMU.EduActivity.DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KMUH.iKASAWebApi.UI.MVC.Models
{
    public class AuthPage : System.Web.UI.Page
    {
        public bool GetUserData()
        {
            EduActivityAppService service = new EduActivityAppService();

            AccountDto acc = service.GetSystemAccountData(Page.User.Identity.Name);
            if (acc != null)
            {
                Session["EduAct_EmpCode"] = acc.Empcode;
                Session["EduAct_EmpName"] = acc.Empname;
                Session["EduAct_DeptCode"] = acc.Deptcode;
                Session["EduAct_DeptName"] = "";
                Session["EduAct_AreaCode"] = "";
                Session["EduAct_LocCode"] = acc.Deptcode;
                Session["EduAct_JobCode"] = "";
                Session["EduAct_HospCode"] = acc.Hospcode;
                Session["EduAct_AuthRoles"] = service.GetAuthRole(acc.Empcode).Select(c => c.RoleID).ToList();

                return true;
            }

            SelectVKmuempData dto = service.ReadVKmuempData(Page.User.Identity.Name);

            if (dto != null)
            {
                Session["EduAct_EmpCode"] = dto.empcode;
                Session["EduAct_EmpName"] = dto.empname;
                Session["EduAct_DeptCode"] = dto.deptcode;
                Session["EduAct_DeptName"] = "";
                Session["EduAct_AreaCode"] = "";
                Session["EduAct_LocCode"] = dto.loccode;
                Session["EduAct_JobCode"] = dto.jobcode;
                Session["EduAct_Member"] = service.ReadMemberByLogin(dto.empcode);
                Session["EduAct_HospCode"] = dto.hospcode;
                Session["EduAct_AuthRoles"] = service.GetAuthRole(dto.empcode).Select(c => c.RoleID).ToList();

                var emp = service.GetVKmuEmpByEmpCode(Page.User.Identity.Name);
                if (emp != null)
                {
                    Session["EduAct_Idno"] = emp.Idno;
                }
                if (AuthRoles.Contains("DepSec"))
                {
                    Session["EduAct_DeptSecDep"] = service.GetDeptSecDeps(dto.empcode);
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        protected List<string> AuthRoles
        {
            get
            {
                return Session["EduAct_AuthRoles"] as List<string>;
            }
        }
    }
}