using Moq;
using AutoMapper;
using Microsoft.Extensions.Logging;
using SimpleProductCatalog.Application.Services;
using SimpleProductCatalog.Domain.Entities;
using SimpleProductCatalog.Abstraction.DTO;
using SimpleProductCatalog.Infra.Data.Repository.Interface;

[TestFixture]
public class CategoryServicesTests
{
    private Mock<ICategoryRepository> _categoryRepositoryMock = null!;
    private Mock<IMapper> _mapperMock = null!;
    private Mock<ILogger<CategoryServices>> _loggerMock = null!;
    private CategoryServices _service = null!;

    [SetUp]
    public void SetUp()
    {
        _categoryRepositoryMock = new Mock<ICategoryRepository>();
        _mapperMock = new Mock<IMapper>();
        _loggerMock = new Mock<ILogger<CategoryServices>>();

        _service = new CategoryServices(_categoryRepositoryMock.Object, _mapperMock.Object);
    }

    [Test]
    public async Task CreateCategory_ShouldAddCategoryAndReturnId()
    {
        // Arrange
        var dto = new CategoryDTO { Name = "TestCategory" };

        _categoryRepositoryMock
            .Setup(r => r.AddAsync(It.IsAny<Category>()))
            .Returns(Task.CompletedTask);

        _categoryRepositoryMock
            .Setup(r => r.SaveChangesAsync())
            .Returns(Task.CompletedTask);

        // Act
        var result = await _service.CreateCategory(dto);

        // Assert
        Assert.IsNotNull(result);
        _categoryRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Category>()), Times.Once);
        _categoryRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    }

    [Test]
    public async Task DeleteCategory_WhenExists_ShouldDeleteAndReturnTrue()
    {
        // Arrange
        var categoryId = "123";
        var category = new Category { Id = categoryId, Name = "ToDelete" };

        _categoryRepositoryMock.Setup(r => r.GetByIdAsync(categoryId)).ReturnsAsync(category);

        // Act
        var result = await _service.DeleteCategory(categoryId);

        // Assert
        Assert.IsTrue(result);
        _categoryRepositoryMock.Verify(r => r.Delete(category), Times.Once);
        _categoryRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    }

    [Test]
    public async Task DeleteCategory_WhenNotExists_ShouldReturnFalse()
    {
        // Arrange
        _categoryRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<string>())).ReturnsAsync((Category?)null);

        // Act
        var result = await _service.DeleteCategory("nonexistent-id");

        // Assert
        Assert.IsFalse(result);
        _categoryRepositoryMock.Verify(r => r.Delete(It.IsAny<Category>()), Times.Never);
    }

    [Test]
    public async Task GetAllCategory_ShouldReturnMappedDtos()
    {
        // Arrange
        var categories = new List<Category>
        {
            new Category { Id = "1", Name = "A" },
            new Category { Id = "2", Name = "B" }
        };
        var dtos = new List<CategoryDTO>
        {
            new CategoryDTO { Id = "1", Name = "A" },
            new CategoryDTO { Id = "2", Name = "B" }
        };

        _categoryRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(categories);
        _mapperMock.Setup(m => m.Map<List<CategoryDTO>>(categories)).Returns(dtos);

        // Act
        var result = await _service.GetAllCategory();

        // Assert
        Assert.AreEqual(2, result.Count);
        Assert.AreEqual("A", result[0].Name);
    }

    [Test]
    public async Task UpdateCategory_WhenExists_ShouldUpdateAndReturnTrue()
    {
        // Arrange
        var dto = new CategoryDTO { Id = "1", Name = "Updated" };
        var category = new Category { Id = "1", Name = "Old" };

        _categoryRepositoryMock.Setup(r => r.GetByIdAsync("1")).ReturnsAsync(category);

        // Act
        var result = await _service.UpdateCategory(dto);

        // Assert
        Assert.IsTrue(result);
        Assert.AreEqual("Updated", category.Name);
        _categoryRepositoryMock.Verify(r => r.Update(category), Times.Once);
        _categoryRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    }

    [Test]
    public async Task UpdateCategory_WhenNotExists_ShouldReturnFalse()
    {
        // Arrange
        var dto = new CategoryDTO { Id = "1", Name = "Updated" };
        _categoryRepositoryMock.Setup(r => r.GetByIdAsync("1")).ReturnsAsync((Category?)null);

        // Act
        var result = await _service.UpdateCategory(dto);

        // Assert
        Assert.IsFalse(result);
        _categoryRepositoryMock.Verify(r => r.Update(It.IsAny<Category>()), Times.Never);
    }
}
