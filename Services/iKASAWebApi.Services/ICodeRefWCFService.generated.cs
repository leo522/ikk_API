using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using AppFramework.ApplicationLayer.DTO;
using KMUH.iKASAWebApi.ApplicationLayer.DTO;
using KMUH.iKASAWebApi.ApplicationLayer.Services;


namespace KMUH.iKASAWebApi.Services
{
	[ServiceContract]
	public interface ICodeRefWCFService
	{
		[OperationContract]
		[FaultContract(typeof(ErrorHandlers.ApplicationServiceError))]
		IEnumerable<CodeRefDto> GetCodeReferenceItems(CodeReferenceType type);
	}
}	


