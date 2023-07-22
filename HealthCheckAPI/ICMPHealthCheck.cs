using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Net.NetworkInformation;

namespace HealthCheckAPI
{
  public class ICMPHealthCheck: IHealthCheck
  {
    private readonly string Host;
    private readonly int HealthRoungtripTime;

    public ICMPHealthCheck(string host, int healthyRoundtripTime)
    { 
      Host = host;
      HealthRoungtripTime = healthyRoundtripTime;
    }

    /// <summary>
    /// Interface requires a single async method which we use to determine if the ICMP request was successful or not. 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
      try
      {
        using var ping = new Ping();
        var reply = await ping.SendPingAsync(Host);

        switch (reply.Status)
        {
          case IPStatus.Success:
            var msg = $"ICMP to {Host} took {reply.RoundtripTime} ms.";
            return (reply.RoundtripTime > HealthRoungtripTime) ? HealthCheckResult.Degraded(msg) : HealthCheckResult.Healthy(msg);
          default:
            var err = $"ICMP to {Host} failed: {reply.Status}";
            return HealthCheckResult.Unhealthy(err);
        }
      }
      catch (Exception ex)
      {
        var err = $"ICMP to {Host} failed: {ex.Message}";
        return HealthCheckResult.Unhealthy(err);
      }
    }
  }
}
