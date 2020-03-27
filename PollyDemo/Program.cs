using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.CircuitBreaker;

namespace PollyDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {

            //Policy.Handle<Exception>().
            //    RetryForever().
            //    Execute(ShowName);
            //Policy.Handle<Exception>()
            //      .Retry(3,onRetry:(ex,i)=> {
            //        Console.WriteLine($"这是第{i}次尝试。。。");
            //      }).
            //Execute(ShowName);
            //Policy.Handle<Exception>()
            //      .WaitAndRetry(new[] { TimeSpan.FromSeconds(1),
            //                        TimeSpan.FromSeconds(2),
            //                        TimeSpan.FromSeconds(3)},(exception,timespan)=> {
            //                            Console.WriteLine($"这是第{timespan}次尝试");
            //                        })
            //      .Execute(ShowName);
            //Action<Exception, TimeSpan> onBreak = (exception, timespan) => {

            //    Console.WriteLine($"onBreak{timespan}");
            //};
            //Action onReset = () => {
            //    Console.WriteLine("onreset");
            //};
            try
            {

                ISyncPolicy policyException = Policy.Handle<Exception>()
                   .Fallback(() =>
                   {
                       Console.WriteLine("Fallback");
                   });
                ISyncPolicy policyTimeout = Policy.Timeout(3, Polly.Timeout.TimeoutStrategy.Pessimistic);
                policyTimeout.Execute(() =>
                {
                    Console.WriteLine("Job Start...");
                    Thread.Sleep(5000);
                    //throw new Exception();
                    Console.WriteLine("Job End...");
                });

                ISyncPolicy mainPolicy = Policy.Wrap(policyTimeout, policyException);
                mainPolicy.Execute(() =>
                {
                    Console.WriteLine("Job Start...");
                    Thread.Sleep(5000);
                    //throw new Exception();
                    Console.WriteLine("Job End...");
                });

            }
            catch (Exception ex)
            {
                Console.WriteLine("发生异常"+ex.ToString());
            }
            Console.ReadLine();
            #region 降级
            //ISyncPolicy policy = Policy.Handle<Exception>()
            //.Fallback(() =>
            //{
            //    Console.WriteLine("Error occured");
            //});

            //policy.Execute(() =>
            //{
            //    Console.WriteLine("Job Start");

            //    throw new ArgumentException("Hello Polly!");

            //    Console.WriteLine("Job End");
            //});
            #endregion
            Console.ReadLine();
            //CircuitBreakerPolicy breaker = Policy
            //    .Handle<Exception>()
            //    .CircuitBreaker(2, TimeSpan.FromMinutes(1), onBreak, onReset);
            //CircuitBreakerPolicy breaker=  Policy.Handle<Exception>()
            //    .CircuitBreaker(
            //    exceptionsAllowedBeforeBreaking: 4,
            //    durationOfBreak: TimeSpan.FromMinutes(1),
            //    onBreak: (ex, t) => {
            //        Console.WriteLine("onbreak...");
            //    },
            //    onReset: () => {
            //        Console.WriteLine("onReset...");
            //    });
            //for (int i = 0; i < 12; i++)  // 模拟多次调用，触发熔断
            //{
            //    try
            //    {
            //        breaker.Execute(ShowName);
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine("try-catch:" + ex.Message);
            //    }
            //    Thread.Sleep(TimeSpan.FromSeconds(10));
            //}
            // CreateHostBuilder(args).Build().Run();
        }

        public static void ShowName()
        {
            Console.WriteLine("showName" + DateTime.Now.ToString());
            throw new Exception("测试。。。");
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
