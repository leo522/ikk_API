using KMU.EduActivity.ApplicationLayer.DTO;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KMUH.iKASAWebApi.UI.MVC.Models
{
    public class IRecordBase : UserControl
    {
        public IRecordBase()
        {
            //
            // TODO: 在這裡新增建構函式邏輯
            //
        }

        public RecordInstanceDto CurrentRecordIns //紀錄者
        {
            get;
            set;
        }

        public void SetReadOnly(bool setvalue)
        {
            if (setvalue)
            {
                foreach (Control ctrl in this.Controls)
                {
                    string id = ctrl.ID;
                    switch (ctrl.GetType().Name)
                    {
                        case "TextBox":
                            (ctrl as TextBox).ReadOnly = true;
                            break;
                        case "CheckBox":
                            (ctrl as CheckBox).Enabled = false;
                            break;
                        case "RadioButton":
                            (ctrl as RadioButton).Enabled = false;
                            break;
                        //case "RadEditor":
                        //    (ctrl as RadEditor).EditModes = EditModes.Preview;
                        //    break;
                        //case "DropDownList":
                        //    (ctrl as DropDownList).Enabled = false;
                        //    break;
                        //case "RadDatePicker":
                        //    (ctrl as RadDatePicker).Enabled = false;
                        //    break;
                        //case "RadDateTimePicker":
                        //    (ctrl as RadDateTimePicker).Enabled = false;
                        //    break;
                        //case "RadTimePicker":
                        //    (ctrl as RadTimePicker).Enabled = false;
                        //    break;
                    }

                }
            }
            else
            {
                foreach (Control ctrl in this.Controls)
                {
                    string id = ctrl.ID;
                    switch (ctrl.GetType().Name)
                    {
                        case "TextBox":
                            (ctrl as TextBox).ReadOnly = false;
                            break;
                        case "CheckBox":
                            (ctrl as CheckBox).Enabled = true;
                            break;
                        case "RadioButton":
                            (ctrl as RadioButton).Enabled = true;
                            break;
                        //case "RadEditor":
                        //    (ctrl as RadEditor).EditModes = EditModes.Design;
                        //    break;
                        //case "DropDownList":
                        //    (ctrl as DropDownList).Enabled = true;
                        //    break;
                        //case "RadDatePicker":
                        //    (ctrl as RadDatePicker).Enabled = true;
                        //    break;
                        //case "RadDateTimePicker":
                        //    (ctrl as RadDateTimePicker).Enabled = true;
                        //    break;
                        //case "RadTimePicker":
                        //    (ctrl as RadTimePicker).Enabled = true;
                        //    break;
                    }

                }
            }
        }

        public NameValueCollection GetControlValues()
        {
            NameValueCollection result = new NameValueCollection();
            foreach (Control ctrl in this.Controls)
            {
                string id = ctrl.ID;
                switch (ctrl.GetType().Name)
                {
                    case "TextBox":
                        result.Add(id, (ctrl as TextBox).Text);
                        break;
                    case "CheckBox":
                        result.Add(id, (ctrl as CheckBox).Checked.ToString());
                        break;
                    case "RadioButton":
                        result.Add(id, (ctrl as RadioButton).Checked.ToString());
                        break;
                    //case "RadEditor":
                    //    result.Add(id, (ctrl as RadEditor).Content);
                    //    break;
                    case "DropDownList":
                        result.Add(id, (ctrl as DropDownList).SelectedValue);
                        break;
                    //case "RadDatePicker":
                    //    if ((ctrl as RadDatePicker).SelectedDate != null)
                    //    {
                    //        result.Add(id, (ctrl as RadDatePicker).SelectedDate.Value.ToString("yyyy/MM/dd"));
                    //    }
                    //    else
                    //    {
                    //        result.Add(id, null);
                    //    }
                    //    break;
                    //case "RadDateTimePicker":
                    //    if ((ctrl as RadDateTimePicker).SelectedDate != null)
                    //    {
                    //        result.Add(id, (ctrl as RadDateTimePicker).SelectedDate.Value.ToString("yyyy/MM/dd HH:mm"));
                    //    }
                    //    else
                    //    {
                    //        result.Add(id, null);
                    //    }
                    //    break;
                    //case "RadTimePicker":
                    //    if ((ctrl as RadTimePicker).SelectedDate != null)
                    //    {
                    //        result.Add(id, (ctrl as RadTimePicker).SelectedDate.Value.ToString("yyyy/MM/dd HH:mm"));
                    //    }
                    //    else
                    //    {
                    //        result.Add(id, null);
                    //    }
                    //    break;
                }
            }
            return result;
        }

        public void SetControlValues(NameValueCollection coll)
        {
            foreach (string id in coll.Keys)
            {
                Control ctrl = this.FindControl(id);
                if (ctrl != null)
                {
                    switch (ctrl.GetType().Name)
                    {
                        case "TextBox":
                            (ctrl as TextBox).Text = coll[id];
                            break;
                        case "CheckBox":
                            (ctrl as CheckBox).Checked = Convert.ToBoolean(coll[id]);
                            break;
                        case "RadioButton":
                            (ctrl as RadioButton).Checked = Convert.ToBoolean(coll[id]);
                            break;
                        //case "RadEditor":
                        //    (ctrl as RadEditor).Content = coll[id];
                        //    break;
                        case "DropDownList":
                            (ctrl as DropDownList).SelectedValue = coll[id];
                            break;
                        //case "RadDatePicker":
                        //    if (coll[id] != null)
                        //    {
                        //        (ctrl as RadDatePicker).SelectedDate = Convert.ToDateTime(coll[id]);
                        //    }
                        //    else
                        //    {
                        //        (ctrl as RadDatePicker).SelectedDate = null;
                        //    }
                        //    break;
                        //case "RadDateTimePicker":
                        //    if (coll[id] != null)
                        //    {
                        //        (ctrl as RadDateTimePicker).SelectedDate = Convert.ToDateTime(coll[id]);
                        //    }
                        //    else
                        //    {
                        //        (ctrl as RadDateTimePicker).SelectedDate = null;
                        //    }
                        //    break;
                        //case "RadTimePicker":
                        //    if (coll[id] != null)
                        //    {
                        //        (ctrl as RadTimePicker).SelectedDate = Convert.ToDateTime(coll[id]);
                        //    }
                        //    else
                        //    {
                        //        (ctrl as RadTimePicker).SelectedDate = null;
                        //    }
                        //    break;
                        case "Literal":
                            (ctrl as Literal).Text = coll[id];
                            break;
                    }
                }
            }
        }
    }
}