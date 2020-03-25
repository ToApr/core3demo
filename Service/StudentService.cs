using Autofac.Extras.DynamicProxy;
using Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    //[Intercept(typeof(CustomInterceptorAttribute))]
    public class StudentService : IStudentService
    {
        public string GetName(Student dto)
        {
            return dto.Name;
        }

        public void SayAge(Student dto)
        {
            Console.WriteLine(dto.Age);
        }
    }
}
