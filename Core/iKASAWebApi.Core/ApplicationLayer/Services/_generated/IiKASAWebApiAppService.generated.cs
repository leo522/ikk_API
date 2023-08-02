namespace KMUH.iKASAWebApi.ApplicationLayer.Services
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using KMUH.iKASAWebApi.ApplicationLayer.DTO;
 
	public partial interface IiKASAWebApiAppService : IDisposable
	{
		
		void InsertInternFormList(string TargetID, string INSTANCE_NAME, string INHOSPID);

	}
}


