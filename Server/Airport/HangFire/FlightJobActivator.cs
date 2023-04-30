using Hangfire;

namespace Airport.HangFire
{
    public class FlightJobActivator : JobActivator
    {
        private readonly IServiceProvider _serviceProvider;

        public FlightJobActivator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public override object ActivateJob(Type type)
        {
            return _serviceProvider.GetService(type);
        }
    }
}
