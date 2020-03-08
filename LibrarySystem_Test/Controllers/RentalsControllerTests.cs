using LibrarySystem.Controllers;
using LibrarySystem.Entities;
using LibrarySystem.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem_Test.Controllers
{
    [TestFixture]
    class RentalsControllerTests
    {
        [Test]
        public async Task Post_Create_ModelValid_Redirected()
        {
            Mock<IRentalsService> rentalsRepository = MockRentalsService();
            Mock<IBooksService> booksRepository = MockBooksService();
            Mock<ICustomersService> customersRepository = MockCustomersService();
            var controller = new RentalsController(rentalsRepository.Object, booksRepository.Object, customersRepository.Object);
            var rental = new Rental { };

            var result = await controller.Create(rental) as RedirectToActionResult;

            Assert.AreEqual(result.ActionName, "Index");
        }

        [Test]
        public async Task Post_Create_ModelNotValid_ViewResultWithModel()
        {
            Mock<IRentalsService> rentalsRepository = MockRentalsService();
            Mock<IBooksService> booksRepository = MockBooksService();
            Mock<ICustomersService> customersRepository = MockCustomersService();
            var controller = new RentalsController(rentalsRepository.Object, booksRepository.Object, customersRepository.Object);
            var rental = new Rental { };
            controller.ModelState.AddModelError("test", "test");

            var result = await controller.Create(rental) as ViewResult;

            Assert.AreEqual(result.Model, rental);
        }

        [Test]
        public async Task Post_Edit_ModelValid_Redirected()
        {
            Mock<IRentalsService> rentalsRepository = MockRentalsService();
            Mock<IBooksService> booksRepository = MockBooksService();
            Mock<ICustomersService> customersRepository = MockCustomersService();
            var controller = new RentalsController(rentalsRepository.Object, booksRepository.Object, customersRepository.Object);
            int id = 1;
            var rental = new Rental { Id = id };

            var result = await controller.Edit(id, rental) as RedirectToActionResult;

            Assert.AreEqual(result.ActionName, "Index");
        }

        [Test]
        public async Task Post_Edit_WrongId_NotFoundResult()
        {
            Mock<IRentalsService> rentalsRepository = MockRentalsService();
            Mock<IBooksService> booksRepository = MockBooksService();
            Mock<ICustomersService> customersRepository = MockCustomersService();
            var controller = new RentalsController(rentalsRepository.Object, booksRepository.Object, customersRepository.Object);
            int id = 1;
            var rental = new Rental { Id = 2 };

            var result = await controller.Edit(id, rental);
            Assert.That(result is NotFoundResult);
        }

        [Test]
        public async Task Post_Edit_ModelInvalid_ViewResultWithModel()
        {
            Mock<IRentalsService> rentalsRepository = MockRentalsService();
            Mock<IBooksService> booksRepository = MockBooksService();
            Mock<ICustomersService> customersRepository = MockCustomersService();
            var controller = new RentalsController(rentalsRepository.Object, booksRepository.Object, customersRepository.Object);
            int id = 1;
            var rental = new Rental { Id = id };
            controller.ModelState.AddModelError("test", "test");

            var result = await controller.Edit(id, rental) as ViewResult;

            Assert.AreEqual(result.Model, rental);
        }

        private Mock<IRentalsService> MockRentalsService()
        {
            var mock = new Mock<IRentalsService>();
            mock.Setup(service => service.Update(It.IsAny<Rental>())).Returns(Task.CompletedTask);
            return mock;
        }

        private Mock<IBooksService> MockBooksService()
        {
            var mock = new Mock<IBooksService>();
            mock.Setup(service => service.GetAllAsync())
                .Returns(Task.FromResult(new List<Book>()));
            return mock;
        }

        private Mock<ICustomersService> MockCustomersService()
        {
            var mock = new Mock<ICustomersService>();
            mock.Setup(service => service.GetAllAsync())
                .Returns(Task.FromResult(new List<Customer>()));
            return mock;
        }
    }
}
