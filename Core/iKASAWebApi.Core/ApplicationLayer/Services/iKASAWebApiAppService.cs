using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;  
using System.Transactions;
using Telerik.OpenAccess;
using AppFramework.Logging;
using AppFramework.Logging.TraceSource;
using AppFramework.Validation;
using AppFramework.Validation.DataAnnotations;
//using KMUH.iKASAWebApi.ApplicationLayer.Assemblers;
//using KMUH.iKASAWebApi.ApplicationLayer.DTO;
//using KMUH.iKASAWebApi.DomainModel.Contracts;
using KMUH.iKASAWebApi.DomainModel.Entities;
using KMUH.iKASAWebApi.DomainModel.Factories;
using KMUH.FunctionLibrary.ApplicationLayer.Services;
using KMU.EduActivity.ApplicationLayer.DTO;
using KMU.EduActivity.ApplicationLayer.Services;
using System.Data;
using KMU.EduActivity.DomainModel.Entities;
using Telerik.OpenAccess.Data.Common;

namespace KMUH.iKASAWebApi.ApplicationLayer.Services
{

    public class iKASAWebApiAppService : IiKASAWebApiAppService
  {
      #region Private Members

      private IEntityValidator validator;
      private ILogger logger;

	private IApConnHelper _apConnHelper;
      #endregion Private Members

      #region Ctor

      public iKASAWebApiAppService ()
      {
          //Set Validator
          EntityValidatorFactory.SetCurrent(new DataAnnotationsEntityValidatorFactory());
          validator = EntityValidatorFactory.CreateValidator();
          //Set Logger
          LoggerFactory.SetCurrent(new TraceSourceLogFactory());
          logger = LoggerFactory.CreateLog();
      }
      public iKASAWebApiAppService (IApConnHelper apConnHelper)
      {
          //Set Validator
          EntityValidatorFactory.SetCurrent(new DataAnnotationsEntityValidatorFactory());
          validator = EntityValidatorFactory.CreateValidator();
          //Set Logger
          LoggerFactory.SetCurrent(new TraceSourceLogFactory());
          logger = LoggerFactory.CreateLog();
          _apConnHelper = apConnHelper;
      }

      #endregion Ctor

      #region IDisposable Members

      public void Dispose()
      {
          //dispose all resources
      }

      #endregion IDisposable Members

      #region IiKASAWebApiAppService Members

      // TODO: 1. Add your application service layer operations here

      //public IEnumerable<CustomerDto> GetCustomers()
      //{
      //  using (IiKASAWebApiOperationService opService = new iKASAWebApiOperationService())
      //  {
      //    return opService.ReadCustomers();
      //  }
      //}


      #endregion IiKASAWebApiAppService Members

      #region 存取實習醫學生學習紀錄及回饋表_
      public void InsertInternFormList(string TargetID, string INSTANCE_NAME, string INHOSPID)
      {
          using (IiKASAWebApiOperationService Service = new iKASAWebApiOperationService())
          {
              string sql = @"Insert Into FORM_INSTANCES(TargetID,INSTANCE_NAME,INHOSPID)";

              var parameters = new List<OAParameter>();
              parameters.Add(new OAParameter("TargetID", TargetID));
              parameters.Add(new OAParameter("INSTANCE_NAME",INSTANCE_NAME));
              parameters.Add(new OAParameter("INHOSPID", INHOSPID));

              Service.ExecuteNonQuery(sql, parameters.ToArray());
              Service.UnitOfWork.SaveChanges();
          }
      }
      #endregion
  }	
}