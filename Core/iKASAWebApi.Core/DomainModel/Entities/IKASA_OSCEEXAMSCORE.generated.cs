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
	public partial class IKASA_OSCEEXAMSCORE
	{
		private string _eXAMID;
		public virtual string EXAMID
		{
			get
			{
				return this._eXAMID;
			}
			set
			{
				this._eXAMID = value;
			}
		}
		
		private string _eMPCODE;
		public virtual string EMPCODE
		{
			get
			{
				return this._eMPCODE;
			}
			set
			{
				this._eMPCODE = value;
			}
		}
		
		private string _eXAMIDNO;
		public virtual string EXAMIDNO
		{
			get
			{
				return this._eXAMIDNO;
			}
			set
			{
				this._eXAMIDNO = value;
			}
		}
		
		private int _sTAGENO;
		public virtual int STAGENO
		{
			get
			{
				return this._sTAGENO;
			}
			set
			{
				this._sTAGENO = value;
			}
		}
		
		private decimal _sCORE;
		public virtual decimal SCORE
		{
			get
			{
				return this._sCORE;
			}
			set
			{
				this._sCORE = value;
			}
		}
		
		private IKASA_OSCEEXAM _iKASA_OSCEEXAM;
		public virtual IKASA_OSCEEXAM IKASA_OSCEEXAM
		{
			get
			{
				return this._iKASA_OSCEEXAM;
			}
			set
			{
				this._iKASA_OSCEEXAM = value;
			}
		}
		
	}
}
#pragma warning restore 1591
