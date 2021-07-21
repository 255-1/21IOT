using Microsoft.AspNetCore.Mvc;
using SensorRecord.Entitys;
using SensorRecord.Helper;
using SensorRecord.Services;
using System.Net;

namespace SensorRecord.Controllers
{
    [ApiController]
    [Route("monitor/dipAngle")]
    public class DipAngleMonitorController : ControllerBase
    {
        //设定塔石服务器的ip地址，端口，和查询时间（单位：秒）
        ClientSocket clientSocket_x;
        ClientSocket clientSocket_y;

        private IPAddress iPAddress = IPAddress.Parse("192.168.0.82");
        private int port = 10123;
        private int queryInterval_s = 2;

        //数据层服务获取
        private readonly IRepository _repository;
        public DipAngleMonitorController(IRepository repository)
        {
            _repository = repository;
        }

        //Get了Url后开始监控风向的数据
        [HttpGet(Name = nameof(StartDipAngleMonitor))]
        public void StartDipAngleMonitor()
        {

            //每次重新建立TCP，不要把new ClientSocket移到外面，会有信息串流问题
            while (true)
            {
                //本机作为client向塔石server请求数据
                clientSocket_x = new ClientSocket(_repository);
                clientSocket_x.Init(iPAddress, port, SensorEnum.xAxisDipAngle);
                clientSocket_x.SendMessage();
                clientSocket_x.RecvMessage();
                clientSocket_x.StopConnect();

                clientSocket_y = new ClientSocket(_repository);
                clientSocket_y.Init(iPAddress, port, SensorEnum.yAxisDipAngle);
                clientSocket_y.SendMessage();
                clientSocket_y.RecvMessage();
                clientSocket_y.StopConnect();
                //倾角传感器由于要两条命令，需要额外使用save
                if (DipAngle.DipAngleIsValid())
                {
                    _repository.AddDipAngle(DipAngle.GetDipAngle());
                    _repository.SaveAsync();
                }

                //如果不合法，初始化DipAngle类内置的dipAngle对象
                DipAngle.SetXAxisAngleToDipAngle((double)-999);
                DipAngle.SetYAxisAngleToDipAngle((double)-999);
                System.Threading.Thread.Sleep(1000 * queryInterval_s);
            }
        }

        //手动结束监控
        [HttpDelete(Name = nameof(StopDipAngleMonitor))]
        public IActionResult StopDipAngleMonitor()
        {
            clientSocket_x.StopConnect();
            clientSocket_y.StopConnect();
            return NoContent();
        }
    }
}
