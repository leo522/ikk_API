using KMUH.iKASAWebApi.UI.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KMUH.iKASAWebApi.UI.MVC.UserControls
{
    public partial class 教學門診記錄表_p : IRecordBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (TextBox5.Text == "")
                {
                    if (CurrentRecordIns != null)
                    {
                        TextBox5.Text = CurrentRecordIns.CreaterName;
                    }
                }
            }
        }
    }
}