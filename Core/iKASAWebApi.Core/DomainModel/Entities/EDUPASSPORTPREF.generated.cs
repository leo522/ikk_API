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
	public partial class EDUPASSPORTPREF
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
		
		private string _iTEMID;
		public virtual string ITEMID
		{
			get
			{
				return this._iTEMID;
			}
			set
			{
				this._iTEMID = value;
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
		
		private EDUPASSPORTITEM _eDUPASSPORTITEM;
		public virtual EDUPASSPORTITEM EDUPASSPORTITEM
		{
			get
			{
				return this._eDUPASSPORTITEM;
			}
			set
			{
				this._eDUPASSPORTITEM = value;
			}
		}
		
	}
}
#pragma warning restore 1591