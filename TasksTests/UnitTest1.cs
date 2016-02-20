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
			//tasks.GetLatestAndRefreshCurrencyRates();
			//tasks.RefreshCurrencies(); //modifies database

		}

		[TestMethod]
		public void NBPCurrencyLoadTest()
		{
			CurrenciesTasks tasks = new CurrenciesTasks();
			//tasks.LoadCurrenciesFromNBP(new DateTime(2016, 2, 14), true, true);
			tasks.LoadCurrenciesFromNBP(new DateTime(2016, 1, 2), true, true);
		}
	}
}
