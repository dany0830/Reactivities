using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Activities;
using Application.Core;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Extensions
{
    // static class 는 객체 생성을 불가능하게 함. 또한, static class 내의 모든 요소 (변수, 메소드 등) 은 static 으로 선언되어야 함
    // 즉 다른 클래스에서 new 를 사용해 객체 생성을 하지 않고도 해당 클래스의 메소드를 사용할 수 있음. EX) ~~~.AddApplicationServices
    public static class ApplicationServiceExtensions
    {
        // this 의 역할은 확장 메소드를 사용하게 함. 첫 번째 파라미터로 this 키워드를 사용해야 함. 또한, static 클래스에서 정의되어야 함
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            // API endpoints 문서화하기 위한 API Explorer 서비스 추가, Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            // => is lambda expression, DataContext 클래스에서 만든 로직 (데이터베이스 연결 통신 관리 등) 을 서비스로 등록하는 과정
            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });

            // CORS Policy
            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000");
                });
            });

            // Mediator 를 사용하기 위한 코드
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(List.Handler).Assembly));

            // AutoMapper 를 사용하기 위한 코드
            services.AddAutoMapper(typeof(MappingProfiles).Assembly);

            return services;
        }
    }
}