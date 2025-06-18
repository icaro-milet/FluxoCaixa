namespace CashFlow.Application.Interfaces.EventBus;

public interface IEventBus
{
    Task PublishAsync(string topic, object data);
}