using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SensorRecord.Entitys
{
    public class WindSpeed
    {

        [Column("WindSpeedValue", TypeName = "double")]
        public double WindSpeedValue { get; set; }

        [Column("WriteTime", TypeName = "varchar(255")]
        public DateTime WriteTime { get; set; }

        public WindSpeed()
        {

        }

        public override string ToString()
        {
            return "风速: " + WindSpeedValue +
                " 时间: " + WriteTime.ToString();
        }
    }
}
