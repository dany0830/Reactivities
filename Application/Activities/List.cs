using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence;

namespace Application.Activities
{
    // 모든 Activity 의 목록을 리턴하는 클래스
    public class List
    {
        public class Query : IRequest<List<Activity>> { }

        // Query 요청을 처리하는 클래스, Query 요청을 처리하고 List<Activity>, 즉 활동 목록을 반환
        public class Handler : IRequestHandler<Query, List<Activity>>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<List<Activity>> Handle(Query request, CancellationToken cancellationToken)
            {
                /**
                이 try catch 문은 로그를 남기는 ILogger 를 사용해 1000ms 간격으로 GET 요청을 수행할 때마다 로그를 남김. 또한, Postman 같은 곳에서 작업이 Cancel 되면 로그를 남김.
                하지만, Controller 에서 이 메소드가 실행되는 메소드의 파라미터에 CancellationToken 을 부여하지 않으면 작업이 cancel 되어도 catch 문 내의 코드가 실행되지 않음.
                try
                {
                    for (var i = 0; i < 10; i++)
                    {
                        cancellationToken.ThrowIfCancellationRequested();
                        await Task.Delay(1000, cancellationToken);
                        _logger.LogInformation($"Task {i} has completed!");
                    }
                }
                catch (System.Exception)
                {
                    _logger.LogInformation("Task was cancelled!");
                }
                */

                return await _context.Activities.ToListAsync();
            }
        }
    }
}