using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using AppFramework.ApplicationLayer.DTO;
using KMUH.iKASAWebApi.ApplicationLayer.Assemblers;
using KMUH.iKASAWebApi.ApplicationLayer.DTO;
using KMUH.iKASAWebApi.Infrastructure.Data.Repositories;

namespace KMUH.iKASAWebApi.ApplicationLayer.Services
{
    public enum CodeReferenceType
    {
        // TODO: Add your CodeRefenceType here(CodeRef Table)
        [DefaultValue("付款方式")]
        付款方式,
        [DefaultValue("Sex")]
        性別,
        伺服器名稱,
    }
        
    public interface ICodeReferenceAppService : IDisposable
    {
        IEnumerable<CodeRefDto> GetCodeReferenceItems(CodeReferenceType type);
        IEnumerable<CodeRefDto> GetCodeReferenceItems(CodeReferenceType type, QueryCodeRefDto parameter);
    }

    public partial class CodeReferenceAppService : ICodeReferenceAppService
    {
        #region Private Members

        private ICodeRefOperationService service = new CodeRefOperationService();

        #endregion Private Members

        #region IDisposable Members

        public void Dispose()
        {
            //dispose all resources
            service.Dispose();
        }

        #endregion IDisposable Members

        public IEnumerable<CodeRefDto> GetCodeReferenceItems(CodeReferenceType type)
        {
            var list = new List<CodeRefDto>().AsEnumerable();

            switch (type)
            {
                case CodeReferenceType.伺服器名稱:
                    using (var operationService = new CodeRefOperationService())
                    {
                        list = operationService.ReadCodeReferenceItems(
                                "select distinct upper(servern) as code, upper(servern) as name from UNILOGINCONNPW")
                                .OrderBy(c => c.Code)
                                .ToList();
                    }
                    break;
                default:
                    //get CodeReferenceType's default attribute value
                    DefaultValueAttribute[] attributes = (DefaultValueAttribute[])type.GetType().GetField(type.ToString()).GetCustomAttributes(typeof(DefaultValueAttribute), false);
                    var defaultValue = attributes.Length > 0 ? attributes[0].Value.ToString() : string.Empty;
                    list = this.service.ReadCodeReferenceItems(string.Format(
                                "SELECT code, name, des, des2 FROM erd.coderef where codetype = '{0}' and code <> '---' order by showseq, code", defaultValue))
                                .ToList();
                    break;
            }
            GetCustomCodeReferenceItems(type, ref list);
            return list;
        }

        public IEnumerable<CodeRefDto> GetCodeReferenceItems(CodeReferenceType type, QueryCodeRefDto parameter)
        {
            if (parameter == null)
            {
                return GetCodeReferenceItems(type);
            }
            else
            {
                var list = new List<CodeRefDto>();
                //switch (type)
                //{
                //}
                return list;
            }
        }

        private void GetCustomCodeReferenceItems(CodeReferenceType type, ref IEnumerable<CodeRefDto> coderefItems)
        {
        }
    }
}