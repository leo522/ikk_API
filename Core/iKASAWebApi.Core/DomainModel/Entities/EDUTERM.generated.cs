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
	public partial class EDUTERM
	{
		private string _eDUTERMID;
		public virtual string EDUTERMID
		{
			get
			{
				return this._eDUTERMID;
			}
			set
			{
				this._eDUTERMID = value;
			}
		}
		
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
		
		private string _dEPCODE;
		public virtual string DEPCODE
		{
			get
			{
				return this._dEPCODE;
			}
			set
			{
				this._dEPCODE = value;
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
		
		private int? _cAPACITY;
		public virtual int? CAPACITY
		{
			get
			{
				return this._cAPACITY;
			}
			set
			{
				this._cAPACITY = value;
			}
		}
		
		private DateTime? _dATEFROM;
		public virtual DateTime? DATEFROM
		{
			get
			{
				return this._dATEFROM;
			}
			set
			{
				this._dATEFROM = value;
			}
		}
		
		private DateTime? _dATETO;
		public virtual DateTime? DATETO
		{
			get
			{
				return this._dATETO;
			}
			set
			{
				this._dATETO = value;
			}
		}
		
		private System.Nullable<System.Char> _iSCLASS;
		public virtual System.Nullable<System.Char> ISCLASS
		{
			get
			{
				return this._iSCLASS;
			}
			set
			{
				this._iSCLASS = value;
			}
		}
		
		private string _rOUNDCODE;
		public virtual string ROUNDCODE
		{
			get
			{
				return this._rOUNDCODE;
			}
			set
			{
				this._rOUNDCODE = value;
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
		
		private string _tEACHER;
		public virtual string TEACHER
		{
			get
			{
				return this._tEACHER;
			}
			set
			{
				this._tEACHER = value;
			}
		}
		
		private string _eBM;
		public virtual string EBM
		{
			get
			{
				return this._eBM;
			}
			set
			{
				this._eBM = value;
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
		
		private string _pARENTEDUTERMID;
		public virtual string PARENTEDUTERMID
		{
			get
			{
				return this._pARENTEDUTERMID;
			}
			set
			{
				this._pARENTEDUTERMID = value;
			}
		}
		
		private string _eDUYEAR;
		public virtual string EDUYEAR
		{
			get
			{
				return this._eDUYEAR;
			}
			set
			{
				this._eDUYEAR = value;
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
		
		private System.Nullable<System.Char> _hOSPITAL;
		public virtual System.Nullable<System.Char> HOSPITAL
		{
			get
			{
				return this._hOSPITAL;
			}
			set
			{
				this._hOSPITAL = value;
			}
		}
		
		private string _dEPARTMENT;
		public virtual string DEPARTMENT
		{
			get
			{
				return this._dEPARTMENT;
			}
			set
			{
				this._dEPARTMENT = value;
			}
		}
		
		private bool _hASMEMBER;
		public virtual bool HASMEMBER
		{
			get
			{
				return this._hASMEMBER;
			}
			set
			{
				this._hASMEMBER = value;
			}
		}
		
		private bool _hASTEACHER;
		public virtual bool HASTEACHER
		{
			get
			{
				return this._hASTEACHER;
			}
			set
			{
				this._hASTEACHER = value;
			}
		}
		
		private string _sCHOOLSEQNO1;
		public virtual string SCHOOLSEQNO1
		{
			get
			{
				return this._sCHOOLSEQNO1;
			}
			set
			{
				this._sCHOOLSEQNO1 = value;
			}
		}
		
		private string _sCHOOLSEQNO2;
		public virtual string SCHOOLSEQNO2
		{
			get
			{
				return this._sCHOOLSEQNO2;
			}
			set
			{
				this._sCHOOLSEQNO2 = value;
			}
		}
		
		private EDUTERM _eDUTERM1;
		public virtual EDUTERM EDUTERM1
		{
			get
			{
				return this._eDUTERM1;
			}
			set
			{
				this._eDUTERM1 = value;
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
		
		private IList<EDUTERM> _eDUTERMs = new List<EDUTERM>();
		public virtual IList<EDUTERM> EDUTERMs
		{
			get
			{
				return this._eDUTERMs;
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
		
	}
}
#pragma warning restore 1591
