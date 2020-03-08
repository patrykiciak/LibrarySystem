using LibrarySystem.Entities;
using LibrarySystem.Interfaces;
using LibrarySystem.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace LibrarySystem_Test.Services
{
    [TestFixture]
    class RentalsServiceTests
    {
        [Test]
        public async Task Add_CorrectData_RentalCreated()
        {
            var repository = MockRentalsRepository();
            var service = new RentalsService(repository.Object);
            var rental = new Rental { Id = 1, BookId = 1, CustomerId = Guid.NewGuid(), StartDate = new DateTime(2012, 12, 12) };

            await service.Add(rental);

            repository.Verify(repository => repository.Add(rental), Times.AtLeastOnce);
        }

        [TestCaseSource(nameof(invalidDataCases))]
        public async Task Add_InvalidData_ExceptionThrown(int id, int bookId, Guid customerId, DateTime startDate, DateTime endDate)
        {
            var repository = MockRentalsRepository();
            var service = new RentalsService(repository.Object);
            var rental = new Rental { BookId = bookId, CustomerId = customerId, Id = id, StartDate = startDate, EndDate = endDate };
            try
            {
                await service.Add(rental);
            }
            catch (ArgumentException)
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [Test]
        public async Task Update_CorrectData_RentalCreated()
        {
            var repository = MockRentalsRepository();
            var service = new RentalsService(repository.Object);
            var rental = new Rental { Id = 1, BookId = 1, CustomerId = Guid.NewGuid(), StartDate = new DateTime(2012, 12, 12) };

            await service.Update(rental);

            repository.Verify(repository => repository.Update(rental), Times.AtLeastOnce);
        }

        [TestCaseSource(nameof(invalidDataCases))]
        public async Task Update_InvalidData_ExceptionThrown(int id, int bookId, Guid customerId, DateTime startDate, DateTime endDate)
        {
            var repository = MockRentalsRepository();
            var service = new RentalsService(repository.Object);
            var rental = new Rental { Id = id, BookId = bookId, CustomerId = customerId, StartDate = startDate, EndDate = endDate };

            try
            {
                await service.Update(rental);
            }
            catch (ArgumentException)
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        private Mock<IRentalsRepository> MockRentalsRepository()
        {
            var repository = new Mock<IRentalsRepository>();
            return repository;
        }

        private static readonly Guid guid = Guid.Parse("123e4567-e89b-12d3-a456-426655440000");
        private static readonly object[] invalidDataCases =
        {   // id, bookId, customerId, startDate, endDate
            new object [] {-1, 1, guid, new DateTime(2012, 12, 12), new DateTime(2013, 12, 12)}, // negative id
            new object [] {1, -1, guid, new DateTime(2012, 12, 12), new DateTime(2013, 12, 12)},  // negative bookId
            new object [] {1, 1, guid, DateTime.MinValue, new DateTime(2013, 12, 12)},  // not entered (min) startDate
            new object [] {1, 1, guid, new DateTime(2012, 12, 12), new DateTime(2011, 12, 12)},  // endDate earlier than startDate
            new object [] {1, 1, guid, DateTime.MinValue, DateTime.MinValue},  // both dates invalid
            new object [] {1, 1, Guid.Empty, new DateTime(2012, 12, 12), new DateTime(2013, 12, 12)}  // empty guid
        };
    }
}
