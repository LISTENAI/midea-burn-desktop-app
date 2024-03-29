## 聆思模组烧录工具

[![test](https://github.com/LISTENAI/midea-burn-desktop-app/actions/workflows/build.yml/badge.svg?branch=master)](https://github.com/LISTENAI/midea-burn-desktop-app/actions/workflows/build.yml)

### 概述

本工具允许工厂产线快速烧录 Midea_CSK6_WB01_EVB_1V0 模组的 CSK6 及 WB01 的固件，配合MES系统的情况下还可以记录产线烧录状态。

### 开发环境

- Visual Studio 2022 (17.4)
- .Net 6.0.11 (SDK 6.0.403)
- SQL Server 2022

### 最终用户系统要求及相关前置依赖包要求

- 操作系统：Windows 10/11 (推荐) / Windows 7 SP1 (最低)
- 屏幕分辨率：1920 x 1080 (推荐) / 1366 x 768 (最低)
- 前置依赖包：
    - .Net Desktop Runtime 6.0.12 ([64位](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-desktop-6.0.12-windows-x64-installer) | [32位](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-desktop-6.0.12-windows-x86-installer))
    - Microsoft Visual C++ 2015-2019 Redistributable ([64位](https://aka.ms/vs/16/release/vc_redist.x64.exe) | [32位](https://aka.ms/vs/16/release/vc_redist.x86.exe))
- 如果最终用户使用的操作系统为 **Windows 7** ，ta们还须安装以下前置依赖包：
    - KB3063858 ([64位](https://www.microsoft.com/download/details.aspx?id=47442) | [32位](https://www.microsoft.com/download/details.aspx?id=47409))
    - KB2999226 ([64位](https://www.microsoft.com/en-us/download/details.aspx?id=49093) | [32位](https://www.microsoft.com/en-us/download/details.aspx?id=49077))

### 快速开始

要快速开始烧录工具的部署并开始第一次烧录，用户可以用以下步骤进行准备

1. 【配置MES记录】点击 `MES记录` ，按产线要求填入对应的信息。如果需要从SQL数据库中导入相关信息，用户需要点击 `从数据库导入` ，并填写 `MES指令单号` 和数据库连接信息，并点击导入。确认信息无误后，点击 `确认` 。<font color="red"><b>如配置正确， `MES记录` 按钮会变为灰色。</b></font>
1. 【配置需要烧录的固件】点击 `浏览` ，按产线要求选择对应的固件包。<font color="red"><b>如固件包选择正确，`浏览` 按钮会变为灰色</b></font>，工具窗口下方会显示固件包相关信息。
1. 【配置串口信息】在主窗口中，用户需要按照产线要求正确填入 `串口` 及 `波特率` ，而 `数据位`、`校验位`、`停止位` 保持默认，不需要更改。如需保存串口配置，请勾选 `是否默认`。如果需要指定 `产品序列号` ，用户应填入产线要求的产品序列号，否则请留空，由工具自动填入。
1. 给本批次待烧录的模组上电。
1. 确定上述配置无误后，点击 `烧录` ，工具将把固件包的内容按配置烧录到模组中。
1. 给本批次待烧录的模组断电并更换下一批次模组，然后重复步骤4-6。

### 错误代码

| 代码 | 业务 | 错误提示 | 解释与解决方案 |
| :--- | :--- | :--- | :--- |
| 101 | 固件选择 | 固件包无法解压 | 检查固件包是否为zip格式，并且没有损坏。|
| 102 | 固件选择 | 配置文件无法解析 | 配置文件内容不正确，请重新下载固件包。|
| 103 | 固件选择 | 固件xxx不存在 | 配置文件中提及的固件文件不存在，请重新下载固件包。|
| 104 | 固件选择 | 固件xxx大小不正确 | 配置文件中提及的固件文件大小与配置文件描述的不一致<br>（文件损坏？），请重新下载固件包。|
| 105 | 固件选择 | 固件xxx校验失败 | 配置文件中提及的固件文件MD5值不匹配，<br>请重新下载固件包。|
| 201 | MES配置 | 请输入MES指令单号<br>数据库连接信息不完整，请补充。<br>数据库表名不正确<br>请完全填写MES记录需要的数据后再点击确认 | 必要参数缺失，请按提示填写。|
| 202 | MES配置 | 根据MES指令单号，没有找到任何记录。| 请检查MES指令单号是否正确，如果确实正确，<br>请联系MES数据库管理添加数据。|
| 203 | MES配置 | 无法连接数据库，请联系运维处理。| 请先检查网络是否正常，如果正常但依然无法连接，<br>请联系运维处理。|
| 204 | MES配置 | 连接数据库出现异常，请联系运维处理。| 请先检查网络是否正常，如果正常但依然无法连接，<br>请联系运维处理。|
| 401 | 固件打包 | 请输入版本号。 | 请填写CSK/ASR/固件包的版本号。|
| 402 | 固件打包 | 请选择固件包保存位置 | 请选择固件包保存位置。 |
| 403 | 固件打包 | 请导入正确的 CSK6/ASR 固件 | 选择了错误的文件作为固件，请再次检查。|
| 404 | 固件打包 | 固件打包失败 | 原因可能是系统临时目录/zip保存目录不可写，请检查权限。|
| 499 | 固件打包 | 打包固件时出现了意外状况 | 通常不会出现，请把错误信息完整反馈给研发。|
