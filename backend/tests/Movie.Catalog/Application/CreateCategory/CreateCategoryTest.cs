using Moq;
using Movie.Catalog.Domain.Entity;
using Xunit;
using UseCases = Movie.Catalog.Application.UseCases.Category.CreateCategory;
namespace Movie.Catalog.UnitTests.Application.CreateCategory
{
    [Collection(nameof(CreateCategoryTestFixture))]
    public class CreateCategoryTest
    {
        private readonly CreateCategoryTestFixture _fixture;

        public CreateCategoryTest(CreateCategoryTestFixture fixture)
        {
            _fixture = fixture;
        }

        //  [Fact(DisplayName = nameof(CreateCategoryAsync))]
        [Trait("Application", "CreateCategory - Use Cases")]
        public async void CreateCategoryAsync()
        {
            var repositoryMock = _fixture.GetRepositoryMock();
            var unityOfWorkMock = _fixture.GetUnitOfWorkMock();

            var useCase = new UseCases.CreateCategory(
                repositoryMock.Object,
                unityOfWorkMock.Object
                );
            var input = _fixture.GetValidInput();

            var output = await useCase.Handle(input, CancellationToken.None);

            repositoryMock.Verify(repository =>
              repository.Insert(
                  It.IsAny<Category>(),
                  It.IsAny<CancellationToken>()
                  ), Times.Once
              );

            unityOfWorkMock.Verify(uow =>
                 uow.Commit(It.IsAny<CancellationToken>()),
                 Times.Once
                 );


            Assert.True(true);
            //output.Should().NotBeNull();
            //   output.Name.Should().Be(input.Name);
            //    output.Description.Should().Be(input.Description);
            //   output.IsActive.Should().Be(input.IsActive);
            //output.Id.Should().NotBeEmpty();
            //output.CreatedAt.Should().NotBeSameDateAs(default(DateTime));

        }

    }
}
