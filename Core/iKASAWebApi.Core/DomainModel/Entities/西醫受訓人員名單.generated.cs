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
	public partial class 西醫受訓人員名單
	{
		private string _人員姓名;
		public virtual string 人員姓名
		{
			get
			{
				return this._人員姓名;
			}
			set
			{
				this._人員姓名 = value;
			}
		}
		
		private string _身分證字號;
		public virtual string 身分證字號
		{
			get
			{
				return this._身分證字號;
			}
			set
			{
				this._身分證字號 = value;
			}
		}
		
		private string _科別;
		public virtual string 科別
		{
			get
			{
				return this._科別;
			}
			set
			{
				this._科別 = value;
			}
		}
		
	}
}
#pragma warning restore 1591
