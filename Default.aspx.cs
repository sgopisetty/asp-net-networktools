using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.NetworkInformation;
using System.Diagnostics;
using System.Text;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblIPv4Address.Text = GetLocalIPv4();
    }
    private string GetLocalIPv4()
    {
        string localIPswithGateways = string.Empty;

        var interfaces = NetworkInterface.GetAllNetworkInterfaces().Where(nic => nic.OperationalStatus == OperationalStatus.Up);
        foreach (var nic in interfaces)
        {
            var ipProps = nic.GetIPProperties();


            if (ipProps.GatewayAddresses.FirstOrDefault() != null)
            {
                var gateways = ipProps.GatewayAddresses.Select(g => g.Address).ToList();
                if (gateways.Count() > 0)
                {
                    var ipv4 = ipProps.UnicastAddresses.FirstOrDefault(ip => ip.Address.AddressFamily == AddressFamily.InterNetwork);
                    if (ipv4 != null)
                    {
                        localIPswithGateways += "<br/>-" + ipv4.Address.ToString();
                    }
                }
            }
        }
        return localIPswithGateways;
    }

    protected void btnPing_Click(object sender, EventArgs e)
    {
        lblPingResult.Text = ExecutePing(tbRemoteIP.Text).Replace(Environment.NewLine, "<br />");
    }

    protected void btnTraceRoute_Click(object sender, EventArgs e)
    {
        lblTraceRouteResult.Text = ExecuteTraceRoute(tbTraceRouteIP.Text).Replace(Environment.NewLine, "<br />");
    }

    private string ExecuteTraceRoute(string ipAddress)
    {
        try
        {
            // Set up the process to execute the tracert command
            ProcessStartInfo processStartInfo = new ProcessStartInfo("tracert", "-h 4 " + ipAddress)
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = new Process())
            {
                process.StartInfo = processStartInfo;
                process.Start();

                // Read the output of the tracert command
                StringBuilder output = new StringBuilder();
                while (!process.StandardOutput.EndOfStream)
                {
                    output.AppendLine(process.StandardOutput.ReadLine());
                }

                return output.ToString();
            }
        }
        catch (Exception ex)
        {
            return string.Format(@"Error: {0}", ex.Message);
        }
    }
    private string ExecutePing(string ipAddress)
    {
        try
        {
            // Set up the process to execute the ping command
            ProcessStartInfo processStartInfo = new ProcessStartInfo("ping", ipAddress)
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = new Process())
            {
                process.StartInfo = processStartInfo;
                process.Start();

                // Read the output of the ping command
                StringBuilder output = new StringBuilder();
                while (!process.StandardOutput.EndOfStream)
                {
                    output.AppendLine(process.StandardOutput.ReadLine());
                }

                return output.ToString();
            }
        }
        catch (Exception ex)
        {
            return string.Format(@"Error: {0}", ex.Message);
        }
    }
}