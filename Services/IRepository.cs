using SensorRecord.Entitys;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SensorRecord.Services
{
    /// <summary>
    /// 提取出Repository的接口，用于Startup中注册服务，让其他类能轻松调用相关数据库操作
    /// </summary>
    public interface IRepository
    {
        void AddDipAngle(DipAngle dipAngle);
        void AddTemperature(Temperature t);
        void AddWindDirection(WindDirection w);
        void AddWindSpeed(WindSpeed t);
        Task<IEnumerable<DipAngle>> GetDipAngles();
        Task<IEnumerable<Temperature>> GetTemperatures();
        Task<IEnumerable<WindDirection>> GetWindDirections();
        Task<IEnumerable<WindSpeed>> GetWindSpeeds();
        Task<bool> SaveAsync();
    }
}