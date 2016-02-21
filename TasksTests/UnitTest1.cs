using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CrazyAppsStudio.Delegacje.Tasks;

namespace TasksTests
{
	[TestClass]
	public class UnitTest1
	{		
		[TestMethod]
		public void CurrencyRateRefreshTest()
		{
			CurrenciesTasks tasks = new CurrenciesTasks();
			tasks.GetLatestAndRefreshCurrencyRates();
			//tasks.RefreshCurrencies(); //modifies database

		}

		[TestMethod]
		public void NBPCurrencyLoadTest()
		{
			CurrenciesTasks tasks = new CurrenciesTasks();
			//tasks.LoadCurrenciesFromNBP(new DateTime(2016, 2, 14), true, true);
			tasks.LoadCurrenciesFromNBP(new DateTime(2016, 1, 2), true, true);
		}

		[TestMethod]
		public void LoadCurrencyForDayTest()
		{
			CurrenciesTasks tasks = new CurrenciesTasks();
			tasks.GetCurrencyRateForDay("AFN", new DateTime(2015, 12, 16));
		}

		[TestMethod]
		public void LoadCurrenciesForDayTest()
		{
			CurrenciesTasks tasks = new CurrenciesTasks();
			tasks.GetAllCurrencyRatesForDay(new DateTime(2015, 3, 25));
		}
	}
}
