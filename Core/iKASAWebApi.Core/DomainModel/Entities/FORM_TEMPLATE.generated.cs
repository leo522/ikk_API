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
using KMUH.iKASAWebApi.DomainModel.Entities;

namespace KMUH.iKASAWebApi.DomainModel.Entities	
{
	public partial class FORM_TEMPLATE
	{
		private int _tEMPLATE_ID;
		public virtual int TEMPLATE_ID
		{
			get
			{
				return this._tEMPLATE_ID;
			}
			set
			{
				this._tEMPLATE_ID = value;
			}
		}
		
		private int? _pARENT_TEMPLATE_ID;
		public virtual int? PARENT_TEMPLATE_ID
		{
			get
			{
				return this._pARENT_TEMPLATE_ID;
			}
			set
			{
				this._pARENT_TEMPLATE_ID = value;
			}
		}
		
		private string _tEMPLATE_NAME;
		public virtual string TEMPLATE_NAME
		{
			get
			{
				return this._tEMPLATE_NAME;
			}
			set
			{
				this._tEMPLATE_NAME = value;
			}
		}
		
		private string _tEMPLATE_CATEGORY;
		public virtual string TEMPLATE_CATEGORY
		{
			get
			{
				return this._tEMPLATE_CATEGORY;
			}
			set
			{
				this._tEMPLATE_CATEGORY = value;
			}
		}
		
		private DateTime _tEMPLATE_CREATE_DATATIME;
		public virtual DateTime TEMPLATE_CREATE_DATATIME
		{
			get
			{
				return this._tEMPLATE_CREATE_DATATIME;
			}
			set
			{
				this._tEMPLATE_CREATE_DATATIME = value;
			}
		}
		
		private DateTime? _tEMPLATE_ALTER_DATATIME;
		public virtual DateTime? TEMPLATE_ALTER_DATATIME
		{
			get
			{
				return this._tEMPLATE_ALTER_DATATIME;
			}
			set
			{
				this._tEMPLATE_ALTER_DATATIME = value;
			}
		}
		
		private string _tEMPLATE_CONTENT;
		public virtual string TEMPLATE_CONTENT
		{
			get
			{
				return this._tEMPLATE_CONTENT;
			}
			set
			{
				this._tEMPLATE_CONTENT = value;
			}
		}
		
		private string _tEMPLATE_REMARK;
		public virtual string TEMPLATE_REMARK
		{
			get
			{
				return this._tEMPLATE_REMARK;
			}
			set
			{
				this._tEMPLATE_REMARK = value;
			}
		}
		
		private string _tEMPLATE_TYPE;
		public virtual string TEMPLATE_TYPE
		{
			get
			{
				return this._tEMPLATE_TYPE;
			}
			set
			{
				this._tEMPLATE_TYPE = value;
			}
		}
		
		private string _aLLOW_ATTACHMENT;
		public virtual string ALLOW_ATTACHMENT
		{
			get
			{
				return this._aLLOW_ATTACHMENT;
			}
			set
			{
				this._aLLOW_ATTACHMENT = value;
			}
		}
		
		private string _sCORE_FIELD_ID;
		public virtual string SCORE_FIELD_ID
		{
			get
			{
				return this._sCORE_FIELD_ID;
			}
			set
			{
				this._sCORE_FIELD_ID = value;
			}
		}
		
		private string _aLERT_NEXT_MESSAGE;
		public virtual string ALERT_NEXT_MESSAGE
		{
			get
			{
				return this._aLERT_NEXT_MESSAGE;
			}
			set
			{
				this._aLERT_NEXT_MESSAGE = value;
			}
		}
		
		private bool? _dISPLAY_TO_EVALTARGET;
		public virtual bool? DISPLAY_TO_EVALTARGET
		{
			get
			{
				return this._dISPLAY_TO_EVALTARGET;
			}
			set
			{
				this._dISPLAY_TO_EVALTARGET = value;
			}
		}
		
		private bool _eNABLED;
		public virtual bool ENABLED
		{
			get
			{
				return this._eNABLED;
			}
			set
			{
				this._eNABLED = value;
			}
		}
		
		private bool _aLLOW_NEXT_TARGET_EDIT;
		public virtual bool ALLOW_NEXT_TARGET_EDIT
		{
			get
			{
				return this._aLLOW_NEXT_TARGET_EDIT;
			}
			set
			{
				this._aLLOW_NEXT_TARGET_EDIT = value;
			}
		}
		
		private bool _iS_USER_DEFINE_TARGET;
		public virtual bool IS_USER_DEFINE_TARGET
		{
			get
			{
				return this._iS_USER_DEFINE_TARGET;
			}
			set
			{
				this._iS_USER_DEFINE_TARGET = value;
			}
		}
		
		private bool _nEEDPASS;
		public virtual bool NEEDPASS
		{
			get
			{
				return this._nEEDPASS;
			}
			set
			{
				this._nEEDPASS = value;
			}
		}
		
		private bool _aLLOW_RETURN;
		public virtual bool ALLOW_RETURN
		{
			get
			{
				return this._aLLOW_RETURN;
			}
			set
			{
				this._aLLOW_RETURN = value;
			}
		}
		
		private bool _aLLOW_TAKEBACK;
		public virtual bool ALLOW_TAKEBACK
		{
			get
			{
				return this._aLLOW_TAKEBACK;
			}
			set
			{
				this._aLLOW_TAKEBACK = value;
			}
		}
		
		private bool _aLLOW_VIEW_EVALTARGETDATA;
		public virtual bool ALLOW_VIEW_EVALTARGETDATA
		{
			get
			{
				return this._aLLOW_VIEW_EVALTARGETDATA;
			}
			set
			{
				this._aLLOW_VIEW_EVALTARGETDATA = value;
			}
		}
		
		private IList<FORM_TEMPLATE_SUBMIT_SQL> _fORM_TEMPLATE_SUBMIT_SQLs = new List<FORM_TEMPLATE_SUBMIT_SQL>();
		public virtual IList<FORM_TEMPLATE_SUBMIT_SQL> FORM_TEMPLATE_SUBMIT_SQLs
		{
			get
			{
				return this._fORM_TEMPLATE_SUBMIT_SQLs;
			}
		}
		
		private IList<FORM_TEMPLATE_PERMISSION> _fORM_TEMPLATE_PERMISSIONs = new List<FORM_TEMPLATE_PERMISSION>();
		public virtual IList<FORM_TEMPLATE_PERMISSION> FORM_TEMPLATE_PERMISSIONs
		{
			get
			{
				return this._fORM_TEMPLATE_PERMISSIONs;
			}
		}
		
		private IList<FORM_INSTANCE> _fORM_INSTANCEs = new List<FORM_INSTANCE>();
		public virtual IList<FORM_INSTANCE> FORM_INSTANCEs
		{
			get
			{
				return this._fORM_INSTANCEs;
			}
		}
		
	}
}
#pragma warning restore 1591
