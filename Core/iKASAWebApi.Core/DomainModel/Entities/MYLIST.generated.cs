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
	public partial class MYLIST
	{
		private string _lISTID;
		public virtual string LISTID
		{
			get
			{
				return this._lISTID;
			}
			set
			{
				this._lISTID = value;
			}
		}
		
		private string _lISTNAME;
		public virtual string LISTNAME
		{
			get
			{
				return this._lISTNAME;
			}
			set
			{
				this._lISTNAME = value;
			}
		}
		
		private string _cREATER;
		public virtual string CREATER
		{
			get
			{
				return this._cREATER;
			}
			set
			{
				this._cREATER = value;
			}
		}
		
		private DateTime _cREATEDATE;
		public virtual DateTime CREATEDATE
		{
			get
			{
				return this._cREATEDATE;
			}
			set
			{
				this._cREATEDATE = value;
			}
		}
		
		private DateTime? _mODIFYDATE;
		public virtual DateTime? MODIFYDATE
		{
			get
			{
				return this._mODIFYDATE;
			}
			set
			{
				this._mODIFYDATE = value;
			}
		}
		
		private bool _iSPUBLIC;
		public virtual bool ISPUBLIC
		{
			get
			{
				return this._iSPUBLIC;
			}
			set
			{
				this._iSPUBLIC = value;
			}
		}
		
		private IList<MYLISTDET> _mYLISTDETs = new List<MYLISTDET>();
		public virtual IList<MYLISTDET> MYLISTDETs
		{
			get
			{
				return this._mYLISTDETs;
			}
		}
		
	}
}
#pragma warning restore 1591