using CrazyAppsStudio.Delegacje.Domain.DTO;
using CrazyAppsStudio.Delegacje.Domain.Entities;
using CrazyAppsStudio.Delegacje.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
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

		public CurrenciesTasks(Repositories repo)
		{
			this.repo = repo;
		}


		public CurrencyRate[] GetLatestAndRefreshCurrencyRates()
		{
			CurrencyRate eur = this.repo.Currencies.GetLastCurrencyRate("EUR");
			if (eur == null || (eur.DateRefreshed.Date < DateTime.Now.AddDays(-1).Date) ||
				(eur.DateRefreshed.Date == DateTime.Now.AddDays(-1).Date && DateTime.Now.Hour > 13))
			{
				RefreshCurrencies(DateTime.Now.Date);
			}

			eur = this.repo.Currencies.GetLastCurrencyRate("EUR");
			return this.repo.Currencies.GetAllRatesForDay(eur.DateRefreshed).OrderBy(c => c.Currency.Code).ToArray();
		}

		public CurrencyRate GetCurrencyRateForDay(string code, DateTime date)
		{
			CurrencyRate rate = this.repo.Currencies.GetCurrencyRate(code, date);
			if (rate == null)
			{
				if (this.repo.Currencies.CurrenciesQueryable.Any(c => c.Code == code))
				{
					RefreshCurrencies(date);
					rate = this.repo.Currencies.GetCurrencyRate(code, date);
				}
			}
			return rate;				
		}

		public CurrencyRate[] GetAllCurrencyRatesForDay(DateTime date)
		{
			CurrencyRate[] rates = this.repo.Currencies.GetAllRatesForDay(date).ToArray();
			if (rates == null || rates.Length == 0)
			{
				RefreshCurrencies(date);
				rates = this.repo.Currencies.GetAllRatesForDay(date).ToArray();
			}
			return rates;
		}

		private void RefreshCurrencies(DateTime refreshDate)
		{
			refreshDate = refreshDate.Date;
			List<NBPCurrencyRate> nbpCurrencies = LoadCurrenciesFromNBP(refreshDate, true, true);
			CurrencyRate[] existingRates = this.repo.Currencies.GetAllRatesForDay(refreshDate).ToArray();
			IEnumerable<Currency> systemCurrencies = this.repo.Dictionaries.GetCurrencies();
			//List<Currency> test = systemCurrencies.ToList();
			List<CurrencyRate> ratesToBeAdded = new List<CurrencyRate>();
			foreach (Currency curr in systemCurrencies)
			{
				NBPCurrencyRate nbp = nbpCurrencies.FirstOrDefault(c => string.Equals(c.CurrencyCode, curr.Code, StringComparison.InvariantCultureIgnoreCase));
				if (nbp != null)
				{
					if (!existingRates.Any(cr => cr.Currency.Code == nbp.CurrencyCode))
					{
						CurrencyRate rate = new CurrencyRate()
						{
							Currency = curr,
							DateRefreshed = refreshDate,
							ExchangeRate = nbp.CurrencyRate / nbp.Converter
						};
						ratesToBeAdded.Add(rate);
					}

					//curr.ExchangeRate = nbp.CurrencyRate;
					//curr.DateRefreshed = nbp.CurrencyDate;
				}
			}

			//List<NBPCurrencyRate> missing = nbpCurrencies.Where(nbpc => !systemCurrencies.Any(sc =>
			//	string.Equals(nbpc.CurrencyCode, sc.Code, StringComparison.InvariantCultureIgnoreCase))).ToList();
			//if (missing.Count > 0)
			//{
			//	List<Currency> toBeAdded = missing.Select(nbpc => new Currency()
			//	{
			//		Name = nbpc.CurrencyName,
			//		Code = nbpc.CurrencyCode,
			//		ExchangeRate = nbpc.CurrencyRate,
			//		DateRefreshed = nbpc.CurrencyDate
			//	}).ToList();
			//	this.repo.Dictionaries.AddCurrencies(toBeAdded);
			//}
			this.repo.Currencies.AddCurrencyRates(ratesToBeAdded);
			this.repo.SaveChanges();
		}

		/// <summary>
		/// LoadA - załaduj waluty kategorii A, wymienialne, bardziej popularne. LoadB - załaduj waluty niewyminialnie, mało popularne
		/// </summary>
		/// <param name="date"></param>
		/// <param name="loadA"></param>
		/// <param name="loadB"></param>
		/// <returns></returns>
		public List<NBPCurrencyRate> LoadCurrenciesFromNBP(DateTime date, bool loadA, bool loadB)
		{
			string fileNameA = null, fileNameB = null;
			if (loadA)
				fileNameA = GetNBPFilenameForDate(date, false);
			if (loadB)
				fileNameB = GetNBPFilenameForDate(date, true);
			List<NBPCurrencyRate> currencies = new List<NBPCurrencyRate>();

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

		private string GetNBPFilenameForDate(DateTime date, bool isExtended)
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

		private DateTime GetDateFromNBPString(string dateString)
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
		private List<NBPCurrencyRate> LoadLatestCurrenciesFromNBP(bool loadA, bool loadB)
		{
			string urlA = "http://www.nbp.pl/kursy/xml/LastA.xml";
			string urlB = "http://www.nbp.pl/kursy/xml/LastB.xml";

			List<NBPCurrencyRate> currencies = new List<NBPCurrencyRate>();

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

		private List<NBPCurrencyRate> LoadNBPCurrenciesFromUrl(string url)
		{
			string dataRead = LoadTextForUrl(url);
			return ParseCurrencies(dataRead);
		}		

		private List<NBPCurrencyRate> ParseCurrencies(string rawCurrencies)
		{
			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.LoadXml(rawCurrencies);
			XmlNodeList currencyListXml = xmlDoc.GetElementsByTagName(Constraints.POSITION);
			var date = xmlDoc.GetElementsByTagName(Constraints.PUBLICATION_DATE);
			DateTime currencyDate = Convert.ToDateTime(date[0].FirstChild.Value);

			List<NBPCurrencyRate> currencyList = new List<NBPCurrencyRate>();
			foreach (XmlNode currencyXml in currencyListXml)
			{
				//XmlNodeList nodes = currencyXml.ChildNodes;
				NBPCurrencyRate currency = new NBPCurrencyRate();
				currency.CurrencyName = currencyXml.ChildNodes[0].FirstChild.Value;
				currency.Converter = Convert.ToDouble(currencyXml.ChildNodes[1].FirstChild.Value);
				currency.CurrencyCode = currencyXml.ChildNodes[2].FirstChild.Value;
				currency.CurrencyRate = Convert.ToDouble(currencyXml.ChildNodes[3].FirstChild.Value, CultureInfo.GetCultureInfo("pl-PL"));
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
