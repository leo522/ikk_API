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
	public partial class IKASA_MENUREF
	{
		private string _mENUID;
		public virtual string MENUID
		{
			get
			{
				return this._mENUID;
			}
			set
			{
				this._mENUID = value;
			}
		}
		
		private string _tOPTEAMCODE;
		public virtual string TOPTEAMCODE
		{
			get
			{
				return this._tOPTEAMCODE;
			}
			set
			{
				this._tOPTEAMCODE = value;
			}
		}
		
		private string _dEPT_CODE;
		public virtual string DEPT_CODE
		{
			get
			{
				return this._dEPT_CODE;
			}
			set
			{
				this._dEPT_CODE = value;
			}
		}
		
		private int _rEFID;
		public virtual int REFID
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
		
		private string _mEMBERTYPE;
		public virtual string MEMBERTYPE
		{
			get
			{
				return this._mEMBERTYPE;
			}
			set
			{
				this._mEMBERTYPE = value;
			}
		}
		
	}
}
#pragma warning restore 1591
