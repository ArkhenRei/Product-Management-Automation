using PMS.API.Models;

namespace PMS.API.UtilityService
{
    public interface IEmailService
    {
        void SendEmail(EmailModel emailModel);
    }
}
