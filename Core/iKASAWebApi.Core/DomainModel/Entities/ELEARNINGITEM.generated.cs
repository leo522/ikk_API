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
	public partial class ELEARNINGITEM
	{
		private string _iTEMID;
		public virtual string ITEMID
		{
			get
			{
				return this._iTEMID;
			}
			set
			{
				this._iTEMID = value;
			}
		}
		
		private string _cLASSID;
		public virtual string CLASSID
		{
			get
			{
				return this._cLASSID;
			}
			set
			{
				this._cLASSID = value;
			}
		}
		
		private string _iTEMNAME;
		public virtual string ITEMNAME
		{
			get
			{
				return this._iTEMNAME;
			}
			set
			{
				this._iTEMNAME = value;
			}
		}
		
		private int? _iTEMORDER;
		public virtual int? ITEMORDER
		{
			get
			{
				return this._iTEMORDER;
			}
			set
			{
				this._iTEMORDER = value;
			}
		}
		
		private string _eXITEMID;
		public virtual string EXITEMID
		{
			get
			{
				return this._eXITEMID;
			}
			set
			{
				this._eXITEMID = value;
			}
		}
		
		private string _iTEMTYPE;
		public virtual string ITEMTYPE
		{
			get
			{
				return this._iTEMTYPE;
			}
			set
			{
				this._iTEMTYPE = value;
			}
		}
		
		private string _rEFID;
		public virtual string REFID
		{
			get
			{
				return this._rEFID;
			}
			set
			{
				this._rEFID = value;
			}
		}
		
		private string _rEFURL;
		public virtual string REFURL
		{
			get
			{
				return this._rEFURL;
			}
			set
			{
				this._rEFURL = value;
			}
		}
		
		private int? _mAXEXAMTIMES;
		public virtual int? MAXEXAMTIMES
		{
			get
			{
				return this._mAXEXAMTIMES;
			}
			set
			{
				this._mAXEXAMTIMES = value;
			}
		}
		
		private ELEARNINGCLASS _eLEARNINGCLASS;
		public virtual ELEARNINGCLASS ELEARNINGCLASS
		{
			get
			{
				return this._eLEARNINGCLASS;
			}
			set
			{
				this._eLEARNINGCLASS = value;
			}
		}
		
		private IList<ELEARNINGEMPITEM> _eLEARNINGEMPITEMs = new List<ELEARNINGEMPITEM>();
		public virtual IList<ELEARNINGEMPITEM> ELEARNINGEMPITEMs
		{
			get
			{
				return this._eLEARNINGEMPITEMs;
			}
		}
		
	}
}
#pragma warning restore 1591
