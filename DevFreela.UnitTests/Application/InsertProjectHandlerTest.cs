using DevFreela.Application.Commands.InsertProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.UnitTests.Fakes;
using MediatR;
using Moq;
using NSubstitute;

namespace DevFreela.UnitTests.Application
{
    public class InsertProjectHandlerTest
    {
        [Fact]
        public async Task InputDataAreOk_Insert_Success_NSubstitute()
        {
            // Arrage
            const int ID = 1;
            var repository = Substitute.For<IProjectRepository>();
            repository.Add(Arg.Any<Project>()).Returns(Task.FromResult(ID));
            var mediator = Substitute.For<IMediator>();
           
            // Inutilizado para usar o Data Faker
            //var command = new InsertProjectCommand
            //{
            //    Title = "Project A",
            //    Description = "Descricao do Projeto",
            //    TotalCost = 20000,
            //    IdClient = 1,
            //    IdFreelancer = 1,
            //};

            var command = FakeDataHelper.CreateFakeInsertProjectCommand();

            var handler = new InsertProjectHandler(mediator,repository);

            // Act
            var result = await handler.Handle(command, new CancellationToken());

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(ID, result.Data);
            await repository.Received(1).Add(Arg.Any<Project>());
        }

        [Fact]
        public async Task InputDataAreOk_Insert_Success_Moq()
        {
            // Arrage
            const int ID = 1;


            //var mock = new Mock<IProjectRepository>();
            //mock.Setup(r => r.Add(It.IsAny<Project>())).ReturnsAsync(1);

            //Definicao, setup e acesso tudo na mesma linha
            var repository = Mock
                .Of<IProjectRepository>(r => r.Add(It.IsAny<Project>()) == Task.FromResult(ID));
            var mediator = Mock .Of<IMediator>();
           
            // Inutilizado para usar o Data Faker
            //var command = new InsertProjectCommand
            //{
            //    Title = "Project A",
            //    Description = "Descricao do Projeto",
            //    TotalCost = 20000,
            //    IdClient = 1,
            //    IdFreelancer = 1,
            //};

            var command = FakeDataHelper.CreateFakeInsertProjectCommand();

            var handler = new InsertProjectHandler(mediator, repository);

            // Act
            var result = await handler.Handle(command, new CancellationToken());

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(ID, result.Data);
            //mock.Verify(m => m.Add(It.IsAny<Project>()), Times.Once);

            Mock.Get(repository).Verify(m => m.Add(It.IsAny<Project>()), Times.Once);
        }


    }
}
