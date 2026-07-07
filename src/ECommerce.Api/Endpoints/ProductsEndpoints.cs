using ECommerce.Api.Contracts.Products;
using ECommerce.Application.Commands;
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
            if (id == Guid.Empty)
            {
                return TypedResults.Problem(new ProblemDetails
                {
                    Title = "Empty Id",
                    Status = StatusCodes.Status400BadRequest,
                    Detail = "Id cannot be empty"
                });
            }

            var result = await handler.HandleAsync(new GetProductByIdQuery(id), ct);

            if (result.IsFailure)
            {
                return TypedResults.Problem(new ProblemDetails
                {
                    Title = "Get product failed",
                    Status = StatusCodes.Status400BadRequest,
                    Detail = result.Error
                });
            }

            if (result.Value is null)
            {
                return TypedResults.Problem(new ProblemDetails
                {
                    Title = "Product not found",
                    Status = StatusCodes.Status404NotFound,
                    Detail = "Product was not found"
                });
            }

            return TypedResults.Ok(result.Value);
        }

        private static async Task<Results<Created<CreateProductResponse>, ProblemHttpResult>> Create(
            [FromBody] CreateProductRequest request,
            [FromServices] CreateProductHandler handler,
            CancellationToken ct)
        {
            if (request.CategoryId == Guid.Empty)
            {
                return TypedResults.Problem(new ProblemDetails
                {
                    Title = "Empty CategoryId",
                    Status = StatusCodes.Status400BadRequest,
                    Detail = "CategoryId cannot be empty"
                });
            }

            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return TypedResults.Problem(new ProblemDetails
                {
                    Title = "Invalid name",
                    Status = StatusCodes.Status400BadRequest,
                    Detail = "Name cannot be empty"
                });
            }

            if (string.IsNullOrWhiteSpace(request.ArticleNumber))
            {
                return TypedResults.Problem(new ProblemDetails
                {
                    Title = "Invalid article number",
                    Status = StatusCodes.Status400BadRequest,
                    Detail = "ArticleNumber cannot be empty"
                });
            }

            if (string.IsNullOrWhiteSpace(request.ImageUrl))
            {
                return TypedResults.Problem(new ProblemDetails
                {
                    Title = "Invalid image url",
                    Status = StatusCodes.Status400BadRequest,
                    Detail = "ImageUrl cannot be empty"
                });
            }

            if (request.Price <= 0)
            {
                return TypedResults.Problem(new ProblemDetails
                {
                    Title = "Invalid price",
                    Status = StatusCodes.Status400BadRequest,
                    Detail = "Price must be greater than zero"
                });
            }

            if (request.StockQuantity < 0)
            {
                return TypedResults.Problem(new ProblemDetails
                {
                    Title = "Invalid stock quantity",
                    Status = StatusCodes.Status400BadRequest,
                    Detail = "StockQuantity cannot be negative"
                });
            }

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
            if (id == Guid.Empty)
            {
                return TypedResults.Problem(new ProblemDetails
                {
                    Title = "Empty Id",
                    Status = StatusCodes.Status400BadRequest,
                    Detail = "Id cannot be empty"
                });
            }

            if (request.CategoryId == Guid.Empty)
            {
                return TypedResults.Problem(new ProblemDetails
                {
                    Title = "Empty CategoryId",
                    Status = StatusCodes.Status400BadRequest,
                    Detail = "CategoryId cannot be empty"
                });
            }

            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return TypedResults.Problem(new ProblemDetails
                {
                    Title = "Invalid name",
                    Status = StatusCodes.Status400BadRequest,
                    Detail = "Name cannot be empty"
                });
            }

            if (string.IsNullOrWhiteSpace(request.ArticleNumber))
            {
                return TypedResults.Problem(new ProblemDetails
                {
                    Title = "Invalid article number",
                    Status = StatusCodes.Status400BadRequest,
                    Detail = "ArticleNumber cannot be empty"
                });
            }

            if (string.IsNullOrWhiteSpace(request.ImageUrl))
            {
                return TypedResults.Problem(new ProblemDetails
                {
                    Title = "Invalid image url",
                    Status = StatusCodes.Status400BadRequest,
                    Detail = "ImageUrl cannot be empty"
                });
            }

            if (request.Price <= 0)
            {
                return TypedResults.Problem(new ProblemDetails
                {
                    Title = "Invalid price",
                    Status = StatusCodes.Status400BadRequest,
                    Detail = "Price must be greater than zero"
                });
            }

            if (request.StockQuantity < 0)
            {
                return TypedResults.Problem(new ProblemDetails
                {
                    Title = "Invalid stock quantity",
                    Status = StatusCodes.Status400BadRequest,
                    Detail = "StockQuantity cannot be negative"
                });
            }

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
            if (id == Guid.Empty)
            {
                return TypedResults.Problem(new ProblemDetails
                {
                    Title = "Empty Id",
                    Status = StatusCodes.Status400BadRequest,
                    Detail = "Id cannot be empty"
                });
            }

            var command = new DeleteProductCommand(id);

            var result = await handler.HandleAsync(command, ct);

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