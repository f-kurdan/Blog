namespace Blog.Services.Email
{
    public interface IEmailService
    {
        public Task SendEmail(string email, string subject, string message);
    }
}
