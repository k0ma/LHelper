namespace LHelper.Services.Implementation
{
    using Data;
    using MailKit.Net.Smtp;
    using MimeKit;
    using System.Linq;

    class EmailService : IEmailService
    {

        private readonly LHelperDbContext db;

        public EmailService(LHelperDbContext db)
        {
            this.db = db;
        }

        public void SendEmails(int topicId, int categoryId, string url)
        {

            var trainers = this.db.Users
                .Where(u => u.Categories.Any(c => c.CategoryId == categoryId))
                .Select(u => new
                {
                    Name = u.Name,
                    Email = u.Email
                })
                .ToList();

            var category = this.db
                .Categories
                .Where(c => c.Id == categoryId)
                .Select(c => c.Name)
                .FirstOrDefault();

            url = string.Concat(url + $"/Topics/Details/{topicId}");

            var name = "Нов въпрос";
            var email = "lhelpersmtp@gmail.com";
            var link = $"<a href='{url}'>{url}</a>";
            var msg = "Здравейте , в категория " + category + " беше публикуван нов въпрос. Тук " + link + " можете да прегледате и отговорите на въпроса.";
            var message = new MimeMessage();

            foreach (var trainer in trainers)
            {
                message.From.Add(new MailboxAddress("LHelper", "lhelpersmtp@gmail.com"));
                message.To.Add(new MailboxAddress(trainer.Email));
                message.Subject = "LHelper";
                message.Body = new TextPart("html")
                {
                    Text = "From: " + name + "<br>" +
                           "Contact Information: " + email + "<br>" +
                           "Message:" + msg

                };

                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, false);
                    client.Authenticate("lhelpersmtp@gmail.com", "lhelpersmtp2017");
                    client.Send(message);
                    client.Disconnect(false);
                }
            }

            
        }
    }
}
