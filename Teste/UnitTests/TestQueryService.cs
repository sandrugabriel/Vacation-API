using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacationAPI.Exceptions;
using VacationAPI.Repository.interfaces;
using VacationAPI.Service.interfaces;
using VacationAPI.Service;
using VacationAPI.Constants;
using VacationAPI.Models;
using Teste.Helpers;

namespace Teste.UnitTests
{
    public class TestQueryService
    {

        Mock<IRepository> _mock;
        IQueryService _service;

        public TestQueryService()
        {
            _mock = new Mock<IRepository>();
            _service = new QueryService(_mock.Object);
        }

        [Fact]
        public async Task GetAll_ItemsDoNotExist()
        {
            _mock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<Vacation>());

            var exception = await Assert.ThrowsAsync<ItemsDoNotExist>(() => _service.GetAll());

            Assert.Equal(exception.Message, Constants.ItemsDoNotExist);

        }

        [Fact]
        public async Task GetAll_ReturnAllVacation()
        {
            var vacations = TestVacationFactory.CreateVacations(5);

            _mock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(vacations);

            var result = await _service.GetAll();

            Assert.NotNull(result);
            Assert.Contains(vacations[1], result);

        }

        [Fact]
        public async Task GetById_ItemDoesNotExist()
        {
            _mock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Vacation)null);

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.GetById(1));

            Assert.Equal(Constants.ItemDoesNotExist, exception.Message);
        }

        [Fact]
        public async Task GetById_ReturnVacation()
        {

            var vacation = TestVacationFactory.CreateVacation(5);

            _mock.Setup(repo => repo.GetByIdAsync(5)).ReturnsAsync(vacation);

            var result = await _service.GetById(5);

            Assert.NotNull(result);
            Assert.Equal(vacation, result);

        }

        [Fact]
        public async Task GetByName_ItemDoesNotExist()
        {

            _mock.Setup(repo => repo.GetByNameAsync("")).ReturnsAsync((Vacation)null);

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.GetByNameAsync(""));

            Assert.Equal(Constants.ItemDoesNotExist, exception.Message);
        }

        [Fact]
        public async Task GetByName_ReturnVacation()
        {
            var vacation = TestVacationFactory.CreateVacation(3);

            vacation.Destination = "test";
            _mock.Setup(repo => repo.GetByNameAsync("test")).ReturnsAsync(vacation);

            var result = await _service.GetByNameAsync("test");

            Assert.NotNull(result);
            Assert.Equal(vacation, result);
        }

    }
}
