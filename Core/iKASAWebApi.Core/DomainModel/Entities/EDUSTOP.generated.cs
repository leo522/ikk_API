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
	public partial class EDUSTOP
	{
		private string _eDUSTOPID;
		public virtual string EDUSTOPID
		{
			get
			{
				return this._eDUSTOPID;
			}
			set
			{
				this._eDUSTOPID = value;
			}
		}
		
		private string _eDUSTOPCODE;
		public virtual string EDUSTOPCODE
		{
			get
			{
				return this._eDUSTOPCODE;
			}
			set
			{
				this._eDUSTOPCODE = value;
			}
		}
		
		private string _nAME;
		public virtual string NAME
		{
			get
			{
				return this._nAME;
			}
			set
			{
				this._nAME = value;
			}
		}
		
		private string _dEPID;
		public virtual string DEPID
		{
			get
			{
				return this._dEPID;
			}
			set
			{
				this._dEPID = value;
			}
		}
		
		private string _nSTATION;
		public virtual string NSTATION
		{
			get
			{
				return this._nSTATION;
			}
			set
			{
				this._nSTATION = value;
			}
		}
		
		private string _cORCHID;
		public virtual string CORCHID
		{
			get
			{
				return this._cORCHID;
			}
			set
			{
				this._cORCHID = value;
			}
		}
		
		private System.Nullable<System.Char> _sTATUS;
		public virtual System.Nullable<System.Char> STATUS
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
		
		private string _dES;
		public virtual string DES
		{
			get
			{
				return this._dES;
			}
			set
			{
				this._dES = value;
			}
		}
		
		private string _yEARCODE;
		public virtual string YEARCODE
		{
			get
			{
				return this._yEARCODE;
			}
			set
			{
				this._yEARCODE = value;
			}
		}
		
		private string _pARENTEDUSTOPID;
		public virtual string PARENTEDUSTOPID
		{
			get
			{
				return this._pARENTEDUSTOPID;
			}
			set
			{
				this._pARENTEDUSTOPID = value;
			}
		}
		
	}
}
#pragma warning restore 1591
