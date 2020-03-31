using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace OptionsDemo
{
    public class Program
    {


        static Task<int> GetLengthAsync()
        {
            Console.WriteLine("GetLengthAsync Start");
            Task<int> task = Task<int>.Run(() => {

                string str = GetStringAsync().Result;
                Console.WriteLine("GetLengthAsync End");
                return str.Length;
            });
            return task;
        }
        static Task<string> GetStringAsync()
        {
            return Task<string>.Run(() => { Thread.Sleep(2000); return "finished"; });
        }
        static void DoRun1()
        {
            Console.WriteLine("Thread Id =" + Thread.CurrentThread.ManagedThreadId);
        }

        static void DoRun2()
        {
                Thread.Sleep(50);
            Console.WriteLine("Task调用Thread Id =" + Thread.CurrentThread.ManagedThreadId);
        }

        static async Task<int> CountCharsAsync(string url)
        {
            WebClient wc = new WebClient();
            string result = await wc.DownloadStringTaskAsync(new Uri(url));
            return result.Length;
        }

        public static async Task<string> call()
        {
            Console.WriteLine("----------->1");
            int s = await foo();
            Console.WriteLine($"开始打印获取到的值{s}");
            Console.WriteLine("----------->2");
            return "1";
        }
        public static async Task<int> foo()
        {
            Console.WriteLine("----------->3");
            await Task.Delay(500);
            Console.WriteLine("----------->4");
            return 1;
        }
        public static async Task<string> Get()
        {
            var info = string.Format("----------->1:{0}", Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine(info);
           var infoTask = await TaskCaller();
            var infoTaskFinished = string.Format("----------->2:{0}", Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine(infoTaskFinished);
            return string.Format("{0},{1},{2}", info, infoTask, infoTaskFinished);
        }

        private static async Task<string> TaskCaller()
        {
            Console.WriteLine(string.Format("----------->4:{0}", Thread.CurrentThread.ManagedThreadId));
            await Task.Delay(500);
            string s= string.Format("----------->3:{0}", Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine(s);
            return s;
        }


        public static async Task<string> call1()
        {
            Console.WriteLine("----------->1");
            Task<int> infoTask = foo1();
            Console.WriteLine("----------->2");
            int s = await infoTask;
            Console.WriteLine("----------->3");
            return "1";
        }
        public static async Task<int> foo1()
        {
            Console.WriteLine("----------->4");
            await Task.Delay(500);
            Console.WriteLine("----------->5");
            return 1;
        }

        public static async Task Main(string[] args)
        {


        
            Console.WriteLine(typeof(string).Assembly.FullName);
            Console.WriteLine(typeof(int).Assembly.FullName);
            Console.ReadLine();
            string s =await call1();
            Console.WriteLine(s);
            Console.ReadLine();
            Console.WriteLine("开始获取博客园首页字符数量");
            Task<int> task1 = CountCharsAsync("http://www.cnblogs.com");
            Console.WriteLine("开始获取百度首页字符数量");
            Task<int> task2 = CountCharsAsync("http://www.baidu.com");

            Console.WriteLine("Main方法中做其他事情");

            Console.WriteLine("博客园:" + task1.Result);
            Console.WriteLine("百度:" + task2.Result);
            Console.Read();
            Console.WriteLine("-------主线程启动-------");
            Task<int> task = GetLengthAsync();
            Console.WriteLine("Main方法做其他事情");
            Console.WriteLine("Task返回的值" + task.Result);
            Console.WriteLine("-------主线程结束-------");
            //让应用程序不立即退出
            Console.Read();

            //Console.WriteLine("主线程开始");

            //Task<string> task = Task<string>.Run(() => { Thread.Sleep(3000); return Thread.CurrentThread.ManagedThreadId.ToString(); });
            //Console.WriteLine(task.Result);
            //Console.WriteLine("主线程结束");


            //for (int i = 0; i < 50; i++)
            //{
            //    new Thread(DoRun1).Start();
            //}

            //for (int i = 0; i < 50; i++)
            //{
            //    Task.Run(() => { DoRun2(); });
            //}



            //CreateHostBuilder(args).Build().Run();
            //var source = new Dictionary<string, string>
            //{
            //    ["A:B:C"] = "ABC"
            //};

            //var root = new ConfigurationBuilder()
            //    .AddInMemoryCollection(source)
            //    .Build();

            //var section1 = root.GetSection("A:B:C");  //A:B:C
            //var section2 = root.GetSection("A:B").GetSection("C");  //A:C->C
            //var section3 = root.GetSection("A").GetSection("B:C");  //A->B:C


            ////CreateHostBuilder(args).Build().Run();

            //int i = 1;
            //int j = 1;
            //var s= i.Equals(j);

            //Profile profile = new Profile();
            //var pro= profile.Equals(profile);

            var configuration = new ConfigurationBuilder()
            .AddJsonFile("profile.json",false,true)
            .Build();

            #region 单个Options
            //var profile = new ServiceCollection()
            //.AddOptions()
            //.Configure<Profile>(configuration)
            //.BuildServiceProvider()
            //.GetRequiredService<IOptions<Profile>>()
            //.Value;
            #endregion

            ServiceProvider serviceProvider = new ServiceCollection()
                .AddOptions()
                .Configure<Profile>("my",a =>
                {
                    a.Age = 18;
                    a.ContactInfo = new ContactInfo() { EmailAddress = "2323@qq.com" };
                })
                .Configure<Profile>("foo", configuration.GetSection("foo"))
                .Configure<Profile>("bar", configuration.GetSection("bar"))
               .BuildServiceProvider();

            serviceProvider.GetRequiredService<IOptionsMonitor<Profile>>()
                .OnChange(profile => {
                    Console.WriteLine(profile.ContactInfo.EmailAddress);

               });
            var options = serviceProvider.GetRequiredService<IOptionsSnapshot<Profile>>();
            var fooProfile= options.Get("foo");
            var barProfile = options.Get("bar");
            var myProfile = options.Get("my");
            //Profile profile= configuration.Get<Profile>();
            Console.WriteLine(fooProfile.ContactInfo.EmailAddress);
            Console.WriteLine(barProfile.ContactInfo.EmailAddress);
            Console.WriteLine(myProfile.ContactInfo.EmailAddress);
            Console.ReadLine();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
