using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CrazyAppsStudio.Delegacje.Tasks;

namespace TasksTests
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void NBPCurrencyRetrievalTest()
		{
			DictionariesTasks tasks = new DictionariesTasks();
			tasks.LoadCurrenciesFromNBP();

		}

		[TestMethod]
		public void NBPCurrencyRefreshTest()
		{
			DictionariesTasks tasks = new DictionariesTasks();
			//tasks.RefreshCurrencies(); //modifies database

		}

		[TestMethod]
		public void NBPCurrencyLoadTest()
		{
			CurrenciesTasks tasks = new CurrenciesTasks();
			tasks.LoadCurrenciesFromNBP(new DateTime(2016, 2, 14), true, true);

		}
	}
}
