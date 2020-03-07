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
            Mock<IRentalsRepository> rentalsRepository = MockRentalsRepository();
            Mock<IBooksRepository> booksRepository = MockBooksRepository();
            Mock<ICustomersRepository> customersRepository = MockCustomersRepository();
            var controller = new RentalsController(rentalsRepository.Object, booksRepository.Object, customersRepository.Object);
            var rental = new Rental { };

            var result = await controller.Create(rental) as RedirectToActionResult;

            Assert.AreEqual(result.ActionName, "Index");
        }

        [Test]
        public async Task Post_Create_ModelNotValid_ViewResultWithModel()
        {
            Mock<IRentalsRepository> rentalsRepository = MockRentalsRepository();
            Mock<IBooksRepository> booksRepository = MockBooksRepository();
            Mock<ICustomersRepository> customersRepository = MockCustomersRepository();
            var controller = new RentalsController(rentalsRepository.Object, booksRepository.Object, customersRepository.Object);
            var rental = new Rental { };
            controller.ModelState.AddModelError("test", "test");

            var result = await controller.Create(rental) as ViewResult;

            Assert.AreEqual(result.Model, rental);
        }

        [Test]
        public async Task Post_Edit_ModelValid_Redirected()
        {
            Mock<IRentalsRepository> rentalsRepository = MockRentalsRepository();
            Mock<IBooksRepository> booksRepository = MockBooksRepository();
            Mock<ICustomersRepository> customersRepository = MockCustomersRepository();
            var controller = new RentalsController(rentalsRepository.Object, booksRepository.Object, customersRepository.Object);
            int id = 1;
            var rental = new Rental { Id = id };

            var result = await controller.Edit(id, rental) as RedirectToActionResult;

            Assert.AreEqual(result.ActionName, "Index");
        }

        [Test]
        public async Task Post_Edit_WrongId_NotFoundResult()
        {
            Mock<IRentalsRepository> rentalsRepository = MockRentalsRepository();
            Mock<IBooksRepository> booksRepository = MockBooksRepository();
            Mock<ICustomersRepository> customersRepository = MockCustomersRepository();
            var controller = new RentalsController(rentalsRepository.Object, booksRepository.Object, customersRepository.Object);
            int id = 1;
            var rental = new Rental { Id = 2 };

            var result = await controller.Edit(id, rental);
            Assert.That(result is NotFoundResult);
        }

        [Test]
        public async Task Post_Edit_ModelInvalid_ViewResultWithModel()
        {
            Mock<IRentalsRepository> rentalsRepository = MockRentalsRepository();
            Mock<IBooksRepository> booksRepository = MockBooksRepository();
            Mock<ICustomersRepository> customersRepository = MockCustomersRepository();
            var controller = new RentalsController(rentalsRepository.Object, booksRepository.Object, customersRepository.Object);
            int id = 1;
            var rental = new Rental { Id = id };
            controller.ModelState.AddModelError("test", "test");

            var result = await controller.Edit(id, rental) as ViewResult;

            Assert.AreEqual(result.Model, rental);
        }

        private Mock<IRentalsRepository> MockRentalsRepository()
        {
            var mock = new Mock<IRentalsRepository>();
            return mock;
        }

        private Mock<IBooksRepository> MockBooksRepository()
        {
            var mock = new Mock<IBooksRepository>();
            mock.Setup(repository => repository.GetAllAvailableAsync())
                .Returns(Task.FromResult(new List<Book>()));
            return mock;
        }

        private Mock<ICustomersRepository> MockCustomersRepository()
        {
            var mock = new Mock<ICustomersRepository>();
            mock.Setup(repository => repository.GetAllAsync())
                .Returns(Task.FromResult(new List<Customer>()));
            return mock;
        }
    }
}
