namespace KMUH.iKASAWebApi.Services.ErrorHandlers
{
	using System;
	using System.Runtime.Serialization;
	using System.ServiceModel;

	/// <summary>
	/// Default ServiceError
	/// </summary>
	[DataContract(Name = "ApplicationServiceError")]
	public class ApplicationServiceError
	{
		/// <summary>
		/// Error message that flow to client services
		/// </summary>
		[DataMember(Name = "Message")]
		public string Message { get; set; }

		/// <summary>
		/// Gets or sets the stack trace information of the fault.
		/// </summary>
		[DataMember(Name = "StackTrace")]
		public string StackTrace { get; set; }

		/// <summary>
		/// Gets or sets the inner exception detailed information of the fault.
		/// </summary>
		[DataMember(Name = "InnerExceptionDetail")]
		public string InnerExceptionDetail { get; set; }

		#region Public Methods

		/// <summary>
		/// Creates the fault exception based on the given <see cref="Exception"/>.
		/// </summary>
		/// <param name="ex">The <see cref="Exception"/> object from which the fault exception creates.</param>
		/// <returns>The instance of the fault exception.</returns>
		public static FaultException<ApplicationServiceError> Create(Exception ex)
		{
			return new FaultException<ApplicationServiceError>(new ApplicationServiceError
			{
				Message = ex.Message,
				StackTrace = ex.StackTrace,
				InnerExceptionDetail = ex.InnerException != null ? ex.InnerException.ToString() : null
			}, ex.Message);
		}

		#endregion Public Methods
	}
}