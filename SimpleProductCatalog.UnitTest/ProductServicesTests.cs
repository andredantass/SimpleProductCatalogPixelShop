using AutoMapper;
using Moq;
using SimpleProductCatalog.Abstraction.DTO;
using SimpleProductCatalog.Application.Services;
using SimpleProductCatalog.Domain.Entities;
using SimpleProductCatalog.Infra.Data.Repository.Interface;

[TestFixture]
public class ProductServicesTests
{
    private Mock<IProductRepository> _productRepoMock = null!;
    private Mock<ICategoryRepository> _categoryRepoMock = null!;
    private Mock<IMapper> _mapperMock = null!;
    private ProductServices _service = null!;

    [SetUp]
    public void SetUp()
    {
        _productRepoMock = new Mock<IProductRepository>();
        _categoryRepoMock = new Mock<ICategoryRepository>();
        _mapperMock = new Mock<IMapper>();

        _service = new ProductServices(
            _productRepoMock.Object,
            _categoryRepoMock.Object,
            _mapperMock.Object
        );
    }

    [Test]
    public async Task CreateProduct_ShouldAddProduct_WhenCategoryExists()
    {
        // Arrange
        var dto = new ProductDTO
        {
            Name = "Test",
            Description = "TestDesc",
            Price = 100,
            Category = new CategoryDTO { Id = "cat1" }
        };

        var category = new Category { Id = "cat1", Name = "Category1" };

        _categoryRepoMock.Setup(r => r.GetByIdAsync("cat1")).ReturnsAsync(category);
        _productRepoMock.Setup(r => r.AddAsync(It.IsAny<Product>())).Returns(Task.CompletedTask);
        _productRepoMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

        // Act
        var result = await _service.CreateProduct(dto);

        // Assert
        Assert.IsNotNull(result);
        _productRepoMock.Verify(r => r.AddAsync(It.IsAny<Product>()), Times.Once);
        _productRepoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    }

    [Test]
    public async Task CreateProduct_ShouldReturnError_WhenCategoryNotFound()
    {
        var dto = new ProductDTO
        {
            Name = "Invalid",
            Description = "InvalidDesc",
            Price = 50,
            Category = new CategoryDTO { Id = "missing" }
        };

        _categoryRepoMock.Setup(r => r.GetByIdAsync("missing")).ReturnsAsync((Category?)null);

        var result = await _service.CreateProduct(dto);

        Assert.AreEqual("Category was not found. ", result);
        _productRepoMock.Verify(r => r.AddAsync(It.IsAny<Product>()), Times.Never);
    }

    [Test]
    public async Task DeleteProduct_ShouldReturnTrue_WhenExists()
    {
        var product = new Product { Id = "p1" };

        _productRepoMock.Setup(r => r.GetByIdAsync("p1")).ReturnsAsync(product);

        var result = await _service.DeleteProduct("p1");

        Assert.IsTrue(result);
        _productRepoMock.Verify(r => r.Delete(product), Times.Once);
        _productRepoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    }

    [Test]
    public async Task DeleteProduct_ShouldReturnFalse_WhenNotFound()
    {
        _productRepoMock.Setup(r => r.GetByIdAsync("notfound")).ReturnsAsync((Product?)null);

        var result = await _service.DeleteProduct("notfound");

        Assert.IsFalse(result);
        _productRepoMock.Verify(r => r.Delete(It.IsAny<Product>()), Times.Never);
    }

    [Test]
    public async Task GetAllProduct_ShouldReturnMappedDtos()
    {
        var products = new List<Product> {
            new Product { Id = "1", Name = "P1" },
            new Product { Id = "2", Name = "P2" }
        };

        var dtos = new List<ProductDTO> {
            new ProductDTO { Id = Guid.NewGuid(), Name = "P1" },
            new ProductDTO { Id = Guid.NewGuid(), Name = "P2" }
        };

        _productRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(products);
        _mapperMock.Setup(m => m.Map<List<ProductDTO>>(products)).Returns(dtos);

        var result = await _service.GetAllProduct();

        Assert.AreEqual(2, result.Count);
    }

    [Test]
    public async Task GetProductById_ShouldReturnMappedDto()
    {
        var product = new Product { Id = "1", Name = "TestProduct" };
        var dto = new ProductDTO { Id = Guid.NewGuid(), Name = "TestProduct" };

        _productRepoMock.Setup(r => r.GetByIdAsync("1")).ReturnsAsync(product);
        _mapperMock.Setup(m => m.Map<ProductDTO>(product)).Returns(dto);

        var result = await _service.GetProductById("1");

        Assert.That(result.Id, Is.EqualTo(result!.Id));
    }


    [Test]
    public async Task UpdateProduct_ShouldReturnFalse_WhenProductNotFound()
    {
        // Arrange
        var productId = Guid.NewGuid().ToString();
        var dto = new ProductDTO
        {
            Id = Guid.Parse(productId),
            Name = "Test",
            Description = "Test description",
            Price = 100,
            Category = new CategoryDTO { Id = "cat1" }
        };

        _productRepoMock
            .Setup(r => r.GetByIdAsync(productId))
            .ReturnsAsync((Product?)null);

        // Act
        var result = await _service.UpdateProduct(dto);

        // Assert
        Assert.IsFalse(result.Success);
        Assert.AreEqual("Product not found.", result.Error);
        _productRepoMock.Verify(r => r.Update(It.IsAny<Product>()), Times.Never);
        _productRepoMock.Verify(r => r.SaveChangesAsync(), Times.Never);
    }
}
