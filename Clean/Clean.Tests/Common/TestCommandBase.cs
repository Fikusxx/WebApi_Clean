﻿using Clean.Persistence;

namespace Clean.Tests.Common;

public abstract class TestCommandBase : IDisposable
{
    protected readonly NotesDbContext Context;

    public TestCommandBase()
    {
        Context = NotesContextFactory.Create();
    }

    public void Dispose()
    {
        NotesContextFactory.Destroy(Context);
    }
}
