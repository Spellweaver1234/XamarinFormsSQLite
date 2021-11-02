using System.Collections.Generic;
using System.Threading.Tasks;

using SQLite;

namespace XamarinFormsSQLite
{
    public class FriendAsyncRepository
    {
        SQLiteAsyncConnection database;

        public FriendAsyncRepository(string databasePath)
        {
            database = new SQLiteAsyncConnection(databasePath);
        }

        public async Task CreateTable()
        {
            await database.CreateTableAsync<Friend>();
        }

        public async Task<List<Friend>> GetItemsAsync()
        {
            return await database.Table<Friend>().ToListAsync();

            // для обработки большой выборки или выборки с условием, чтобы не зависал UI поток
            //var friends = await database.Table<Friend>().ToListAsync().ConfigureAwait(false);
            // Операции продолжают выполняться в фоновом потоке, а не в UI-потоке
            //for(var friend in friends) { ... }

        }
        public async Task<Friend> GetItemAsync(int id)
        {
            return await database.GetAsync<Friend>(id);
        }
        public async Task<int> DeleteItemAsync(Friend item)
        {
            return await database.DeleteAsync(item);
        }
        public async Task<int> SaveItemAsync(Friend item)
        {
            if (item.Id != 0)
            {
                await database.UpdateAsync(item);
                return item.Id;
            }
            else
            {
                return await database.InsertAsync(item);
            }
        }
    }
}
