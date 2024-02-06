using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController] // API Controller 임을 나타냄, 자동으로 요청의 유효성 검사, 만약 유효하지 않으면 400 Bad Request 반환
    [Route("api/[controller]")] // 라우팅 설정, "[controller]" 는 placeholder 로, 자동으로 라우트 설정됨
    public class BaseApiController : ControllerBase
    {
        // private 은 오직 이 클래스 안에서만 사용 가능
        private IMediator _mediator;

        // 주로 상속 관계에서 많이 쓰임, private 은 선언된 클래스 내에서만 사용 가능하지만, protected 는 선언된 클래스 그리고 상속된 자식 클래스에서 사용 가능
        // ??= 는 null 조건부 연산자로, _mediator 가 null 인 경우에만 ??= 다음에 오는 코드를 실행함
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}