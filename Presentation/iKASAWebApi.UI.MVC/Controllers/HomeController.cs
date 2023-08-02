using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OAuthLibrary;
using OAuthLibrary.Extensions;
using OAuthLibrary.Extensions.Profiles;
using KMUH.iKASAWebApi.UI.MVC.SecurityReadWcf ;


namespace KMUH.iKASAWebApi.UI.MVC.Controllers
{

    public class HomeController : Controller
    {
        string SystemID = "iKASAWebApi"; 
        //[KMUHAuthentication]
        public ActionResult Index()
        {
            return View();
        }

        //[KMUHAuthentication]
        public ActionResult List()
        {
            return View();
        }

        [KMUHAuthentication]
        public ActionResult EditDataPartial()
        {
            return PartialView();
        }

        [KMUHAuthentication]
        public ActionResult EditMulti_Table()
        {
            return View();
        }


        public PartialViewResult PageHeader()
        {
            var userProfile = Request.GetUserProfile();

            ViewBag.SystemName = "高醫資訊系統公版";
            //ViewBag.UserInfo = string.Format("{0}({1}) <br />{2}-{3} {4}({5})", userProfile.SourceIPAddress, userProfile.IsIntranetIP ? "院內" : "院外", userProfile.Hospital, userProfile.DepartmentName, userProfile.UserName, userProfile.UserId);

            //ViewBag.UserInfo = "";
            return PartialView("PageHeaderPartial");
        }

        public PartialViewResult FunctionMenu()
        {

            if (Session["FunctionList"] == null)
            {
                //using (SecurityReadWCFServiceClient service = new SecurityReadWCFServiceClient())
                //{
                //    var userProfile = Request.GetUserProfile();
                //    var FunctionList = service.QueryUserAppWins(userProfile.UserId, SystemID).Select(c => c.WINDOW).ToList();
                //    FunctionList.Add("FunctionListTMP");
                //    Session["FunctionList"] = FunctionList;
                //}
            }

            //ViewBag.FunctionList = (Session["FunctionList"] as List<string>);

           return PartialView("FunctionMenuPartial");            

        }

    }
}