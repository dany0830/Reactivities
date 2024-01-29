using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController] // API Controller 임을 나타냄, 자동으로 요청의 유효성 검사, 만약 유효하지 않으면 400 Bad Request 반환
    [Route("api/[controller]")] // 라우팅 설정, "[controller]" 는 placeholder 로, 자동으로 라우트 설정됨
    public class BaseApiController : ControllerBase
    {
        
    }
}