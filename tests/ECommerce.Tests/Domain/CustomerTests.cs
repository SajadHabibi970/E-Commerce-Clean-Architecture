using ECommerce.Domain.Entities;
using ECommerce.Domain.Exceptions;
using ECommerce.Domain.ValueObjects;

namespace ECommerce.Tests.Domain
{
    public class CustomerTests
    {
        [Fact]
        public void Constructor_ShouldCreateCustomer_WhenDataIsValid()
        {
            // Arrange
            var firstName = "John";
            var lastName = "Dou";
            var email = "johndou@hotmail.com";
            var phoneNumber = "0721234567";
            var address = new Address("Storgatan1", "Stckholm", "11122", "Sweden");

            // Act
            var customer = new Customer(
                firstName,
                lastName,
                email,
                phoneNumber,
                address
            );

            // Assert
            Assert.Equal(firstName, customer.FirstName);
            Assert.Equal(lastName, customer.LastName);
            Assert.Equal(email, customer.Email);
            Assert.Equal(phoneNumber, customer.PhoneNumber);
            Assert.Equal(address, customer.Address);

            Assert.True(customer.IsActive);
            Assert.NotEqual(Guid.Empty, customer.Id);
            Assert.NotEqual(default,customer.CreatedAt);
            Assert.Null(customer.UpdatedAt);
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenFirstNameIsEmpty()
        {
            // Arrange
            var firstName = "";
            var lastName = "Dou";
            var email = "johndou@hotmail.com";
            var phoneNumber = "0721234567";
            var address = new Address("Storgatan1", "Stckholm", "11122", "Sweden");

            // Act & Assert
            Assert.Throws<DomainException>(() =>
            new Customer(
                firstName,
                lastName,
                email,
                phoneNumber,
                address
            ));
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenLastNameIsEmpty()
        {
            // Arrange
            var firstName = "John";
            var lastName = "";
            var email = "johndou@hotmail.com";
            var phoneNumber = "0721234567";
            var address = new Address("Storgatan1", "Stckholm", "11122", "Sweden");

            // Act & Assert
            Assert.Throws<DomainException>(() =>
            new Customer(
                firstName,
                lastName,
                email,
                phoneNumber,
                address
            ));
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenEmailIsEmpty()
        {
            // Arrange
            var firstName = "John";
            var lastName = "Doe";
            var email = "";
            var phoneNumber = "0721234567";
            var address = new Address("Storgatan1", "Stckholm", "11122", "Sweden");

            // Act & Assert
            Assert.Throws<DomainException>(() =>
            new Customer(
                firstName,
                lastName,
                email,
                phoneNumber,
                address
            ));
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenEmailIsInvalid()
        {
            // Arrange
            var firstName = "John";
            var lastName = "Doe";
            var email = "johndouhotmail.com";
            var phoneNumber = "0721234567";
            var address = new Address("Storgatan1", "Stckholm", "11122", "Sweden");

            // Act & Assert
            Assert.Throws<DomainException>(() =>
            new Customer(
                firstName,
                lastName,
                email,
                phoneNumber,
                address
            ));
        }

        [Fact]
        public void Edit_ShouldUpdateCustomer_WhenDataIsValid()
        {
            // Arrange
            var customer = new Customer(
                "John",
                "Dou",
                "johndou@hotmail.com",
                "0721234567",
                new Address("Storgatan1", "Stckholm", "11122", "Sweden")
            );

            // Act
            customer.Edit(
                "Jane",
                "Smith",
                "janesmith@hotmail.com",
                "0727654321",
                new Address("Storgatan1", "Stckholm", "11122", "Sweden")
            );

            // Assert
            Assert.Equal("Jane",customer.FirstName);
            Assert.Equal("Smith", customer.LastName);
            Assert.Equal("janesmith@hotmail.com", customer.Email);
            Assert.Equal("0727654321", customer.PhoneNumber);
            Assert.NotNull(customer.UpdatedAt);
        }

        [Fact]
        public void Edit_ShouldThrowException_WhenFirstNameIsEmpty()
        {
            // Arrange
            var customer = new Customer(
                "John",
                "Dou",
                "johndou@hotmail.com",
                "0721234567",
                new Address("Storgatan1", "Stckholm", "11122", "Sweden")
            );

            // Act & Assert
            Assert.Throws<DomainException>(() =>
            customer.Edit(
                "",
                "Smith",
                "janesmith@hotmail.com",
                "0727654321",
                new Address("Storgatan1", "Stckholm", "11122", "Sweden")
            ));
        }

        [Fact]
        public void Edit_ShouldThrowException_WhenLastNameIsEmpty()
        {
            // Arrange
            var customer = new Customer(
                "John",
                "Dou",
                "johndou@hotmail.com",
                "0721234567",
                new Address("Storgatan1", "Stckholm", "11122", "Sweden")
            );

            // Act & Assert
            Assert.Throws<DomainException>(() =>
            customer.Edit(
                "Jane",
                "",
                "janesmith@hotmail.com",
                "0727654321",
                new Address("Storgatan1", "Stckholm", "11122", "Sweden")
            ));
        }

        [Fact]
        public void Edit_ShouldThrowException_WhenEmailIsEmpty()
        {
            // Arrange
            var customer = new Customer(
                "John",
                "Dou",
                "johndou@hotmail.com",
                "0721234567",
                new Address("Storgatan1", "Stckholm", "11122", "Sweden")
            );

            // Act & Assert
            Assert.Throws<DomainException>(() =>
            customer.Edit(
                "Jane",
                "Smith",
                "",
                "0727654321",
                new Address("Storgatan1", "Stckholm", "11122", "Sweden")
            ));
        }

        [Fact]
        public void Edit_ShouldThrowException_WhenEmailIsInvalid()
        {
            // Arrange
            var customer = new Customer(
                "John",
                "Dou",
                "johndou@hotmail.com",
                "0721234567",
                new Address("Storgatan1", "Stckholm", "11122", "Sweden")
            );

            // Act & Assert
            Assert.Throws<DomainException>(() =>
            customer.Edit(
                "Jane",
                "Smith",
                "janesmithhotmail.com",
                "0727654321",
                new Address("Storgatan1", "Stckholm", "11122", "Sweden")
            ));
        }

        [Fact]
        public void Deactivate_ShouldSetIsActiveToFalse()
        {
            // Arrange
            var customer = new Customer(
                "John",
                "Doe",
                "johndoe@hotmail.com",
                "0721234567",
                new Address("Storgatan1", "Stckholm", "11122", "Sweden")
            );

            //Act
            customer.Deactivate();
            
            // Assert
            Assert.False(customer.IsActive);
            Assert.NotNull(customer.UpdatedAt);
        }

        [Fact]
        public void Activate_ShouldSetIsActiveToTrue()
        {
            // Arrange
            var customer = new Customer(
                "John",
                "Doe",
                "johndoe@hotmail.com",
                "0721234567",
                new Address("Storgatan1", "Stckholm", "11122", "Sweden")
            );

            // Act
            customer.Deactivate();
            customer.Activate();

            // Assert
            Assert.True(customer.IsActive);
            Assert.NotNull(customer.UpdatedAt);
        }

        [Fact]
        public void SoftDelete_ShouldMarkCustomerAsDeleted()
        {
            // Arrange
            var customer = new Customer(
                "John",
                "Doe",
                "johndoe@hotmail.com",
                "0721234567",
                new Address("Storgatan1", "Stckholm", "11122", "Sweden")
            );

            // Act
            customer.SoftDelete();

            // Assert
            Assert.False(customer.IsActive);
            Assert.True(customer.IsDeleted);
            Assert.NotNull(customer.UpdatedAt);
            Assert.NotNull(customer.DeletedAt);
        }
    }
}