using Coravel.Invocable;
using System.Threading.Tasks;

namespace minhCore1.Services
{
    public class ScheduledEmailTask : IInvocable
    {
        public async Task Invoke()
        {
            await SendGridService.SendTestEmail();
        }
    }
}
