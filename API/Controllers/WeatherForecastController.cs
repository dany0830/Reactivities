using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController] // API Controller 임을 나타냄, 자동으로 요청의 유효성 검사, 만약 유효하지 않으면 400 Bad Request 반환
[Route("[controller]")] // 라우팅 설정, "[controller]" 는 placeholder 로, 자동으로 라우트 설정됨, 이 경우 "/WeatherForecast"
public class WeatherForecastController : ControllerBase // "ControllerBase" 는 MVC 컨트롤러에서 사용되는 기본 클래스
{
    private static readonly string[] Summaries = new[] // readonly 는 한 번 값이 할당되면 할당된 장소 이외의 곳에선 해당 값을 변경할 수 없게 만듦.
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    // ILogger : 소프트웨어 실행 중 발생하는 이벤트들에 대한 로그를 남기기 위한 인터페이스
    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")] // GET 요청에 응답
    public IEnumerable<WeatherForecast> Get()
    {
        // WeatherForecast.cs 파일에 있는 Date, TemperatureC, TemperatureF, Summary 형식에 맞춰 결과값 return
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}