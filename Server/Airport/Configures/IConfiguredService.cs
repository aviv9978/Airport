namespace FlightSimulator.Configures
{
    public interface IConfiguredService
    {
        string GetSecretKey();
        string GetClient();
        string GetServer();
    }
}
