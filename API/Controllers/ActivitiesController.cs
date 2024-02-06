using Application.Activities;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controllers
{
    public class ActivitiesController : BaseApiController
    {
        // 모든 요소 불러오기 (GET 하기)
        [HttpGet] // api/activities
        public async Task<ActionResult<List<Activity>>> GetActivities()
        {
            // Activities 는 내 데이터베이스 테이블 이름, ToListAsync() 는 모든 활동 데이터를 async 형식으로 가져옴
            // return await _context.Activities.ToListAsync();

            // GET request 를 받으면 Mediator 를 사용해서 Activity 목록을 불러옴
            // Mediator 는 BaseApiController 에서 protected 로 선언한 변수, 이 클래스는 자식 클래스이므로 사용 가능
            return await Mediator.Send(new List.Query());
        }

        /**
        메소드의 파라미터에 CancellationToken 을 줌으로써 작업이 cancel 되었을 시 제대로 cancel 을 수행함.
        public async Task<ActionResult<List<Activity>>> GetActivities(CancellationToken ct)
        {
            return await Mediator.Send(new List.Query(), ct);
        }
        */

        // ID 에 따른 특정 요소 하나만 불러오기 (GET 하기)
        [HttpGet("{id}")] // api/activities/ffffff
        public async Task<ActionResult<Activity>> GetActivity(Guid id)
        {
            // Details 클래스 안에 있는 Query 클래스의 객체를 생성, 그 클래스 안에 있는 Id 에 파라미터로 받은 id 할당해서 특정 Activity 리턴
            return await Mediator.Send(new Details.Query{Id = id});
        }

        // 새로운 Activity 생성하기 (POST)
        [HttpPost]
        public async Task<IActionResult> CreateActivity(Activity activity)
        {
            // Create 클래스 안에 있는 Command 클래스의 객체를 생성, 그 클래스 안에 있는 Activity 에 파라미터로 받은 activity 할당, Create 클래스 안에 있는 Handle 메소드 통해 새로운 Activity 생성
            await Mediator.Send(new Create.Command {Activity = activity});
            
            return Ok();
        }

        // 기존에 존재하는 Activity 수정 (PUT)
        [HttpPut("{id}")]
        public async Task<IActionResult> EditActivity(Guid id, Activity activity)
        {
            activity.Id = id;

            await Mediator.Send(new Edit.Command {Activity = activity});

            return Ok();
        }

        // 기존에 존재하는 Activity 삭제 (DELETE)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivity(Guid id)
        {
            await Mediator.Send(new Delete.Command {Id = id});

            return Ok();
        }
    }
}