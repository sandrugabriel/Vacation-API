using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teste.Helpers;
using VacationAPI.Constants;
using VacationAPI.Controllers;
using VacationAPI.Controllers.interfaces;
using VacationAPI.Dto;
using VacationAPI.Exceptions;
using VacationAPI.Models;
using VacationAPI.Service.interfaces;

namespace Teste.UnitTests
{
    public class TestController
    {

        Mock<ICommandService> _command;
        Mock<IQueryService> _query;
        ControllerAPI _controller;

        public TestController()
        {
            _command = new Mock<ICommandService>();
            _query = new Mock<IQueryService>();
            _controller = new ControllerVacation(_query.Object, _command.Object);
        }

        [Fact]
        public async Task GetAll_ItemsDoNotExist()
        {
            _query.Setup(repo => repo.GetAll()).ThrowsAsync(new ItemsDoNotExist(Constants.ItemsDoNotExist));
            var result = await _controller.GetAll();

            var notFound = Assert.IsType<NotFoundObjectResult>(result.Result);

            Assert.Equal(404, notFound.StatusCode);
            Assert.Equal(Constants.ItemsDoNotExist, notFound.Value);

        }

        [Fact]
        public async Task GetAll_ValidData()
        {
            var vacations = TestVacationFactory.CreateVacations(5);

            _query.Setup(repo => repo.GetAll()).ReturnsAsync(vacations);

            var result = await _controller.GetAll();

            var okresult = Assert.IsType<OkObjectResult>(result.Result);

            var vacationsAll = Assert.IsType<List<Vacation>>(okresult.Value);

            Assert.Equal(5, vacationsAll.Count);
            Assert.Equal(200, okresult.StatusCode);

        }

        [Fact]
        public async Task Create_InvalidData()
        {
            var create = new CreateRequest
            {
                Destination = "Test",
                Duration = 10,
                Price = 0
            };

            _command.Setup(repo => repo.Create(It.IsAny<CreateRequest>())).ThrowsAsync(new InvaidPrice(Constants.InvalidPrice));

            var result = await _controller.CreateVacation(create);

            var bad = Assert.IsType<BadRequestObjectResult>(result.Result);

            Assert.Equal(400, bad.StatusCode);
            Assert.Equal(Constants.InvalidPrice, bad.Value);

        }

        [Fact]
        public async Task Create_ValidData()
        {
            var create = new CreateRequest
            {
                Destination = "Test",
                Duration = 10,
                Price = 0
            };
            var vacation = TestVacationFactory.CreateVacation(5);
            vacation.Price = create.Price;

            _command.Setup(repo => repo.Create(create)).ReturnsAsync(vacation);

            var result = await _controller.CreateVacation(create);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            Assert.Equal(okResult.StatusCode, 200);
            Assert.Equal(vacation, okResult.Value);

        }

        [Fact]
        public async Task Update_InvalidPrice()
        {
            var update = new UpdateRequest
            {
                Price = 0
            };

            var vacation = TestVacationFactory.CreateVacation(5);
            vacation.Price = update.Price.Value;

            _command.Setup(repo => repo.Update(5, update)).ThrowsAsync(new InvaidPrice(Constants.InvalidPrice));

            var result = await _controller.UpdateVacation(5, update);

            var bad = Assert.IsType<BadRequestObjectResult>(result.Result);

            Assert.Equal(bad.StatusCode, 400);
            Assert.Equal(bad.Value, Constants.InvalidPrice);


        }

        [Fact]
        public async Task Update_ValidData()
        {
            var update = new UpdateRequest
            {
                Price = 200
            };
            var vacation = TestVacationFactory.CreateVacation(5);
            vacation.Price = update.Price.Value;

            _command.Setup(repo => repo.Update(5, update)).ReturnsAsync(vacation);

            var result = await _controller.UpdateVacation(5, update);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            Assert.Equal(okResult.StatusCode, 200);
            Assert.Equal(okResult.Value, vacation);

        }

        [Fact]
        public async Task Delete_ItemDoesNotExist()
        {
            _command.Setup(repo => repo.Delete(1)).ThrowsAsync(new ItemDoesNotExist(Constants.ItemDoesNotExist));

            var result = await _controller.DeleteVacation(1);

            var notFound = Assert.IsType<NotFoundObjectResult>(result.Result);

            Assert.Equal(notFound.StatusCode, 404);
            Assert.Equal(notFound.Value, Constants.ItemDoesNotExist);

        }

        [Fact]
        public async Task Delete_ValidData()
        {
            var vacation = TestVacationFactory.CreateVacation(1);

            _command.Setup(repo => repo.Delete(1)).ReturnsAsync(vacation);

            var result = await _controller.DeleteVacation(1);

            var okReult = Assert.IsType<OkObjectResult>(result.Result);

            Assert.Equal(200, okReult.StatusCode);
            Assert.Equal(vacation, okReult.Value);

        }

    }
}
