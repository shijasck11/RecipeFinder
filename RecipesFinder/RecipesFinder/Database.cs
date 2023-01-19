using SQLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace RecipesFinder
{
    public class Database
    {
        private readonly SQLiteAsyncConnection _database;  
        

        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Recipe>();
        }

        public Task<List<Recipe>> GetFavouriteRecipes()
        {
            return _database.Table<Recipe>().ToListAsync();
        }

        public Task<int> SaveFavourite(Recipe recipe)
        {
            return _database.InsertAsync(recipe);
        }

        public Task<int> RemoveFavourite(Recipe recipe)
        {
            return _database.DeleteAsync(recipe);
        }
       


    }
}
