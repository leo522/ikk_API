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
using KMUH.iKASAWebApi.DomainModel.Entities;

namespace KMUH.iKASAWebApi.DomainModel.Entities	
{
	public partial class IKASA_ERCASE_EVALDET
	{
		private int _eVALID;
		public virtual int EVALID
		{
			get
			{
				return this._eVALID;
			}
			set
			{
				this._eVALID = value;
			}
		}
		
		private string _cONTROLID;
		public virtual string CONTROLID
		{
			get
			{
				return this._cONTROLID;
			}
			set
			{
				this._cONTROLID = value;
			}
		}
		
		private string _cONTROLVALUE;
		public virtual string CONTROLVALUE
		{
			get
			{
				return this._cONTROLVALUE;
			}
			set
			{
				this._cONTROLVALUE = value;
			}
		}
		
		private IKASA_ERCASE_EVAL _iKASA_ERCASE_EVAL;
		public virtual IKASA_ERCASE_EVAL IKASA_ERCASE_EVAL
		{
			get
			{
				return this._iKASA_ERCASE_EVAL;
			}
			set
			{
				this._iKASA_ERCASE_EVAL = value;
			}
		}
		
	}
}
#pragma warning restore 1591