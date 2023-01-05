using Movie.Catalog.Application.Interfaces;
using Movie.Catalog.Domain.Repository;
using DomainEntity = Movie.Catalog.Domain.Entity;

namespace Movie.Catalog.Application.UseCases.Category.CreateCategory
{
    public class CreateCategory : ICreateCategory
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryRepository _categoryRepository;

        public CreateCategory(
            ICategoryRepository categoryRepository,
            IUnitOfWork unitOfWork
            )
        {
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
        }

        public async Task<CreateCategoryOutput> Handle(
            CreateCategoryInput input,
            CancellationToken cancellationToken)
        {
            var category = new DomainEntity.Category(
               input.Name,
               input.Description,
               input.IsActive
                );
            await _categoryRepository.Insert(category, cancellationToken);
            await _unitOfWork.Commit(cancellationToken);

            return new CreateCategoryOutput(
                category.Id,
                category.Name,
                category.Description,
                category.IsActive,
                category.CreatedAt
                );
        }
    }
}
