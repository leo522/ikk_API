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
	public partial class EDUSTOPACTATTACHMENT
	{
		private string _aCTATTACHMENTID;
		public virtual string ACTATTACHMENTID
		{
			get
			{
				return this._aCTATTACHMENTID;
			}
			set
			{
				this._aCTATTACHMENTID = value;
			}
		}
		
		private string _eDUSTOPACTSCHEDULEID;
		public virtual string EDUSTOPACTSCHEDULEID
		{
			get
			{
				return this._eDUSTOPACTSCHEDULEID;
			}
			set
			{
				this._eDUSTOPACTSCHEDULEID = value;
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
		
		private byte[] _aTTACHMENT;
		public virtual byte[] ATTACHMENT
		{
			get
			{
				return this._aTTACHMENT;
			}
			set
			{
				this._aTTACHMENT = value;
			}
		}
		
		private bool _iSPUBLIC;
		public virtual bool ISPUBLIC
		{
			get
			{
				return this._iSPUBLIC;
			}
			set
			{
				this._iSPUBLIC = value;
			}
		}
		
		private DateTime _cREATEDATE;
		public virtual DateTime CREATEDATE
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
		
		private EDUSTOPACTSCHEDULE _eDUSTOPACTSCHEDULE;
		public virtual EDUSTOPACTSCHEDULE EDUSTOPACTSCHEDULE
		{
			get
			{
				return this._eDUSTOPACTSCHEDULE;
			}
			set
			{
				this._eDUSTOPACTSCHEDULE = value;
			}
		}
		
	}
}
#pragma warning restore 1591
