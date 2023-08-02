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
	[ErrorHandlers.ApplicationErrorHandlerAttribute()] // manage all unhandled exceptions
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Single)]
    public class CodeRefWCFService : ICodeRefWCFService
    {
        private ICodeReferenceAppService service = new CodeReferenceAppService();

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

        public IEnumerable<CodeRefDto> GetCodeReferenceItems(CodeReferenceType type)
        {
            return this.service.GetCodeReferenceItems(type);
        }

        #endregion Members        
    }	
}


