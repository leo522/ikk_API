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
	public partial class NNOTEITEM
	{
		private string _nNOTEITEMID;
		public virtual string NNOTEITEMID
		{
			get
			{
				return this._nNOTEITEMID;
			}
			set
			{
				this._nNOTEITEMID = value;
			}
		}
		
		private string _nNOTEID;
		public virtual string NNOTEID
		{
			get
			{
				return this._nNOTEID;
			}
			set
			{
				this._nNOTEID = value;
			}
		}
		
		private string _sTUDYKEYWORD;
		public virtual string STUDYKEYWORD
		{
			get
			{
				return this._sTUDYKEYWORD;
			}
			set
			{
				this._sTUDYKEYWORD = value;
			}
		}
		
		private string _nOTINGDESABBREV;
		public virtual string NOTINGDESABBREV
		{
			get
			{
				return this._nOTINGDESABBREV;
			}
			set
			{
				this._nOTINGDESABBREV = value;
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
		
	}
}
#pragma warning restore 1591