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
	public partial class EDUTERMFORMREQTEMPLATEDET
	{
		private int _dETID;
		public virtual int DETID
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
		
		private int _tEMPLATEID;
		public virtual int TEMPLATEID
		{
			get
			{
				return this._tEMPLATEID;
			}
			set
			{
				this._tEMPLATEID = value;
			}
		}
		
		private string _rEQTYPE;
		public virtual string REQTYPE
		{
			get
			{
				return this._rEQTYPE;
			}
			set
			{
				this._rEQTYPE = value;
			}
		}
		
		private int _rEQID;
		public virtual int REQID
		{
			get
			{
				return this._rEQID;
			}
			set
			{
				this._rEQID = value;
			}
		}
		
		private int _rEQCOUNT;
		public virtual int REQCOUNT
		{
			get
			{
				return this._rEQCOUNT;
			}
			set
			{
				this._rEQCOUNT = value;
			}
		}
		
		private bool _nEEDPASS;
		public virtual bool NEEDPASS
		{
			get
			{
				return this._nEEDPASS;
			}
			set
			{
				this._nEEDPASS = value;
			}
		}
		
		private EDUTERMFORMREQTEMPLATE _eDUTERMFORMREQTEMPLATE;
		public virtual EDUTERMFORMREQTEMPLATE EDUTERMFORMREQTEMPLATE
		{
			get
			{
				return this._eDUTERMFORMREQTEMPLATE;
			}
			set
			{
				this._eDUTERMFORMREQTEMPLATE = value;
			}
		}
		
	}
}
#pragma warning restore 1591