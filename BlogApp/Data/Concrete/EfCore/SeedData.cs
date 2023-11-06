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
                    new Tag {Text = "BackEnd", Url = "BackEnd", Color = TagColor.primary},
                    new Tag {Text = "Web Programming", Url = "WebProgramming", Color = TagColor.success},
                    new Tag {Text = "FrontEnd", Url = "FrontEnd", Color = TagColor.warning},
                    new Tag {Text = "FullStack", Url = "FullStack", Color = TagColor.danger},
                    new Tag {Text = "Php", Url = "Php", Color = TagColor.info}
                );
                context.SaveChanges();
            }
            if(!context.Users.Any()){
                context.Users.AddRange(
                    new User {UserName = "KenanNuri",Name = "Kenan",Email = "kenan@gmail.com",Password = "kenan123",Image = "p1.jpg"},
                    new User {UserName = "IsmayilRhmv",Name = "Ismayil",Email = "ismayil@gmail.com",Password = "ismayil123",Image = "p2.jpg"}
                );
                context.SaveChanges();
            }
            if(!context.Posts.Any()){
                context.Posts.AddRange(
                    new Post {
                        Title = "Asp.Net Core 8.0",
                        Content = "Asp.Net Core 8.0 is in preview",
                        Url = "aspnetcore8",
                        PublishedOn = DateTime.Now.AddDays(-10),
                        Tags = context.Tags.Take(3).ToList(),
                        Image = "1.jpg",
                        IsActive = true,
                        UserId = 1,
                        Comments = new List<Comment> {
                            new Comment {
                                Text = "its Perfect Course", 
                                PublishedOn = new DateTime(),
                                UserId = 1
                            },
                            new Comment {
                                Text = "Yes i agree", 
                                PublishedOn = new DateTime(),
                                UserId = 2
                            }
                        }
                    },
                    new Post {
                        Title = "Php ",
                        Content = "Php Laravel is in preview",
                        Url = "php",
                        PublishedOn = DateTime.Now.AddDays(-20),
                        Tags = context.Tags.Take(2).ToList(),
                        Image = "2.jpg",
                        IsActive = true,
                        UserId = 1
                    },
                    new Post {
                        Title = "Angular ",
                        Content = "Angular Lessons",
                        Url = "angular",
                        PublishedOn = DateTime.Now.AddDays(-10),
                        Tags = context.Tags.Take(2).ToList(),
                        Image = "2.jpg",
                        IsActive = true,
                        UserId = 1
                    },
                    new Post {
                        Title = "React",
                        Content = "React is in preview",
                        Url = "react-lessons",
                        PublishedOn = DateTime.Now.AddDays(-5),
                        Tags = context.Tags.Take(2).ToList(),
                        Image = "2.jpg",
                        IsActive = true,
                        UserId = 1
                    },
                    new Post {
                        Title = "Django",
                        Content = "Django is in preview",
                        Url = "django",
                        PublishedOn = DateTime.Now.AddDays(-5),
                        Tags = context.Tags.Take(4).ToList(),
                        Image = "3.jpg",
                        IsActive = true,
                        UserId = 2
                    }
                );
                context.SaveChanges();
            }
        }
    }
}