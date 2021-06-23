﻿using Deus_Models.Models;
using deusbarbershop.Request;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests
{
    public class AppointmentControllerTests : IntegrationTests.IntegrationTest
    {
        public AppointmentControllerTests(ApiWebApplicationFactory fixture) : base(fixture)
        { }
    
        [Fact]
        public async Task Get_Should_Return_Appointment()
        {
            var response = await _client.GetAsync("/Appointment");
            response.EnsureSuccessStatusCode();

            var appointment = JsonConvert.DeserializeObject<List<Appointment>>(
                await response.Content.ReadAsStringAsync()
                );

            Assert.True(appointment.Count > 0);
        }

        [Fact]
        public async Task Get_Should_Return_AppointmentWithId()
        {
            var response = await _client.GetAsync("/Appointment/41");
            response.EnsureSuccessStatusCode();

            var appointment = JsonConvert.DeserializeObject<Appointment>(
                await response.Content.ReadAsStringAsync()
                );

            response.EnsureSuccessStatusCode();

        }

        [Fact]
        public async Task Post_Appointment_Returns_BadRequest()
        {
            using (_client)
            {
                var response = await _client.PostAsync("/Appointment", new StringContent(JsonConvert.SerializeObject(new Appointment()
                {
                    AppointmentDate = DateTime.Now.AddDays(1),
                    Customer_Id = 1,
                    Service_Id = 2,
                    Customer = new Customer
                    {
                        FirstName = "N",
                        LastName = "T",
                        EmailAddress = "",
                        PhoneNumber = "2102725313",
                        
                    },
                    Service = new Service
                    {
                        
                        ServiceDescription = "T",
                        ServiceDuration = 25,
                        ServiceName = "T",
                        ServicePrice = 10
                    }
                }),
                Encoding.UTF8,
                "application/json"           
                ));

                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
            }
        }


        [Theory]
        [InlineData("/Appointment")]
        [InlineData("/Appointment/41")]
        public async Task Endpoint_Test_Should_ResultInOK(string endpoint)
        {
            var response = await _client.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();
            Assert.Equal("application/json; charset=utf-8",
            response.Content.Headers.ContentType.ToString());
            
        }
    }
}
