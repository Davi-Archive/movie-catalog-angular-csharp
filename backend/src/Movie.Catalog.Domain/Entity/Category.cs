﻿using Movie.Catalog.Domain.Exceptions;
using Movie.Catalog.Domain.SeedWork;

namespace Movie.Catalog.Domain.Entity
{
    public class Category : AggregateRoot
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public Category(string name, string description, bool isActive) : base()
        {
            Name = name;
            Description = description;
            IsActive = isActive;
            CreatedAt = DateTime.Now;

            Validate();
        }

        public Category(string name, string description)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            IsActive = true;
            CreatedAt = DateTime.Now;

            Validate();
        }

        public void Activate()
        {
            IsActive = true;
            Validate();
        }

        public void Deactivate()
        {
            IsActive = false;
            Validate();
        }

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                throw new EntityValidationException($"{nameof(Name)} should not be empty or null.");
            }
            if (Description == null)
            {
                throw new EntityValidationException($"{nameof(Description)} should not be empty or null.");
            }
            if (Name.Length < 3)
            {
                throw new EntityValidationException($"{nameof(Name)} should be at least 3 characters long");
            }
            if (Name.Length > 255)
            {
                throw new EntityValidationException($"{nameof(Name)} should be less or equal 255 characters long");
            }
            if (Description.Length > 10000)
            {
                throw new EntityValidationException($"{nameof(Description)} should be less or equal 10.000 characters long");
            }
        }

        public void Update(string? name, string? description = null)
        {
            if (string.IsNullOrWhiteSpace(name)) { throw new EntityValidationException($"{nameof(Name)} should not be empty or null."); }
            Name = name;
            Description = description ?? Description; // if binario
            Validate();
        }
    }
}