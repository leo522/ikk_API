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
	public partial class RECORDINSVIEWER
	{
		private string _iNSTANCEID;
		public virtual string INSTANCEID
		{
			get
			{
				return this._iNSTANCEID;
			}
			set
			{
				this._iNSTANCEID = value;
			}
		}
		
		private int _vIEWORDER;
		public virtual int VIEWORDER
		{
			get
			{
				return this._vIEWORDER;
			}
			set
			{
				this._vIEWORDER = value;
			}
		}
		
		private string _vIEWER;
		public virtual string VIEWER
		{
			get
			{
				return this._vIEWER;
			}
			set
			{
				this._vIEWER = value;
			}
		}
		
		private string _vIEWSTATUS;
		public virtual string VIEWSTATUS
		{
			get
			{
				return this._vIEWSTATUS;
			}
			set
			{
				this._vIEWSTATUS = value;
			}
		}
		
		private DateTime? _vIEWTIME;
		public virtual DateTime? VIEWTIME
		{
			get
			{
				return this._vIEWTIME;
			}
			set
			{
				this._vIEWTIME = value;
			}
		}
		
		private string _vIEWMEMO;
		public virtual string VIEWMEMO
		{
			get
			{
				return this._vIEWMEMO;
			}
			set
			{
				this._vIEWMEMO = value;
			}
		}
		
		private int _sN;
		public virtual int SN
		{
			get
			{
				return this._sN;
			}
			set
			{
				this._sN = value;
			}
		}
		
		private RECORDINSTANCE _rECORDINSTANCE;
		public virtual RECORDINSTANCE RECORDINSTANCE
		{
			get
			{
				return this._rECORDINSTANCE;
			}
			set
			{
				this._rECORDINSTANCE = value;
			}
		}
		
	}
}
#pragma warning restore 1591