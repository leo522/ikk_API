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
	public partial class FORMCATEGORY
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
		
		private string _cATEGORYNAME;
		public virtual string CATEGORYNAME
		{
			get
			{
				return this._cATEGORYNAME;
			}
			set
			{
				this._cATEGORYNAME = value;
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
		
		private DateTime? _cREATEDATE;
		public virtual DateTime? CREATEDATE
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
		
	}
}
#pragma warning restore 1591
