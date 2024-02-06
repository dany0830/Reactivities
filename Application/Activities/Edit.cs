using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.Activities
{
    // 기존의 Activity 를 수정하는 클래스
    public class Edit
    {
        public class Command : IRequest
        {
            public Activity Activity { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = await _context.Activities.FindAsync(request.Activity.Id);

                // ?? 는 null 병합 연산자로, request.Activity.Title 이 null 이면 activity.Title 을 리턴, requset.Activity.Title 이 null 이 아니면 본인을 리턴함
                // activity.Title = request.Activity.Title ?? activity.Title;

                // request.Activity 를 매핑함, activity 는 매핑한 데이터를 받아서 저장하거나 업데이트됨
                _mapper.Map(request.Activity, activity);

                await _context.SaveChangesAsync();
            }
        }
    }
}