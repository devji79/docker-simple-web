using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SimpleWeb.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    
    public List<WeatherForecast> Forecasts { get; set; }

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
        this.Forecasts = new List<WeatherForecast>();
    }

    public async Task OnGetAsync()
    {
        var client = new HttpClient();
        var response = await client.GetAsync("http://simple-api/weatherforecast");
        var content = await response.Content.ReadAsStringAsync();
        var weatherForecasts = JsonSerializer.Deserialize<List<WeatherForecast>>(content);
        if (weatherForecasts != null)
        {
            this.Forecasts = weatherForecasts;
        }
    }


    public class WeatherForecast {
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("temperatureC")]
        public int TemperatureC { get; set; }

        [JsonPropertyName("temperatureF")]
        public int TemperatureF { get; set; }

        [JsonPropertyName("summary")]
        public string? Summary { get; set; }

        public WeatherForecast(DateTime date, int temperatureC, int temperatureF, string? summary){
            this.Date = date;
            this.TemperatureC = temperatureC;
            this.TemperatureF = temperatureF;
            this.Summary = summary;
        }
    }
}
