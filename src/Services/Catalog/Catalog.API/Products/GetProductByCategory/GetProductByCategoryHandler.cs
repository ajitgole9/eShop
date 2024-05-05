using Catalog.API.Exceptions;
using Catalog.API.Products.GetProductById;

namespace Catalog.API.Products.GetProductByCategory;

public record GetProductByCategoryQuery(string category) : IQuery<GetProductByCategoryResult>;
public record GetProductByCategoryResult(IEnumerable<Product> products);
public class GetProductByCategoryQueryHandler(IDocumentSession session, ILogger<GetProductByCategoryQuery> logger) : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
{
    public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProductByCategoryQueryHandler.Handle Called with Query {@Query}", query);

        var products = await session.Query<Product>()
            .Where(p => p.Categories.Contains(query.category))
            .ToListAsync();

        return new GetProductByCategoryResult(products);
    }
}
