using MediatR;

namespace DevFreela.Application.Notification.ProjectCreated
{
    public class GenerateProjectBoardHandler : INotificationHandler<ProjectCreateNotification>
    {
        public Task Handle(ProjectCreateNotification notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Criando painel para o projeto {notification.Title}");
            
            return Task.CompletedTask;
        }
    }
}
