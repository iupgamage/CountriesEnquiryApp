using Azure.Messaging.ServiceBus;
using CountriesEnquiryApp.Common.DTOs;
using CountriesEnquiryApp.Common.Models;
using CountriesEnquiryApp.Messaging.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CountriesEnquiryApp.Messaging.Services
{
    public class ServiceBusMessageSender : IServiceBusMessageSender
    {
        private readonly IConfiguration _configuration;

        public ServiceBusMessageSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendMessageAsync(CountryDto countryDto)
        {
            // create a Service Bus client 
            await using (ServiceBusClient client = new ServiceBusClient(_configuration["ServiceBusDetails:ConnectionString"]))
            {
                // create a sender for the queue 
                ServiceBusSender sender = client.CreateSender(_configuration["ServiceBusDetails:QueueName"]);

                // create the message
                var jsonString = JsonConvert.SerializeObject(countryDto);
                ServiceBusMessage message = new ServiceBusMessage(jsonString);

                // send the message
                await sender.SendMessageAsync(message);
            }
        }
    }
}
