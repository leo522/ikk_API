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
	public partial class EDUMENU
	{
		private int _mENUID;
		public virtual int MENUID
		{
			get
			{
				return this._mENUID;
			}
			set
			{
				this._mENUID = value;
			}
		}
		
		private string _mENUTEXT;
		public virtual string MENUTEXT
		{
			get
			{
				return this._mENUTEXT;
			}
			set
			{
				this._mENUTEXT = value;
			}
		}
		
		private string _pARENTMENUTEXT;
		public virtual string PARENTMENUTEXT
		{
			get
			{
				return this._pARENTMENUTEXT;
			}
			set
			{
				this._pARENTMENUTEXT = value;
			}
		}
		
		private string _nAVIGATEURL;
		public virtual string NAVIGATEURL
		{
			get
			{
				return this._nAVIGATEURL;
			}
			set
			{
				this._nAVIGATEURL = value;
			}
		}
		
		private int? _dISPLAYORDER;
		public virtual int? DISPLAYORDER
		{
			get
			{
				return this._dISPLAYORDER;
			}
			set
			{
				this._dISPLAYORDER = value;
			}
		}
		
	}
}
#pragma warning restore 1591
