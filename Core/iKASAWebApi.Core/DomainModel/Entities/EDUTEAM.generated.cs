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
	public partial class EDUTEAM
	{
		private string _eDUTEAMCODE;
		public virtual string EDUTEAMCODE
		{
			get
			{
				return this._eDUTEAMCODE;
			}
			set
			{
				this._eDUTEAMCODE = value;
			}
		}
		
		private string _tEAMMEMBERTYPE;
		public virtual string TEAMMEMBERTYPE
		{
			get
			{
				return this._tEAMMEMBERTYPE;
			}
			set
			{
				this._tEAMMEMBERTYPE = value;
			}
		}
		
		private Char _sTATUS;
		public virtual Char STATUS
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
		
		private string _eDUTEAMNAME;
		public virtual string EDUTEAMNAME
		{
			get
			{
				return this._eDUTEAMNAME;
			}
			set
			{
				this._eDUTEAMNAME = value;
			}
		}
		
		private string _pARENTEDUTEAMCODE;
		public virtual string PARENTEDUTEAMCODE
		{
			get
			{
				return this._pARENTEDUTEAMCODE;
			}
			set
			{
				this._pARENTEDUTEAMCODE = value;
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
		
		private string _jOBCODE;
		public virtual string JOBCODE
		{
			get
			{
				return this._jOBCODE;
			}
			set
			{
				this._jOBCODE = value;
			}
		}
		
		private EDUTEAM _eDUTEAM1;
		public virtual EDUTEAM EDUTEAM1
		{
			get
			{
				return this._eDUTEAM1;
			}
			set
			{
				this._eDUTEAM1 = value;
			}
		}
		
		private IList<EDUTEAMRUNDOWN> _eDUTEAMRUNDOWNs = new List<EDUTEAMRUNDOWN>();
		public virtual IList<EDUTEAMRUNDOWN> EDUTEAMRUNDOWNs
		{
			get
			{
				return this._eDUTEAMRUNDOWNs;
			}
		}
		
		private IList<EDUTEAMMEMBER> _eDUTEAMMEMBERs = new List<EDUTEAMMEMBER>();
		public virtual IList<EDUTEAMMEMBER> EDUTEAMMEMBERs
		{
			get
			{
				return this._eDUTEAMMEMBERs;
			}
		}
		
		private IList<EDUTEAM> _eDUTEAMs = new List<EDUTEAM>();
		public virtual IList<EDUTEAM> EDUTEAMs
		{
			get
			{
				return this._eDUTEAMs;
			}
		}
		
	}
}
#pragma warning restore 1591
