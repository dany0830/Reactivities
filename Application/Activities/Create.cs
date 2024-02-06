using Domain;
using MediatR;
using Persistence;

namespace Application.Activities
{
    // 새로운 Activity 만드는 클래스
    public class Create
    {
        public class Command : IRequest
        {
            public Activity Activity { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                // Activites 는 내 데이터 테이블 이름, .Add() 는 Activity 를 데이터베이스에 추가하는 역할
                _context.Activities.Add(request.Activity);

                await _context.SaveChangesAsync();
            }
        }
    }
}