using FC.Codeflix.Catalog.Domain.Exceptions;
using Xunit;

using DomainEntity = FC.Codeflix.Catalog.Domain.Entity;

namespace FC.Codeflix.Catalog.UnitTests.Domain.Entity.Category
{
    public class CategoryTest
    {
        [Fact(DisplayName = nameof(Instantiate))]
        [Trait("Domain", "Category - Aggregates")]
        public void Instantiate()
        {
            // Arrange
            var validData = new
            {
                Name = "category name",
                Description = "category description"
            };
            var datetimeBefore = DateTime.Now;
            // Act
            var category = new DomainEntity.Category(
                 validData.Name,
                 validData.Description,
                 true);
            var datetimeAfter = DateTime.Now;



            // Assert
            Assert.NotNull(category);
            Assert.Equal(validData.Name, category.Name);
            Assert.Equal(validData.Description, category.Description);
            Assert.NotEqual(default(Guid), category.Id);
            Assert.NotEqual(default(DateTime), category.CreatedAt);
            Assert.True(category.CreatedAt > datetimeBefore);
            Assert.True(category.CreatedAt < datetimeAfter);
            Assert.True(category.IsActive);

        }

        [Theory(DisplayName = nameof(InstantiateWithIsActive))]
        [Trait("Domain", "Category - Aggregates")]
        [InlineData(true)]
        [InlineData(false)]
        public void InstantiateWithIsActive(bool isActive)
        {
            // Arrange
            var validData = new
            {
                Name = "category name",
                Description = "category description"
            };
            var datetimeBefore = DateTime.Now;
            // Act
            var category = new DomainEntity.Category(
                 validData.Name,
                 validData.Description,
                 isActive);
            var datetimeAfter = DateTime.Now;



            // Assert
            Assert.NotNull(category);
            Assert.Equal(validData.Name, category.Name);
            Assert.Equal(validData.Description, category.Description);
            Assert.NotEqual(default(Guid), category.Id);
            Assert.NotEqual(default(DateTime), category.CreatedAt);
            Assert.True(category.CreatedAt > datetimeBefore);
            Assert.True(category.CreatedAt < datetimeAfter);
            Assert.Equal(isActive, category.IsActive);
        }

        [Theory(DisplayName = nameof(InstantiateErrorWhenNameIsEmpty))]
        [Trait("Domain", "Category - Aggregates")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("   ")]
        public void InstantiateErrorWhenNameIsEmpty(string? name)
        {
            Action action = () => new DomainEntity.Category(name!, "Category Description", true).Validate();
            var exception = Assert.ThrowsAny<EntityValidationException>(action);
            Assert.Equal("Name should not be empty or null.", exception.Message);

        }

        [Theory(DisplayName = nameof(InstantiateErrorWhenDescriptionIsNull))]
        [Trait("Domain", "Category - Aggregates")]
        [InlineData(null)]
        public void InstantiateErrorWhenDescriptionIsNull(string? description)
        {
            Action action = () => new DomainEntity.Category("Category Name", description, true).Validate();
            var exception = Assert.ThrowsAny<EntityValidationException>(action);
            Assert.Equal("Description should not be empty or null.", exception.Message);

        }



        // nome deve ter no minimo 3 caracteres
        // nome deve ter no minimo 255 caracteres
        // descrição deve ter no minimo 10_000 caracteres

        [Theory(DisplayName = nameof(InstantiateErrorWhenNameIsLessThan3Characters))]
        [Trait("Domain", "Category - Aggregates")]
        [InlineData("1")]
        [InlineData("12")]
        [InlineData("a")]
        [InlineData("ab")]
        public void InstantiateErrorWhenNameIsLessThan3Characters(string invalidName)
        {
            Action action =
                () => new DomainEntity.Category(invalidName, "Category Description", true);
            var exception = Assert.Throws<EntityValidationException>(action);
            Assert.Equal("Name should be at least 3 characters long", exception.Message);
        }
    }
}
