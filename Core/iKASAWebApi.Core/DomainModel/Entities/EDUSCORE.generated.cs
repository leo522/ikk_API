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
	public partial class EDUSCORE
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
		
		private string _mEMBERCODE;
		public virtual string MEMBERCODE
		{
			get
			{
				return this._mEMBERCODE;
			}
			set
			{
				this._mEMBERCODE = value;
			}
		}
		
		private string _dEPLEVEL;
		public virtual string DEPLEVEL
		{
			get
			{
				return this._dEPLEVEL;
			}
			set
			{
				this._dEPLEVEL = value;
			}
		}
		
		private string _cLASSNAME;
		public virtual string CLASSNAME
		{
			get
			{
				return this._cLASSNAME;
			}
			set
			{
				this._cLASSNAME = value;
			}
		}
		
		private decimal _sCORE;
		public virtual decimal SCORE
		{
			get
			{
				return this._sCORE;
			}
			set
			{
				this._sCORE = value;
			}
		}
		
		private string _uPLOADER;
		public virtual string UPLOADER
		{
			get
			{
				return this._uPLOADER;
			}
			set
			{
				this._uPLOADER = value;
			}
		}
		
		private DateTime _uPLOADTIME;
		public virtual DateTime UPLOADTIME
		{
			get
			{
				return this._uPLOADTIME;
			}
			set
			{
				this._uPLOADTIME = value;
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
		
	}
}
#pragma warning restore 1591
