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
	public partial class ACTTYPETREE_TEMP
	{
		private int? _lEVEL;
		public virtual int? LEVEL
		{
			get
			{
				return this._lEVEL;
			}
			set
			{
				this._lEVEL = value;
			}
		}
		
		private string _cLS_NAME;
		public virtual string CLS_NAME
		{
			get
			{
				return this._cLS_NAME;
			}
			set
			{
				this._cLS_NAME = value;
			}
		}
		
	}
}
#pragma warning restore 1591
