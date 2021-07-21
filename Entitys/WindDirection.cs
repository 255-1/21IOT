using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SensorRecord.Entitys
{
    public class WindDirection
    {
        [Column("WindDirectionAngle", TypeName = "int")]
        public double WindDirectionAngle { get; set; }

        [Column("WindDirectionGrade", TypeName = "int")]
        public double WindDirectionGrade { get; set; }

        [Column("WriteTime", TypeName = "varchar(255")]
        public DateTime WriteTime { get; set; }

        public WindDirection()
        {

        }

        public override string ToString()
        {
            return "风向(角度): " + WindDirectionAngle +
                " 风向(档): " + WindDirectionGrade +
                " 时间: " + WriteTime.ToString();
        }
    }
}
