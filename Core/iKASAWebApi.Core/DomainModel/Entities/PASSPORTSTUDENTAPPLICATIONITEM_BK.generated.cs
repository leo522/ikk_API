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
	public partial class PASSPORTSTUDENTAPPLICATIONITEM_BK
	{
		private int _aPPLICATIONID;
		public virtual int APPLICATIONID
		{
			get
			{
				return this._aPPLICATIONID;
			}
			set
			{
				this._aPPLICATIONID = value;
			}
		}
		
		private int? _iTEMCODE;
		public virtual int? ITEMCODE
		{
			get
			{
				return this._iTEMCODE;
			}
			set
			{
				this._iTEMCODE = value;
			}
		}
		
		private string _aPPLICATIONMEMBERNUMBER;
		public virtual string APPLICATIONMEMBERNUMBER
		{
			get
			{
				return this._aPPLICATIONMEMBERNUMBER;
			}
			set
			{
				this._aPPLICATIONMEMBERNUMBER = value;
			}
		}
		
		private int? _cHECKORDER;
		public virtual int? CHECKORDER
		{
			get
			{
				return this._cHECKORDER;
			}
			set
			{
				this._cHECKORDER = value;
			}
		}
		
		private string _dESIGNATIONTEACHERNUMBER;
		public virtual string DESIGNATIONTEACHERNUMBER
		{
			get
			{
				return this._dESIGNATIONTEACHERNUMBER;
			}
			set
			{
				this._dESIGNATIONTEACHERNUMBER = value;
			}
		}
		
		private string _iMPLEMENTPLACE;
		public virtual string IMPLEMENTPLACE
		{
			get
			{
				return this._iMPLEMENTPLACE;
			}
			set
			{
				this._iMPLEMENTPLACE = value;
			}
		}
		
		private DateTime? _iMPLEMENTDATE;
		public virtual DateTime? IMPLEMENTDATE
		{
			get
			{
				return this._iMPLEMENTDATE;
			}
			set
			{
				this._iMPLEMENTDATE = value;
			}
		}
		
		private DateTime? _aPPLICATIONDATE;
		public virtual DateTime? APPLICATIONDATE
		{
			get
			{
				return this._aPPLICATIONDATE;
			}
			set
			{
				this._aPPLICATIONDATE = value;
			}
		}
		
		private bool? _iSFINALCHECK;
		public virtual bool? ISFINALCHECK
		{
			get
			{
				return this._iSFINALCHECK;
			}
			set
			{
				this._iSFINALCHECK = value;
			}
		}
		
		private bool? _cHECKRESULT;
		public virtual bool? CHECKRESULT
		{
			get
			{
				return this._cHECKRESULT;
			}
			set
			{
				this._cHECKRESULT = value;
			}
		}
		
		private string _cHECKSTATUS;
		public virtual string CHECKSTATUS
		{
			get
			{
				return this._cHECKSTATUS;
			}
			set
			{
				this._cHECKSTATUS = value;
			}
		}
		
		private DateTime? _cHECKDATE;
		public virtual DateTime? CHECKDATE
		{
			get
			{
				return this._cHECKDATE;
			}
			set
			{
				this._cHECKDATE = value;
			}
		}
		
		private string _sTUDENTREMARK;
		public virtual string STUDENTREMARK
		{
			get
			{
				return this._sTUDENTREMARK;
			}
			set
			{
				this._sTUDENTREMARK = value;
			}
		}
		
		private string _iMPLEMENTOBJECT;
		public virtual string IMPLEMENTOBJECT
		{
			get
			{
				return this._iMPLEMENTOBJECT;
			}
			set
			{
				this._iMPLEMENTOBJECT = value;
			}
		}
		
		private string _cASEHISTORYNUMBER;
		public virtual string CASEHISTORYNUMBER
		{
			get
			{
				return this._cASEHISTORYNUMBER;
			}
			set
			{
				this._cASEHISTORYNUMBER = value;
			}
		}
		
		private string _tEACHERREMARK;
		public virtual string TEACHERREMARK
		{
			get
			{
				return this._tEACHERREMARK;
			}
			set
			{
				this._tEACHERREMARK = value;
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
		
		private PASSPORTCHECKITEM _pASSPORTCHECKITEM;
		public virtual PASSPORTCHECKITEM PASSPORTCHECKITEM
		{
			get
			{
				return this._pASSPORTCHECKITEM;
			}
			set
			{
				this._pASSPORTCHECKITEM = value;
			}
		}
		
	}
}
#pragma warning restore 1591
