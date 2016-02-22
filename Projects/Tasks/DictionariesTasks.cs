using System.Linq;
using CrazyAppsStudio.Delegacje.Domain.DTO;
using CrazyAppsStudio.Delegacje.Domain.Entities;
using CrazyAppsStudio.Delegacje.Repository;
using CrazyAppsStudio.Delegacje.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;

namespace CrazyAppsStudio.Delegacje.Tasks
{
    public class DictionariesTasks
    {
        private Repositories repo;

		private CurrenciesTasks currenciesTasks;

        public DictionariesTasks()
        {
            repo = new Repositories();
			currenciesTasks = new CurrenciesTasks(repo);

        }

        public DictionariesDTO GetDictionaries()
        {
            DictionariesDTO result = new DictionariesDTO()
            {
                Countries = repo.Dictionaries.GetCountries(),
                ExpenseDocumentTypes = repo.Dictionaries.GetExpenseDocumentTypes(),
                ExpenseTypes = repo.Dictionaries.GetExpenseTypes(),
                VehicleTypes = repo.Dictionaries.GetVehicleTypes(),
				Currencies = currenciesTasks.GetLatestAndRefreshCurrencyRates().Select(cr => cr.MapToDTO())
            };

            return result;
        }

		//public Currency[] GetLatestAndRefreshCurrencies()
		//{
		//	Currency eur = this.repo.Dictionaries.GetCurrencies().First(c => c.Code == "EUR");
		//	if ((eur.DateRefreshed.Date < DateTime.Now.AddDays(-1).Date) ||
		//		eur.DateRefreshed.Date == DateTime.Now.AddDays(-1).Date && DateTime.Now.Hour > 12)
		//	{
		//		RefreshCurrencies();
		//	}

		//	return this.repo.Dictionaries.GetCurrencies().ToArray();
		//}

		//public void RefreshCurrencies()
		//{
		//	List<NBPCurrency> nbpCurrencies = LoadCurrenciesFromNBP();
		//	IEnumerable<Currency> systemCurrencies = this.repo.Dictionaries.GetCurrencies();
		//	//List<Currency> test = systemCurrencies.ToList();
		//	foreach(Currency curr in systemCurrencies)
		//	{
		//		NBPCurrency nbp = nbpCurrencies.FirstOrDefault(c => string.Equals(c.CurrencyCode, curr.Code, StringComparison.InvariantCultureIgnoreCase));
		//		if (nbp != null)
		//		{
		//			curr.ExchangeRate = nbp.CurrencyRate;
		//			curr.DateRefreshed = nbp.CurrencyDate;
		//		}
		//	}

		//	List<NBPCurrency> missing = nbpCurrencies.Where(nbpc => !systemCurrencies.Any(sc =>
		//		string.Equals(nbpc.CurrencyCode, sc.Code, StringComparison.InvariantCultureIgnoreCase))).ToList();
		//	if (missing.Count > 0)
		//	{
		//		List<Currency> toBeAdded = missing.Select(nbpc => new Currency()
		//		{
		//			Name = nbpc.CurrencyName,
		//			Code = nbpc.CurrencyCode,
		//			ExchangeRate = nbpc.CurrencyRate,
		//			DateRefreshed = nbpc.CurrencyDate
		//		}).ToList();
		//		this.repo.Dictionaries.AddCurrencies(toBeAdded);				
		//	}
		//	this.repo.SaveChanges();
		//}		

		//public List<NBPCurrency> LoadCurrenciesFromNBP()
		//{
		//	string url = "http://www.nbp.pl/kursy/xml/LastA.xml";

		//	// Creates an HttpWebRequest with the specified URL. 
		//	HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(url);
		//	// Sends the HttpWebRequest and waits for the response.            
		//	HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
		//	// Gets the stream associated with the response.
		//	Stream receiveStream = myHttpWebResponse.GetResponseStream();
		//	Encoding encode = System.Text.Encoding.GetEncoding("iso-8859-2");
		//	// Pipes the stream to a higher level stream reader with the required encoding format. 
		//	StreamReader readStream = new StreamReader(receiveStream, encode);
		//	string readedData = readStream.ReadToEnd();
		//	myHttpWebResponse.Close();
		//	// Releases the resources of the Stream.
		//	readStream.Close();
		//	XmlDocument xmlDoc = new XmlDocument();
		//	xmlDoc.LoadXml(readedData);
		//	XmlNodeList currencyListXml = xmlDoc.GetElementsByTagName(Constraints.POSITION);
		//	var date = xmlDoc.GetElementsByTagName(Constraints.PUBLICATION_DATE);
		//	DateTime currencyDate = Convert.ToDateTime(date[0].FirstChild.Value);

		//	List<NBPCurrency> currencyList = new List<NBPCurrency>();
		//	foreach (XmlNode currencyXml in currencyListXml)
		//	{
		//		//XmlNodeList nodes = currencyXml.ChildNodes;
		//		NBPCurrency currency = new NBPCurrency();
		//		currency.CurrencyName = currencyXml.ChildNodes[0].FirstChild.Value;
		//		currency.Converter = currencyXml.ChildNodes[1].FirstChild.Value;
		//		currency.CurrencyCode = currencyXml.ChildNodes[2].FirstChild.Value;
		//		currency.CurrencyRate = Convert.ToDouble(currencyXml.ChildNodes[3].FirstChild.Value);
		//		currency.CurrencyDate = currencyDate;
		//		currencyList.Add(currency);
		//	}
		//	return currencyList;        
		//}

		//class Constraints
		//{
		//	public static readonly string PUBLICATION_DATE = "data_publikacji";
		//	public static readonly string POSITION = "pozycja";
		//	public static readonly string CURRENCY_NAME = "nazwa_waluty";
		//	public static readonly string CONVERETER = "przelicznik";
		//	public static readonly string CURRENCY_CODE = "kod_waluty";
		//	public static readonly string CURRENCY_RATE = "kurs_sredni";
		//}

		
    }
}
