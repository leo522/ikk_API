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
	public partial class EDUACTTYPE
	{
		private int _cLS_ID;
		public virtual int CLS_ID
		{
			get
			{
				return this._cLS_ID;
			}
			set
			{
				this._cLS_ID = value;
			}
		}
		
		private string _cLS_NAME;
		public virtual string CLS_NAME
		{
			get
			{
				return this._cLS_NAME;
			}
			set
			{
				this._cLS_NAME = value;
			}
		}
		
		private int? _cLS_PID;
		public virtual int? CLS_PID
		{
			get
			{
				return this._cLS_PID;
			}
			set
			{
				this._cLS_PID = value;
			}
		}
		
		private int? _cLS_FLOOR;
		public virtual int? CLS_FLOOR
		{
			get
			{
				return this._cLS_FLOOR;
			}
			set
			{
				this._cLS_FLOOR = value;
			}
		}
		
		private int? _cLS_PERMISSION;
		public virtual int? CLS_PERMISSION
		{
			get
			{
				return this._cLS_PERMISSION;
			}
			set
			{
				this._cLS_PERMISSION = value;
			}
		}
		
		private int? _cLS_GROUP_KEY;
		public virtual int? CLS_GROUP_KEY
		{
			get
			{
				return this._cLS_GROUP_KEY;
			}
			set
			{
				this._cLS_GROUP_KEY = value;
			}
		}
		
		private string _cLS_EMP_CODE;
		public virtual string CLS_EMP_CODE
		{
			get
			{
				return this._cLS_EMP_CODE;
			}
			set
			{
				this._cLS_EMP_CODE = value;
			}
		}
		
		private string _cLS_DEPT_CODE;
		public virtual string CLS_DEPT_CODE
		{
			get
			{
				return this._cLS_DEPT_CODE;
			}
			set
			{
				this._cLS_DEPT_CODE = value;
			}
		}
		
		private string _cLS_TYPE;
		public virtual string CLS_TYPE
		{
			get
			{
				return this._cLS_TYPE;
			}
			set
			{
				this._cLS_TYPE = value;
			}
		}
		
		private EDUACTTYPE _eDUACTTYPE1;
		public virtual EDUACTTYPE EDUACTTYPE1
		{
			get
			{
				return this._eDUACTTYPE1;
			}
			set
			{
				this._eDUACTTYPE1 = value;
			}
		}
		
		private IList<EDUACTTYPE> _eDUACTTYPEs = new List<EDUACTTYPE>();
		public virtual IList<EDUACTTYPE> EDUACTTYPEs
		{
			get
			{
				return this._eDUACTTYPEs;
			}
		}
		
	}
}
#pragma warning restore 1591
