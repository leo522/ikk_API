using KMU.EduActivity.ApplicationLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KMUH.iKASAWebApi.UI.MVC.Models
{
    public class CommAcrossTeamsModel : FORM_INSTANCEDto, IDtoWithKey
    {
        string CDate = string.Empty;

        public string _INSTANCE_CREATE_DATETIME { get { return INSTANCE_CREATE_DATETIME.ToString("yyyyMMdd"); } }

        public override string CREATER { get; set; }
        public bool? DISPLAY_TO_EVALTARGET { get; set; }
        public override string DtoKey { get; set; }
        public override string EvalTargetID { get; set; }
        public override DateTime? ExpireDate { get; set; }
        public override IList<FORM_INSTANCE_ATTACHMENTDto> FORM_INSTANCE_ATTACHMENTs { get; set; }
        public override IList<FORM_INSTANCE_TARGETDto> FORM_INSTANCE_TARGETs { get; set; }
        public override FORM_TEMPLATEDto FORM_TEMPLATE { get; set; }
        public override string INHOSPID { get; set; }
        public override DateTime? INSTANCE_ALTER_DATETIME { get; set; }
        public override string INSTANCE_CONTENT { get; set; }
        public override DateTime INSTANCE_CREATE_DATETIME { get; set; }
        public override int INSTANCE_ID { get; set; }
        public override string INSTANCE_NAME { get; set; }
        public override string INSTANCE_REMARK { get; set; }
        public override bool? IsPass { get; set; }
        public override int? PARENT_INSTANCE_ID { get; set; }
        public override char Status { get; set; }
        public override string TargetID { get; set; }
        public override string TargetType { get; set; }
        public override int TEMPLATE_ID { get; set; }
    }

}