using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Shared.Command;
using CleanArchitecture.Shared.ResultMechanism;

namespace CleanArchitecture.UseCases.Feature1.Create;

public class CreateSampleHandler : ICommandHandler<CreateSampleCommand, Result<bool>>
{
    private readonly IRepository<SampleEntity> _repository;
    private readonly IDomainFactory<CreateSampleCommand, SampleEntity> _domainFactory;

    public CreateSampleHandler(IRepository<SampleEntity> repository, 
        IDomainFactory<CreateSampleCommand, SampleEntity> domainFactory)
    {
        _repository = repository;
        _domainFactory = domainFactory;
    }

    public async Task<Result<bool>> Handle(CreateSampleCommand request, CancellationToken cancellationToken)
    {
        var userPolicyCreator = _domainFactory.CreateEntityObject(request);
        if (userPolicyCreator is null)
        {
            return Result.Error("Something has gone wrong and we couldn't provide you an entity instance.");
        }

        var result = await _repository.AddAsync(userPolicyCreator, cancellationToken);
        if (result is null)
        {
            return Result.Error("Something has gone wrong and we could not persist the data.");
        }

        return Result<bool>.Success(true);
    }
}