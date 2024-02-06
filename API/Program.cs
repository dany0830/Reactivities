using API.Extensions;
using Microsoft.EntityFrameworkCore;
using Persistence;

// Web Application 을 빌드하기 위함
var builder = WebApplication.CreateBuilder(args);

// Add services to the container. (Application logic, 다른 cs 클래스에서 만들어진 로직 services 로 변환해 실행)

// 컨트롤러 추가, MVC (Model-View-Controller) 서비스 사용 가능하게 함
builder.Services.AddControllers();

// ApplicationServiceExtensions 클래스에서 정의된 메소드. 이 메소드 안에 builder.Services 로 만들었던 기능들 모두 포함되어 있음 (Mediator, AutoMapper 사용하게 해주는 코드)
builder.Services.AddApplicationServices(builder.Configuration);

// app 변수에 Web Application 빌드한 거 저장
var app = builder.Build();

// Configure the HTTP request pipeline. Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 위 builder 서비스에 저장한 CorsPolicy
app.UseCors("CorsPolicy");

// 사용자 인증하기 위한 수단, 보안 담당
app.UseAuthorization();

// 등록된 컨트롤러 매핑, 라우팅 설정, HTTP request 에 따라 알맞은 API Endpoints 매핑 & Respond
app.MapControllers();

// 일시적인 scope 를 담아두기 위한 변수, 일시적이 지나면 자동으로 해체됨
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    // 여기서 데이터베이스 migration 수행 (새로운 데이터베이스 생성하거나 수정을 적용)
    var context = services.GetRequiredService<DataContext>();
    await context.Database.MigrateAsync();
    await Seed.SeedData(context);
}
catch (Exception ex)
{
    // 애플리케이션 실행 중 발생하는 Logger 사용해서 오류 로그 기록
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occured during migration!");
}

// 최종적으로 실행하는 코드
app.Run();