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
	public partial class TEACHTIMERATE
	{
		private int _sETTINGID;
		public virtual int SETTINGID
		{
			get
			{
				return this._sETTINGID;
			}
			set
			{
				this._sETTINGID = value;
			}
		}
		
		private string _sETTINGTYPE;
		public virtual string SETTINGTYPE
		{
			get
			{
				return this._sETTINGTYPE;
			}
			set
			{
				this._sETTINGTYPE = value;
			}
		}
		
		private string _sETTINGCODE;
		public virtual string SETTINGCODE
		{
			get
			{
				return this._sETTINGCODE;
			}
			set
			{
				this._sETTINGCODE = value;
			}
		}
		
		private string _cODETYPE;
		public virtual string CODETYPE
		{
			get
			{
				return this._cODETYPE;
			}
			set
			{
				this._cODETYPE = value;
			}
		}
		
		private int? _rATE1;
		public virtual int? RATE1
		{
			get
			{
				return this._rATE1;
			}
			set
			{
				this._rATE1 = value;
			}
		}
		
		private int? _rATE2;
		public virtual int? RATE2
		{
			get
			{
				return this._rATE2;
			}
			set
			{
				this._rATE2 = value;
			}
		}
		
		private string _uNIT;
		public virtual string UNIT
		{
			get
			{
				return this._uNIT;
			}
			set
			{
				this._uNIT = value;
			}
		}
		
		private int? _rEFID;
		public virtual int? REFID
		{
			get
			{
				return this._rEFID;
			}
			set
			{
				this._rEFID = value;
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
		
	}
}
#pragma warning restore 1591
