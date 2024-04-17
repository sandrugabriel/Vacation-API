using Moq;
using VacationAPI.Dto;
using VacationAPI.Exceptions;
using VacationAPI.Repository.interfaces;
using VacationAPI.Service.interfaces;
using VacationAPI.Service;
using VacationAPI.Models;
using VacationAPI.Constants;
using Teste.Helpers;

namespace Teste.UnitTests
{
    public class TestCommandService
    {

        Mock<IRepository> _mock;
        ICommandService _service;

        public TestCommandService()
        {
            _mock = new Mock<IRepository>();
            _service = new CommandService(_mock.Object);
        }

        [Fact]
        public async Task Create_InvalidData()
        {

            var create = new CreateRequest
            {
                Destination = "Test",
                Duration = 10,
                Price =  0
            };

            _mock.Setup(repo => repo.Create(create)).ReturnsAsync((Vacation)null);

            var exception = await Assert.ThrowsAsync<InvaidPrice>(() => _service.Create(create));

            Assert.Equal(Constants.InvalidPrice, exception.Message);


        }

        [Fact]
        public async Task Create_ReturnVacation()
        {
            var create = new CreateRequest
            {

                Destination = "Test",
                Duration = 10,
                Price = 2000
            };

            var vacation = TestVacationFactory.CreateVacation(5);
            vacation.Price = create.Price;
            _mock.Setup(repo => repo.Create(It.IsAny<CreateRequest>())).ReturnsAsync(vacation);

            var result = await _service.Create(create);

            Assert.NotNull(result);

            Assert.Equal(result, vacation);

        }

        [Fact]
        public async Task Update_ItemDoesNotExist()
        {
            var update = new UpdateRequest
            {
                Price = 2000
            };

            _mock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Vacation)null);

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.Update(1, update));

            Assert.Equal(Constants.ItemDoesNotExist, exception.Message);

        }

        [Fact]
        public async Task Update_InvalidData()
        {
            var update = new UpdateRequest
            {
                Price = 0
            };

            var vacation = TestVacationFactory.CreateVacation(5);
            vacation.Price = update.Price.Value;


            _mock.Setup(repo => repo.GetByIdAsync(5)).ReturnsAsync(vacation);

            var exception = await Assert.ThrowsAsync<InvaidPrice>(() => _service.Update(5, update));

            Assert.Equal(Constants.InvalidPrice, exception.Message);
        }

        [Fact]
        public async Task Update_ValidData()
        {
            var update = new UpdateRequest
            {
                Price = 2010
            };

            var vacation = TestVacationFactory.CreateVacation(5);
            vacation.Price = update.Price.Value;

            _mock.Setup(repo => repo.GetByIdAsync(5)).ReturnsAsync(vacation);
            _mock.Setup(repo => repo.Update(It.IsAny<int>(), It.IsAny<UpdateRequest>())).ReturnsAsync(vacation);

            var result = await _service.Update(5, update);

            Assert.NotNull(result);
            Assert.Equal(vacation, result);

        }

        [Fact]
        public async Task Delete_ItemDoesNotExist()
        {
            _mock.Setup(repo => repo.DeleteById(It.IsAny<int>())).ReturnsAsync((Vacation)null);

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.Delete(5));

            Assert.Equal(exception.Message, Constants.ItemDoesNotExist);
        }

        [Fact]
        public async Task Delete_ValidData()
        {
            var vacation = TestVacationFactory.CreateVacation(1);

            _mock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(vacation);

            var result = await _service.Delete(1);

            Assert.NotNull(result);
            Assert.Equal(vacation, result);
        }

    }
}