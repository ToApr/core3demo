using Consul;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicorServices.Demo.Util
{
    public static  class ConsulHelper
    {

        public static void ConsulRegist(this IConfiguration configuration)
        {

            Consul.ConsulClient consulClient = new Consul.ConsulClient(config => {
                config.Address = new Uri("http://localhost:8500/");
                config.Datacenter = "DC1";
            });
            string ip = configuration["ip"];
            int port = int.Parse(configuration["port"]);//命令行参数必须传入
            consulClient.Agent.ServiceRegister(new Consul.AgentServiceRegistration() {
                ID = "service" + Guid.NewGuid(),//唯一的---科比 奥尼尔
                Name = "DemoService",//组名称-Group  
                Address = ip,//其实应该写ip地址
                Port = port,//不同实例
                //Tags = new string[] { weight.ToString() },//标签
                Check = new AgentServiceCheck()
                {
                    Interval = TimeSpan.FromSeconds(12),//间隔12s一次
                    HTTP = $"http://{ip}:{port}/api/Health/Index",
                    Timeout = TimeSpan.FromSeconds(5),//检测等待时间
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(60)//失败后多久移除
                }

            });


        }
    }
}
