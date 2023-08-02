namespace KMUH.iKASAWebApi.ApplicationLayer.Services
{
    using System;

    public class iKASAWebApiNormalAppService
    {
        public Type getTypeOfOperationService()
        {
           return typeof(iKASAWebApiOperationService);
        }
    }
}

