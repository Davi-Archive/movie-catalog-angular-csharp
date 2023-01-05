using Xunit;
using DomainEntity = Movie.Catalog.Domain.Entity;
namespace Movie.Catalog.UnitTests.Domain.Entity.Category
{
    public class CategoryTestFixture
    {
        public Catalog.Domain.Entity.Category GetValidCategory() => new DomainEntity.Category("Category Name", "Category Description");
    }

    [CollectionDefinition(nameof(CategoryTestFixture))]
    public class CategoryTestFixtureCollection : ICollectionFixture<CategoryTestFixture>
    { };
}
