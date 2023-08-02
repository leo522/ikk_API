using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using KMUH.iKASAWebApi.ApplicationLayer.DTO;


namespace KMUH.iKASAWebApi.Services
{
	[ServiceContract]
	public partial interface IiKASAWebApiWCFService : IDisposable
	{
		[OperationContract]
		[FaultContract(typeof(ErrorHandlers.ApplicationServiceError))]
		void InsertInternFormList(string TargetID, string INSTANCE_NAME, string INHOSPID);

	}
}


