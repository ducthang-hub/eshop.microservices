﻿using BuildingBlocks.Helpers;
using BuildingBlocks.MessageQueue.Consumer;

namespace Basket.API.BackgroundServices;

public class MessageConsumerService(ILogger<MessageConsumerService> logger, IConsumer consumer) : ConsumerService(consumer, logger)
{
    protected override string QueueName { get; set; } = "Hello";
    
    protected override async Task HandleMessage(string message)
    {
        logger.LogInformation($" [x] Consumer by {nameof(MessageConsumerService)} Received {message}");
                
        int dots = message.Split('.').Length - 1;
        await Task.Delay(dots * 1000);

        logger.LogInformation($" [x] Consumer by {nameof(MessageConsumerService)} Done");
    }
}