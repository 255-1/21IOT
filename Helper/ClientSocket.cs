using SensorRecord.Entitys;
using SensorRecord.Services;
using System;
using System.Net;
using System.Net.Sockets;

namespace SensorRecord.Helper
{
    public class ClientSocket
    {
        //传感器类型
        private SensorEnum sensorType;

        //绑定服务器地址和端口
        private Socket clientSocket;
        private IPAddress iPAddress;
        private int port;

        //存储服务注入
        private readonly IRepository _repository;

        public ClientSocket(IRepository repository)
        {
            _repository = repository;
        }

        //初始化操作
        public void Init(IPAddress ipaddress, 
                            int port,
                            SensorEnum sensorType)
        {
            this.iPAddress = ipaddress;
            this.port = port;
            this.sensorType = sensorType;
            
            ConnectServer();
        }
        //建立Socket
        private Socket CreateSocket()
        {
            return new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
        //连接塔石server
        public void ConnectServer()
        {
            clientSocket = CreateSocket();
            clientSocket.Connect(new IPEndPoint(iPAddress, port));
        }

        //接受来自塔石server的消息
        public void RecvMessage()
        {
            Byte[] rec = new Byte[15];
            int count = clientSocket.Receive(rec);

            //按照传感器类型，解析其中的数据
            object o = SensorCommandParse.ReceiveParse(rec, sensorType);


            if (o == null)
            {
                return;
            }

            //将接收到的object转换成对应的类
            switch (sensorType)
            {
                case SensorEnum.WindDirection:
                    _repository.AddWindDirection((WindDirection)o);
                    break;
                case SensorEnum.Temperature:
                    _repository.AddTemperature((Temperature)o);
                    break;
                case SensorEnum.WindSpeed:
                    _repository.AddWindSpeed((WindSpeed)o);
                    break;
                case SensorEnum.xAxisDipAngle:
                    DipAngle.SetXAxisAngleToDipAngle((double)o);
                    break;
                case SensorEnum.yAxisDipAngle:
                    DipAngle.SetYAxisAngleToDipAngle((double)o);
                    break;
                default:
                    break;

            }

            _repository.SaveAsync();

        }

        /// <summary>
        /// 按照传感器类型向塔石server发送查询命令
        /// </summary>
        public void SendMessage()
        {
            Byte[] queryCommand = SensorCommandParse.SendParse(sensorType);
            if (queryCommand == null)
            {
                throw new ArgumentNullException(nameof(SendMessage));
            }
            clientSocket.Send(queryCommand);
        }

        //释放所有连接资源
        public void StopConnect()
        {
            clientSocket.Close();
        }

    }
}
