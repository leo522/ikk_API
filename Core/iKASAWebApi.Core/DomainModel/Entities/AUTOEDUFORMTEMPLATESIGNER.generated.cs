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
	public partial class AUTOEDUFORMTEMPLATESIGNER
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
		
		private string _aUTOFTLISTID;
		public virtual string AUTOFTLISTID
		{
			get
			{
				return this._aUTOFTLISTID;
			}
			set
			{
				this._aUTOFTLISTID = value;
			}
		}
		
		private int? _sIGNORDER;
		public virtual int? SIGNORDER
		{
			get
			{
				return this._sIGNORDER;
			}
			set
			{
				this._sIGNORDER = value;
			}
		}
		
		private string _sIGNROLETYPE;
		public virtual string SIGNROLETYPE
		{
			get
			{
				return this._sIGNROLETYPE;
			}
			set
			{
				this._sIGNROLETYPE = value;
			}
		}
		
	}
}
#pragma warning restore 1591
