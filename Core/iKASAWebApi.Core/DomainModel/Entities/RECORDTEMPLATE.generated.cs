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
	public partial class RECORDTEMPLATE
	{
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
		
		private string _tEMPLATENAME;
		public virtual string TEMPLATENAME
		{
			get
			{
				return this._tEMPLATENAME;
			}
			set
			{
				this._tEMPLATENAME = value;
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
		
		private bool _bINDACT;
		public virtual bool BINDACT
		{
			get
			{
				return this._bINDACT;
			}
			set
			{
				this._bINDACT = value;
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
		
		private string _oLDVERSION;
		public virtual string OLDVERSION
		{
			get
			{
				return this._oLDVERSION;
			}
			set
			{
				this._oLDVERSION = value;
			}
		}
		
		private IList<RECORDINSTANCE> _rECORDINSTANCEs = new List<RECORDINSTANCE>();
		public virtual IList<RECORDINSTANCE> RECORDINSTANCEs
		{
			get
			{
				return this._rECORDINSTANCEs;
			}
		}
		
	}
}
#pragma warning restore 1591
