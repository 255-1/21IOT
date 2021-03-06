
using Microsoft.AspNetCore.Mvc;
using SensorRecord.Data;
using SensorRecord.Entitys;
using SensorRecord.Helper;
using SensorRecord.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SensorRecord.Controllers
{
    [ApiController]
    [Route("monitor/winddirection")]
    public class WindDirectionMonitorController:ControllerBase
    {
        //设定塔石服务器的ip地址，端口，和查询时间（单位：秒）
        ClientSocket clientSocket;
        private IPAddress iPAddress = IPAddress.Parse("192.168.0.82");
        private int port = 10123;
        private int queryInterval_s = 2;

        //数据层服务获取
        private readonly IRepository _repository;
        public WindDirectionMonitorController(IRepository repository)
        {
            _repository = repository;
        }

        //Get了Url后开始监控风向的数据
        [HttpGet(Name=nameof(StartWindDirectionMonitor))]
        public void StartWindDirectionMonitor()
        {
            //每次重新建立TCP，不要把new ClientSocket移到外面，会有信息串流问题
            while (true)
            {
                //本机作为client向塔石server请求数据
                clientSocket = new ClientSocket(_repository);
                clientSocket.Init(iPAddress, port, SensorEnum.WindDirection);
                clientSocket.SendMessage();
                clientSocket.RecvMessage();
                clientSocket.StopConnect();
                System.Threading.Thread.Sleep(1000 * queryInterval_s);
            }
        }

        //手动结束监控
        [HttpDelete(Name =nameof(StopWindDirectionMonitor))]
        public IActionResult StopWindDirectionMonitor()
        {
            clientSocket.StopConnect();
            return NoContent();
        }
    }
}
