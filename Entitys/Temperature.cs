using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SensorRecord.Entitys
{
    public class Temperature
    {
        
        [Column("HumidityValue", TypeName ="double")]
        public double HumidityValue { get; set; }
        
        [Column("TemperatureValue", TypeName = "double")]
        public double TemperatureValue { get; set; }
        
        [Column("WriteTime", TypeName = "varchar(255)")]
        public DateTime WriteTime { get; set; } 

        public Temperature()
        {

        }

        public override string ToString()
        {
            return "湿度: " + HumidityValue +
                "% 温度: " + TemperatureValue +
                " 时间: " + WriteTime.ToString();
        }
    }
}
