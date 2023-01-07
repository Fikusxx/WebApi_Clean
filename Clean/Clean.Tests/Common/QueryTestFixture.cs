using AutoMapper;
using Clean.Application.Common.Mappings;
using Clean.Application.Interfaces;
using Clean.Persistence;

namespace Clean.Tests.Common;

public class QueryTestFixture
{
    public NotesDbContext Context;
    public IMapper Mapper;

    public QueryTestFixture()
    {
        Context = NotesContextFactory.Create();
        var configurationProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new AssemblyMappingProfile(
                typeof(INotesDbContext).Assembly));
        });
        Mapper = configurationProvider.CreateMapper();
    }

    public void Dispose()
    {
        NotesContextFactory.Destroy(Context);
    }
}

[CollectionDefinition("QueryCollection")]
public class QueryCollection : ICollectionFixture<QueryTestFixture> { }
