namespace FlightSimulator.Errors
{
    public class APIResponce
    {
        public int statusCode { get; set; }
        public string Message { get; set; }

        public APIResponce(int StatusCode, string message = null)
        {
            statusCode = StatusCode;
            Message = message ?? DefaultStatusCodeMessage(StatusCode);
        }
        private string DefaultStatusCodeMessage(int StatusCode)
        {
            return StatusCode switch
            {
                400 => "A bad request you have made",
                401 => "You have not authorized",
                404 => "Resource was not Found",
                500 => "Error 500",
                0 => "Something Went Wrong",
                _ => throw new NotImplementedException()
            };
        }
    }
}
