using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevFreela.Application.Notification.ProjectCreated;
using MediatR;

namespace DevFreela.Application.Notification.UserCreated
{
    public class EmailSendNoticeHandler : INotificationHandler<UserCreatedNotification>
    {
        public Task Handle(UserCreatedNotification notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"enviando um e-mail no replay para o endereço {notification.Email} que o usuario {notification.FullName} foi criado ");

            return Task.CompletedTask;
        }
    }
}
