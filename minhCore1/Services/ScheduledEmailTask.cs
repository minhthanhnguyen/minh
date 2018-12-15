using Coravel.Invocable;
using System.Threading.Tasks;

namespace minhCore1.Services
{
    public class ScheduledEmailTask : IInvocable
    {
        public async Task Invoke()
        {
            //return Task.Run(() => System.Diagnostics.Debug.WriteLine("yikes"));
            await SendGridService.SendTestEmail();
        }
    }
}
