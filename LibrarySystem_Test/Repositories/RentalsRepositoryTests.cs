using LibrarySystem.Entities;
using LibrarySystem.Repositories;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LibrarySystem_Test.Repositories
{
    [TestFixture]
    class RentalsRepositoryTests
    {
        [Test]
        public async Task Add_RentalCreated()
        {
            var context = MockLibrarySystemContext();
            var repository = new RentalsRepository(context.Object);
            var rental = new Rental { };
            
            await repository.Add(rental);

            context.Verify(context => context.Add(rental), Times.AtLeastOnce);
            context.Verify(context => context.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.AtLeastOnce);
        }

        [Test]
        public async Task Update_RentalUpdated()
        {
            var context = MockLibrarySystemContext();
            var repository = new RentalsRepository(context.Object);
            var rental = new Rental { };

            await repository.Update(rental);

            context.Verify(context => context.Update(rental), Times.AtLeastOnce);
            context.Verify(context => context.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.AtLeastOnce);
        }


        private Mock<LibrarySystemContext> MockLibrarySystemContext()
        {
            var mock = new Mock<LibrarySystemContext>();
            return mock;
        }
    }
}
