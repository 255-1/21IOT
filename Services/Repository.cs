using Microsoft.EntityFrameworkCore;
using SensorRecord.Data;
using SensorRecord.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SensorRecord.Services
{
    /// <summary>
    /// 操作Database的函数
    /// </summary>
    public class Repository : IRepository
    {
        private readonly SensorDbContext _context;

        public Repository(SensorDbContext context)
        {
            _context = context;
        }

        


        //添加倾角信息
        public async void AddDipAngle(DipAngle dipAngle)
        {
            dipAngle.WriteTime = DateTime.Now;
            await _context.dipAngle.AddAsync(dipAngle);
        }

        //获取倾角信息
        public async Task<IEnumerable<DipAngle>> GetDipAngles()
        {
            return await _context.dipAngle.ToListAsync();
        }

        //添加气温
        public async void AddTemperature(Temperature t)
        {
            if (t == null)
            {
                throw new ArgumentNullException(nameof(AddTemperature));
            }

            t.WriteTime = DateTime.Now;
            await _context.temperature.AddAsync(t);
        }

        //获取气温
        public async Task<IEnumerable<Temperature>> GetTemperatures()
        {
            return await _context.temperature.ToListAsync();
        }

        //用于Update数据
        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }

        //添加风向
        public async void AddWindDirection(WindDirection w)
        {
            if (w == null)
            {
                throw new ArgumentNullException(nameof(AddWindDirection));
            }

            w.WriteTime = DateTime.Now;
            await _context.winddirection.AddAsync(w);
        }

        //查询风向
        public async Task<IEnumerable<WindDirection>> GetWindDirections()
        {
            return await _context.winddirection.ToListAsync();
        }

        //添加风速
        public async void AddWindSpeed(WindSpeed t)
        {
            if (t == null)
            {
                throw new ArgumentNullException(nameof(AddTemperature));
            }

            t.WriteTime = DateTime.Now;
            await _context.windSpeed.AddAsync(t);
        }

        //查询风速
        public async Task<IEnumerable<WindSpeed>> GetWindSpeeds()
        {
           return await _context.windSpeed.ToListAsync();
        }
    }
}
