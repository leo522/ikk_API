﻿namespace KMUH.iKASAWebApi.Services.ErrorHandlers
{
	using System;
	using System.ServiceModel.Dispatcher;
	using System.ServiceModel;
	using System.ServiceModel.Channels;

	/// <summary>
	/// Default error handler for WCF Service Facade
	/// </summary>
	public sealed class ApplicationErrorHandler
		: IErrorHandler
	{
		/// <summary>
		/// Enables error-related processing and returns a value that indicates whether
		/// the dispatcher aborts the session and the instance context in certain cases
		/// </summary>
		/// <remarks>
		/// Trace error and handle this
		/// </remarks>
		/// <param name="error">The exception thrown during processing</param>
		/// <returns>
		/// true if should not abort the session (if there is one) and instance context
		/// if the instance context is not System.ServiceModel.InstanceContextMode.Single;
		/// otherwise, false. The default is false.
		/// </returns>
		public bool HandleError(Exception error)
		{
			if (error != null)
			{
				//LoggerFactory.CreateLog().LogError(Resources.Messages.error_unmanagederror, error);
			}
			//set  error as handled
			return true;
		}

		/// <summary>
		/// Enables the creation of a custom System.ServiceModel.FaultException{TDetail}
		/// that is returned from an exception in the course of a service method.
		/// </summary>
		/// <param name="error">The System.Exception object thrown in the course of the service operation.</param>
		/// <param name="version">The SOAP version of the message.</param>
		/// <param name="fault">The System.ServiceModel.Channels.Message object that is returned to the client, or service in duplex case</param>
		public void ProvideFault(Exception error, System.ServiceModel.Channels.MessageVersion version, ref System.ServiceModel.Channels.Message fault)
		{
			if (error is FaultException<ApplicationServiceError>)
			{
				MessageFault messageFault = ((FaultException<ApplicationServiceError>)error).CreateMessageFault();

				//propagate FaultException
				fault = Message.CreateMessage(version, messageFault, ((FaultException<ApplicationServiceError>)error).Action);
			}
			else
			{
				//create service error
				var serviceError = ApplicationServiceError.Create(error);
				MessageFault messageFault = serviceError.CreateMessageFault();

				//propagate FaultException
				fault = Message.CreateMessage(version, messageFault, serviceError.Action);
			}
		}
	}
}