using System;
using System.Collections.Generic;
using System.Linq;
using SQLite.Net;
using Xamarin.Forms;

namespace Project4Bicycle
{
	//public interface ISQLite
	//{
	//	SQLiteConnection GetConnection();
	//}

	//public class BikeContainerDatabase
	//{
	//	static object locker = new object();
	//	private SQLiteConnection database;

	//	public BikeContainerDatabase()
	//	{
	//		database = DependencyService.Get<ISQLite>().GetConnection();
	//		database.CreateTable<BikeContainer>();
	//	}

	//	public IEnumerable<BikeContainer> GetItems()
	//	{
	//		lock (locker)
	//		{
	//			return (from i in database.Table<BikeContainer>() select i).ToList();
	//		}
	//	}

	//	public bool Count()
	//	{
	//		return database.Table<BikeContainer>().Count() == 0;
	//	}

	//	public int CountItems()
	//	{
	//		return database.Table<BikeContainer>().Count();
	//	}



	//	public void Drop()
	//	{
	//		database.DeleteAll<BikeContainer>();
	//	}

	//	public BikeContainer GetItem(int id)
	//	{
	//		lock (locker)
	//		{
	//			return database.Table<BikeContainer>().FirstOrDefault(x => x.Id == id);
	//		}
	//	}



	//	public int SaveItem(BikeContainer item)
	//	{
	//		lock (locker)
	//		{
	//			if (item.Id != 0)
	//			{
	//				database.Update(item);
	//				return item.Id;
	//			}
	//			else {
	//				return database.Insert(item);
	//			}
	//		}
	//	}

	//	public int DeleteItem(int id)
	//	{
	//		lock (locker)
	//		{
	//			return database.Delete<BikeContainer>(id);
	//		}
	//	}
	//}
}

