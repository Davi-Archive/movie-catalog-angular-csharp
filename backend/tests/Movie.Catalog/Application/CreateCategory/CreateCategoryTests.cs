using FluentAssertions;
using Moq;
using Movie.Catalog.Application.Interfaces;
using Movie.Catalog.Domain.Entity;
using Movie.Catalog.Domain.Repository;
using Xunit;
using UseCases = Movie.Catalog.Application.UseCases.Category.CreateCategory;
namespace Movie.Catalog.UnitTests.Application.CreateCategory
{
    public class CreateCategoryTests
    {

        [Fact(DisplayName = nameof(CreateCategoryAsync))]
        [Trait("Application", "CreateCategory - Use Cases")]
        public async Task CreateCategoryAsync()
        {
            var repositoryMock = new Mock<ICategoryRepository>();
            var unityOfWorkMock = new Mock<IUnitOfWork>();
            var useCase = new UseCases.CreateCategory(
                repositoryMock.Object,
                unityOfWorkMock.Object
                );
            var input = new UseCases.CreateCategoryInput(
                "Category Name",
                "Category Description",
                true
                );
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

            output.Should().NotBeNull();
            output.Name.Should().Be("Category Name");
            output.Description.Should().Be("Category Description");
            output.IsActive.Should().Be(true);
            output.Id.Should().NotBeEmpty();
            output.CreatedAt.Should().NotBeSameDateAs(default(DateTime));

        }

    }
}
