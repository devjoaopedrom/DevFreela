﻿using DevFreela.Core.Entities;
using System.Net.Sockets;

namespace DevFreela.Application.Models
{
    public class CreateUserInputModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public User ToEntity()
    => new(FullName, Email, BirthDate );
    }
}
