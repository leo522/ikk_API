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
	public partial class SCORESETTINGSCORETYPE
	{
		private int _tYPEID;
		public virtual int TYPEID
		{
			get
			{
				return this._tYPEID;
			}
			set
			{
				this._tYPEID = value;
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
		
		private string _tYPENAME;
		public virtual string TYPENAME
		{
			get
			{
				return this._tYPENAME;
			}
			set
			{
				this._tYPENAME = value;
			}
		}
		
		private decimal? _tYPEPERCENT;
		public virtual decimal? TYPEPERCENT
		{
			get
			{
				return this._tYPEPERCENT;
			}
			set
			{
				this._tYPEPERCENT = value;
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
