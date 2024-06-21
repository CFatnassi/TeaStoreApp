using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeaStoreApp.Models;

namespace TeaStoreApp.Services
{
	public class BookMarkItemService
	{
		private readonly SQLiteConnection _database;
        public BookMarkItemService() 
		{
			var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "entities.db");
		    _database = new SQLiteConnection(dbPath);
			_database.CreateTable<BookMarkProduct>();
		
		}

		public BookMarkProduct Read(int id)
		{
			return _database.Table<BookMarkProduct>().Where(x => x.productId == id).FirstOrDefault();

		}

		public List<BookMarkProduct> ReadAll()
		{
			return _database.Table<BookMarkProduct>().ToList();

		}

		public void Create(BookMarkProduct bookMarkedProduct)
		{
			_database.Insert(bookMarkedProduct);

		}

		public void Delete(BookMarkProduct bookMarkedProduct)
		{
			_database.Delete(bookMarkedProduct);

		}
	}
}
