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
	public partial class CICBOOK
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
		
		private string _eID;
		public virtual string EID
		{
			get
			{
				return this._eID;
			}
			set
			{
				this._eID = value;
			}
		}
		
		private string _eMPNAME;
		public virtual string EMPNAME
		{
			get
			{
				return this._eMPNAME;
			}
			set
			{
				this._eMPNAME = value;
			}
		}
		
		private string _tEACHERLIST;
		public virtual string TEACHERLIST
		{
			get
			{
				return this._tEACHERLIST;
			}
			set
			{
				this._tEACHERLIST = value;
			}
		}
		
		private string _sERIALNO;
		public virtual string SERIALNO
		{
			get
			{
				return this._sERIALNO;
			}
			set
			{
				this._sERIALNO = value;
			}
		}
		
		private DateTime? _sTARTDATE;
		public virtual DateTime? STARTDATE
		{
			get
			{
				return this._sTARTDATE;
			}
			set
			{
				this._sTARTDATE = value;
			}
		}
		
		private DateTime? _eNDDATE;
		public virtual DateTime? ENDDATE
		{
			get
			{
				return this._eNDDATE;
			}
			set
			{
				this._eNDDATE = value;
			}
		}
		
		private string _dEPT;
		public virtual string DEPT
		{
			get
			{
				return this._dEPT;
			}
			set
			{
				this._dEPT = value;
			}
		}
		
	}
}
#pragma warning restore 1591
