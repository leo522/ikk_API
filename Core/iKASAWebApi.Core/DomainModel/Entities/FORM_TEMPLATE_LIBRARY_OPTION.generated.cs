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
	public partial class FORM_TEMPLATE_LIBRARY_OPTION
	{
		private int _oPTION_ID;
		public virtual int OPTION_ID
		{
			get
			{
				return this._oPTION_ID;
			}
			set
			{
				this._oPTION_ID = value;
			}
		}
		
		private int? _lIBRARY_ID;
		public virtual int? LIBRARY_ID
		{
			get
			{
				return this._lIBRARY_ID;
			}
			set
			{
				this._lIBRARY_ID = value;
			}
		}
		
		private string _oPTION_TYPE;
		public virtual string OPTION_TYPE
		{
			get
			{
				return this._oPTION_TYPE;
			}
			set
			{
				this._oPTION_TYPE = value;
			}
		}
		
		private string _oPTION_TEXT;
		public virtual string OPTION_TEXT
		{
			get
			{
				return this._oPTION_TEXT;
			}
			set
			{
				this._oPTION_TEXT = value;
			}
		}
		
		private double? _oPTION_POINTS;
		public virtual double? OPTION_POINTS
		{
			get
			{
				return this._oPTION_POINTS;
			}
			set
			{
				this._oPTION_POINTS = value;
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
		
		private System.Nullable<System.Char> _iS_ANSWER;
		public virtual System.Nullable<System.Char> IS_ANSWER
		{
			get
			{
				return this._iS_ANSWER;
			}
			set
			{
				this._iS_ANSWER = value;
			}
		}
		
	}
}
#pragma warning restore 1591
