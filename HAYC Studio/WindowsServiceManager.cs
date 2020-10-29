using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/// <summary>
/// Windows服务管理类
/// </summary>
public class WindowsServiceManager
{
    private List<ServiceController> _controllerList;

    public WindowsServiceManager(List<string> serviceNameList)
    {
        _controllerList = new List<ServiceController>();
        try
        {
            foreach (string serviceName in serviceNameList)
            {
                if (_controllerList.FirstOrDefault(s => s.ServiceName == serviceName) == null)
                {
                    ServiceController controller = new ServiceController(serviceName);
                    _controllerList.Add(controller);
                }
            }
        }
        catch (Exception ex)
        {
            LogHelper.WriteLog("初始化windows服务控制器失败：" + ex.Message);
        }
    }

    public void StopService(string serviceName)
    {
        var controller = _controllerList.FirstOrDefault(s => s.ServiceName == serviceName);
        if (controller == null)
        {
            LogHelper.WriteLog("未找到名为" + serviceName + "的服务");
            throw new Exception("未找到名为" + serviceName + "的服务，请确保本系统相关的windows服务都已正确安装");
        }
        else
        {
            try
            {
                if (controller.Status == ServiceControllerStatus.Running)
                {
                    controller.Stop();
                    controller.WaitForStatus(ServiceControllerStatus.Stopped);
                    controller.Close();
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("停止服务" + serviceName + "时发生异常。异常原因：" + ex.Message);
                throw new Exception("停止服务" + serviceName + "时发生异常。异常原因：" + ex.Message);
            }
        }
    }

    public void StartService(string serviceName,bool needRestart=false)
    {
        var controller = _controllerList.FirstOrDefault(s => s.ServiceName == serviceName);
        if (controller == null)
        {
            LogHelper.WriteLog("未找到名为" + serviceName + "的服务");
            throw new Exception("未找到名为" + serviceName + "的服务，请确保本系统相关的windows服务都已正确安装");
        }
        else
        {
            try
            {
                if (controller.Status == ServiceControllerStatus.Stopped)
                {
                    controller.Start();
                    controller.WaitForStatus(ServiceControllerStatus.Running);
                    controller.Close();
                }
                else
                {
                    if (needRestart)
                    {
                        controller.Stop();
                        controller.WaitForStatus(ServiceControllerStatus.Stopped);
                        controller.Start();
                        controller.WaitForStatus(ServiceControllerStatus.Running);
                        controller.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("启动服务" + serviceName + "时发生异常。异常原因：" + ex.Message);
                throw new Exception("启动服务" + serviceName + "时发生异常。异常原因：" + ex.Message);
            }
        }
    }

    public void ResetService(string serviceName)
    {
        var controller = _controllerList.FirstOrDefault(s => s.ServiceName == serviceName);
        if (controller == null)
        {
            throw new Exception("未找到名为" + serviceName + "的服务");
        }
        else
        {
            try
            {
                if (controller.Status == ServiceControllerStatus.Stopped)
                {
                    controller.Stop();
                    controller.WaitForStatus(ServiceControllerStatus.Stopped);
                    controller.Start();
                    controller.WaitForStatus(ServiceControllerStatus.Running);
                    controller.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("重启服务" + serviceName + "时发生异常。异常原因：" + ex.Message);
            }
        }
    }

    public bool ServiceIsRunning(string serviceName)
    {
        ServiceController controller = new ServiceController(serviceName);
        if (controller == null)
        {
            throw new Exception("未找到名为" + serviceName + "的服务，请确保本系统相关的windows服务都已正确安装");
        }
        else
        {
            if (controller.Status == ServiceControllerStatus.Running)
            {
                controller.Close();
                return true;
            }
            else
            {
                controller.Close();
                return false;
            }
        }
    }
}
