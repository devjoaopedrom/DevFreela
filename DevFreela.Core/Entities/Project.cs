using DevFreela.Core.Enums;

namespace DevFreela.Core.Entities
{
    public class Project : BaseEntity
    {
        public const string INVALID_STATE_MASSAGE = "Project is in invalid state";

        private object idClient;
        private int idFreelance;

        protected Project(string description)
        {
            Description = description;
        }
        public Project(string title, string description, int idClient, int idFreelancer, User freelancer, decimal totalCost)
            : base()
        {
            Title = title;
            Description = description;
            IdClient = idClient;
            IdFreelancer = idFreelancer;
            Freelancer = freelancer;
            TotalCost = totalCost;

            Status = ProjectStatusEnum.Created;
            Comments = [];
        }

        public Project(string title, string description,int idClient, int idFreelancer, decimal totalCost)
        {
            Title = title;
            Description = description;
            IdClient = idClient;
            IdFreelancer = idFreelancer;
            TotalCost = totalCost;
        }

        public string Title { get; private set; }
        public string Description { get; private set; }
        public int IdClient { get; private set; }
        public User Client { get; private set; }
        public int IdFreelancer { get; private set; }
        public User Freelancer { get; private set; }
        public decimal TotalCost { get; private set; }
        public DateTime? StartedAt { get; private set; }
        public DateTime? CompletedAt { get; private set; }
        public ProjectStatusEnum Status { get; private set; }
        public List<ProjectComment> Comments { get; private set; }

        public void Cancel()
        {
            if (Status == ProjectStatusEnum.InProgress || Status == ProjectStatusEnum.Suspended)
            {
                Status = ProjectStatusEnum.Cancelled;
            }
        }

        public void Start()
        {
            if (Status != ProjectStatusEnum.Created)
            {
                throw new InvalidOperationException(INVALID_STATE_MASSAGE);
                
            }
            Status = ProjectStatusEnum.InProgress;
            StartedAt = DateTime.Now;
        }

        public void Complete()
        {
            if (Status == ProjectStatusEnum.PaymentPeding || Status == ProjectStatusEnum.InProgress )
            {
                Status = ProjectStatusEnum.Completed;
                CompletedAt = DateTime.Now;    
            }
        }

        public void SetPaymentPeding()
        {
            if (Status == ProjectStatusEnum.InProgress)
            {
                Status = ProjectStatusEnum.InProgress;
                StartedAt = DateTime.Now;
            }
        }

        public void Update(string title, string description, decimal totalCost)
        {
            Title = title;
            Description = description;
            TotalCost = totalCost;
        }

    }
}
