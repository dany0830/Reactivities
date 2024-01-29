using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controllers
{
    public class ActivitiesController : BaseApiController
    {
        // Dependency Injection (의존성 주입), _context 변수를 컨트롤러 내에서 사용 가능, 얘는 데이터베이스 컨텍스트에 접근 가능
        private readonly DataContext _context;
        public ActivitiesController(DataContext context)
        {
            _context = context;
        }

        // 모든 요소 불러오기 (GET 하기)
        [HttpGet] // api/activities
        public async Task<ActionResult<List<Activity>>> GetActivities()
        {
            // Activities 는 내 데이터베이스 테이블 이름, ToListAsync() 는 모든 활동 데이터를 async 형식으로 가져옴
            return await _context.Activities.ToListAsync();
        }

        // ID 에 따른 특정 요소 하나만 불러오기 (GET 하기)
        [HttpGet("{id}")] // api/activities/ffffff
        public async Task<ActionResult<Activity>> GetActivity(Guid id)
        {
            return await _context.Activities.FindAsync(id);
        }
    }
}