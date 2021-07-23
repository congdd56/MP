using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NCrontab;
using System;
using System.Threading;
using System.Threading.Tasks;
using VAS.Dealer.Models.CRM;
using VAS.Dealer.Services.Interfaces;

namespace VAS.Dealer.Provider
{
    public class VINARegis30HostedToken : BackgroundService
    {
        private readonly IServiceScopeFactory _ServiceScopeFactory;
        private readonly IConfiguration _Configuration;
        private CrontabSchedule _Schedule;
        private readonly ILogger<VINARegis30HostedToken> _logger;
        private readonly FTPConfigModel _FTPConfigModel;
        private DateTime _nextRun;
        private string Schedule { get; set; }

        public VINARegis30HostedToken(IServiceScopeFactory IServiceScopeFactory, IConfiguration configuration,
            ILogger<VINARegis30HostedToken> logger, IOptions<FTPConfigModel> FTPConfig)
        {
            _Configuration = configuration;
            _ServiceScopeFactory = IServiceScopeFactory;
            Schedule = _FTPConfigModel.PrefixRegis30Schedule;
            _Schedule = CrontabSchedule.Parse(Schedule, new CrontabSchedule.ParseOptions { IncludingSeconds = false });
            _nextRun = _Schedule.GetNextOccurrence(DateTime.Now);
            _logger = logger;
            _FTPConfigModel = FTPConfig.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            do
            {
                var now = DateTime.Now;
                var nextrun = _Schedule.GetNextOccurrence(now);
                if (now > _nextRun)
                {
                    await Process();
                    _nextRun = _Schedule.GetNextOccurrence(DateTime.Now);
                }
                await Task.Delay(5000, stoppingToken); //5 seconds delay
            }
            while (!stoppingToken.IsCancellationRequested);
        }


        private async Task Process()
        {
            try
            {
                using (var scope = _ServiceScopeFactory.CreateScope())
                {
                    var _FTPServices = scope.ServiceProvider.GetRequiredService<IFTPServices>();
                    await _FTPServices.InitDataRegis30(DateTime.Now, DateTime.Now, "Binhncccccccccccc");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
        }
    }

    public class VINARegis1DayHostedToken : BackgroundService
    {
        private readonly IServiceScopeFactory _ServiceScopeFactory;
        private readonly IConfiguration _Configuration;
        private CrontabSchedule _Schedule;
        private readonly ILogger<VINARegis1DayHostedToken> _logger;
        private DateTime _nextRun;
        private readonly FTPConfigModel _FTPConfigModel;
        private string Schedule { get; set; }

        public VINARegis1DayHostedToken(IServiceScopeFactory IServiceScopeFactory, IConfiguration configuration,
            ILogger<VINARegis1DayHostedToken> logger, IOptions<FTPConfigModel> FTPConfig)
        {
            _Configuration = configuration;
            _ServiceScopeFactory = IServiceScopeFactory;
            Schedule = _FTPConfigModel.PrefixRegis1DaySchedule;
            _Schedule = CrontabSchedule.Parse(Schedule, new CrontabSchedule.ParseOptions { IncludingSeconds = false });
            _nextRun = _Schedule.GetNextOccurrence(DateTime.Now);
            _logger = logger;
            _FTPConfigModel = FTPConfig.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            do
            {
                var now = DateTime.Now;
                var nextrun = _Schedule.GetNextOccurrence(now);
                if (now > _nextRun)
                {
                    await Process();
                    _nextRun = _Schedule.GetNextOccurrence(DateTime.Now);
                }
                await Task.Delay(5000, stoppingToken); //5 seconds delay
            }
            while (!stoppingToken.IsCancellationRequested);
        }

        private async Task Process()
        {
            try
            {
                using (var scope = _ServiceScopeFactory.CreateScope())
                {
                    var _FTPServices = scope.ServiceProvider.GetRequiredService<IFTPServices>();
                    await _FTPServices.InitDataRegis1Day(DateTime.Now, DateTime.Now, "Binhncccccccccccc");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
        }
    }

    public class VINARenew1DayHostedToken : BackgroundService
    {
        private readonly IServiceScopeFactory _ServiceScopeFactory;
        private readonly IConfiguration _Configuration;
        private CrontabSchedule _Schedule;
        private readonly ILogger<VINARenew1DayHostedToken> _logger;
        private DateTime _nextRun;
        private readonly FTPConfigModel _FTPConfigModel;
        private string Schedule { get; set; }

        public VINARenew1DayHostedToken(IServiceScopeFactory IServiceScopeFactory, IConfiguration configuration,
            ILogger<VINARenew1DayHostedToken> logger, IOptions<FTPConfigModel> FTPConfig)
        {
            _Configuration = configuration;
            _ServiceScopeFactory = IServiceScopeFactory;
            Schedule = _FTPConfigModel.PrefixRenew1DaySchedule;
            _Schedule = CrontabSchedule.Parse(Schedule, new CrontabSchedule.ParseOptions { IncludingSeconds = false });
            _nextRun = _Schedule.GetNextOccurrence(DateTime.Now);
            _logger = logger;
            _FTPConfigModel = FTPConfig.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            do
            {
                var now = DateTime.Now;
                var nextrun = _Schedule.GetNextOccurrence(now);
                if (now > _nextRun)
                {
                    await Process();
                    _nextRun = _Schedule.GetNextOccurrence(DateTime.Now);
                }
                await Task.Delay(5000, stoppingToken); //5 seconds delay
            }
            while (!stoppingToken.IsCancellationRequested);
        }

        private async Task Process()
        {
            try
            {
                using (var scope = _ServiceScopeFactory.CreateScope())
                {
                    var _FTPServices = scope.ServiceProvider.GetRequiredService<IFTPServices>();
                    await _FTPServices.InitDataRenew1Day(DateTime.Now, DateTime.Now, "Binhncccccccccccc");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
        }
    }
}
