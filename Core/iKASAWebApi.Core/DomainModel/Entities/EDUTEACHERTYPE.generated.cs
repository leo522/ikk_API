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
	public partial class EDUTEACHERTYPE
	{
		private string _cODE;
		public virtual string CODE
		{
			get
			{
				return this._cODE;
			}
			set
			{
				this._cODE = value;
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
		
		private int? _dISPLAY_ORDER;
		public virtual int? DISPLAY_ORDER
		{
			get
			{
				return this._dISPLAY_ORDER;
			}
			set
			{
				this._dISPLAY_ORDER = value;
			}
		}
		
		private string _rEFTYPE;
		public virtual string REFTYPE
		{
			get
			{
				return this._rEFTYPE;
			}
			set
			{
				this._rEFTYPE = value;
			}
		}
		
	}
}
#pragma warning restore 1591