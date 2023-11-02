using BlogApp.Entity;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Data.Concrete.EfCore;

public static class SeedData
{
    public static void FillTestData(IApplicationBuilder app){
        var context = app.ApplicationServices.CreateScope().ServiceProvider.GetService<BlogContext>();
        if (context != null){
            if(context.Database.GetPendingMigrations().Any()){
                context.Database.Migrate();
            }
            if(!context.Tags.Any()){
                context.Tags.AddRange(
                    new Tag {Text = "BackEnd"},
                    new Tag {Text = "Web Programming"},
                    new Tag {Text = "FrontEnd"},
                    new Tag {Text = "FullStack"},
                    new Tag {Text = "Php"}
                );
                context.SaveChanges();
            }
            if(!context.Users.Any()){
                context.Users.AddRange(
                    new User {UserName = "KenanNuri"},
                    new User {UserName = "IsmayilRhmv"}
                );
                context.SaveChanges();
            }
            if(!context.Posts.Any()){
                context.Posts.AddRange(
                    new Post {
                        Title = "Asp.Net Core 8.0",
                        Content = "Asp.Net Core 8.0 is in preview",
                        PublishedOn = DateTime.Now.AddDays(-10),
                        Tags = context.Tags.Take(3).ToList(),
                        IsActive = true,
                        UserId = 1
                    },
                    new Post {
                        Title = "Php ",
                        Content = "Php is in preview",
                        PublishedOn = DateTime.Now.AddDays(-20),
                        Tags = context.Tags.Take(2).ToList(),
                        IsActive = true,
                        UserId = 1
                    },
                    new Post {
                        Title = "Django",
                        Content = "React Js is in preview",
                        PublishedOn = DateTime.Now.AddDays(-5),
                        Tags = context.Tags.Take(4).ToList(),
                        IsActive = true,
                        UserId = 2
                    }
                );
                context.SaveChanges();
            }
        }
    }
}