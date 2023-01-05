using Movie.Catalog.UnitTests.Common;
using Xunit;
namespace Movie.Catalog.UnitTests.Domain.Entity.Category
{
    public class CategoryTestFixture : BaseFixture
    {
        public CategoryTestFixture() : base() { }

        public Catalog.Domain.Entity.Category GetValidCategory()
            => new(Faker.Commerce.Categories(1)[0],
                Faker.Commerce.ProductDescription());
    }

    [CollectionDefinition(nameof(CategoryTestFixture))]
    public class CategoryTestFixtureCollection : ICollectionFixture<CategoryTestFixture>
    { };
}
