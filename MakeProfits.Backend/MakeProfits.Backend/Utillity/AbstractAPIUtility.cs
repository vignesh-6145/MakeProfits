namespace MakeProfits.Backend.Utillity
{
    public abstract class AbstractAPIUtility
    {
        public HttpClient _httpClient {  get; set; }
        public HttpResponseMessage _response {  get; set; }
        public string _responseMessage {  get; set; }
        public AbstractAPIUtility()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://financialmodelingprep.com/api/v3/");
        }

        public abstract Task MakeGet(string url);
    }
}
