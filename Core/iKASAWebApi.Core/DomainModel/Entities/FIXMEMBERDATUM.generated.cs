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
	public partial class FIXMEMBERDATUM
	{
		private string _學號;
		public virtual string 學號
		{
			get
			{
				return this._學號;
			}
			set
			{
				this._學號 = value;
			}
		}
		
		private string _姓名;
		public virtual string 姓名
		{
			get
			{
				return this._姓名;
			}
			set
			{
				this._姓名 = value;
			}
		}
		
		private string _職稱;
		public virtual string 職稱
		{
			get
			{
				return this._職稱;
			}
			set
			{
				this._職稱 = value;
			}
		}
		
		private DateTime? _起始日;
		public virtual DateTime? 起始日
		{
			get
			{
				return this._起始日;
			}
			set
			{
				this._起始日 = value;
			}
		}
		
		private DateTime? _結束日;
		public virtual DateTime? 結束日
		{
			get
			{
				return this._結束日;
			}
			set
			{
				this._結束日 = value;
			}
		}
		
		private string _組別;
		public virtual string 組別
		{
			get
			{
				return this._組別;
			}
			set
			{
				this._組別 = value;
			}
		}
		
		private string _說明;
		public virtual string 說明
		{
			get
			{
				return this._說明;
			}
			set
			{
				this._說明 = value;
			}
		}
		
		private string _員工編號;
		public virtual string 員工編號
		{
			get
			{
				return this._員工編號;
			}
			set
			{
				this._員工編號 = value;
			}
		}
		
	}
}
#pragma warning restore 1591
