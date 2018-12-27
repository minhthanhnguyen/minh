using Coravel.Invocable;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace minhCore1.Services
{
    public class ScheduledEmailTask : IInvocable
    {
        public IConfiguration Config { get; private set; }

        public ScheduledEmailTask(IConfiguration config)
        {
            Config = config;
        }

        public async Task Invoke()
        {
            //return Task.Run(() => System.Diagnostics.Debug.WriteLine("yikes"));
            string sendGridApiKey = Config["sendGridApiKey"];
            await SendGridService.SendTestEmail(sendGridApiKey);
        }
    }
}
