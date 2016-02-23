using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CrazyAppsStudio.Delegacje.Tasks;
using CrazyAppsStudio.Delegacje.Domain.Entities;
using CrazyAppsStudio.Delegacje.Repository;

namespace TasksTests
{
	[TestClass]
	public class UnitTest1
	{
        //[TestMethod]
        //public void CurrencyRateRefreshTest()
        //{
        //	CurrenciesTasks tasks = new CurrenciesTasks();
        //	tasks.GetLatestAndRefreshCurrencyRates();
        //	//tasks.RefreshCurrencies(); //modifies database

        //}

        //[TestMethod]
        //public void NBPCurrencyLoadTest()
        //{
        //	CurrenciesTasks tasks = new CurrenciesTasks();
        //	//tasks.LoadCurrenciesFromNBP(new DateTime(2016, 2, 14), true, true);
        //	tasks.LoadCurrenciesFromNBP(new DateTime(2016, 1, 2), true, true);
        //}

        //[TestMethod]
        //public void LoadCurrencyForDayTest()
        //{
        //	//modifies database
        //	CurrenciesTasks tasks = new CurrenciesTasks();
        //	tasks.GetCurrencyRateForDay("AFN", new DateTime(2015, 12, 16));
        //}

        //[TestMethod]
        //public void LoadCurrenciesForDayTest()
        //{
        //	//modifies database
        //	CurrenciesTasks tasks = new CurrenciesTasks();
        //	tasks.GetAllCurrencyRatesForDay(new DateTime(2015, 3, 25));
        //}

        //[TestMethod]
        //public void LoadFor2015()
        //{
        //    //modifies database
        //    CurrenciesTasks tasks = new CurrenciesTasks();
        //    CurrencyRate[] rates;
        //    DateTime currentDate = new DateTime(2015, 12, 31);

        //    while (currentDate.Year == 2015)
        //    {
        //        rates = tasks.GetAllCurrencyRatesForDay(currentDate);
        //        currentDate = currentDate.AddDays(-1);
        //    }
        //    return;
        //}

        //[TestMethod]
        //public void LoadFor2016()
        //{
        //	//modifies database
        //	CurrenciesTasks tasks = new CurrenciesTasks();
        //	CurrencyRate[] rates;
        //	DateTime currentDate = DateTime.Now;

        //	while (currentDate.Year == 2016)
        //	{
        //		rates = tasks.GetAllCurrencyRatesForDay(currentDate);
        //		currentDate = currentDate.AddDays(-1);
        //	}
        //	return;
        //}

        [TestMethod]
        public void SubsTest()
        {
            Repositories repo = new Repositories();

            Subsistence sub = null;
            sub = repo.Subsistences.Create(new Subsistence()
            {
                StartDate = new DateTime(2016, 2, 5),
                EndDate = new DateTime(2016, 2, 15),
                City = "TEST CITY",
                Country = repo.Dictionaries.GetCountry(35)
            });
            repo.SaveChanges();
        }

        
    }
}
