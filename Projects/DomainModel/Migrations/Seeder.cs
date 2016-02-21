using CrazyAppsStudio.Delegacje.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;
using CrazyAppsStudio.Delegacje.Domain.Entities;

namespace CrazyAppsStudio.Delegacje.DomainModel.Migrations
{
	public class Seeder
	{
		public void SeedContext(BusinessTripsContext context)
		{
			CreateUsers(context);
			CreateCountries(context);
			CreateVehicleTypes(context);
            CreateExpenseTypes(context);
            CreateExpenseDocumentTypes(context);
            CreateMealTypes(context);
        }

		public void CreateUsers(BusinessTripsContext context)
		{
			//Roles
			Role basicUser = new Role();
			basicUser.Name = "BasicUser";

			User maciej = new User()
			{
				UserName = "Maciej",
				Email = "suruwa@gmail.com",
				EmailConfirmed = true,
				PasswordHash = "AM+qVffwovJO6ncY4wFxZ12RBOlGQ23Z9yyNF95SI6Ij+cW7F0QzPI090Y6FYbimtQ==", //P@ssword1
				SecurityStamp = "ef0ce398-45dd-41a7-ad99-0f39a28c0f32",
				PhoneNumber = null,
				PhoneNumberConfirmed = false,
				TwoFactorEnabled = false,
				LockoutEndDateUtc = null,
				LockoutEnabled = false,
				AccessFailedCount = 0
			};
			UserRole maciejBasic = new UserRole() { Role = basicUser, User = maciej };			

			User karol = new User()
			{
				UserName = "Karol",
				Email = "karol.barkowski@gmail.com",
				EmailConfirmed = true,
				PasswordHash = "AM+qVffwovJO6ncY4wFxZ12RBOlGQ23Z9yyNF95SI6Ij+cW7F0QzPI090Y6FYbimtQ==", //P@ssword1
				SecurityStamp = "ef0ce398-45dd-41a7-ad99-0f39a28c0f32",
				PhoneNumber = null,
				PhoneNumberConfirmed = false,
				TwoFactorEnabled = false,
				LockoutEndDateUtc = null,
				LockoutEnabled = false,
				AccessFailedCount = 0
			};
			UserRole karolBasic = new UserRole() { Role = basicUser, User = karol };

			maciej.Roles.Add(maciejBasic);
			karol.Roles.Add(karolBasic);

			context.Roles.AddOrUpdate(r => new { r.Name }, basicUser);
			context.Users.AddOrUpdate(u => new { u.UserName }, maciej);
			context.Users.AddOrUpdate(u => new { u.UserName }, karol);		
		}

		public void CreateCountries(BusinessTripsContext context)
		{
            context.Countries.RemoveRange(context.Countries.ToList());
            context.SaveChanges();

			Currency afa = new Currency() { Code = "AFA", Name = "afgani" };
            Country Afganistan = new Country() { Name = "Afganistan", SubsistenceAllowance = 47, AccomodationLimit = 140, Currency = afa, LimitCurrency = afa };
            context.Countries.AddOrUpdate(vt => vt.Name, Afganistan);
			//Country Albania = new Country() { Name = "Albania", CurrencyCode = "EUR", SubsistenceAllowance = 41, AccomodationLimit = 120 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Albania);
			//Country Algieria = new Country() { Name = "Algieria", CurrencyCode = "EUR", SubsistenceAllowance = 50, AccomodationLimit = 200 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Algieria);
			//Country Andora = new Country() { Name = "Andora", CurrencyCode = "EUR", SubsistenceAllowance = 50, AccomodationLimit = 160 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Andora);
			//Country Angola = new Country() { Name = "Angola", CurrencyCode = "USD", SubsistenceAllowance = 61, AccomodationLimit = 180 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Angola);
			//Country ArabiaSaudyjska = new Country() { Name = "Arabia Saudyjska", CurrencyCode = "EUR", SubsistenceAllowance = 45, AccomodationLimit = 180 };
			//context.Countries.AddOrUpdate(vt => vt.Name, ArabiaSaudyjska);
			//Country Argentyna = new Country() { Name = "Argentyna", CurrencyCode = "USD", SubsistenceAllowance = 50, AccomodationLimit = 150 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Argentyna);
			//Country Armenia = new Country() { Name = "Armenia", CurrencyCode = "EUR", SubsistenceAllowance = 42, AccomodationLimit = 145 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Armenia);
			//Country Australia = new Country() { Name = "Australia", CurrencyCode = "AUD", SubsistenceAllowance = 88, AccomodationLimit = 250 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Australia);
			//Country Austria = new Country() { Name = "Austria", CurrencyCode = "EUR", SubsistenceAllowance = 52, AccomodationLimit = 130 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Austria);
			//Country Azerbejdzan = new Country() { Name = "Azerbejdżan", CurrencyCode = "EUR", SubsistenceAllowance = 43, AccomodationLimit = 150 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Azerbejdzan);
			//Country Bangladesz = new Country() { Name = "Bangladesz", CurrencyCode = "USD", SubsistenceAllowance = 50, AccomodationLimit = 120 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Bangladesz);
			//Country Belgia = new Country() { Name = "Belgia", CurrencyCode = "EUR", SubsistenceAllowance = 48, AccomodationLimit = 160 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Belgia);
			//Country Białoruś = new Country() { Name = "Białoruś", CurrencyCode = "EUR", SubsistenceAllowance = 42, AccomodationLimit = 130 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Białoruś);
			//Country BośniaiHercegowina = new Country() { Name = "Bośnia i Hercegowina", CurrencyCode = "EUR", SubsistenceAllowance = 41, AccomodationLimit = 100 };
			//context.Countries.AddOrUpdate(vt => vt.Name, BośniaiHercegowina);
			//Country Brazylia = new Country() { Name = "Brazylia", CurrencyCode = "EUR", SubsistenceAllowance = 43, AccomodationLimit = 120 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Brazylia);
			//Country Bułgaria = new Country() { Name = "Bułgaria", CurrencyCode = "EUR", SubsistenceAllowance = 40, AccomodationLimit = 120 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Bułgaria);
			//Country Chile = new Country() { Name = "Chile", CurrencyCode = "USD", SubsistenceAllowance = 60, AccomodationLimit = 120 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Chile);
			//Country Chiny = new Country() { Name = "Chiny", CurrencyCode = "EUR", SubsistenceAllowance = 55, AccomodationLimit = 170 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Chiny);
			//Country Chorwacja = new Country() { Name = "Chorwacja", CurrencyCode = "EUR", SubsistenceAllowance = 42, AccomodationLimit = 125 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Chorwacja);
			//Country Cypr = new Country() { Name = "Cypr", CurrencyCode = "EUR", SubsistenceAllowance = 43, AccomodationLimit = 160 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Cypr);
			//Country Czechy = new Country() { Name = "Czechy", CurrencyCode = "EUR", SubsistenceAllowance = 41, AccomodationLimit = 120 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Czechy);
			//Country Dania = new Country() { Name = "Dania", CurrencyCode = "DKK", SubsistenceAllowance = 406, AccomodationLimit = 1300 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Dania);
			//Country Egipt = new Country() { Name = "Egipt", CurrencyCode = "USD", SubsistenceAllowance = 55, AccomodationLimit = 150 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Egipt);
			//Country Ekwador = new Country() { Name = "Ekwador", CurrencyCode = "USD", SubsistenceAllowance = 44, AccomodationLimit = 110 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Ekwador);
			//Country Estonia = new Country() { Name = "Estonia", CurrencyCode = "EUR", SubsistenceAllowance = 41, AccomodationLimit = 100 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Estonia);
			//Country Etiopia = new Country() { Name = "Etiopia", CurrencyCode = "USD", SubsistenceAllowance = 55, AccomodationLimit = 300 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Etiopia);
			//Country Finlandia = new Country() { Name = "Finlandia", CurrencyCode = "EUR", SubsistenceAllowance = 48, AccomodationLimit = 160 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Finlandia);
			//Country Francja = new Country() { Name = "Francja", CurrencyCode = "EUR", SubsistenceAllowance = 50, AccomodationLimit = 180 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Francja);
			//Country Grecja = new Country() { Name = "Grecja", CurrencyCode = "EUR", SubsistenceAllowance = 48, AccomodationLimit = 140 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Grecja);
			//Country Gruzja = new Country() { Name = "Gruzja", CurrencyCode = "EUR", SubsistenceAllowance = 43, AccomodationLimit = 140 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Gruzja);
			//Country Hiszpania = new Country() { Name = "Hiszpania", CurrencyCode = "EUR", SubsistenceAllowance = 50, AccomodationLimit = 160 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Hiszpania);
			//Country Indie = new Country() { Name = "Indie", CurrencyCode = "EUR", SubsistenceAllowance = 38, AccomodationLimit = 190 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Indie);
			//Country Indonezja = new Country() { Name = "Indonezja", CurrencyCode = "EUR", SubsistenceAllowance = 41, AccomodationLimit = 110 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Indonezja);
			//Country Irak = new Country() { Name = "Irak", CurrencyCode = "USD", SubsistenceAllowance = 60, AccomodationLimit = 120 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Irak);
			//Country Iran = new Country() { Name = "Iran", CurrencyCode = "EUR", SubsistenceAllowance = 41, AccomodationLimit = 95 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Iran);
			//Country Irlandia = new Country() { Name = "Irlandia", CurrencyCode = "EUR", SubsistenceAllowance = 52, AccomodationLimit = 160 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Irlandia);
			//Country Islandia = new Country() { Name = "Islandia", CurrencyCode = "EUR", SubsistenceAllowance = 56, AccomodationLimit = 160 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Islandia);
			//Country Izrael = new Country() { Name = "Izrael", CurrencyCode = "EUR", SubsistenceAllowance = 50, AccomodationLimit = 150 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Izrael);
			//Country Japonia = new Country() { Name = "Japonia", CurrencyCode = "JPY", SubsistenceAllowance = 7532, AccomodationLimit = 22000 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Japonia);
			//Country Jemen = new Country() { Name = "Jemen", CurrencyCode = "USD", SubsistenceAllowance = 48, AccomodationLimit = 160 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Jemen);
			//Country Jordania = new Country() { Name = "Jordania", CurrencyCode = "EUR", SubsistenceAllowance = 40, AccomodationLimit = 95 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Jordania);
			//Country Kambodza = new Country() { Name = "Kambodża", CurrencyCode = "USD", SubsistenceAllowance = 45, AccomodationLimit = 100 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Kambodza);
			//Country Kanada = new Country() { Name = "Kanada", CurrencyCode = "CAD", SubsistenceAllowance = 71, AccomodationLimit = 190 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Kanada);
			//Country Katar = new Country() { Name = "Katar", CurrencyCode = "EUR", SubsistenceAllowance = 41, AccomodationLimit = 200 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Katar);
			//Country Kazachstan = new Country() { Name = "Kazachstan", CurrencyCode = "EUR", SubsistenceAllowance = 41, AccomodationLimit = 140 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Kazachstan);
			//Country Kenia = new Country() { Name = "Kenia", CurrencyCode = "EUR", SubsistenceAllowance = 41, AccomodationLimit = 150 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Kenia);
			//Country Kirgistan = new Country() { Name = "Kirgistan", CurrencyCode = "USD", SubsistenceAllowance = 41, AccomodationLimit = 150 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Kirgistan);
			//Country Kolumbia = new Country() { Name = "Kolumbia", CurrencyCode = "USD", SubsistenceAllowance = 49, AccomodationLimit = 120 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Kolumbia);
			//Country Kongo = new Country() { Name = "Kongo", CurrencyCode = "USD", SubsistenceAllowance = 66, AccomodationLimit = 220 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Kongo);
			//Country KoreaPoludniowa = new Country() { Name = "Korea Południowa", CurrencyCode = "EUR", SubsistenceAllowance = 46, AccomodationLimit = 170 };
			//context.Countries.AddOrUpdate(vt => vt.Name, KoreaPoludniowa);
			//Country KoreańskaRepublikaLudowoDemokratyczna = new Country() { Name = "Koreańska Republika Ludowo-Demokratyczna", CurrencyCode = "EUR", SubsistenceAllowance = 48, AccomodationLimit = 170 };
			//context.Countries.AddOrUpdate(vt => vt.Name, KoreańskaRepublikaLudowoDemokratyczna);
			//Country Kostaryka = new Country() { Name = "Kostaryka", CurrencyCode = "USD", SubsistenceAllowance = 50, AccomodationLimit = 140 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Kostaryka);
			//Country Kuba = new Country() { Name = "Kuba", CurrencyCode = "EUR", SubsistenceAllowance = 42, AccomodationLimit = 110 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Kuba);
			//Country Kuwejt = new Country() { Name = "Kuwejt", CurrencyCode = "EUR", SubsistenceAllowance = 39, AccomodationLimit = 200 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Kuwejt);
			//Country Laos = new Country() { Name = "Laos", CurrencyCode = "USD", SubsistenceAllowance = 54, AccomodationLimit = 100 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Laos);
			//Country Liban = new Country() { Name = "Liban", CurrencyCode = "USD", SubsistenceAllowance = 57, AccomodationLimit = 150 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Liban);
			//Country Libia = new Country() { Name = "Libia", CurrencyCode = "EUR", SubsistenceAllowance = 52, AccomodationLimit = 100 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Libia);
			//Country Liechtenstein = new Country() { Name = "Liechtenstein", CurrencyCode = "CHF", SubsistenceAllowance = 88, AccomodationLimit = 200 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Liechtenstein);
			//Country Litwa = new Country() { Name = "Litwa", CurrencyCode = "EUR", SubsistenceAllowance = 39, AccomodationLimit = 130 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Litwa);
			//Country Luksemburg = new Country() { Name = "Luksemburg", CurrencyCode = "EUR", SubsistenceAllowance = 48, AccomodationLimit = 160 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Luksemburg);
			//Country Łotwa = new Country() { Name = "Łotwa", CurrencyCode = "EUR", SubsistenceAllowance = 57, AccomodationLimit = 132 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Łotwa);
			//Country Macedonia = new Country() { Name = "Macedonia", CurrencyCode = "EUR", SubsistenceAllowance = 39, AccomodationLimit = 125 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Macedonia);
			//Country Malezja = new Country() { Name = "Malezja", CurrencyCode = "EUR", SubsistenceAllowance = 41, AccomodationLimit = 140 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Malezja);
			//Country Malta = new Country() { Name = "Malta", CurrencyCode = "EUR", SubsistenceAllowance = 43, AccomodationLimit = 180 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Malta);
			//Country Maroko = new Country() { Name = "Maroko", CurrencyCode = "EUR", SubsistenceAllowance = 41, AccomodationLimit = 130 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Maroko);
			//Country Meksyk = new Country() { Name = "Meksyk", CurrencyCode = "USD", SubsistenceAllowance = 53, AccomodationLimit = 140 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Meksyk);
			//Country Mołdowa = new Country() { Name = "Mołdowa", CurrencyCode = "EUR", SubsistenceAllowance = 41, AccomodationLimit = 85 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Mołdowa);
			//Country Monako = new Country() { Name = "Monako", CurrencyCode = "EUR", SubsistenceAllowance = 50, AccomodationLimit = 180 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Monako);
			//Country Mongolia = new Country() { Name = "Mongolia", CurrencyCode = "EUR", SubsistenceAllowance = 45, AccomodationLimit = 140 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Mongolia);
			//Country Niderlandy = new Country() { Name = "Niderlandy", CurrencyCode = "EUR", SubsistenceAllowance = 50, AccomodationLimit = 130 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Niderlandy);
			//Country Niemcy = new Country() { Name = "Niemcy", CurrencyCode = "EUR", SubsistenceAllowance = 49, AccomodationLimit = 150 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Niemcy);
			//Country Nigeria = new Country() { Name = "Nigeria", CurrencyCode = "EUR", SubsistenceAllowance = 46, AccomodationLimit = 240 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Nigeria);
			//Country Norwegia = new Country() { Name = "Norwegia", CurrencyCode = "NOK", SubsistenceAllowance = 451, AccomodationLimit = 1500 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Norwegia);
			//Country NowaZelandia = new Country() { Name = "Nowa Zelandia", CurrencyCode = "USD", SubsistenceAllowance = 58, AccomodationLimit = 180 };
			//context.Countries.AddOrUpdate(vt => vt.Name, NowaZelandia);
			//Country Oman = new Country() { Name = "Oman", CurrencyCode = "EUR", SubsistenceAllowance = 40, AccomodationLimit = 240 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Oman);
			//Country Pakistan = new Country() { Name = "Pakistan", CurrencyCode = "EUR", SubsistenceAllowance = 38, AccomodationLimit = 200 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Pakistan);
			//Country PalestyńskaWładzaNarodowa = new Country() { Name = "Palestyńska Władza Narodowa", CurrencyCode = "EUR", SubsistenceAllowance = 50, AccomodationLimit = 150 };
			//context.Countries.AddOrUpdate(vt => vt.Name, PalestyńskaWładzaNarodowa);
			//Country Panama = new Country() { Name = "Panama", CurrencyCode = "USD", SubsistenceAllowance = 52, AccomodationLimit = 140 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Panama);
			//Country Peru = new Country() { Name = "Peru", CurrencyCode = "USD", SubsistenceAllowance = 50, AccomodationLimit = 150 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Peru);
			//Country Polska = new Country() { Name = "Polska", CurrencyCode = "PLN", SubsistenceAllowance = 0, AccomodationLimit = 0 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Polska);
			//Country Portugalia = new Country() { Name = "Portugalia", CurrencyCode = "EUR", SubsistenceAllowance = 49, AccomodationLimit = 120 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Portugalia);
			//Country RepublikaPołudniowejAfryki = new Country() { Name = "Republika Południowej Afryki", CurrencyCode = "USD", SubsistenceAllowance = 52, AccomodationLimit = 275 };
			//context.Countries.AddOrUpdate(vt => vt.Name, RepublikaPołudniowejAfryki);
			//Country Rosja = new Country() { Name = "Rosja", CurrencyCode = "EUR", SubsistenceAllowance = 48, AccomodationLimit = 200 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Rosja);
			//Country Rumunia = new Country() { Name = "Rumunia", CurrencyCode = "EUR", SubsistenceAllowance = 38, AccomodationLimit = 100 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Rumunia);
			//Country SanMarino = new Country() { Name = "San Marino", CurrencyCode = "EUR", SubsistenceAllowance = 48, AccomodationLimit = 174 };
			//context.Countries.AddOrUpdate(vt => vt.Name, SanMarino);
			//Country Senegal = new Country() { Name = "Senegal", CurrencyCode = "EUR", SubsistenceAllowance = 44, AccomodationLimit = 120 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Senegal);
			//Country RepublikaSerbi = new Country() { Name = "Republika Serbii i Republika Czarnogóry", CurrencyCode = "EUR", SubsistenceAllowance = 40, AccomodationLimit = 100 };
			//context.Countries.AddOrUpdate(vt => vt.Name, RepublikaSerbi);
			//Country Singapur = new Country() { Name = "Singapur", CurrencyCode = "USD", SubsistenceAllowance = 56, AccomodationLimit = 230 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Singapur);
			//Country Słowacja = new Country() { Name = "Słowacja", CurrencyCode = "EUR", SubsistenceAllowance = 43, AccomodationLimit = 120 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Słowacja);
			//Country Słowenia = new Country() { Name = "Słowenia", CurrencyCode = "EUR", SubsistenceAllowance = 41, AccomodationLimit = 130 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Słowenia);
			//Country USA = new Country() { Name = "USA", CurrencyCode = "USD", SubsistenceAllowance = 59, AccomodationLimit = 200 };
			//context.Countries.AddOrUpdate(vt => vt.Name, USA);
			//Country USANY = new Country() { Name = "USA – Nowy Jork", CurrencyCode = "USD", SubsistenceAllowance = 59, AccomodationLimit = 350 };
			//context.Countries.AddOrUpdate(vt => vt.Name, USANY);
			//Country USAW = new Country() { Name = "USA – Waszyngton", CurrencyCode = "USD", SubsistenceAllowance = 59, AccomodationLimit = 200 };
			//context.Countries.AddOrUpdate(vt => vt.Name, USAW);
			//Country Syria = new Country() { Name = "Syria", CurrencyCode = "USD", SubsistenceAllowance = 50, AccomodationLimit = 150 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Syria);
			//Country Szwajcaria = new Country() { Name = "Szwajcaria", CurrencyCode = "CHF", SubsistenceAllowance = 88, AccomodationLimit = 200 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Szwajcaria);
			//Country Szwecja = new Country() { Name = "Szwecja", CurrencyCode = "SEK", SubsistenceAllowance = 459, AccomodationLimit = 1800 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Szwecja);
			//Country Tadżykistan = new Country() { Name = "Tadżykistan", CurrencyCode = "EUR", SubsistenceAllowance = 41, AccomodationLimit = 140 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Tadżykistan);
			//Country Tajlandia = new Country() { Name = "Tajlandia", CurrencyCode = "USD", SubsistenceAllowance = 42, AccomodationLimit = 110 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Tajlandia);
			//Country Tanzania = new Country() { Name = "Tanzania", CurrencyCode = "USD", SubsistenceAllowance = 53, AccomodationLimit = 150 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Tanzania);
			//Country Tunezja = new Country() { Name = "Tunezja", CurrencyCode = "EUR", SubsistenceAllowance = 37, AccomodationLimit = 100 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Tunezja);
			//Country Turcja = new Country() { Name = "Turcja", CurrencyCode = "USD", SubsistenceAllowance = 53, AccomodationLimit = 173 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Turcja);
			//Country Turkmenistan = new Country() { Name = "Turkmenistan", CurrencyCode = "EUR", SubsistenceAllowance = 47, AccomodationLimit = 90 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Turkmenistan);
			//Country Ukraina = new Country() { Name = "Ukraina", CurrencyCode = "EUR", SubsistenceAllowance = 41, AccomodationLimit = 180 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Ukraina);
			//Country Urugwaj = new Country() { Name = "Urugwaj", CurrencyCode = "USD", SubsistenceAllowance = 50, AccomodationLimit = 80 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Urugwaj);
			//Country Uzbekistan = new Country() { Name = "Uzbekistan", CurrencyCode = "EUR", SubsistenceAllowance = 41, AccomodationLimit = 140 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Uzbekistan);
			//Country Wenezuela = new Country() { Name = "Wenezuela", CurrencyCode = "USD", SubsistenceAllowance = 60, AccomodationLimit = 220 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Wenezuela);
			//Country Węgry = new Country() { Name = "Węgry", CurrencyCode = "EUR", SubsistenceAllowance = 44, AccomodationLimit = 130 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Węgry);
			//Country WielkaBrytania = new Country() { Name = "Wielka Brytania", CurrencyCode = "GBP", SubsistenceAllowance = 35, AccomodationLimit = 200 };
			//context.Countries.AddOrUpdate(vt => vt.Name, WielkaBrytania);
			//Country Wietnam = new Country() { Name = "Wietnam", CurrencyCode = "USD", SubsistenceAllowance = 53, AccomodationLimit = 160 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Wietnam);
			//Country Włochy = new Country() { Name = "Włochy", CurrencyCode = "EUR", SubsistenceAllowance = 48, AccomodationLimit = 174 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Włochy);
			//Country Wybrzeże = new Country() { Name = "Wybrzeże Kości Słoniowej", CurrencyCode = "EUR", SubsistenceAllowance = 33, AccomodationLimit = 100 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Wybrzeże);
			//Country Zimbabwe = new Country() { Name = "Zimbabwe", CurrencyCode = "EUR", SubsistenceAllowance = 39, AccomodationLimit = 90 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Zimbabwe);
			//Country ZjednoczoneEmiraty = new Country() { Name = "Zjednoczone Emiraty Arabskie", CurrencyCode = "EUR", SubsistenceAllowance = 39, AccomodationLimit = 200 };
			//context.Countries.AddOrUpdate(vt => vt.Name, ZjednoczoneEmiraty);
			//Country Inne = new Country() { Name = "Inne", CurrencyCode = "EUR", SubsistenceAllowance = 41, AccomodationLimit = 140 };
			//context.Countries.AddOrUpdate(vt => vt.Name, Inne);
        }

		public void CreateVehicleTypes(BusinessTripsContext context)
		{
			VehicleType motorcycle = new VehicleType()
			{
				Name = "Motocykl",
				Rate = 0.2302m
            };

			VehicleType carCapacity1 = new VehicleType()
			{
				Name = "Samochód poj. 1",
				Rate = 0.5214m
			};

			VehicleType carCapacity2 = new VehicleType()
			{
				Name = "Samochód poj. 2",
				Rate = 0.8358m
            };

			context.VehicleTypes.AddOrUpdate(vt => vt.Name, motorcycle);
			context.VehicleTypes.AddOrUpdate(vt => vt.Name, carCapacity1);
			context.VehicleTypes.AddOrUpdate(vt => vt.Name, carCapacity2);
		}

        public void CreateExpenseTypes(BusinessTripsContext context)
        {
            ExpenseType expenseType1 = new ExpenseType()
            {
                Id = 1,
                Name = "Training"
            };

            ExpenseType expenseType2 = new ExpenseType()
            {
                Id = 2,
                Name = "Stationery"
            };

            ExpenseType expenseType3 = new ExpenseType()
            {
                Id = 3,
                Name = "MobilePhone"
            };

            ExpenseType expenseType4 = new ExpenseType()
            {
                Id = 4,
                Name = "Entertainment"
            };

            context.ExpenseTypes.AddOrUpdate(et => et.Name, expenseType1);
            context.ExpenseTypes.AddOrUpdate(et => et.Name, expenseType2);
            context.ExpenseTypes.AddOrUpdate(et => et.Name, expenseType3);
            context.ExpenseTypes.AddOrUpdate(et => et.Name, expenseType4);
        }

        public void CreateExpenseDocumentTypes(BusinessTripsContext context)
        {
            ExpenseDocumentType type1 = new ExpenseDocumentType()
            {
                Id = 1,
                Name = "Invoice"
            };

            ExpenseDocumentType type2 = new ExpenseDocumentType()
            {
                Id = 2,
                Name = "Receipt"
            };

            ExpenseDocumentType type3 = new ExpenseDocumentType()
            {
                Id = 3,
                Name = "NoDocument"
            };

            context.ExpenseDocumentTypes.AddOrUpdate(et => et.Name, type1);
            context.ExpenseDocumentTypes.AddOrUpdate(et => et.Name, type2);
            context.ExpenseDocumentTypes.AddOrUpdate(et => et.Name, type3);
        }

        public void CreateMealTypes(BusinessTripsContext context)
        {
            MealType type1 = new MealType()
            {
                Id = 1,
                Name = "Breakfast"
            };

            MealType type2 = new MealType()
            {
                Id = 2,
                Name = "Lunch"
            };

            MealType type3 = new MealType()
            {
                Id = 3,
                Name = "Supper"
            };

            context.MealTypes.AddOrUpdate(et => et.Name, type1);
            context.MealTypes.AddOrUpdate(et => et.Name, type2);
            context.MealTypes.AddOrUpdate(et => et.Name, type3);
        }
    }
}
