using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevFreela.Core;
using DevFreela.Core.Entities;
using DevFreela.Core.Enums;
using DevFreela.UnitTests.Fakes;

namespace DevFreela.UnitTests.Core
{
    public class ProjectTests
    {
        [Fact]
        public void ProjectIsCreated_Start_Success()
        {
            // Arrange
            // Inutilizado para usar o Data Faker
            //var project = new Project("Projeto A", "Descricao do projeto", 1, 2, 1000);

            var project = FakeDataHelper.CreateFakeProject();
            // Act
            project.Start();

            // Assert
            Assert.Equal(ProjectStatusEnum.InProgress, project.Status);
            Assert.NotNull(project.StartedAt);

            Assert.True(project.Status == ProjectStatusEnum.InProgress);
            Assert.False(project.StartedAt is null);
        }
        
        [Fact]
        public void ProjectIsInvalidState_Start_TrowsException()
        {
            // Arrange
            // Inutilizado para usar o Data Faker
            //var project = new Project("Projeto A", "Descricao do projeto", 1, 2, 1000);
            var project = FakeDataHelper.CreateFakeProject();
            
            project.Start();

            //Act + Assert
            Action? start = project.Start;

            var exception = Assert.Throws<InvalidOperationException>(start);
            Assert.Equal(Project.INVALID_STATE_MASSAGE, exception.Message);
        }

    }
}
