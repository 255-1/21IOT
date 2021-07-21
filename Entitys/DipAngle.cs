using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SensorRecord.Entitys
{
    public class DipAngle
    {
        [Column("xAxisAngle", TypeName = "double")]
        public double xAxisAngle { get; set; }

        [Column("yAxisAngle", TypeName = "double")]
        public double yAxisAngle { get; set; }

        [Column("WriteTime", TypeName = "varchar(255)")]
        public DateTime WriteTime { get; set; }

        //由于倾角需要读x, y两个值，所以需要等到2个数据都有了才可以存入数据库,如果dipAngle有值未读入，则为-999
        private static DipAngle dipAngle = new DipAngle() { xAxisAngle = -999, yAxisAngle = -999 };
        //设置x轴倾角
        public static void SetXAxisAngleToDipAngle(double xAxisAngle)
        {
            dipAngle.xAxisAngle = xAxisAngle;
        }
        //设置y轴倾角
        public static void SetYAxisAngleToDipAngle(double yAxisAngle)
        {
            dipAngle.yAxisAngle = yAxisAngle;
        }

        //合法判断
        public static bool DipAngleIsValid()
        {
            return dipAngle.xAxisAngle != -999 && dipAngle.yAxisAngle != -999;
        }

        //获取本类中的实例dipAngle
        public static DipAngle GetDipAngle()
        {
            return dipAngle;
        }

        public override string ToString()
        {
            return "x轴倾角: " + xAxisAngle +
                " y轴倾角: " + yAxisAngle +
                " 时间: " + WriteTime.ToString();
        }
    }
}
