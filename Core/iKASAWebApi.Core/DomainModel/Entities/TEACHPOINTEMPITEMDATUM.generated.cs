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
	public partial class TEACHPOINTEMPITEMDATUM
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
		
		private int _iTEMID;
		public virtual int ITEMID
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
		
		private string _eMPCODE;
		public virtual string EMPCODE
		{
			get
			{
				return this._eMPCODE;
			}
			set
			{
				this._eMPCODE = value;
			}
		}
		
		private decimal _iTEMCOUNT;
		public virtual decimal ITEMCOUNT
		{
			get
			{
				return this._iTEMCOUNT;
			}
			set
			{
				this._iTEMCOUNT = value;
			}
		}
		
		private DateTime _oCCURDATE;
		public virtual DateTime OCCURDATE
		{
			get
			{
				return this._oCCURDATE;
			}
			set
			{
				this._oCCURDATE = value;
			}
		}
		
		private string _cREATER;
		public virtual string CREATER
		{
			get
			{
				return this._cREATER;
			}
			set
			{
				this._cREATER = value;
			}
		}
		
		private DateTime _cREATEDATE;
		public virtual DateTime CREATEDATE
		{
			get
			{
				return this._cREATEDATE;
			}
			set
			{
				this._cREATEDATE = value;
			}
		}
		
		private string _sTATUS;
		public virtual string STATUS
		{
			get
			{
				return this._sTATUS;
			}
			set
			{
				this._sTATUS = value;
			}
		}
		
		private string _rEMARK;
		public virtual string REMARK
		{
			get
			{
				return this._rEMARK;
			}
			set
			{
				this._rEMARK = value;
			}
		}
		
		private TEACHPOINTITEM _tEACHPOINTITEM;
		public virtual TEACHPOINTITEM TEACHPOINTITEM
		{
			get
			{
				return this._tEACHPOINTITEM;
			}
			set
			{
				this._tEACHPOINTITEM = value;
			}
		}
		
	}
}
#pragma warning restore 1591
