namespace RMS.Services.Strategies.EventFilterStrategies
{
    public interface IEventFilterFactory
    {
        IEventFilterStrategy GetEventFilterStrategy(string type);
    }
}
