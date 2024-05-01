namespace Catalog.API.Products.CreateProduct;

public record CreateProductCommand(string Name, string Description, List<string> Categories, string Image, decimal Price) : ICommand<CreateProductResult>;
public record CreateProductResult(Guid Id);
public class CreateProductCommandHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Name = command.Name,
            Description = command.Description,
            Categories = command.Categories,
            Image = command.Image,
            Price = command.Price,
        };

        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);

        return new CreateProductResult(product.Id);
    }
}
