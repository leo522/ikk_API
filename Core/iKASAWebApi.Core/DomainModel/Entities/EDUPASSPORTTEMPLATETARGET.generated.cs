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
	public partial class EDUPASSPORTTEMPLATETARGET
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
		
		private string _tEMPLATEID;
		public virtual string TEMPLATEID
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
		
		private string _tARGETTYPE;
		public virtual string TARGETTYPE
		{
			get
			{
				return this._tARGETTYPE;
			}
			set
			{
				this._tARGETTYPE = value;
			}
		}
		
		private string _tARGETID;
		public virtual string TARGETID
		{
			get
			{
				return this._tARGETID;
			}
			set
			{
				this._tARGETID = value;
			}
		}
		
		private EDUPASSPORTTEMPLATE _eDUPASSPORTTEMPLATE;
		public virtual EDUPASSPORTTEMPLATE EDUPASSPORTTEMPLATE
		{
			get
			{
				return this._eDUPASSPORTTEMPLATE;
			}
			set
			{
				this._eDUPASSPORTTEMPLATE = value;
			}
		}
		
	}
}
#pragma warning restore 1591
