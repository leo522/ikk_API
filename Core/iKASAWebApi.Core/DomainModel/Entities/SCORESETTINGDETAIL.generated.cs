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
	public partial class SCORESETTINGDETAIL
	{
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
		
		private string _vALUETYPE;
		public virtual string VALUETYPE
		{
			get
			{
				return this._vALUETYPE;
			}
			set
			{
				this._vALUETYPE = value;
			}
		}
		
		private string _vALUEID;
		public virtual string VALUEID
		{
			get
			{
				return this._vALUEID;
			}
			set
			{
				this._vALUEID = value;
			}
		}
		
		private string _vALUEPARAMETER;
		public virtual string VALUEPARAMETER
		{
			get
			{
				return this._vALUEPARAMETER;
			}
			set
			{
				this._vALUEPARAMETER = value;
			}
		}
		
		private decimal? _sCOREPERCENT;
		public virtual decimal? SCOREPERCENT
		{
			get
			{
				return this._sCOREPERCENT;
			}
			set
			{
				this._sCOREPERCENT = value;
			}
		}
		
		private string _vALUENAME;
		public virtual string VALUENAME
		{
			get
			{
				return this._vALUENAME;
			}
			set
			{
				this._vALUENAME = value;
			}
		}
		
		private int? _dISPLAYORDER;
		public virtual int? DISPLAYORDER
		{
			get
			{
				return this._dISPLAYORDER;
			}
			set
			{
				this._dISPLAYORDER = value;
			}
		}
		
		private string _sCORETYPE;
		public virtual string SCORETYPE
		{
			get
			{
				return this._sCORETYPE;
			}
			set
			{
				this._sCORETYPE = value;
			}
		}
		
		private SCORESETTING _sCORESETTING;
		public virtual SCORESETTING SCORESETTING
		{
			get
			{
				return this._sCORESETTING;
			}
			set
			{
				this._sCORESETTING = value;
			}
		}
		
	}
}
#pragma warning restore 1591