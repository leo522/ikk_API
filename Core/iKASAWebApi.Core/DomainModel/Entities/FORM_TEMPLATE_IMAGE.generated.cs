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
	public partial class FORM_TEMPLATE_IMAGE
	{
		private int _tEMPLATE_IMAGE_ID;
		public virtual int TEMPLATE_IMAGE_ID
		{
			get
			{
				return this._tEMPLATE_IMAGE_ID;
			}
			set
			{
				this._tEMPLATE_IMAGE_ID = value;
			}
		}
		
		private int _tEMPLATE_ID;
		public virtual int TEMPLATE_ID
		{
			get
			{
				return this._tEMPLATE_ID;
			}
			set
			{
				this._tEMPLATE_ID = value;
			}
		}
		
		private string _fILE_NAME;
		public virtual string FILE_NAME
		{
			get
			{
				return this._fILE_NAME;
			}
			set
			{
				this._fILE_NAME = value;
			}
		}
		
		private byte _iMAGE_CONTENT;
		public virtual byte IMAGE_CONTENT
		{
			get
			{
				return this._iMAGE_CONTENT;
			}
			set
			{
				this._iMAGE_CONTENT = value;
			}
		}
		
	}
}
#pragma warning restore 1591