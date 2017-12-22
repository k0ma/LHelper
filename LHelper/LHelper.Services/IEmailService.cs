namespace LHelper.Services
{
    using System.Threading.Tasks;

    public interface IEmailService
    {
        Task SendEmails(int topicId, int categoryId, string url);
    }
}
