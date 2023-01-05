using Moq;
using Movie.Catalog.Domain.Entity;
using Xunit;
using UseCases = Movie.Catalog.Application.UseCases.CreateCategory;
namespace Movie.Catalog.UnitTests.Application.CreateCategory
{
    public class CreateCategoryTests
    {

        [Fact(DisplayName = nameof(CreateCategoryAsync))]
        [Trait("Application", "CreateCategory - Use Cases")]
        public async Task CreateCategoryAsync()
        {
            var repositoryMock = new Mock<ICategoryRepository>();
            var unityOfWorkMock = new Mock<IunitOfWork>();
            var useCase = new UseCases.CreateCategory(
                repositoryMock.Object,
                unityOfWorkMock.Object
                );
            var input = new CreateCategoryInput(
                "Category Name",
                "Category Description",
                true
                );
            var output = await useCase.Handle(input, CancellationToken.None);

            repositoryMock.Verify(repository =>
              repository.Create(
                  It.IsAny<Category>(),
                  It.IsAny<CancellationToken>()
                  ), Times.Once
              );

            unityOfWorkMock.Verify(uow =>
                 uow.Commit(It.IsAny<CancellationToken>()),
                 Times.Once
                 );

            output.Should().NotBeNull();
            output.Name.Should().Be("Category Name");
            output.Description.Should().Be("Category Description");
            output.IsActive.Should().Be(true);
            (output.Id != null && output.Id != Guid.Empty).Should().BeTrue();
            (output.CreatedAt != null && output.CreatedAt != default(DateTime))
                .Should().BeTrue();

        }

    }
}
