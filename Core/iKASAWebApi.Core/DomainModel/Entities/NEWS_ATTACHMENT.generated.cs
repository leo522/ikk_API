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
	public partial class NEWS_ATTACHMENT
	{
		private int _aTTACHMENTID;
		public virtual int ATTACHMENTID
		{
			get
			{
				return this._aTTACHMENTID;
			}
			set
			{
				this._aTTACHMENTID = value;
			}
		}
		
		private string _nEWID;
		public virtual string NEWID
		{
			get
			{
				return this._nEWID;
			}
			set
			{
				this._nEWID = value;
			}
		}
		
		private string _aTTACHMENTNAME;
		public virtual string ATTACHMENTNAME
		{
			get
			{
				return this._aTTACHMENTNAME;
			}
			set
			{
				this._aTTACHMENTNAME = value;
			}
		}
		
		private byte[] _aTTACHMENT;
		public virtual byte[] ATTACHMENT
		{
			get
			{
				return this._aTTACHMENT;
			}
			set
			{
				this._aTTACHMENT = value;
			}
		}
		
		private NEWS _nEWS;
		public virtual NEWS NEWS
		{
			get
			{
				return this._nEWS;
			}
			set
			{
				this._nEWS = value;
			}
		}
		
	}
}
#pragma warning restore 1591
