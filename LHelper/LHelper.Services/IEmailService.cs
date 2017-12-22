namespace LHelper.Services
{
    using System.Threading.Tasks;

    public interface IEmailService
    {
        void SendEmails(int topicId, int categoryId, string url);
    }
}
