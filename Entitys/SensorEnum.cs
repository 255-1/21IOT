namespace SensorRecord.Entitys
{
    //有新的传感器先加入到此枚举类中，
    public enum SensorEnum
    {
        Temperature, //土壤温湿度传感器
        WindDirection,//风向传感器
        WindSpeed, //风速传感器


        ///如果有两种查询命令添加一个总类，以及两个查询类
        /////倾角传感器
        DipAngle,
        //x轴倾
        xAxisDipAngle,
        //y轴倾角
        yAxisDipAngle
    }
}
