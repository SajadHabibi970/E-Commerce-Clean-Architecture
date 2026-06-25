using System.Reflection;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Exceptions;

namespace ECommerce.Tests.Domain
{
    public class CategoryTests
    {
        [Fact]
        public void Constructor_ShouldThrowException_WhenNameIsEmpty()
        {
            // Arrange
            string name = "";
            string description = "Some description";

            // Act & Assert
            Assert.Throws<DomainException>(() =>
            {
                new Category(name, description);
            });
        }
        
        [Fact]
        public void Constructor_CreateCategory_WhenDataIsValid()
        {
            // Arrange
            string name = "Vegetables";
            string description = "All vegetables";

            //Act
            var category = new Category(name, description);

            // Assert
            Assert.Equal("Vegetables", category.Name);
            Assert.Equal("All vegetables", category.Description);
            Assert.True(category.IsActive);    
        }

        [Fact]
        public void SoftDelete_ShoudMarkCategoryAsDeleted()
        {
            // Arrange
            var category = new Category("Electronics", "Phones");

            // Act
            category.SoftDelete();

            // Assert
            Assert.True(category.IsDeleted);
            Assert.False(category.IsActive);
            Assert.NotNull(category.DeletedAt);
        }

        [Fact]
        public void Activate_ShouldSetIsActiveToTrue()
        {
            // Arrange
            var category = new Category("Electronics", "Phones");

            // Act
            category.Deactivate();
            category.Activate();

            // Assert
            Assert.True(category.IsActive);
            Assert.NotNull(category.UpdatedAt);
        }

        [Fact]
        public void Deactivate_ShouldSetIsToFalse()
        {
            // Arrange
            var category = new Category("Electronics", "Phones");

            // Act
            category.Deactivate();

            // Assert
            Assert.False(category.IsActive);
            Assert.NotNull(category.UpdatedAt);
        }

        [Fact]
        public void Edit_ShouldUpdateCategory_WhenDataIsValid()
        {
            // Arrange
            var category = new Category("Electronics", "Phones");

            // Act
            string name = "Food";
            string description = "Chicken";
            category.Edit(name, description);

            // Assert
            Assert.Equal("Food", category.Name);
            Assert.Equal("Chicken", category.Description);
            Assert.NotNull(category.UpdatedAt);
        }

        [Fact]
        public void Edit_ShouldThrowException_WhenNameIsEmpty()
        {
            // Arrange
            var category = new Category("Electronics", "Phones");

            // Act
            string name = "";
            string description = "TV";

            // Assert
            Assert.Throws<DomainException>(() =>
            category.Edit(name, description));
        }
    }
}