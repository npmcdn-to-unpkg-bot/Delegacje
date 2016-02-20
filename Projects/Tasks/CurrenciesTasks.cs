using CrazyAppsStudio.Delegacje.Domain.DTO;
using CrazyAppsStudio.Delegacje.Domain.Entities;
using CrazyAppsStudio.Delegacje.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace CrazyAppsStudio.Delegacje.Tasks
{
	public class CurrenciesTasks
	{
		private Repositories repo;

		public CurrenciesTasks()
        {
            repo = new Repositories();
        }

		//public Currency GetCurrencyForDate(string currencyCode, DateTime date)
		//{
		//	Currency curr = this.repo.Dictionaries
		//}

		//public Currency[] GetAndRefreshCurrencies()
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
		//	foreach (Currency curr in systemCurrencies)
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

		/// <summary>
		/// LoadA - załaduj waluty kategorii A, wymienialne, bardziej popularne. LoadB - załaduj waluty niewyminialnie, mało popularne
		/// </summary>
		/// <param name="date"></param>
		/// <param name="loadA"></param>
		/// <param name="loadB"></param>
		/// <returns></returns>
		public List<NBPCurrency> LoadCurrenciesFromNBP(DateTime date, bool loadA, bool loadB)
		{
			string fileNameA = null, fileNameB = null;
			if (loadA)
				fileNameA = GetNBPFilenameForDate(date, false);
			if (loadB)
				fileNameB = GetNBPFilenameForDate(date, true);
			List<NBPCurrency> currencies = new List<NBPCurrency>();

			if (fileNameA == null)
			{
				DateTime lastDayPreviousYear = new DateTime(date.Year - 1, 12, 31);
				fileNameA = GetNBPFilenameForDate(lastDayPreviousYear, false);
			}

			if (fileNameB == null)
			{
				DateTime lastDayPreviousYear = new DateTime(date.Year - 1, 12, 31);
				fileNameB = GetNBPFilenameForDate(lastDayPreviousYear, true);
			}
			
			if (loadA)
				currencies.AddRange(LoadNBPCurrenciesFromUrl(string.Format("http://www.nbp.pl/kursy/xml/{0}.xml", fileNameA)));

			if (loadB)
				currencies.AddRange(LoadNBPCurrenciesFromUrl(string.Format("http://www.nbp.pl/kursy/xml/{0}.xml", fileNameB)));

			return currencies;
		}

		public string GetNBPFilenameForDate(DateTime date, bool isExtended)
		{
			DateTime currentDate = DateTime.Now;
			if (date.Date > currentDate.Date)
				throw new ArgumentException("Data nie może być przyszła");
			if (date.Year < 2002)
				throw new ArgumentException("Daty starze niż 2002 nie wspierane");
			string dirUrl = null;
			if (date.Year == DateTime.Now.Year)
			{
				dirUrl = "http://www.nbp.pl/kursy/xml/dir.txt";
			}
			else
			{
				dirUrl = string.Format("http://www.nbp.pl/kursy/xml/dir{0}.txt", date.Year);
			}
			string dirContents = LoadTextForUrl(dirUrl);
			string[] entries = dirContents.Split('\n');
			string firstDateString = entries[0].Trim().Substring(5);
			DateTime firstDate = GetDateFromNBPString(firstDateString);

			string fileName = null;

			string patternStart = null;
			if (!isExtended)
				patternStart = @"a\d{3}z";
			else
				patternStart = @"b\d{3}z";
			//string patternA = @"a\d{3}z";
			//string patternB = @"b\d{3}z";
			//string pattern = patternStart + date.ToString("yymmdd");

			//Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
			//Match match = regex.Match(dirContents);

			string currentDateString = date.ToString("yyMMdd");

			Regex regex;
			Match match;

			DateTime currentlyMatchedDate;

			do
			{
				string pattern = patternStart + currentDateString;
				regex = new Regex(pattern, RegexOptions.IgnoreCase);
				match = regex.Match(dirContents);

				if (match.Groups[0].Captures.Count > 0)
				{
					fileName = match.Groups[0].Captures[0].Value;
					break;
				}

				currentlyMatchedDate = GetDateFromNBPString(currentDateString);
				currentlyMatchedDate = currentlyMatchedDate.AddDays(-1);
				currentDateString = currentlyMatchedDate.ToString("yyMMdd");
			}
			while (currentlyMatchedDate >= firstDate);

			//List<string> fileNames = new List<string>();
			//if (fileNameA != null)
			//{
			//	if (loadA)
			//		fileNames.Add(fileNameA);
			//	if (loadB)
			//	{
			//		string pattern = patternB + currentDateString;
			//		regex = new Regex(pattern, RegexOptions.IgnoreCase);
			//		match = regex.Match(dirContents);
			//		string fileNameB = match.Groups[0].Captures[0].Value;
			//		fileNames.Add(fileNameB);
			//	}
			//}

			return fileName;
		}

		public DateTime GetDateFromNBPString(string dateString)
		{
			return new DateTime(
					2000 + int.Parse(dateString.Substring(0, 2)),
					int.Parse(dateString.Substring(2, 2)),
					int.Parse(dateString.Substring(4, 2))
				);
		}

		/// <summary>
		/// LoadA - załaduj waluty kategorii A, wymienialne, bardziej popularne. LoadB - załaduj waluty niewyminialnie, mało popularne
		/// </summary>
		/// <param name="loadA"></param>
		/// <param name="loadB"></param>
		/// <returns></returns>
		public List<NBPCurrency> LoadLatestCurrenciesFromNBP(bool loadA, bool loadB)
		{
			string urlA = "http://www.nbp.pl/kursy/xml/LastA.xml";
			string urlB = "http://www.nbp.pl/kursy/xml/LastB.xml";

			List<NBPCurrency> currencies = new List<NBPCurrency>();

			if (loadA)
			{
				currencies.AddRange(LoadNBPCurrenciesFromUrl(urlA));
			}

			if (loadB)
			{
				currencies.AddRange(LoadNBPCurrenciesFromUrl(urlB));
			}

			return currencies;
		}

		public List<NBPCurrency> LoadNBPCurrenciesFromUrl(string url)
		{
			string dataRead = LoadTextForUrl(url);
			return ParseCurrencies(dataRead);
		}		

		private List<NBPCurrency> ParseCurrencies(string rawCurrencies)
		{
			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.LoadXml(rawCurrencies);
			XmlNodeList currencyListXml = xmlDoc.GetElementsByTagName(Constraints.POSITION);
			var date = xmlDoc.GetElementsByTagName(Constraints.PUBLICATION_DATE);
			DateTime currencyDate = Convert.ToDateTime(date[0].FirstChild.Value);

			List<NBPCurrency> currencyList = new List<NBPCurrency>();
			foreach (XmlNode currencyXml in currencyListXml)
			{
				//XmlNodeList nodes = currencyXml.ChildNodes;
				NBPCurrency currency = new NBPCurrency();
				currency.CurrencyName = currencyXml.ChildNodes[0].FirstChild.Value;
				currency.Converter = currencyXml.ChildNodes[1].FirstChild.Value;
				currency.CurrencyCode = currencyXml.ChildNodes[2].FirstChild.Value;
				currency.CurrencyRate = Convert.ToDouble(currencyXml.ChildNodes[3].FirstChild.Value);
				currency.CurrencyDate = currencyDate;
				currencyList.Add(currency);
			}
			return currencyList;
		}

		private string LoadTextForUrl(string url)
		{
			// Creates an HttpWebRequest with the specified URL. 
			HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(url);
			// Sends the HttpWebRequest and waits for the response.            
			HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
			// Gets the stream associated with the response.
			Stream receiveStream = myHttpWebResponse.GetResponseStream();
			Encoding encode = System.Text.Encoding.GetEncoding("iso-8859-2");
			// Pipes the stream to a higher level stream reader with the required encoding format. 
			StreamReader readStream = new StreamReader(receiveStream, encode);
			string dataRead = readStream.ReadToEnd();
			myHttpWebResponse.Close();
			// Releases the resources of the Stream.
			readStream.Close();

			return dataRead;
		}

		class Constraints
		{
			public static readonly string PUBLICATION_DATE = "data_publikacji";
			public static readonly string POSITION = "pozycja";
			public static readonly string CURRENCY_NAME = "nazwa_waluty";
			public static readonly string CONVERETER = "przelicznik";
			public static readonly string CURRENCY_CODE = "kod_waluty";
			public static readonly string CURRENCY_RATE = "kurs_sredni";
		}
	}
}
