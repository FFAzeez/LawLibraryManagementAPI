using LibraryManagementAPI.Domain.Model;
using LibraryManagementAPI.Infrastructure.Persistence.Context;

namespace LibraryManagementAPI.Infrastructure.Persistence.Seeds;


public static class DbSeeder
{
    public static void Seed(AppDbContext context)
    {
        // Ensure database is created
        context.Database.EnsureCreated();

        // Check if any books exist to avoid duplicate seeding
        if (!context.Books.Any())
        {
            var books = new[]
            {
                new Book
                {
                    Title = "To Kill it",
                    Author = "Azeez Lee",
                    ISBN = "978-0446310789",
                    PublishedDate = new DateTime(1960, 7, 11),
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    IsDeleted = false
                },
                new Book
                {
                    Title = "Make money",
                    Author = "Seun Orwell",
                    ISBN = "978-0451524935",
                    PublishedDate = new DateTime(1949, 6, 8),
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    IsDeleted = false
                },
                new Book
                {
                    Title = "Pride and Prejudice",
                    Author = "Jane Austen",
                    ISBN = "978-0141439518",
                    PublishedDate = new DateTime(1813, 1, 28),
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    IsDeleted = false
                }
            };

            context.Books.AddRange(books);
            context.SaveChanges();
        }

        if (!context.Users.Any())
        {
            var users = new[]
            {
                new User
                {
                    Email = "mayowa.ayodeji@gmail.com",
                    UserName = "mayowa.ayodeji@gmail.com",
                    Password = "Mayowa@01",
                    PhoneNumber = "08155501230",
                    FirstName = "Mayowa",
                    LastName = "Ayodeji",
                    Address = "12, Ogoluwa street, Bariga",
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    IsDeleted = false
                },
                new User
                {
                    Email = "seun.kunle@gmail.com",
                    UserName = "seun.kunle@gmail.com",
                    Password = "Kunle@02",
                    PhoneNumber = "08145625550",
                    FirstName = "Seun",
                    LastName = "OyeKunle",
                    Address = "17 Akoka Street, Bariga",
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    IsDeleted = false
                }
            };

            context.Users.AddRange(users);
        }
    }
}