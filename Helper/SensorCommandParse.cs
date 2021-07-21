using SensorRecord.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SensorRecord.Helper
{
    /// <summary>
    /// 传感器查询数据的命令以及解析
    /// </summary>
    public static class SensorCommandParse
    {
        //相关查询命令，详情看各个产品的说明书
        public static Byte[] windDirectionQueryCommand = new Byte[] { 0x01, 0x03, 0x00, 0x00, 0x00, 0x02, 0xC4, 0x0B };
        public static Byte[] temperatureQueryCommand = new Byte[] { 0x01, 0x03, 0x00, 0x00, 0x00, 0x02, 0xC4, 0x0B };
        public static Byte[] windSpeedQueryCommand = new Byte[] { 0x01, 0x03, 0x00, 0x00, 0x00, 0x01, 0x84, 0x0A };
        public static Byte[] xAxisAngleQueryCommand = new Byte[] { 0x01, 0x03, 0x00, 0x00, 0x00, 0x01, 0x84, 0x0A };
        public static Byte[] yAxisAngleQueryCommand = new Byte[] { 0x01, 0x03, 0x00, 0x01, 0x00, 0x01, 0xD5, 0xCA };

        /// <summary>
        /// 获取传感器查询命令
        /// </summary>
        /// <param name="sensorType">通过传感器枚举类的内容获得对应的查询命令</param>
        /// <returns></returns>
        public static Byte[] SendParse(SensorEnum sensorType)
        {
            switch (sensorType)
            {
                case SensorEnum.WindDirection:
                    return windDirectionQueryCommand;
                case SensorEnum.Temperature:
                    return temperatureQueryCommand;
                case SensorEnum.WindSpeed:
                    return windSpeedQueryCommand;
                case SensorEnum.xAxisDipAngle:
                    return xAxisAngleQueryCommand;
                case SensorEnum.yAxisDipAngle:
                    return yAxisAngleQueryCommand;
                default:
                    return null;
            }
        }

        /// <summary>
        /// 解析从塔石server中接收到的数据
        /// </summary>
        /// <param name="rec"></param>
        /// <param name="sensorType"></param>
        /// <returns></returns>
        public static object ReceiveParse(Byte[] rec, SensorEnum sensorType)
        {
            switch (sensorType)
            {
                case SensorEnum.WindDirection:
                    return ReciveByteToWindDirection(rec);
                case SensorEnum.Temperature:
                    return ReciveByteToTemperature(rec);
                case SensorEnum.WindSpeed:
                    return ReciveByteToWindSpeed(rec);
                case SensorEnum.xAxisDipAngle:
                    return ReciveByteToXAxisAngle(rec);
                case SensorEnum.yAxisDipAngle:
                    return ReciveByteToYAxisAngle(rec);
                default:
                    return null;
            }
        }

        

        //将塔石server相应的数据解析成y倾角类型
        public static double ReciveByteToYAxisAngle(Byte[] rec)
        {
            double AxisAngle = YAxisAngleTransform(rec);

            if (yAxisAngleIsValid(AxisAngle))
            {
                return AxisAngle;
            }
            Console.WriteLine("y轴倾角数据不合法");
            return -999;

        }

        

        //将塔石server相应的数据解析成x倾角类型
        public static double ReciveByteToXAxisAngle(Byte[] rec)
        {
            double AxisAngle = XAxisAngleTransform(rec);

            if (xAxisAngleIsValid(AxisAngle))
            {
                return AxisAngle;
            }
            Console.WriteLine("x轴倾角数据不合法");
            return -999;

        }

        

        //将塔石server相应的数据解析成WindSpeed类型
        public static WindSpeed ReciveByteToWindSpeed(Byte[] rec)
        {
            double windSpeedValue = (rec[3] * 256 + rec[4]) / 10.0;

            if (WinSpeedIsValid(windSpeedValue))
            {
                return new WindSpeed
                {
                    WindSpeedValue = windSpeedValue
                };
            }
            Console.WriteLine("风速数据不合法");
            return null;

        }

        //将塔石server相应的数据解析成Temperature类型
        public static Temperature ReciveByteToTemperature(Byte[] rec)
        {
            double humidityValue = (rec[3] * 256 + rec[4]) / 10.0;
            double temperatureValue = (rec[5] * 256 + rec[6]) / 10.0;

            if (TemperatureIsValid(humidityValue, temperatureValue))
            {
                return new Temperature
                {
                    HumidityValue = humidityValue,
                    TemperatureValue = temperatureValue
                };
            }
            Console.WriteLine("温度数据不合法");
            return null;

        }

        //将塔石server相应的数据解析成WindDirection类型
        public static WindDirection ReciveByteToWindDirection(Byte[] rec)
        {
            int windDirectionGrade = rec[3] * 256 + rec[4];
            int windDirectionAngle = rec[5] * 256 + rec[6];

            if (WindDirectionIsValid(windDirectionGrade, windDirectionAngle))
            {
                return new WindDirection
                {
                    WindDirectionGrade = windDirectionGrade,
                    WindDirectionAngle = windDirectionAngle
                };
            }
            Console.WriteLine("风向数据不合法");
            return null;
        
        }


        //检测WindDirection的数据是否合法
        private static bool WindDirectionIsValid(int windDirectionGrade, int windDirectionAngle)
        {
            return windDirectionAngle >= 0 && windDirectionAngle <= 360
                && windDirectionGrade >= 0 && windDirectionGrade <= 7;
        }

        //检测WinSpeed的数据是否合法
        private static bool WinSpeedIsValid(double windSpeedValue)
        {
            return windSpeedValue >= 0 && windSpeedValue <= 30;
        }
        //检测Temperature的数据是否合法
        private static bool TemperatureIsValid(double humidityValue, double temperatureValue)
        {
            return humidityValue >= 30 && humidityValue <= 80
                && temperatureValue >= 1 && temperatureValue <= 50;
        }
        //检测y轴倾角的数据是否合法
        private static bool yAxisAngleIsValid(double yAxisAngle)
        {
            return yAxisAngle >= -90 && yAxisAngle <= 90;
        }
        //检测x轴倾角的数据是否合法
        private static bool xAxisAngleIsValid(double xAxisAngle)
        {
            return xAxisAngle >= -180 && xAxisAngle <= 180;
        }



        //倾角范围为x:-180-180, y: -90-90
        //倾角数据为负，为补码表示

        //转换x轴倾角数值为合法值
        private static double XAxisAngleTransform(Byte[] rec)
        {
            int data = rec[3] * 256 + rec[4];
            //数据16位，1位符号位和15位数字位，表示上线为2^15
            int ceiling = (int)Math.Pow(2, 15);
            //超出x轴的正数范围，计算补码数值
            if (data > 18000)
            {
                int overflow = data - ceiling;
                data = overflow - ceiling;
            }
            return data / 100.0;
        }

        //转换y轴倾角数值为合法值
        private static double YAxisAngleTransform(Byte[] rec)
        {
            int data = rec[3] * 256 + rec[4];
            //数据16位，1位符号位和15位数字位，表示上线为2^15
            int ceiling = (int)Math.Pow(2, 15);
            //超出y轴的正数范围，计算补码数值
            if (data > 9000)
            {
                int overflow = data - ceiling;
                data = overflow - ceiling;
            }
            return data / 100.0;
        }
    }
}
