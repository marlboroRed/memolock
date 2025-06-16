using SQLite;
using memolock.Models;

namespace memolock.Services
{
    public class DatabaseService
    {
        private readonly SQLiteAsyncConnection _database;

        // 생성자: 데이터베이스 연결 및 테이블 생성
        public DatabaseService()
        {
            // 앱의 데이터 디렉토리에 데이터베이스 파일 경로 설정
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "memolock.db3");
            _database = new SQLiteAsyncConnection(dbPath);
            // Memo 테이블이 없으면 생성
            _database.CreateTableAsync<Memo>().Wait();
        }

        // 모든 메모를 비동기적으로 가져옵니다.
        public Task<List<Memo>> GetMemosAsync() => _database.Table<Memo>().ToListAsync();

        // 특정 ID의 메모를 가져옵니다.
        public Task<Memo> GetMemoAsync(int id) => _database.Table<Memo>().Where(i => i.Id == id).FirstOrDefaultAsync();

        // 메모를 저장하거나 업데이트합니다.
        public Task<int> SaveMemoAsync(Memo memo)
        {
            if (memo.Id != 0)
            {
                // ID가 있으면 기존 메모 업데이트
                return _database.UpdateAsync(memo);
            }
            else
            {
                // ID가 없으면 새 메모 삽입
                return _database.InsertAsync(memo);
            }
        }

        // 메모를 삭제합니다.
        public Task<int> DeleteMemoAsync(Memo memo) => _database.DeleteAsync(memo);
    }
}
