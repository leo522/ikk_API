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
	public partial class IKASA_ACTUPLOADFILE
	{
		private string _iD;
		public virtual string ID
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
		
		private string _fILENAME;
		public virtual string FILENAME
		{
			get
			{
				return this._fILENAME;
			}
			set
			{
				this._fILENAME = value;
			}
		}
		
		private string _dISPLAYTITLE;
		public virtual string DISPLAYTITLE
		{
			get
			{
				return this._dISPLAYTITLE;
			}
			set
			{
				this._dISPLAYTITLE = value;
			}
		}
		
		private string _dESCRIPTION;
		public virtual string DESCRIPTION
		{
			get
			{
				return this._dESCRIPTION;
			}
			set
			{
				this._dESCRIPTION = value;
			}
		}
		
		private string _fILEPATH;
		public virtual string FILEPATH
		{
			get
			{
				return this._fILEPATH;
			}
			set
			{
				this._fILEPATH = value;
			}
		}
		
		private string _fILECATEGORY;
		public virtual string FILECATEGORY
		{
			get
			{
				return this._fILECATEGORY;
			}
			set
			{
				this._fILECATEGORY = value;
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
		
		private DateTime? _dELETETIME;
		public virtual DateTime? DELETETIME
		{
			get
			{
				return this._dELETETIME;
			}
			set
			{
				this._dELETETIME = value;
			}
		}
		
		private DateTime? _aCTDATE;
		public virtual DateTime? ACTDATE
		{
			get
			{
				return this._aCTDATE;
			}
			set
			{
				this._aCTDATE = value;
			}
		}
		
	}
}
#pragma warning restore 1591
