using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace DevFreela.Application.Notification.ProjectCreated
{
    public class FreelancerNotificationHandler : INotificationHandler<ProjectCreateNotification>
    {
        public Task Handle(ProjectCreateNotification notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Notificando os freelancers sobre o projeto {notification.Title}");

            return Task.CompletedTask;
        }
    }
}
