using Bogus;
using FluentAssertions;
using Movie.Catalog.Domain.Exceptions;
using Movie.Catalog.Domain.Validation;
using Xunit;

namespace Movie.Catalog.UnitTests.Domain.Validation
{
    public class DomainValidationTest
    {

        public Faker Faker { get; set; } = new Faker();

        // not null
        [Fact(DisplayName = nameof(NotNullOk))]
        [Trait("Domain", "DomainValidation - Validation")]
        public void NotNullOk()
        {
            var value = Faker.Commerce.ProductName();
            try
            {
                Action action = () => DomainValidation.NotNull(value, "Value");
            }
            catch (Exception ex)
            {
                Assert.Fail($"Domain Validation did throw: {ex.Message}");
            }


        }


        // not null or empty

        [Fact(DisplayName = nameof(NotNull))]
        [Trait("Domain", "DomainValidation - Validation")]
        public void NotNull()
        {
            string? value = null;
            Action action = () => DomainValidation.NotNull(value, "Value");

            Assert.Throws<EntityValidationException>(action);
        }



        [Theory(DisplayName = nameof(NotNullOrEmptyThrowWhenEmpty))]
        [Trait("Domain", "DomainValidation - Validation")]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        public void NotNullOrEmptyThrowWhenEmpty(string? target)
        {
            Action action =
                () => DomainValidation.NotNullOrEmpty(target, "fieldName");


            var exception = Assert.Throws<EntityValidationException>(action);
            Assert.Equal("fieldName should not be null or empty", exception.Message);
        }

        [Fact(DisplayName = nameof(NotNullOrEmptyOk))]
        [Trait("Domain", "DomainValidation - Validation")]
        public void NotNullOrEmptyOk()
        {
            var target = Faker.Commerce.ProductName();


            Action action =
                () => DomainValidation.NotNullOrEmpty(target, "fieldName");


            action.Should().NotThrow();
        }

        // min length

        [Theory(DisplayName = nameof(MinLengthThrowWhenLess))]
        [Trait("Domain", "DomainValidation - Validation")]
        public void MinLengthThrowWhenLess(string target, int minLength)
        {
            Action action =
                () => DomainValidation.MinLength(target, minLength, "fieldName");

            action.Should().Throw<EntityValidationException>()
                .WithMessage($"fieldName should not be less than {minLength} long");
        }

        public static IEnumerable<object[]> GetValuesSmallerThan { get; }

        // max length

    }
}

