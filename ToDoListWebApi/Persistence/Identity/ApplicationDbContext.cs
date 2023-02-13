﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToDoListWebApi.Persistence.Models.Identity;

namespace ToDoListWebApi.Persistence.Identity;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<IdentityRole>().HasData(
              new { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
              new { Id = "2", Name = "WebApi", NormalizedName = "WEBAPI" }
              );
    }
}
