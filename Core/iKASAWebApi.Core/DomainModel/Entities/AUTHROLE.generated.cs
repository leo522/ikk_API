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
	public partial class AUTHROLE
	{
		private string _rOLEID;
		public virtual string ROLEID
		{
			get
			{
				return this._rOLEID;
			}
			set
			{
				this._rOLEID = value;
			}
		}
		
		private string _rOLENAME;
		public virtual string ROLENAME
		{
			get
			{
				return this._rOLENAME;
			}
			set
			{
				this._rOLENAME = value;
			}
		}
		
		private bool _aLLOWSETTING;
		public virtual bool ALLOWSETTING
		{
			get
			{
				return this._aLLOWSETTING;
			}
			set
			{
				this._aLLOWSETTING = value;
			}
		}
		
	}
}
#pragma warning restore 1591