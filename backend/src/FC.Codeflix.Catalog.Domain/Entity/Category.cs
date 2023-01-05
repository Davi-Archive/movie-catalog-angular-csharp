using FC.Codeflix.Catalog.Domain.Exceptions;

namespace FC.Codeflix.Catalog.Domain.Entity
{
    public class Category
    {
        public Category(string name, string description, bool isActive)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            IsActive = isActive;
            CreatedAt = DateTime.Now;
        }

        public Category(string name, string description)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            IsActive = true;
            CreatedAt = DateTime.Now;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }


        public Validate()
        {
            if (String.IsNullOrEmpty(Name))
            {
                throw new EntityValidationException($"{nameof(Name)} should not be empty or null.");
            }
        }
    }
}
