using Bogus;
using FluentAssertions;
using Movie.Catalog.Domain.Exceptions;
using Movie.Catalog.Domain.Validation;
using Xunit;

namespace Movie.Catalog.UnitTests.Domain.Validation
{
    public class DomainValidationTest
    {

        public Faker faker { get; set; } = new Faker();

        // not null
        [Fact(DisplayName = nameof(NotNullOk))]
        [Trait("Domain", "DomainValidation - Validation")]
        public void NotNullOk()
        {
            var value = faker.Commerce.ProductName();
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
            var target = faker.Commerce.ProductName();


            Action action =
                () => DomainValidation.NotNullOrEmpty(target, "fieldName");


            action.Should().NotThrow();
        }

        // min length

        [Theory(DisplayName = nameof(MinLengthThrowWhenLess))]
        [Trait("Domain", "DomainValidation - Validation")]
        [MemberData(nameof(GetValuesSmallerThanMin), parameters: 10)]
        public void MinLengthThrowWhenLess(string target, int minLength)
        {
            Action action =
                () => DomainValidation.MinLength(target, minLength, "fieldName");

            action.Should().Throw<EntityValidationException>()
                .WithMessage($"fieldName should not be less than {minLength} characters long");
        }

        public static IEnumerable<object[]> GetValuesSmallerThanMin(int numberOfTests)
        {
            yield return new object[] { "123456", 10 };

            var faker = new Faker();

            for (int i = 0; i < numberOfTests; i++)
            {
                string example = faker.Commerce.ProductName();
                var minLength = example.Length + (new Random()).Next(1, 20);
                yield return new object[] { example, minLength };
            }
        }


        [Theory(DisplayName = nameof(MinLengthThrowWhenLess))]
        [Trait("Domain", "DomainValidation - Validation")]
        [MemberData(nameof(GetValuesGreaterThanMin), parameters: 10)]
        public void MinLengthOk(string target, int minLength)
        {
            Action action =
                () => DomainValidation.MinLength(target, minLength, "fieldName");

            action.Should().NotThrow();
        }

        public static IEnumerable<object[]> GetValuesGreaterThanMin(int numberOfTests)
        {
            yield return new object[] { "123456", 6 };

            var faker = new Faker();

            for (int i = 0; i < numberOfTests; i++)
            {
                string example = faker.Commerce.ProductName();
                var minLength = example.Length - (new Random()).Next(1, 5);
                yield return new object[] { example, minLength };
            }
        }

        // max length

        [Theory(DisplayName = nameof(MinLengthThrowWhenLess))]
        [Trait("Domain", "DomainValidation - Validation")]
        [MemberData(nameof(GetValuesGreaterThanMax), parameters: 10)]
        public void maxLengthThrowWhenGreater(string target, int maxLength)
        {
            Action action =
                () => DomainValidation.MaxLength(target, maxLength, "fieldName");

            action.Should().Throw<EntityValidationException>()
                .WithMessage($"fieldName should not be greater than {maxLength} characters long");
        }

        public static IEnumerable<object[]> GetValuesGreaterThanMax(int numberOfTests)
        {
            yield return new object[] { "123456", 5 };

            var faker = new Faker();

            for (int i = 0; i < (numberOfTests - 1); i++)
            {
                string example = faker.Commerce.ProductName();
                var minLength = example.Length - (new Random()).Next(1, 5);
                yield return new object[] { example, minLength };
            }
        }

    }
}

