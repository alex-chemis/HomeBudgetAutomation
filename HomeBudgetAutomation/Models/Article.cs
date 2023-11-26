using System;
using System.Collections.Generic;

namespace HomeBudgetAutomation.Models
{
    public class Article
    {
        public Article()
        {
            Operations = new HashSet<Operation>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<Operation> Operations { get; set; }
    }


public static class ArticleEndpoints
{
	public static void MapArticleEndpoints (this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/Article", () =>
        {
            return new [] { new Article() };
        })
        .WithName("GetAllArticles");

        routes.MapGet("/api/Article/{id}", (int id) =>
        {
            //return new Article { ID = id };
        })
        .WithName("GetArticleById");

        routes.MapPut("/api/Article/{id}", (int id, Article input) =>
        {
            return Results.NoContent();
        })
        .WithName("UpdateArticle");

        routes.MapPost("/api/Article/", (Article model) =>
        {
            //return Results.Created($"//api/Articles/{model.ID}", model);
        })
        .WithName("CreateArticle");

        routes.MapDelete("/api/Article/{id}", (int id) =>
        {
            //return Results.Ok(new Article { ID = id });
        })
        .WithName("DeleteArticle");
    }
}}
