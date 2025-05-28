using MediatR;

namespace DevFreela.Application.Notification.UserCreated
{
    public class UserCreatedNotification : INotification
    {
        public string FullName { get; private set; }
        public string Email { get; private set; }
        public UserCreatedNotification(string fullName, string email)
        {
            FullName = fullName;
            Email = email;
        }
    }
}
