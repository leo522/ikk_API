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
	public partial class TEACHTIMEKPIREFTEMP
	{
		private string _kPICODE;
		public virtual string KPICODE
		{
			get
			{
				return this._kPICODE;
			}
			set
			{
				this._kPICODE = value;
			}
		}
		
		private int _tEACHTIMESETTINGID;
		public virtual int TEACHTIMESETTINGID
		{
			get
			{
				return this._tEACHTIMESETTINGID;
			}
			set
			{
				this._tEACHTIMESETTINGID = value;
			}
		}
		
		private decimal _tRANSRATE;
		public virtual decimal TRANSRATE
		{
			get
			{
				return this._tRANSRATE;
			}
			set
			{
				this._tRANSRATE = value;
			}
		}
		
		private DateTime _eNABLEDATE;
		public virtual DateTime ENABLEDATE
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
		
	}
}
#pragma warning restore 1591
