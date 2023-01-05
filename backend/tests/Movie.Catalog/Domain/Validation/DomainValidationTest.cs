using Bogus;
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
            string value = null;
            Action action = () => DomainValidation.NotNull(value, "Value");

            Assert.Throws<EntityValidationException>(action);
        }


    }
    // min length
    // max length
}

