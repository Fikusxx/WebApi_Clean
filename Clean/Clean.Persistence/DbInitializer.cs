﻿

namespace Clean.Persistence;

public class DbInitializer
{
    public static void Init(NotesDbContext context)
    {
        context.Database.EnsureCreated();
    }
}
