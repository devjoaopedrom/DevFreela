using DevFreela.Application.Commands.DeleteProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.UnitTests.Fakes;
using Moq;
using NSubstitute;

namespace DevFreela.UnitTests.Application
{
    public class DeleteProjectHandlerTests
    {

        [Fact]
        public async Task ProjectExists_Delete_Success_NSubstitute()
        {
            const int ID = 1;
            // Arrange
            //Inutilizado para uso do Data Faker
            //var project = new Project("Projeto A", "Descricao de Projeto", 1, 2, 10000);

            var project = FakeDataHelper.CreateFakeProject();
            var repository = Substitute.For<IProjectRepository>();
            repository.GetById(ID).Returns(Task.FromResult((Project?)project));
            repository.Update(Arg.Any<Project>()).Returns(Task.CompletedTask);

            var handler = new DeleteProjectHandler(repository);

            var command = new DeleteProjectCommand(1);

            // Act
            var result = await handler.Handle(command, new CancellationToken());

            // Assert
            Assert.True(result.IsSuccess);
            await repository.Received(ID).GetById(ID);
            await repository.Received(ID).Update(Arg.Any<Project>());
        }
        
        [Fact]
        public async Task ProjectExists_Delete_Success_Moq()
        {
            const int ID = 1;
            // Arrange
            //Inutilizado para uso do Data Faker
            //var project = new Project("Projeto A", "Descricao de Projeto", 1, 2, 10000);
            
            var project = FakeDataHelper.CreateFakeProject();
            var repository = Mock.Of<IProjectRepository>(p =>
                p.GetById(It.IsAny<int>()) == Task.FromResult(project) &&
                p.Update(It.IsAny<Project>()) == Task.CompletedTask
                );

            var handler = new DeleteProjectHandler(repository);

            var command = new DeleteProjectCommand(1);

            // Act
            var result = await handler.Handle(command, new CancellationToken());

            // Assert
            Assert.True(result.IsSuccess);
            Mock.Get(repository).Verify(r => r.GetById(1), Times.Once);
            Mock.Get(repository).Verify(r => r.Update(It.IsAny<Project>()), Times.Once);
        }


        [Fact]
        public async Task ProjectDoesNotExist_Delete_Error_NSubstitute()
        {
            const int ID = 1;
            // Arrange
            //Inutilizado para uso do Data Faker
            //var project = new Project("Projeto A", "Descricao de Projeto", 1 , 2, 10000);

            var project = FakeDataHelper.CreateFakeProject();
            var repository = Substitute.For<IProjectRepository>();
            repository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Project?)null)); 

            var handler = new DeleteProjectHandler(repository);

            var command = new DeleteProjectCommand(1);

            // Act
            var result = await handler.Handle(command, new CancellationToken());

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(DeleteProjectHandler.PROJECT_NOT_FOUND_MESSAGE, result.Message);
            await repository.Received(ID).GetById(Arg.Any<int>());
            await repository.DidNotReceive().Update(Arg.Any<Project>());
        }
        [Fact]
        public async Task ProjectDoesNotExist_Delete_Error_Moq()
        {
            const int ID = 1;
            // Arrange
            var repository = Mock.Of<IProjectRepository>(r =>
                r.GetById(It.IsAny<int>()) == Task.FromResult((Project)null)
            );

            var handler = new DeleteProjectHandler(repository);

            var command = new DeleteProjectCommand(1);

            // Act
            var result = await handler.Handle(command, new CancellationToken());

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(DeleteProjectHandler.PROJECT_NOT_FOUND_MESSAGE, result.Message);

            Mock.Get(repository).Verify(r => r.GetById(1), Times.Once);
            Mock.Get(repository).Verify(r => r.Update(It.IsAny<Project>()), Times.Never);

        }
    }
}
