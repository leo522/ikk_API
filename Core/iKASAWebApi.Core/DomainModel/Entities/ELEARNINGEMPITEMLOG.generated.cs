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
	public partial class ELEARNINGEMPITEMLOG
	{
		private string _iTEMID;
		public virtual string ITEMID
		{
			get
			{
				return this._iTEMID;
			}
			set
			{
				this._iTEMID = value;
			}
		}
		
		private string _eMPCODE;
		public virtual string EMPCODE
		{
			get
			{
				return this._eMPCODE;
			}
			set
			{
				this._eMPCODE = value;
			}
		}
		
		private string _rEFID;
		public virtual string REFID
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
		
		private DateTime? _eXECUTETIME;
		public virtual DateTime? EXECUTETIME
		{
			get
			{
				return this._eXECUTETIME;
			}
			set
			{
				this._eXECUTETIME = value;
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
		
		private int? _eXECUTETIMES;
		public virtual int? EXECUTETIMES
		{
			get
			{
				return this._eXECUTETIMES;
			}
			set
			{
				this._eXECUTETIMES = value;
			}
		}
		
	}
}
#pragma warning restore 1591