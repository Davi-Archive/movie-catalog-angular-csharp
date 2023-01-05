using FC.Codeflix.Catalog.Domain.Exceptions;
using FluentAssertions;
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

            category.Should().NotBeNull();
            category.Name.Should().Be(validData.Name);
            category.Description.Should().Be(validData.Description);
            category.Id.Should().NotBeEmpty();
            category.CreatedAt.Should().NotBeSameDateAs(default(DateTime));
            (category.CreatedAt > datetimeBefore).Should().BeTrue();
            (category.CreatedAt < datetimeAfter).Should().BeTrue();
            (category.IsActive).Should().BeTrue();


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

            category.Should().NotBeNull();
            category.Name.Should().Be(validData.Name);
            category.Description.Should().Be(validData.Description);
            category.Id.Should().NotBeEmpty();
            category.CreatedAt.Should().NotBeSameDateAs(default(DateTime));
            (category.CreatedAt > datetimeBefore).Should().BeTrue();
            (category.CreatedAt < datetimeAfter).Should().BeTrue();
            (category.IsActive).Should().Be(isActive);

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

            action.Should()
                .Throw<EntityValidationException>()
                .WithMessage("Name should not be empty or null.");

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


        // nome deve ter no minimo 255 caracteres

        [Fact(DisplayName = nameof(InstantiateErrorWhenNameIsGreaterThan255Characters))]
        [Trait("Domain", "Category - Aggregates")]
        public void InstantiateErrorWhenNameIsGreaterThan255Characters()
        {
            var invalidName = String.Join(null, Enumerable.Range(0, 256).Select(_ => "a").ToArray());
            Action action =
                () => new DomainEntity.Category(invalidName, "Category Description", true);
            var exception = Assert.Throws<EntityValidationException>(action);
            Assert.Equal("Name should be less or equal 255 characters long", exception.Message);
        }

        // descrição deve ter no máximo 10_000 caracteres


        [Fact(DisplayName = nameof(InstantiateErrorWhenDescriptionIsGreaterThan10_000Characters))]
        [Trait("Domain", "Category - Aggregates")]
        public void InstantiateErrorWhenDescriptionIsGreaterThan10_000Characters()
        {
            var invalidDescription = String.Join(null, Enumerable.Range(0, 10001).Select(_ => "a").ToArray());
            Action action =
                () => new DomainEntity.Category("Invalid name", invalidDescription, true);
            var exception = Assert.Throws<EntityValidationException>(action);
            Assert.Equal("Description should be less or equal 10.000 characters long", exception.Message);
        }

        [Fact(DisplayName = nameof(Activate))]
        [Trait("Domain", "Category - Aggregates")]
        public void Activate()
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
                 false);
            var datetimeAfter = DateTime.Now;

            category.Activate();

            Assert.True(category.IsActive);
        }

        [Fact(DisplayName = nameof(Deactivate))]
        [Trait("Domain", "Category - Aggregates")]
        public void Deactivate()
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

            category.Deactivate();

            Assert.False(category.IsActive);
        }

        [Fact(DisplayName = nameof(Update))]
        [Trait("Domain", "Category - Aggregates")]
        public void Update()
        {
            var category = new DomainEntity.Category("Category Name", "Category Description");
            var newValues = new { name = "New Name", Description = "New Description" };

            category.Update(newValues.name, newValues.Description);

            Assert.Equal(newValues.name, category.Name);
            Assert.Equal(newValues.Description, category.Description);
        }


        [Fact(DisplayName = nameof(UpdateOnlyName))]
        [Trait("Domain", "Category - Aggregates")]
        public void UpdateOnlyName()
        {
            var category = new DomainEntity.Category("Category Name", "Category Description");
            var newValues = new { Name = "New Name" };
            var currentDescription = category.Description;

            category.Update(newValues.Name);

            Assert.Equal(newValues.Name, category.Name);
            Assert.Equal(currentDescription, category.Description);
        }


        [Theory(DisplayName = nameof(InstantiateErrorWhenNameIsEmpty))]
        [Trait("Domain", "Category - Aggregates")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("   ")]
        public void UpdateErrorWhenNameIsEmpty(string? name)
        {
            var category = new DomainEntity.Category("Category Name", "Category Description");

            Action action = () => category.Update(name);
            var exception = Assert.Throws<EntityValidationException>(action);
            Assert.Equal("Name should not be empty or null.", exception.Message);

        }

        [Theory(DisplayName = nameof(UpdateErrorWhenNameIsLessThan3Characters))]
        [Trait("Domain", "Category - Aggregates")]
        [InlineData("1")]
        [InlineData("12")]
        [InlineData("a")]
        [InlineData("ab")]
        public void UpdateErrorWhenNameIsLessThan3Characters(string invalidName)
        {
            var category = new DomainEntity.Category("Category Name", "Category Description");

            Action action = () => category.Update(invalidName);
            var exception = Assert.Throws<EntityValidationException>(action);
            Assert.Equal("Name should be at least 3 characters long", exception.Message);
        }


        [Fact(DisplayName = nameof(UpdateErrorWhenNameIsGreaterThan255Characters))]
        [Trait("Domain", "Category - Aggregates")]
        public void UpdateErrorWhenNameIsGreaterThan255Characters()
        {
            var invalidName = String.Join(null, Enumerable.Range(0, 256).Select(_ => "a").ToArray());
            var category = new DomainEntity.Category("Category Name", "Category Description");

            Action action = () => category.Update(invalidName);
            var exception = Assert.Throws<EntityValidationException>(action);
            Assert.Equal("Name should be less or equal 255 characters long", exception.Message);
        }

        [Fact(DisplayName = nameof(UpdateErrorWhenDescriptionIsGreaterThan10_000Characters))]
        [Trait("Domain", "Category - Aggregates")]
        public void UpdateErrorWhenDescriptionIsGreaterThan10_000Characters()
        {
            var invalidDescription = String.Join(null, Enumerable.Range(0, 10001).Select(_ => "a").ToArray());
            var category = new DomainEntity.Category("Category Name", "Category Description");

            Action action = () => category.Update("Category new name", invalidDescription);
            var exception = Assert.Throws<EntityValidationException>(action);
            Assert.Equal("Description should be less or equal 10.000 characters long", exception.Message);
        }

    }
}
