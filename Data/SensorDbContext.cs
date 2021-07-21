using Microsoft.EntityFrameworkCore;
using SensorRecord.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SensorRecord.Data
{
    public class SensorDbContext:DbContext
    {
        public SensorDbContext(DbContextOptions<SensorDbContext> options) : base(options)
        {

        }

        //和数据库中的表名字对应
        public DbSet<Temperature> temperature { get; set; }
        public DbSet<WindDirection> winddirection { get; set; }

        public DbSet<WindSpeed> windSpeed { get; set; }

        public DbSet<DipAngle> dipAngle { get; set; }

        //合法性设置
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Temperature的限制
            modelBuilder.Entity<Temperature>().HasKey(new string[] { "WriteTime" });
            modelBuilder.Entity<Temperature>().Property(x => x.HumidityValue).IsRequired();
            modelBuilder.Entity<Temperature>().Property(x => x.TemperatureValue).IsRequired();
            modelBuilder.Entity<Temperature>().Property(x => x.WriteTime).IsRequired().HasMaxLength(255);
            //WindDirection限制
            modelBuilder.Entity<WindDirection>().HasKey(new string[] { "WriteTime" });
            modelBuilder.Entity<WindDirection>().Property(x => x.WindDirectionGrade).IsRequired();
            modelBuilder.Entity<WindDirection>().Property(x => x.WindDirectionAngle).IsRequired();
            modelBuilder.Entity<WindDirection>().Property(x => x.WriteTime).IsRequired().HasMaxLength(255);
            //WindSpeed限制
            modelBuilder.Entity<WindSpeed>().HasKey(new string[] { "WriteTime" });
            modelBuilder.Entity<WindSpeed>().Property(x => x.WriteTime).IsRequired();
            modelBuilder.Entity<WindSpeed>().Property(x => x.WindSpeedValue).IsRequired();
            //DipAngle限制
            modelBuilder.Entity<DipAngle>().HasKey(new string[] { "WriteTime" });
            modelBuilder.Entity<DipAngle>().Property(x => x.WriteTime).IsRequired();
            modelBuilder.Entity<DipAngle>().Property(x => x.xAxisAngle).IsRequired();
            modelBuilder.Entity<DipAngle>().Property(x => x.yAxisAngle).IsRequired();
        }
    }
}
