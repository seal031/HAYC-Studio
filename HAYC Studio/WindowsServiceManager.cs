using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Windows服务管理类
/// </summary>
public class WindowsServiceManager
{
    private List<ServiceController> _controllerList;

    public WindowsServiceManager(List<string> serviceNameList)
    {
        _controllerList = new List<ServiceController>();
        foreach (string serviceName in serviceNameList)
        {
            if (_controllerList.FirstOrDefault(s => s.ServiceName == serviceName) == null)
            {
                ServiceController controller = new ServiceController(serviceName);
                _controllerList.Add(controller);
            }
        }
    }

    public void StopService(string serviceName)
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
                if (controller.Status == ServiceControllerStatus.Running)
                {
                    controller.Stop();
                    controller.WaitForStatus(ServiceControllerStatus.Stopped);
                    controller.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("停止服务" + serviceName + "时发生异常。异常原因：" + ex.Message);
            }
        }
    }

    public void StartService(string serviceName,bool needRestart=false)
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
            //throw new Exception("未找到名为" + serviceName + "的服务");
            return false;
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
