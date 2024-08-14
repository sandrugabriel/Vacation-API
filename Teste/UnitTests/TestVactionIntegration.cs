using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Teste.Helpers;
using Teste.Infrastructrue;
using VacationAPI.Dto;
using VacationAPI.Models;

namespace Teste.UnitTests
{
    public class TestVactionIntegration : IClassFixture<ApiWebApplicationFactory>
    {

        private readonly HttpClient _client;

        public TestVactionIntegration(ApiWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAllVacations_VacationsFound_ReturnsOkStatusCode_ValidResponse()
        {
            var createVacationRequest = TestVacationFactory.CreateVacation(1);
            var content = new StringContent(JsonConvert.SerializeObject(createVacationRequest), Encoding.UTF8, "application/json");
            await _client.PostAsync("/api/v1/ControllerVacation/CreateVacation", content);

            var response = await _client.GetAsync("/api/v1/ControllerVacation/All");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetVacationById_VacationFound_ReturnsOkStatusCode()
        {
            var createVacationRequest = new CreateRequest
            { Price = 1000, Duration = 2, Destination = "Asdasd" };
            var content = new StringContent(JsonConvert.SerializeObject(createVacationRequest), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/v1/ControllerVacation/CreateVacation", content);
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Vacation>(responseString);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(result.Destination, createVacationRequest.Destination);
        }

        [Fact]
        public async Task GetVacationById_VacationNotFound_ReturnsNotFoundStatusCode()
        {
            var response = await _client.GetAsync("/api/v1/Vacation/FindById?id=9999");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Post_Create_ValidRequest_ReturnsCreatedStatusCode()
        {
            var request = "/api/v1/ControllerVacation/CreateVacation";
            var createVacationRequest = new CreateRequest
            { Price = 1000, Duration = 2, Destination = "Asdasd" };
            var content = new StringContent(JsonConvert.SerializeObject(createVacationRequest), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(request, content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Vacation>(responseString);

            Assert.NotNull(result);
            Assert.Equal(createVacationRequest.Destination, result.Destination);
        }

        [Fact]
        public async Task Put_Update_ValidRequest_ReturnsAcceptedStatusCode()
        {
            var request = "/api/v1/ControllerVacation/createVacation";
            var createVacation = new CreateRequest
            { Price = 1000, Duration = 2, Destination = "Asdasd" };
            var content = new StringContent(JsonConvert.SerializeObject(createVacation), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(request, content);
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Vacation>(responseString);

            request = $"/api/v1/ControllerVacation/UpdateVacation?id={result.Id}";
            var updateVacation = new UpdateRequest { Destination = "12test" };
            content = new StringContent(JsonConvert.SerializeObject(updateVacation), Encoding.UTF8, "application/json");

            response = await _client.PutAsync(request, content);
            var responceStringUp = await response.Content.ReadAsStringAsync();
            var result1 = JsonConvert.DeserializeObject<Vacation>(responceStringUp);


            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(result1.Destination, updateVacation.Destination);
        }

        [Fact]
        public async Task Put_Update_InvalidVacationDate_ReturnsBadRequestStatusCode()
        {
            var request = "/api/v1/ControllerVacation/CreateVacation";
            var createVacation = new CreateRequest
            { Price = 1000, Duration = 2, Destination = "Asdasd" };
            var content = new StringContent(JsonConvert.SerializeObject(createVacation), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(request, content);
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Vacation>(responseString);

            request = $"/api/v1/ControllerVacation/UpdateVacation?id={result.Id}";
            var updateVacation = new UpdateRequest { Price = 0 };
            content = new StringContent(JsonConvert.SerializeObject(updateVacation), Encoding.UTF8, "application/json");

            response = await _client.PutAsync(request, content);
            var responceStringUp = await response.Content.ReadAsStringAsync();
            var result1 = JsonConvert.DeserializeObject<Vacation>(responseString);


            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.NotEqual(result1.Destination, updateVacation.Destination);
        }

        [Fact]
        public async Task Put_Update_VacationDoesNotExist_ReturnsNotFoundStatusCode()
        {
            var request = "/api/v1/ControllerVacation/UpdateVacation";
            var updateVacation = new UpdateRequest { Destination = "asd" };
            var content = new StringContent(JsonConvert.SerializeObject(updateVacation), Encoding.UTF8, "application/json");

            var response = await _client.PutAsync(request, content);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Delete_Delete_VacationExists_ReturnsDeletedVacation()
        {
            var request = "/api/v1/ControllerVacation/CreateVacation";
            var createVacation = new CreateRequest
            { Price = 1000, Duration = 2, Destination = "Asdasd" };
            var content = new StringContent(JsonConvert.SerializeObject(createVacation), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(request, content);
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Vacation>(responseString)!;

            request = $"/api/v1/ControllerVacation/DeleteVacation?id={result.Id}";

            response = await _client.DeleteAsync(request);
            var responceString = await response.Content.ReadAsStringAsync();
            var result1 = JsonConvert.DeserializeObject<Vacation>(responseString);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(result1.Destination, createVacation.Destination);
        }

        [Fact]
        public async Task Delete_Delete_VacationDoesNotExist_ReturnsNotFoundStatusCode()
        {
            var request = "/api/v1/ControllerVacation/DeleteVacation?id=7";

            var response = await _client.DeleteAsync(request);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

    }
}
