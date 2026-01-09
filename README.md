# EWT 学习助手

![.NET Framework 4.8](https://img.shields.io/badge/.NET%20Framework-4.8-blue)
![Platform](https://img.shields.io/badge/Platform-Windows-lightgrey)
![License](https://img.shields.io/badge/License-MIT-green)

## 📖 项目简介

EWT 学习助手是一款基于 .NET Framework 4.8 开发的自动化学习工具，旨在帮助用户高效完成在线课程学习任务。

## ⚠️ 重要声明

1. **本软件仅供技术学习和研究目的使用**
2. **请严格遵守所在教育平台的使用条款和学校/机构的相关规定**
3. **不得用于任何商业用途或违反学术诚信的行为**
4. **使用本软件产生的任何后果由用户自行承担**
5. **建议仅用于个人学习时间管理，不建议完全依赖自动化工具**

## ✨ 功能特性

### 核心功能
- ✅ 自动化课程学习流程
- ✅ 视频播放进度自动完成
- ✅ 章节测验自动处理
- ✅ 作业提交辅助功能
- ✅ 学习数据统计与分析

### 辅助功能
- 🔧 多账户管理支持
- 📊 学习进度实时监控
- ⚙️ 自定义学习策略配置
- 🔔 学习状态提醒通知
- 📁 学习数据本地备份

## 🛠 系统要求

### 最低配置
- **操作系统**: Windows 7 SP1 或更高版本
- **运行时**: .NET Framework 4.8 Runtime
- **内存**: 2GB RAM
- **硬盘空间**: 100MB 可用空间
- **显示器**: 1024×768 分辨率

### 推荐配置
- **操作系统**: Windows 10/11
- **内存**: 4GB RAM 或更高
- **硬盘空间**: 500MB 可用空间
- **网络**: 稳定的互联网连接

## 📦 安装与使用

### 快速开始
1. **下载软件**
   - 访问 [Releases 页面](https://github.com/greenChlorine/ewt/releases)
   - 下载最新版本的 `EWT_Setup.exe` 或便携版 `EWT_Portable.zip`

2. **安装运行**
   - 安装版: 运行安装程序，按向导提示完成安装
   - 便携版: 解压到任意目录，运行 `EWT.exe`

3. **首次配置**
   - 启动软件，进入设置页面
   - 配置学习平台账号
   - 设置基本参数

### 从源码构建
```bash
# 克隆仓库
git clone https://github.com/greenChlorine/ewt.git
cd ewt

# 恢复 NuGet 包
nuget restore EWT.sln
# 或使用 dotnet
dotnet restore

# 编译项目
msbuild EWT.sln /p:Configuration=Release
# 或使用 Visual Studio 打开解决方案文件进行编译
