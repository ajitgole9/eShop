using Catalog.API.Exceptions;

namespace Catalog.API.Products.GetProductById;

public record GetProductByIdQuery(Guid id) : IQuery<GetProductByIdResult>;
public record GetProductByIdResult(Product product);
public class GetProductByIdQueryHandler(IDocumentSession session, ILogger<GetProductByIdQuery> logger) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {

        logger.LogInformation("GetProductByIdQueryHandler.Handle Called with Query {@Query}", query);

        var product = await session.LoadAsync<Product>(query.id, cancellationToken);

        if(product is null)
        {
            throw new ProductNotFoundException(query.id);
        }

        return new GetProductByIdResult(product);
    }
}
