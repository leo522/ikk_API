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
	public partial class EDUTEAMMEMBERRUNDOWN
	{
		private string _eDUTEAMMEMBERID;
		public virtual string EDUTEAMMEMBERID
		{
			get
			{
				return this._eDUTEAMMEMBERID;
			}
			set
			{
				this._eDUTEAMMEMBERID = value;
			}
		}
		
		private string _eDUTERMID;
		public virtual string EDUTERMID
		{
			get
			{
				return this._eDUTERMID;
			}
			set
			{
				this._eDUTERMID = value;
			}
		}
		
		private string _mEMBERID;
		public virtual string MEMBERID
		{
			get
			{
				return this._mEMBERID;
			}
			set
			{
				this._mEMBERID = value;
			}
		}
		
		private string _mEMBERCODE;
		public virtual string MEMBERCODE
		{
			get
			{
				return this._mEMBERCODE;
			}
			set
			{
				this._mEMBERCODE = value;
			}
		}
		
		private string _cOACHID;
		public virtual string COACHID
		{
			get
			{
				return this._cOACHID;
			}
			set
			{
				this._cOACHID = value;
			}
		}
		
		private MEMBER _mEMBER;
		public virtual MEMBER MEMBER
		{
			get
			{
				return this._mEMBER;
			}
			set
			{
				this._mEMBER = value;
			}
		}
		
	}
}
#pragma warning restore 1591
