# V0.01支持

## 支持功能
    读取温湿度传感器，风速传感器和风向传感器，并且存入数据库中
##版本：
    VisualStduio 2019+
    MySQL 5.7+
    .Net 3.1+(长期支持)

    Nuget包版本
        Microsoft.EntityFrameworkCore 3.1.17
        Microsoft.EntityFrameworkCore.Tools 3.1.17
        MySql.Data 8.0.25
        MySql.Data.EntityFrameworkCore 8.0.22
## 数据库脚本
```
create database sensor;

use sensor;

create table temperature(
WriteTime varchar(255) primary key,
TemperatureValue double,
HumidityValue double);

create table windDirection(
WriteTime varchar(255) primary key,
WindDirectionGrade int,
WindDirectionAngle int);

create table windSpeed(
WriteTime varchar(255) primary key,
WindSpeedValue double);

create table dipAngle(
WriteTime varchar(255) primary key,
xAxisAngle double,
yAxisAngle double);
```

## 塔石设置
    设备工作模式： TCP Server模式
    波特率：设备波特率
    本机地址：不冲突情况下自行设置

## 工程设置
    appsettings.json中修改数据库用户名和密码


## 添加更多传感器监控
    1.建立好对应的数据库表信息
    2.在Entitys/SensorEnum中添加新的传感器变量名
    3.在Entitys下添加新的传感器类
    4.在Data/SensorDbContext中添加新数据库表名字以及模型属性限制条件
    5.在Services/Repository中添加数据库相关的操作
    6.Helper/SensorCommandParse中添加查询命令数组，以及其中两个switch语句补全
    7.Helper/ClientSocket中switch语句添加新的传感器数据库操作
    8.Controllers中添加新的MonitorController监控组件

## Todo
    多个传感器测试(x)
    多个塔石的数据传输测试(x)