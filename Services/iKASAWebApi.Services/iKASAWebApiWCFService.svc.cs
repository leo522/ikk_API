using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using KMUH.iKASAWebApi.ApplicationLayer.DTO;
using KMUH.iKASAWebApi.ApplicationLayer.Services;


namespace KMUH.iKASAWebApi.Services
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
	[ErrorHandlers.ApplicationErrorHandlerAttribute()] // manage all unhandled exceptions
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Single)]
	public partial class iKASAWebApiWCFService : IiKASAWebApiWCFService
	{
		private IiKASAWebApiAppService service = new iKASAWebApiAppService();

		#region IDisposable Members

		/// <summary>
		/// <see cref="M:System.IDisposable.Dispose"/>
		/// </summary>
		public void Dispose()
		{
			//dispose all resources
			service.Dispose();
		}

		#endregion

		#region Members

		public void InsertInternFormList(string TargetID, string INSTANCE_NAME, string INHOSPID)
		{
			service.InsertInternFormList(TargetID, INSTANCE_NAME, INHOSPID);
		}

		#endregion Members
	}
}


