using ECommerce.Api.Contracts.Products;
using ECommerce.Application.Commands;
using ECommerce.Application.Common;
using ECommerce.Application.DTOs;
using ECommerce.Application.Queries;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Endpoints
{
    public static class ProductsEndpoints
    {
        private static async Task<Results<Ok<IReadOnlyList<ProductDto>>, ProblemHttpResult>> GetAll(
            [FromServices] GetAllProductsHandler handler,
            CancellationToken ct)
        {
            var result = await handler.HandleAsync(new GetAllProductsQuery(), ct);

            if (result.IsFailure)
            {
                return TypedResults.Problem(new ProblemDetails
                {
                    Title = "Get products failed",
                    Status = StatusCodes.Status400BadRequest,
                    Detail = result.Error
                });
            }

            return TypedResults.Ok(result.Value);
        }

        private static async Task<Results<Ok<ProductDto>, ProblemHttpResult>> GetById(
            Guid id,
            [FromServices] GetProductByIdHandler handler,
            CancellationToken ct)
        {
            var result = await handler.HandleAsync(new GetProductByIdQuery(id), ct);

            if (result.ErrorType == ErrorType.NotFound)
            {
                return TypedResults.Problem(new ProblemDetails
                {
                    Title = "Product not found",
                    Status = StatusCodes.Status404NotFound,
                    Detail = "Product was not found"
                });
            }

            if (result.IsFailure)
            {
                return TypedResults.Problem(new ProblemDetails
                {
                    Title = "Get product failed",
                    Status = StatusCodes.Status400BadRequest,
                    Detail = result.Error
                });
            }

            return TypedResults.Ok(result.Value);
        }

        private static async Task<Results<Created<CreateProductResponse>, ProblemHttpResult>> Create(
            [FromBody] CreateProductRequest request,
            [FromServices] CreateProductHandler handler,
            CancellationToken ct)
        {
            var cmd = new CreateProductCommand(
                request.CategoryId,
                request.Name,
                request.Description,
                request.ArticleNumber,
                request.ImageUrl,
                request.Price,
                request.StockQuantity
            );

            var result = await handler.HandleAsync(cmd, ct);

            if (result.IsFailure)
            {
                return TypedResults.Problem(new ProblemDetails
                {
                    Title = "Create product failed",
                    Status = StatusCodes.Status400BadRequest,
                    Detail = result.Error
                });
            }

            return TypedResults.Created(
                $"/products/{result.Value}",
                new CreateProductResponse(result.Value)
            );
        }

        private static async Task<Results<Ok<CreateProductResponse>, ProblemHttpResult>> Update(
            Guid id,
            [FromBody] UpdateProductRequest request,
            [FromServices] UpdateProductHandler handler,
            CancellationToken ct)
        {
            var cmd = new UpdateProductCommand(
                id,
                request.CategoryId,
                request.Name,
                request.Description,
                request.ArticleNumber,
                request.ImageUrl,
                request.Price,
                request.StockQuantity
            );

            var result = await handler.HandleAsync(cmd, ct);

            if (result.ErrorType == ErrorType.NotFound)
            {
                return TypedResults.Problem(new ProblemDetails
                {
                    Title = "Product not Found",
                    Status = StatusCodes.Status404NotFound,
                    Detail = "Product was not found"
                });
            }

            if (result.IsFailure)
            {
                return TypedResults.Problem(new ProblemDetails
                {
                    Title = "Update product failed",
                    Status = StatusCodes.Status400BadRequest,
                    Detail = result.Error
                });
            }

            return TypedResults.Ok(new CreateProductResponse(id));
        }

        private static async Task<Results<NoContent, ProblemHttpResult>> Delete(
            Guid id,
            [FromServices] DeleteProductHandler handler,
            CancellationToken ct)
        {
            var cmd = new DeleteProductCommand(id);

            var result = await handler.HandleAsync(cmd, ct);

            if (result.ErrorType == ErrorType.NotFound)
            {
                return TypedResults.Problem(new ProblemDetails
                {
                    Title = "Product not found",
                    Status = StatusCodes.Status404NotFound,
                    Detail = "Product was not found"
                });
            }

            if (result.IsFailure)
            {
                return TypedResults.Problem(new ProblemDetails
                {
                    Title = "Delete product failed",
                    Status = StatusCodes.Status400BadRequest,
                    Detail = result.Error
                });
            }

            return TypedResults.NoContent();
        }

        public static IEndpointRouteBuilder MapProductsEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/products")
            .WithTags("Products");

            group.MapGet("/", GetAll)
            .Produces<IReadOnlyList<ProductDto>>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest);

            group.MapGet("/{id:guid}", GetById)
            .Produces<ProductDto>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound);

            group.MapPost("/", Create)
            .Produces<CreateProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest);

            group.MapPut("/{id:guid}", Update)
            .Produces<CreateProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound);

            group.MapDelete("/{id:guid}", Delete)
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound);

            return app;
        }
    }
}