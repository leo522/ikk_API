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
	public partial class EDUACTTYPEROLEREF
	{
		private int _aCTTYPE;
		public virtual int ACTTYPE
		{
			get
			{
				return this._aCTTYPE;
			}
			set
			{
				this._aCTTYPE = value;
			}
		}
		
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
		
		private DateTime? _eNABLEDATE;
		public virtual DateTime? ENABLEDATE
		{
			get
			{
				return this._eNABLEDATE;
			}
			set
			{
				this._eNABLEDATE = value;
			}
		}
		
		private DateTime? _dISABLEDATE;
		public virtual DateTime? DISABLEDATE
		{
			get
			{
				return this._dISABLEDATE;
			}
			set
			{
				this._dISABLEDATE = value;
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
		
	}
}
#pragma warning restore 1591