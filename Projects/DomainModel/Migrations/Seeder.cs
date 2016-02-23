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

            User basia = new User()
            {
                UserName = "b.zaleska@saffron.pl",
                Email = "b.zaleska@saffron.pl",
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
            UserRole basiaBasic = new UserRole() { Role = basicUser, User = basia };

            maciej.Roles.Add(maciejBasic);
			karol.Roles.Add(karolBasic);
            basia.Roles.Add(basiaBasic);

			context.Roles.AddOrUpdate(r => new { r.Name }, basicUser);
			context.Users.AddOrUpdate(u => new { u.UserName }, maciej);
			context.Users.AddOrUpdate(u => new { u.UserName }, karol);
            context.Users.AddOrUpdate(u => new { u.UserName }, basia);
        }

		public void CreateCountries(BusinessTripsContext context)
		{
            context.Countries.RemoveRange(context.Countries.ToList());
            context.SaveChanges();			

Currency USD = new Currency() { Code = "USD", Name = "dolar amerykański" };	
Currency EUR = new Currency() { Code = "EUR", Name = "euro" };

Currency AFN = new Currency() { Code = "AFN", Name = "afgani" };	Country Afganistan = new Country() { Name = "Afganistan", SubsistenceAllowance = 47, AccomodationLimit = 140, Currency = AFN, LimitCurrency = EUR };	context.Countries.AddOrUpdate(vt => vt.Name, Afganistan);
Currency ALL = new Currency() { Code = "ALL", Name = "lek" };	Country Albania = new Country() { Name = "Albania", SubsistenceAllowance = 41, AccomodationLimit = 120, Currency = ALL, LimitCurrency = EUR };	context.Countries.AddOrUpdate(vt => vt.Name, Albania);
Currency DZD = new Currency() { Code = "DZD", Name = "dinar" };	Country Algieria = new Country() { Name = "Algieria", SubsistenceAllowance = 50, AccomodationLimit = 200, Currency = DZD, LimitCurrency = EUR };	context.Countries.AddOrUpdate(vt => vt.Name, Algieria);
	Country Andora = new Country() { Name = "Andora", SubsistenceAllowance = 50, AccomodationLimit = 160, Currency = EUR, LimitCurrency = EUR };	context.Countries.AddOrUpdate(vt => vt.Name, Andora);
Currency AOA = new Currency() { Code = "AOA", Name = "kwanza" };	Country Angola = new Country() { Name = "Angola", SubsistenceAllowance = 61, AccomodationLimit = 180, Currency = AOA, LimitCurrency = USD };	context.Countries.AddOrUpdate(vt => vt.Name, Angola);
Currency SAR = new Currency() { Code = "SAR", Name = "rial" };	Country ArabiaSaudyjska = new Country() { Name = "Arabia Saudyjska", SubsistenceAllowance = 45, AccomodationLimit = 180, Currency = SAR, LimitCurrency = EUR };	context.Countries.AddOrUpdate(vt => vt.Name, ArabiaSaudyjska);
Currency ARS = new Currency() { Code = "ARS", Name = "peso" };	Country Argentyna = new Country() { Name = "Argentyna", SubsistenceAllowance = 50, AccomodationLimit = 150, Currency = ARS, LimitCurrency = USD };	context.Countries.AddOrUpdate(vt => vt.Name, Argentyna);
Currency AMD = new Currency() { Code = "AMD", Name = "dram" };	Country Armenia = new Country() { Name = "Armenia", SubsistenceAllowance = 42, AccomodationLimit = 145, Currency = AMD, LimitCurrency = EUR };	context.Countries.AddOrUpdate(vt => vt.Name, Armenia);
Currency AUD = new Currency() { Code = "AUD", Name = "dolar australijski" };	Country Australia = new Country() { Name = "Australia", SubsistenceAllowance = 88, AccomodationLimit = 250, Currency = AUD, LimitCurrency = AUD };	context.Countries.AddOrUpdate(vt => vt.Name, Australia);
Country Austria = new Country() { Name = "Austria", SubsistenceAllowance = 52, AccomodationLimit = 130, Currency = EUR, LimitCurrency = EUR };	context.Countries.AddOrUpdate(vt => vt.Name, Austria);
Currency AZN = new Currency() { Code = "AZN", Name = "manat" };	Country Azerbejdżan = new Country() { Name = "Azerbejdżan", SubsistenceAllowance = 43, AccomodationLimit = 150, Currency = AZN, LimitCurrency = EUR };	context.Countries.AddOrUpdate(vt => vt.Name, Azerbejdżan);
Currency BDT = new Currency() { Code = "BDT", Name = "taka" };	Country Bangladesz = new Country() { Name = "Bangladesz", SubsistenceAllowance = 50, AccomodationLimit = 120, Currency = BDT, LimitCurrency = USD };	context.Countries.AddOrUpdate(vt => vt.Name, Bangladesz);
	Country Belgia = new Country() { Name = "Belgia", SubsistenceAllowance = 48, AccomodationLimit = 160, Currency = EUR, LimitCurrency = EUR };	context.Countries.AddOrUpdate(vt => vt.Name, Belgia);
Currency BYR = new Currency() { Code = "BYR", Name = "rubel białoruski" };	Country Białoruś = new Country() { Name = "Białoruś", SubsistenceAllowance = 42, AccomodationLimit = 130, Currency = BYR, LimitCurrency = EUR };	context.Countries.AddOrUpdate(vt => vt.Name, Białoruś);
Currency BAM = new Currency() { Code = "BAM", Name = "marka" };	Country BośniaiHercegowina = new Country() { Name = "Bośnia i Hercegowina", SubsistenceAllowance = 41, AccomodationLimit = 100, Currency = BAM, LimitCurrency = EUR };	context.Countries.AddOrUpdate(vt => vt.Name, BośniaiHercegowina);
Currency BRL = new Currency() { Code = "BRL", Name = "real" };	Country Brazylia = new Country() { Name = "Brazylia", SubsistenceAllowance = 43, AccomodationLimit = 120, Currency = BRL, LimitCurrency = EUR };	context.Countries.AddOrUpdate(vt => vt.Name, Brazylia);
Currency BGN = new Currency() { Code = "BGN", Name = "lew bułgarski" };	Country Bułgaria = new Country() { Name = "Bułgaria", SubsistenceAllowance = 40, AccomodationLimit = 120, Currency = BGN, LimitCurrency = EUR };	context.Countries.AddOrUpdate(vt => vt.Name, Bułgaria);
Currency CLP = new Currency() { Code = "CLP", Name = "peso" };	Country Chile = new Country() { Name = "Chile", SubsistenceAllowance = 60, AccomodationLimit = 120, Currency = CLP, LimitCurrency = USD };	context.Countries.AddOrUpdate(vt => vt.Name, Chile);
Currency CNY = new Currency() { Code = "CNY", Name = "juan(renminbi)" };	Country Chiny = new Country() { Name = "Chiny", SubsistenceAllowance = 55, AccomodationLimit = 170, Currency = CNY, LimitCurrency = EUR };	context.Countries.AddOrUpdate(vt => vt.Name, Chiny);
Currency HRK = new Currency() { Code = "HRK", Name = "kuna chorwacka" };	Country Chorwacja = new Country() { Name = "Chorwacja", SubsistenceAllowance = 42, AccomodationLimit = 125, Currency = HRK, LimitCurrency = EUR };	context.Countries.AddOrUpdate(vt => vt.Name, Chorwacja);
	Country Cypr = new Country() { Name = "Cypr", SubsistenceAllowance = 43, AccomodationLimit = 160, Currency = EUR, LimitCurrency = EUR };	context.Countries.AddOrUpdate(vt => vt.Name, Cypr);
Currency CZK = new Currency() { Code = "CZK", Name = "korona czeska" };	Country Czechy = new Country() { Name = "Czechy", SubsistenceAllowance = 41, AccomodationLimit = 120, Currency = CZK, LimitCurrency = EUR };	context.Countries.AddOrUpdate(vt => vt.Name, Czechy);
Currency DKK = new Currency() { Code = "DKK", Name = "korona duńska" };	Country Dania = new Country() { Name = "Dania", SubsistenceAllowance = 406, AccomodationLimit = 1300, Currency = DKK, LimitCurrency = DKK };	context.Countries.AddOrUpdate(vt => vt.Name, Dania);
Currency EGP = new Currency() { Code = "EGP", Name = "funt egipski" };	Country Egipt = new Country() { Name = "Egipt", SubsistenceAllowance = 55, AccomodationLimit = 150, Currency = EGP, LimitCurrency = USD };	context.Countries.AddOrUpdate(vt => vt.Name, Egipt);
Country Ekwador = new Country() { Name = "Ekwador", SubsistenceAllowance = 44, AccomodationLimit = 110, Currency = USD, LimitCurrency = USD };	context.Countries.AddOrUpdate(vt => vt.Name, Ekwador);
Country Estonia = new Country() { Name = "Estonia", SubsistenceAllowance = 41, AccomodationLimit = 100, Currency = EUR, LimitCurrency = EUR };	context.Countries.AddOrUpdate(vt => vt.Name, Estonia);
Currency ETB = new Currency() { Code = "ETB", Name = "birr" };	Country Etiopia = new Country() { Name = "Etiopia", SubsistenceAllowance = 55, AccomodationLimit = 300, Currency = ETB, LimitCurrency = USD };	context.Countries.AddOrUpdate(vt => vt.Name, Etiopia);
	Country Finlandia = new Country() { Name = "Finlandia", SubsistenceAllowance = 48, AccomodationLimit = 160, Currency = EUR, LimitCurrency = EUR };	context.Countries.AddOrUpdate(vt => vt.Name, Finlandia);
	Country Francja = new Country() { Name = "Francja", SubsistenceAllowance = 50, AccomodationLimit = 180, Currency = EUR, LimitCurrency = EUR };	context.Countries.AddOrUpdate(vt => vt.Name, Francja);
	Country Grecja = new Country() { Name = "Grecja", SubsistenceAllowance = 48, AccomodationLimit = 140, Currency = EUR, LimitCurrency = EUR };	context.Countries.AddOrUpdate(vt => vt.Name, Grecja);
Currency GEL = new Currency() { Code = "GEL", Name = "lari" };	Country Gruzja = new Country() { Name = "Gruzja", SubsistenceAllowance = 43, AccomodationLimit = 140, Currency = GEL, LimitCurrency = EUR };	context.Countries.AddOrUpdate(vt => vt.Name, Gruzja);
	Country Hiszpania = new Country() { Name = "Hiszpania", SubsistenceAllowance = 50, AccomodationLimit = 160, Currency = EUR, LimitCurrency = EUR };	context.Countries.AddOrUpdate(vt => vt.Name, Hiszpania);
Currency INR = new Currency() { Code = "INR", Name = "rupia" };	Country Indie = new Country() { Name = "Indie", SubsistenceAllowance = 38, AccomodationLimit = 190, Currency = INR, LimitCurrency = EUR };	context.Countries.AddOrUpdate(vt => vt.Name, Indie);
Currency IDR = new Currency() { Code = "IDR", Name = "rupia" };	Country Indonezja = new Country() { Name = "Indonezja", SubsistenceAllowance = 41, AccomodationLimit = 110, Currency = IDR, LimitCurrency = EUR };	context.Countries.AddOrUpdate(vt => vt.Name, Indonezja);
Currency InneCurrency = new Currency() { Code = "Inne", Name = "Inne" };	Country InneCountry = new Country() { Name = "Inne", SubsistenceAllowance = 41, AccomodationLimit = 140, Currency = InneCurrency, LimitCurrency = EUR };	context.Countries.AddOrUpdate(vt => vt.Name, InneCountry);
Currency IQD = new Currency() { Code = "IQD", Name = "dinar" };	Country Irak = new Country() { Name = "Irak", SubsistenceAllowance = 60, AccomodationLimit = 120, Currency = IQD, LimitCurrency = USD };	context.Countries.AddOrUpdate(vt => vt.Name, Irak);
Currency IRR = new Currency() { Code = "IRR", Name = "rial" };	Country Iran = new Country() { Name = "Iran", SubsistenceAllowance = 41, AccomodationLimit = 95, Currency = IRR, LimitCurrency = EUR };	context.Countries.AddOrUpdate(vt => vt.Name, Iran);
	Country Irlandia = new Country() { Name = "Irlandia", SubsistenceAllowance = 52, AccomodationLimit = 160, Currency = EUR, LimitCurrency = EUR };	context.Countries.AddOrUpdate(vt => vt.Name, Irlandia);
Currency ISK = new Currency() { Code = "ISK", Name = "korona" };	Country Islandia = new Country() { Name = "Islandia", SubsistenceAllowance = 56, AccomodationLimit = 160, Currency = ISK, LimitCurrency = EUR };	context.Countries.AddOrUpdate(vt => vt.Name, Islandia);
Currency ILS = new Currency() { Code = "ILS", Name = "nowy szekel" };	Country Izrael = new Country() { Name = "Izrael", SubsistenceAllowance = 50, AccomodationLimit = 150, Currency = ILS, LimitCurrency = EUR };	context.Countries.AddOrUpdate(vt => vt.Name, Izrael);
Currency JPY = new Currency() { Code = "JPY", Name = "jen japoński" };	Country Japonia = new Country() { Name = "Japonia", SubsistenceAllowance = 7532, AccomodationLimit = 22000, Currency = JPY, LimitCurrency = JPY };	context.Countries.AddOrUpdate(vt => vt.Name, Japonia);
Currency YER = new Currency() { Code = "YER", Name = "rial" };	Country Jemen = new Country() { Name = "Jemen", SubsistenceAllowance = 48, AccomodationLimit = 160, Currency = YER, LimitCurrency = USD };	context.Countries.AddOrUpdate(vt => vt.Name, Jemen);
Currency JOD = new Currency() { Code = "JOD", Name = "dinar" };	Country Jordania = new Country() { Name = "Jordania", SubsistenceAllowance = 40, AccomodationLimit = 95, Currency = JOD, LimitCurrency = EUR };	context.Countries.AddOrUpdate(vt => vt.Name, Jordania);
Currency KHR = new Currency() { Code = "KHR", Name = "riel" };	Country Kambodża = new Country() { Name = "Kambodża", SubsistenceAllowance = 45, AccomodationLimit = 100, Currency = KHR, LimitCurrency = USD };	context.Countries.AddOrUpdate(vt => vt.Name, Kambodża);
Currency CAD = new Currency() { Code = "CAD", Name = "dolar kanadyjski" };	Country Kanada = new Country() { Name = "Kanada", SubsistenceAllowance = 71, AccomodationLimit = 190, Currency = CAD, LimitCurrency = CAD };	context.Countries.AddOrUpdate(vt => vt.Name, Kanada);
Currency QAR = new Currency() { Code = "QAR", Name = "rial" };	Country Katar = new Country() { Name = "Katar", SubsistenceAllowance = 41, AccomodationLimit = 200, Currency = QAR, LimitCurrency = EUR };	context.Countries.AddOrUpdate(vt => vt.Name, Katar);
Currency KZT = new Currency() { Code = "KZT", Name = "tenge" };	Country Kazachstan = new Country() { Name = "Kazachstan", SubsistenceAllowance = 41, AccomodationLimit = 140, Currency = KZT, LimitCurrency = EUR };	context.Countries.AddOrUpdate(vt => vt.Name, Kazachstan);
Currency KES = new Currency() { Code = "KES", Name = "szyling" };	Country Kenia = new Country() { Name = "Kenia", SubsistenceAllowance = 41, AccomodationLimit = 150, Currency = KES, LimitCurrency = EUR };	context.Countries.AddOrUpdate(vt => vt.Name, Kenia);
Currency KGS = new Currency() { Code = "KGS", Name = "som" };	Country Kirgistan = new Country() { Name = "Kirgistan", SubsistenceAllowance = 41, AccomodationLimit = 150, Currency = KGS, LimitCurrency = USD };	context.Countries.AddOrUpdate(vt => vt.Name, Kirgistan);
Currency COP = new Currency() { Code = "COP", Name = "peso" };	Country Kolumbia = new Country() { Name = "Kolumbia", SubsistenceAllowance = 49, AccomodationLimit = 120, Currency = COP, LimitCurrency = USD };	context.Countries.AddOrUpdate(vt => vt.Name, Kolumbia);
Currency XAF = new Currency() { Code = "XAF", Name = "frank CFA" };	Country Kongo = new Country() { Name = "Kongo", SubsistenceAllowance = 66, AccomodationLimit = 220, Currency = XAF, LimitCurrency = USD };	context.Countries.AddOrUpdate(vt => vt.Name, Kongo);
Currency KRW = new Currency() { Code = "KRW", Name = "won" };	Country KoreaPołudniowa = new Country() { Name = "Korea Południowa", SubsistenceAllowance = 46, AccomodationLimit = 170, Currency = KRW, LimitCurrency = EUR };	context.Countries.AddOrUpdate(vt => vt.Name, KoreaPołudniowa);
Currency WN = new Currency() { Code = "WN", Name = "won" };	Country KoreańskaRepublikaLudowoDemokratyczna = new Country() { Name = "Koreańska Republika Ludowo-Demokratyczna", SubsistenceAllowance = 48, AccomodationLimit = 170, Currency = WN, LimitCurrency = EUR };	context.Countries.AddOrUpdate(vt => vt.Name, KoreańskaRepublikaLudowoDemokratyczna);
Currency CRC = new Currency() { Code = "CRC", Name = "colon" };	Country Kostaryka = new Country() { Name = "Kostaryka", SubsistenceAllowance = 50, AccomodationLimit = 140, Currency = CRC, LimitCurrency = USD };	context.Countries.AddOrUpdate(vt => vt.Name, Kostaryka);
Currency CUP = new Currency() { Code = "CUP", Name = "peso" };	Country Kuba = new Country() { Name = "Kuba", SubsistenceAllowance = 42, AccomodationLimit = 110, Currency = CUP, LimitCurrency = EUR };	context.Countries.AddOrUpdate(vt => vt.Name, Kuba);
Currency KWD = new Currency() { Code = "KWD", Name = "dinar" };	Country Kuwejt = new Country() { Name = "Kuwejt", SubsistenceAllowance = 39, AccomodationLimit = 200, Currency = KWD, LimitCurrency = EUR };	context.Countries.AddOrUpdate(vt => vt.Name, Kuwejt);
Currency LAK = new Currency() { Code = "LAK", Name = "kip" };	Country Laos = new Country() { Name = "Laos", SubsistenceAllowance = 54, AccomodationLimit = 100, Currency = LAK, LimitCurrency = USD };	context.Countries.AddOrUpdate(vt => vt.Name, Laos);
Currency LBP = new Currency() { Code = "LBP", Name = "funt" };	Country Liban = new Country() { Name = "Liban", SubsistenceAllowance = 57, AccomodationLimit = 150, Currency = LBP, LimitCurrency = USD };	context.Countries.AddOrUpdate(vt => vt.Name, Liban);
Currency LYD = new Currency() { Code = "LYD", Name = "dinar" };	Country Libia = new Country() { Name = "Libia", SubsistenceAllowance = 52, AccomodationLimit = 100, Currency = LYD, LimitCurrency = EUR };	context.Countries.AddOrUpdate(vt => vt.Name, Libia);
Currency CHF = new Currency() { Code = "CHF", Name = "frank szwajcarski" };	Country Liechtenstein = new Country() { Name = "Liechtenstein", SubsistenceAllowance = 88, AccomodationLimit = 200, Currency = CHF, LimitCurrency = CHF };	context.Countries.AddOrUpdate(vt => vt.Name, Liechtenstein);
	Country Litwa = new Country() { Name = "Litwa", SubsistenceAllowance = 39, AccomodationLimit = 130, Currency = EUR, LimitCurrency = EUR };	context.Countries.AddOrUpdate(vt => vt.Name, Litwa);
	Country Luksemburg = new Country() { Name = "Luksemburg", SubsistenceAllowance = 48, AccomodationLimit = 160, Currency = EUR, LimitCurrency = EUR };	context.Countries.AddOrUpdate(vt => vt.Name, Luksemburg);
	Country Łotwa = new Country() { Name = "Łotwa", SubsistenceAllowance = 57, AccomodationLimit = 132, Currency = EUR, LimitCurrency = EUR };	context.Countries.AddOrUpdate(vt => vt.Name, Łotwa);
Currency MKD = new Currency() { Code = "MKD", Name = "denar" };	Country Macedonia = new Country() { Name = "Macedonia", SubsistenceAllowance = 39, AccomodationLimit = 125, Currency = MKD, LimitCurrency = EUR };	context.Countries.AddOrUpdate(vt => vt.Name, Macedonia);
Currency MYR = new Currency() { Code = "MYR", Name = "ringgit" };	Country Malezja = new Country() { Name = "Malezja", SubsistenceAllowance = 41, AccomodationLimit = 140, Currency = MYR, LimitCurrency = EUR };	context.Countries.AddOrUpdate(vt => vt.Name, Malezja);
	Country Malta = new Country() { Name = "Malta", SubsistenceAllowance = 43, AccomodationLimit = 180, Currency = EUR, LimitCurrency = EUR };	context.Countries.AddOrUpdate(vt => vt.Name, Malta);
Currency MAD = new Currency() { Code = "MAD", Name = "dirham" };	Country Maroko = new Country() { Name = "Maroko", SubsistenceAllowance = 41, AccomodationLimit = 130, Currency = MAD, LimitCurrency = EUR };	context.Countries.AddOrUpdate(vt => vt.Name, Maroko);
Currency MXN = new Currency() { Code = "MXN", Name = "peso" };	Country Meksyk = new Country() { Name = "Meksyk", SubsistenceAllowance = 53, AccomodationLimit = 140, Currency = MXN, LimitCurrency = USD };	context.Countries.AddOrUpdate(vt => vt.Name, Meksyk);
Currency MDL = new Currency() { Code = "MDL", Name = "lej" };	Country Mołdawia = new Country() { Name = "Mołdawia", SubsistenceAllowance = 41, AccomodationLimit = 85, Currency = MDL, LimitCurrency = EUR };	context.Countries.AddOrUpdate(vt => vt.Name, Mołdawia);
	Country Monako = new Country() { Name = "Monako", SubsistenceAllowance = 50, AccomodationLimit = 180, Currency = EUR, LimitCurrency = EUR };	context.Countries.AddOrUpdate(vt => vt.Name, Monako);
Currency MNT = new Currency() { Code = "MNT", Name = "tugrik" };	Country Mongolia = new Country() { Name = "Mongolia", SubsistenceAllowance = 45, AccomodationLimit = 140, Currency = MNT, LimitCurrency = EUR };	context.Countries.AddOrUpdate(vt => vt.Name, Mongolia);
	Country Holandia = new Country() { Name = "Holandia", SubsistenceAllowance = 50, AccomodationLimit = 130, Currency = EUR, LimitCurrency = EUR };	context.Countries.AddOrUpdate(vt => vt.Name, Holandia);
	Country Niemcy = new Country() { Name = "Niemcy", SubsistenceAllowance = 49, AccomodationLimit = 150, Currency = EUR, LimitCurrency = EUR };	context.Countries.AddOrUpdate(vt => vt.Name, Niemcy);
Currency NGN = new Currency() { Code = "NGN", Name = "naira" };	Country Nigeria = new Country() { Name = "Nigeria", SubsistenceAllowance = 46, AccomodationLimit = 240, Currency = NGN, LimitCurrency = EUR };	context.Countries.AddOrUpdate(vt => vt.Name, Nigeria);
Currency NOK = new Currency() { Code = "NOK", Name = "korona norweska" };	Country Norwegia = new Country() { Name = "Norwegia", SubsistenceAllowance = 451, AccomodationLimit = 1500, Currency = NOK, LimitCurrency = NOK };	context.Countries.AddOrUpdate(vt => vt.Name, Norwegia);
Currency NZD = new Currency() { Code = "NZD", Name = "dolar" };	Country NowaZelandia = new Country() { Name = "Nowa Zelandia", SubsistenceAllowance = 58, AccomodationLimit = 180, Currency = NZD, LimitCurrency = USD };	context.Countries.AddOrUpdate(vt => vt.Name, NowaZelandia);
Currency OMR = new Currency() { Code = "OMR", Name = "rial" };	Country Oman = new Country() { Name = "Oman", SubsistenceAllowance = 40, AccomodationLimit = 240, Currency = OMR, LimitCurrency = EUR };	context.Countries.AddOrUpdate(vt => vt.Name, Oman);
Currency PKR = new Currency() { Code = "PKR", Name = "rupia" };	Country Pakistan = new Country() { Name = "Pakistan", SubsistenceAllowance = 38, AccomodationLimit = 200, Currency = PKR, LimitCurrency = EUR };	context.Countries.AddOrUpdate(vt => vt.Name, Pakistan);
Currency NIS = new Currency() { Code = "NIS", Name = "nowy szekel" };	Country PalestyńskaWładzaNarodowa = new Country() { Name = "Palestyńska Władza Narodowa", SubsistenceAllowance = 50, AccomodationLimit = 150, Currency = NIS, LimitCurrency = EUR };	context.Countries.AddOrUpdate(vt => vt.Name, PalestyńskaWładzaNarodowa);
Currency PAB = new Currency() { Code = "PAB", Name = "balboa" };	Country Panama = new Country() { Name = "Panama", SubsistenceAllowance = 52, AccomodationLimit = 140, Currency = PAB, LimitCurrency = USD };	context.Countries.AddOrUpdate(vt => vt.Name, Panama);
Currency PEN = new Currency() { Code = "PEN", Name = "nowy sol" };	Country Peru = new Country() { Name = "Peru", SubsistenceAllowance = 50, AccomodationLimit = 150, Currency = PEN, LimitCurrency = USD };	context.Countries.AddOrUpdate(vt => vt.Name, Peru);
Currency PLN = new Currency() { Code = "PLN", Name = "złoty" };	Country Polska = new Country() { Name = "Polska", SubsistenceAllowance = 30, AccomodationLimit = 45, Currency = PLN, LimitCurrency = PLN };	context.Countries.AddOrUpdate(vt => vt.Name, Polska);
	Country Portugalia = new Country() { Name = "Portugalia", SubsistenceAllowance = 49, AccomodationLimit = 120, Currency = EUR, LimitCurrency = EUR };	context.Countries.AddOrUpdate(vt => vt.Name, Portugalia);
Currency ZAR = new Currency() { Code = "ZAR", Name = "rand" };	Country RepublikaPołudniowejAfryki = new Country() { Name = "Republika Południowej Afryki", SubsistenceAllowance = 52, AccomodationLimit = 275, Currency = ZAR, LimitCurrency = USD };	context.Countries.AddOrUpdate(vt => vt.Name, RepublikaPołudniowejAfryki);
Currency CSD = new Currency() { Code = "CSD", Name = "dinar" };	Country RepublikaSerbiiiRepublikaCzarnogóry = new Country() { Name = "Republika Serbii i Republika Czarnogóry", SubsistenceAllowance = 40, AccomodationLimit = 100, Currency = CSD, LimitCurrency = EUR };	context.Countries.AddOrUpdate(vt => vt.Name, RepublikaSerbiiiRepublikaCzarnogóry);
Currency RUB = new Currency() { Code = "RUB", Name = "rubel rosyjski" };	Country Rosja = new Country() { Name = "Rosja", SubsistenceAllowance = 48, AccomodationLimit = 200, Currency = RUB, LimitCurrency = EUR };	context.Countries.AddOrUpdate(vt => vt.Name, Rosja);
Currency RON = new Currency() { Code = "RON", Name = "lej rumuński" };	Country Rumunia = new Country() { Name = "Rumunia", SubsistenceAllowance = 38, AccomodationLimit = 100, Currency = RON, LimitCurrency = EUR };	context.Countries.AddOrUpdate(vt => vt.Name, Rumunia);
	Country SanMarino = new Country() { Name = "San Marino", SubsistenceAllowance = 48, AccomodationLimit = 174, Currency = EUR, LimitCurrency = EUR };	context.Countries.AddOrUpdate(vt => vt.Name, SanMarino);
Currency XOF = new Currency() { Code = "XOF", Name = "frank CFA" };	Country Senegal = new Country() { Name = "Senegal", SubsistenceAllowance = 44, AccomodationLimit = 120, Currency = XOF, LimitCurrency = EUR };	context.Countries.AddOrUpdate(vt => vt.Name, Senegal);
Currency SGD = new Currency() { Code = "SGD", Name = "dolar" };	Country Singapur = new Country() { Name = "Singapur", SubsistenceAllowance = 56, AccomodationLimit = 230, Currency = SGD, LimitCurrency = USD };	context.Countries.AddOrUpdate(vt => vt.Name, Singapur);
	Country Słowacja = new Country() { Name = "Słowacja", SubsistenceAllowance = 43, AccomodationLimit = 120, Currency = EUR, LimitCurrency = EUR };	context.Countries.AddOrUpdate(vt => vt.Name, Słowacja);
	Country Słowenia = new Country() { Name = "Słowenia", SubsistenceAllowance = 41, AccomodationLimit = 130, Currency = EUR, LimitCurrency = EUR };	context.Countries.AddOrUpdate(vt => vt.Name, Słowenia);
Currency SYP = new Currency() { Code = "SYP", Name = "funt" };	Country Syria = new Country() { Name = "Syria", SubsistenceAllowance = 50, AccomodationLimit = 150, Currency = SYP, LimitCurrency = USD };	context.Countries.AddOrUpdate(vt => vt.Name, Syria);
Country Szwajcaria = new Country() { Name = "Szwajcaria", SubsistenceAllowance = 88, AccomodationLimit = 200, Currency = CHF, LimitCurrency = CHF };	context.Countries.AddOrUpdate(vt => vt.Name, Szwajcaria);
Currency SEK = new Currency() { Code = "SEK", Name = "korona szwedzka" };	Country Szwecja = new Country() { Name = "Szwecja", SubsistenceAllowance = 459, AccomodationLimit = 1800, Currency = SEK, LimitCurrency = SEK };	context.Countries.AddOrUpdate(vt => vt.Name, Szwecja);
Currency TJS = new Currency() { Code = "TJS", Name = "somoni" };	Country Tadżykistan = new Country() { Name = "Tadżykistan", SubsistenceAllowance = 41, AccomodationLimit = 140, Currency = TJS, LimitCurrency = EUR };	context.Countries.AddOrUpdate(vt => vt.Name, Tadżykistan);
Currency THB = new Currency() { Code = "THB", Name = "bat" };	Country Tajlandia = new Country() { Name = "Tajlandia", SubsistenceAllowance = 42, AccomodationLimit = 110, Currency = THB, LimitCurrency = USD };	context.Countries.AddOrUpdate(vt => vt.Name, Tajlandia);
Currency TZS = new Currency() { Code = "TZS", Name = "szyling" };	Country Tanzania = new Country() { Name = "Tanzania", SubsistenceAllowance = 53, AccomodationLimit = 150, Currency = TZS, LimitCurrency = USD };	context.Countries.AddOrUpdate(vt => vt.Name, Tanzania);
Currency TND = new Currency() { Code = "TND", Name = "dinar" };	Country Tunezja = new Country() { Name = "Tunezja", SubsistenceAllowance = 37, AccomodationLimit = 100, Currency = TND, LimitCurrency = EUR };	context.Countries.AddOrUpdate(vt => vt.Name, Tunezja);
Currency TRY = new Currency() { Code = "TRY", Name = "lira turecka" };	Country Turcja = new Country() { Name = "Turcja", SubsistenceAllowance = 53, AccomodationLimit = 173, Currency = TRY, LimitCurrency = USD };	context.Countries.AddOrUpdate(vt => vt.Name, Turcja);
Currency TMM = new Currency() { Code = "TMM", Name = "manat" };	Country Turkmenistan = new Country() { Name = "Turkmenistan", SubsistenceAllowance = 47, AccomodationLimit = 90, Currency = TMM, LimitCurrency = EUR };	context.Countries.AddOrUpdate(vt => vt.Name, Turkmenistan);
Currency UAH = new Currency() { Code = "UAH", Name = "hrywna ukraińska" };	Country Ukraina = new Country() { Name = "Ukraina", SubsistenceAllowance = 41, AccomodationLimit = 180, Currency = UAH, LimitCurrency = EUR };	context.Countries.AddOrUpdate(vt => vt.Name, Ukraina);
Currency UYU = new Currency() { Code = "UYU", Name = "peso" };	Country Urugwaj = new Country() { Name = "Urugwaj", SubsistenceAllowance = 50, AccomodationLimit = 80, Currency = UYU, LimitCurrency = USD };	context.Countries.AddOrUpdate(vt => vt.Name, Urugwaj);
Country USA = new Country() { Name = "USA", SubsistenceAllowance = 59, AccomodationLimit = 200, Currency = USD, LimitCurrency = USD };	context.Countries.AddOrUpdate(vt => vt.Name, USA);
Country USANowyJork = new Country() { Name = "USA – Nowy Jork", SubsistenceAllowance = 59, AccomodationLimit = 350, Currency = USD, LimitCurrency = USD };	context.Countries.AddOrUpdate(vt => vt.Name, USANowyJork);
Country USAWaszyngton = new Country() { Name = "USA – Waszyngton", SubsistenceAllowance = 59, AccomodationLimit = 200, Currency = USD, LimitCurrency = USD };	context.Countries.AddOrUpdate(vt => vt.Name, USAWaszyngton);
Currency UZS = new Currency() { Code = "UZS", Name = "som" };	Country Uzbekistan = new Country() { Name = "Uzbekistan", SubsistenceAllowance = 41, AccomodationLimit = 140, Currency = UZS, LimitCurrency = EUR };	context.Countries.AddOrUpdate(vt => vt.Name, Uzbekistan);
Currency VEF = new Currency() { Code = "VEF", Name = "boliwar" };	Country Wenezuela = new Country() { Name = "Wenezuela", SubsistenceAllowance = 60, AccomodationLimit = 220, Currency = VEF, LimitCurrency = USD };	context.Countries.AddOrUpdate(vt => vt.Name, Wenezuela);
Currency HUF = new Currency() { Code = "HUF", Name = "forint węgierski" };	Country Węgry = new Country() { Name = "Węgry", SubsistenceAllowance = 44, AccomodationLimit = 130, Currency = HUF, LimitCurrency = EUR };	context.Countries.AddOrUpdate(vt => vt.Name, Węgry);
Currency GBP = new Currency() { Code = "GBP", Name = "funt brytyjski" };	Country WielkaBrytania = new Country() { Name = "Wielka Brytania", SubsistenceAllowance = 35, AccomodationLimit = 200, Currency = GBP, LimitCurrency = GBP };	context.Countries.AddOrUpdate(vt => vt.Name, WielkaBrytania);
Currency VND = new Currency() { Code = "VND", Name = "dong" };	Country Wietnam = new Country() { Name = "Wietnam", SubsistenceAllowance = 53, AccomodationLimit = 160, Currency = VND, LimitCurrency = USD };	context.Countries.AddOrUpdate(vt => vt.Name, Wietnam);
	Country Włochy = new Country() { Name = "Włochy", SubsistenceAllowance = 48, AccomodationLimit = 174, Currency = EUR, LimitCurrency = EUR };	context.Countries.AddOrUpdate(vt => vt.Name, Włochy);
Country WybrzeżeKościSłoniowej = new Country() { Name = "Wybrzeże Kości Słoniowej", SubsistenceAllowance = 33, AccomodationLimit = 100, Currency = XOF, LimitCurrency = EUR };	context.Countries.AddOrUpdate(vt => vt.Name, WybrzeżeKościSłoniowej);
Currency ZWD = new Currency() { Code = "ZWD", Name = "dolar" };	Country Zimbabwe = new Country() { Name = "Zimbabwe", SubsistenceAllowance = 39, AccomodationLimit = 90, Currency = ZWD, LimitCurrency = EUR };	context.Countries.AddOrUpdate(vt => vt.Name, Zimbabwe);
Currency AED = new Currency() { Code = "AED", Name = "dirham" };	Country ZjednoczoneEmiratyArabskie = new Country() { Name = "Zjednoczone Emiraty Arabskie", SubsistenceAllowance = 39, AccomodationLimit = 200, Currency = AED, LimitCurrency = EUR };	context.Countries.AddOrUpdate(vt => vt.Name, ZjednoczoneEmiratyArabskie);


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
    }
}
