﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace DevFreela.Application.Notification.ProjectCreated
{
    public class ProjectCreateNotification : INotification
    {
        public ProjectCreateNotification(int id, string title, decimal totalCost)
        {
            Id = id;
            Title = title;
            TotalCost = totalCost;
        }

        public int Id { get; private set; }
        public string Title { get; private set; }
        public decimal TotalCost { get; private set; }
    }
}
