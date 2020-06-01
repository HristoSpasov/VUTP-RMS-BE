namespace RMS.Services.Strategies.EventFilterStrategies
{
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Linq;

    public class EventFilterFactory : IEventFilterFactory
    {
        private readonly IServiceProvider serviceProvider;

        public EventFilterFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public IEventFilterStrategy GetEventFilterStrategy(string type)
        {
            return this.serviceProvider.GetServices<IEventFilterStrategy>()
                .SingleOrDefault(s => s.IsApplicable(type));
        }
    }
}
