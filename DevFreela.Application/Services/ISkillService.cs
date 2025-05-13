using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevFreela.Application.Models;
using Microsoft.AspNetCore.Http;

namespace DevFreela.Application.Services
{
    public interface ISkillService
    {
        ResultViewModel<List<SkillViewModel>> GetAll(string search = "");
        ResultViewModel<int> Insert(CreateSkillInputModel model);

    }
}
