using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : DbContext // DbContext : Entity Framework 에서 제공하는 핵심 클래스, 데이터베이스와의 연결, 통신 관리, 데이터베이스 쿼리 변경 사항 반영
    {
        // Constructor, DbContextOptions : 데이터베이스 연결 및 구성을 위한 옵션 설정, base(options) 는 위의 상속받은 DbContext 클래스의 Constructor 호출
        public DataContext(DbContextOptions options) : base(options) { }
        
        public DbSet<Activity> Activities { get; set; }
    }
}