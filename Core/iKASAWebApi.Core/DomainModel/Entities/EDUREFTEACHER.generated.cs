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
	public partial class EDUREFTEACHER
	{
		private int _eDUREFTEACHERID;
		public virtual int EDUREFTEACHERID
		{
			get
			{
				return this._eDUREFTEACHERID;
			}
			set
			{
				this._eDUREFTEACHERID = value;
			}
		}
		
		private string _tEACHERID;
		public virtual string TEACHERID
		{
			get
			{
				return this._tEACHERID;
			}
			set
			{
				this._tEACHERID = value;
			}
		}
		
		private string _tEACHERTYPE;
		public virtual string TEACHERTYPE
		{
			get
			{
				return this._tEACHERTYPE;
			}
			set
			{
				this._tEACHERTYPE = value;
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
		
		private string _rEFTABLE;
		public virtual string REFTABLE
		{
			get
			{
				return this._rEFTABLE;
			}
			set
			{
				this._rEFTABLE = value;
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
		
		private DateTime? _cREATEDATE;
		public virtual DateTime? CREATEDATE
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
		
		private string _cREATEEMP;
		public virtual string CREATEEMP
		{
			get
			{
				return this._cREATEEMP;
			}
			set
			{
				this._cREATEEMP = value;
			}
		}
		
		private int? _tEACHERORDER;
		public virtual int? TEACHERORDER
		{
			get
			{
				return this._tEACHERORDER;
			}
			set
			{
				this._tEACHERORDER = value;
			}
		}
		
	}
}
#pragma warning restore 1591
