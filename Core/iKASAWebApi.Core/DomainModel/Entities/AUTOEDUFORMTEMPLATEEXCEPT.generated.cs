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
	public partial class AUTOEDUFORMTEMPLATEEXCEPT
	{
		private string _aUTOFTLISTID;
		public virtual string AUTOFTLISTID
		{
			get
			{
				return this._aUTOFTLISTID;
			}
			set
			{
				this._aUTOFTLISTID = value;
			}
		}
		
		private string _eXCEPTTYPE;
		public virtual string EXCEPTTYPE
		{
			get
			{
				return this._eXCEPTTYPE;
			}
			set
			{
				this._eXCEPTTYPE = value;
			}
		}
		
		private string _eXCEPTKEY;
		public virtual string EXCEPTKEY
		{
			get
			{
				return this._eXCEPTKEY;
			}
			set
			{
				this._eXCEPTKEY = value;
			}
		}
		
	}
}
#pragma warning restore 1591
