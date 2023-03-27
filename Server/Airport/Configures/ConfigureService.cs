namespace FlightSimulator.Configures
{
    public class ConfigureService
    {

        public string GetClient()
        {
            return "http://localhost:4200";
        }

        public string GetServer()
        {
            return "https://localhost:7297";
        }
    }
}
