using Autofac.Extras.DynamicProxy;
using Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    [Intercept(typeof(CustomInterceptorAttribute))]
    public interface IStudentService
    {

        string GetName(Student dto);

        void SayAge(Student dto);
    }
}
