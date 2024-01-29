namespace API;

public class WeatherForecast
{
    public DateOnly Date { get; set; }
    public int TemperatureC { get; set; }
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    // ? 은 해당 값이 null 을 허용하는지 아닌지 여부
    public string Summary { get; set; }
}