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
	public partial class EDUPASSPORTINSITEMDET
	{
		private int _iITDETID;
		public virtual int IITDETID
		{
			get
			{
				return this._iITDETID;
			}
			set
			{
				this._iITDETID = value;
			}
		}
		
		private string _iITEMID;
		public virtual string IITEMID
		{
			get
			{
				return this._iITEMID;
			}
			set
			{
				this._iITEMID = value;
			}
		}
		
		private int? _dETID;
		public virtual int? DETID
		{
			get
			{
				return this._dETID;
			}
			set
			{
				this._dETID = value;
			}
		}
		
		private string _fIELDDESC;
		public virtual string FIELDDESC
		{
			get
			{
				return this._fIELDDESC;
			}
			set
			{
				this._fIELDDESC = value;
			}
		}
		
		private string _fIELDTARGET;
		public virtual string FIELDTARGET
		{
			get
			{
				return this._fIELDTARGET;
			}
			set
			{
				this._fIELDTARGET = value;
			}
		}
		
		private int? _sEQ;
		public virtual int? SEQ
		{
			get
			{
				return this._sEQ;
			}
			set
			{
				this._sEQ = value;
			}
		}
		
		private string _fIELDTYPE;
		public virtual string FIELDTYPE
		{
			get
			{
				return this._fIELDTYPE;
			}
			set
			{
				this._fIELDTYPE = value;
			}
		}
		
		private bool? _iSNECESSARY;
		public virtual bool? ISNECESSARY
		{
			get
			{
				return this._iSNECESSARY;
			}
			set
			{
				this._iSNECESSARY = value;
			}
		}
		
		private string _fIELDVALUE;
		public virtual string FIELDVALUE
		{
			get
			{
				return this._fIELDVALUE;
			}
			set
			{
				this._fIELDVALUE = value;
			}
		}
		
		private string _sELECTOPTIONS;
		public virtual string SELECTOPTIONS
		{
			get
			{
				return this._sELECTOPTIONS;
			}
			set
			{
				this._sELECTOPTIONS = value;
			}
		}
		
		private EDUPASSPORTINSITEM _eDUPASSPORTINSITEM;
		public virtual EDUPASSPORTINSITEM EDUPASSPORTINSITEM
		{
			get
			{
				return this._eDUPASSPORTINSITEM;
			}
			set
			{
				this._eDUPASSPORTINSITEM = value;
			}
		}
		
	}
}
#pragma warning restore 1591