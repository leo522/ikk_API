#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by the ClassGenerator.ttinclude code generation file.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Common;
using System.Collections.Generic;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Metadata;
using Telerik.OpenAccess.Data.Common;
using Telerik.OpenAccess.Metadata.Fluent;
using Telerik.OpenAccess.Metadata.Fluent.Advanced;

namespace KMUH.iKASAWebApi.DomainModel.Entities	
{
	public partial class FORM_TEMPLATE_ELEMENT_READONLY
	{
		private int _iD;
		public virtual int ID
		{
			get
			{
				return this._iD;
			}
			set
			{
				this._iD = value;
			}
		}
		
		private string _tEMPLATE_ELEMELT_ID;
		public virtual string TEMPLATE_ELEMELT_ID
		{
			get
			{
				return this._tEMPLATE_ELEMELT_ID;
			}
			set
			{
				this._tEMPLATE_ELEMELT_ID = value;
			}
		}
		
		private string _sETTINGTYPE;
		public virtual string SETTINGTYPE
		{
			get
			{
				return this._sETTINGTYPE;
			}
			set
			{
				this._sETTINGTYPE = value;
			}
		}
		
		private string _sETTINGID;
		public virtual string SETTINGID
		{
			get
			{
				return this._sETTINGID;
			}
			set
			{
				this._sETTINGID = value;
			}
		}
		
		private string _jOB_CODE;
		public virtual string JOB_CODE
		{
			get
			{
				return this._jOB_CODE;
			}
			set
			{
				this._jOB_CODE = value;
			}
		}
		
	}
}
#pragma warning restore 1591
