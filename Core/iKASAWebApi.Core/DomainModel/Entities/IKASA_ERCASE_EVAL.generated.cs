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
	public partial class IKASA_ERCASE_EVAL
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
		
		private string _eVALTYPE;
		public virtual string EVALTYPE
		{
			get
			{
				return this._eVALTYPE;
			}
			set
			{
				this._eVALTYPE = value;
			}
		}
		
		private DateTime? _cREATEDATE;
		public virtual DateTime? CREATEDATE
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
		
		private string _tEACHERID;
		public virtual string TEACHERID
		{
			get
			{
				return this._tEACHERID;
			}
			set
			{
				this._tEACHERID = value;
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
		
		private DateTime? _sUBMITTIME;
		public virtual DateTime? SUBMITTIME
		{
			get
			{
				return this._sUBMITTIME;
			}
			set
			{
				this._sUBMITTIME = value;
			}
		}
		
		private int _cASEID;
		public virtual int CASEID
		{
			get
			{
				return this._cASEID;
			}
			set
			{
				this._cASEID = value;
			}
		}
		
		private IList<IKASA_ERCASE_EVALDET> _iKASA_ERCASE_EVALDETs = new List<IKASA_ERCASE_EVALDET>();
		public virtual IList<IKASA_ERCASE_EVALDET> IKASA_ERCASE_EVALDETs
		{
			get
			{
				return this._iKASA_ERCASE_EVALDETs;
			}
		}
		
	}
}
#pragma warning restore 1591
